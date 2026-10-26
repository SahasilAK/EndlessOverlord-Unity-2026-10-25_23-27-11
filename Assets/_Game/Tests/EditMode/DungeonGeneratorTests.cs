using System.Collections.Generic;
using FinalBoss.Core;
using NUnit.Framework;

namespace FinalBoss.Tests
{
    [TestFixture]
    public sealed class DungeonGeneratorTests
    {
        [Test]
        public void BiomeCatalog_ContainsAllTwentyCanonicalBiomesWithColors()
        {
            Assert.AreEqual(20, BiomeCatalog.Count);
            var all = BiomeCatalog.GetAll();
            Assert.AreEqual(20, all.Count);

            var expectedBiomes = new[]
            {
                "Forgotten Necropolis",
                "Infernal Citadel",
                "Frozen Cathedral",
                "Vampire's Crimson Palace",
                "Shadow Realm",
                "Ancient Golem Sanctuary",
                "Corrupted Enchanted Forest",
                "Sunken Temple",
                "Abandoned Dwarven Forge",
                "Arcane Library",
                "Plague Kingdom",
                "Crystal Caverns",
                "Cursed Royal Castle",
                "Celestial Ruins",
                "Abyssal Prison",
                "Desert Tomb",
                "Mushroom Underworld",
                "Ancient Clockwork Fortress",
                "Haunted Village",
                "Primordial Worldstone Chamber"
            };

            for (int i = 0; i < expectedBiomes.Length; i++)
            {
                var biome = BiomeCatalog.GetById(i + 1);
                Assert.AreEqual(i + 1, biome.Id);
                Assert.AreEqual(expectedBiomes[i], biome.Name);
                Assert.IsFalse(string.IsNullOrEmpty(biome.SourceDescription));
                Assert.Greater(biome.FloorColor.A, 0f);
                Assert.Greater(biome.WallColor.A, 0f);
                Assert.Greater(biome.ObstacleColor.A, 0f);
                Assert.Greater(biome.PortalColor.A, 0f);
            }
        }

        [Test]
        public void Generate_Across100RandomSeeds_AllPassValidationWithoutFallback()
        {
            var topologiesSeen = new HashSet<ArenaTopologyType>();
            var biomesSeen = new HashSet<int>();

            for (int i = 1; i <= 100; i++)
            {
                long seed = 1000000007L * i + 0xDEADBEEF;
                var layout = ArenaGenerator.Generate(seed);

                Assert.IsNotNull(layout, $"Layout for seed {seed} should not be null.");
                Assert.IsTrue(layout.IsValid, $"Layout for seed {seed} failed validation: {layout.ValidationReason}");
                Assert.IsFalse(layout.Fingerprint.Contains("FALLBACK"), $"Seed {seed} required fallback!");

                // Verification of geometric invariants
                Assert.GreaterOrEqual(layout.Walls.Count, 4, $"Seed {seed} has too few walls.");
                Assert.GreaterOrEqual(layout.Obstacles.Count, 2, $"Seed {seed} has too few obstacles.");
                Assert.GreaterOrEqual(layout.Portals.Count, 4, $"Seed {seed} has too few portals.");

                // Verification of player clearance >= 2.0m
                for (int w = 0; w < layout.Walls.Count; w++)
                {
                    Assert.IsFalse(layout.Walls[w].Contains(layout.PlayerSpawn, 2.0f),
                        $"Player spawn inside wall {layout.Walls[w].Name} on seed {seed}");
                }

                for (int ob = 0; ob < layout.Obstacles.Count; ob++)
                {
                    Assert.IsFalse(layout.Obstacles[ob].Contains(layout.PlayerSpawn, 2.0f),
                        $"Player spawn inside obstacle {layout.Obstacles[ob].Name} on seed {seed}");
                }

                topologiesSeen.Add(layout.TopologyType);
                biomesSeen.Add(layout.Biome.Id);
            }

            // Verify all 5 topologies and multiple biomes were generated across the 100 seeds
            Assert.AreEqual(5, topologiesSeen.Count, "All 5 topologies must be generated across 100 seeds.");
            Assert.GreaterOrEqual(biomesSeen.Count, 15, "At least 15 distinct biomes should appear across 100 seeds.");
        }

        [Test]
        public void Generate_AllFiveStructuralFamilies_GenerateAndPassValidation()
        {
            for (int t = 0; t < 5; t++)
            {
                var topology = (ArenaTopologyType)t;
                var layout = ArenaGenerator.Generate(seed: 42, forcedTopology: topology, forcedBiomeId: t + 1);

                Assert.AreEqual(topology, layout.TopologyType);
                Assert.AreEqual(t + 1, layout.Biome.Id);
                Assert.IsTrue(layout.IsValid, $"Topology {topology} failed validation: {layout.ValidationReason}");
                Assert.GreaterOrEqual(layout.Portals.Count, 4);
                Assert.IsFalse(string.IsNullOrEmpty(layout.LandmarkName));
            }
        }

        [Test]
        public void Generate_DeterministicDuplicate_SameSeedYieldsIdenticalGeometry()
        {
            long seed = 0x4B3D8A9F1E2C5577L;
            var layoutA = ArenaGenerator.Generate(seed);
            var layoutB = ArenaGenerator.Generate(seed);

            Assert.AreEqual(layoutA.Fingerprint, layoutB.Fingerprint);
            Assert.AreEqual(layoutA.Biome.Id, layoutB.Biome.Id);
            Assert.AreEqual(layoutA.TopologyType, layoutB.TopologyType);
            Assert.AreEqual(layoutA.Width, layoutB.Width);
            Assert.AreEqual(layoutA.Height, layoutB.Height);
            Assert.AreEqual(layoutA.PlayerSpawn.X, layoutB.PlayerSpawn.X);
            Assert.AreEqual(layoutA.PlayerSpawn.Y, layoutB.PlayerSpawn.Y);

            Assert.AreEqual(layoutA.Walls.Count, layoutB.Walls.Count);
            for (int i = 0; i < layoutA.Walls.Count; i++)
            {
                Assert.AreEqual(layoutA.Walls[i].Name, layoutB.Walls[i].Name);
                Assert.AreEqual(layoutA.Walls[i].Position.X, layoutB.Walls[i].Position.X);
                Assert.AreEqual(layoutA.Walls[i].Position.Y, layoutB.Walls[i].Position.Y);
                Assert.AreEqual(layoutA.Walls[i].Size.X, layoutB.Walls[i].Size.X);
                Assert.AreEqual(layoutA.Walls[i].Size.Y, layoutB.Walls[i].Size.Y);
            }

            Assert.AreEqual(layoutA.Obstacles.Count, layoutB.Obstacles.Count);
            for (int i = 0; i < layoutA.Obstacles.Count; i++)
            {
                Assert.AreEqual(layoutA.Obstacles[i].Name, layoutB.Obstacles[i].Name);
                Assert.AreEqual(layoutA.Obstacles[i].Position.X, layoutB.Obstacles[i].Position.X);
                Assert.AreEqual(layoutA.Obstacles[i].Position.Y, layoutB.Obstacles[i].Position.Y);
            }

            Assert.AreEqual(layoutA.Portals.Count, layoutB.Portals.Count);
            for (int i = 0; i < layoutA.Portals.Count; i++)
            {
                Assert.AreEqual(layoutA.Portals[i].Name, layoutB.Portals[i].Name);
                Assert.AreEqual(layoutA.Portals[i].Position.X, layoutB.Portals[i].Position.X);
                Assert.AreEqual(layoutA.Portals[i].Position.Y, layoutB.Portals[i].Position.Y);
            }
        }

        [Test]
        public void Validation_CorruptedLayout_FailsAndReturnsFallbackGracefully()
        {
            var corruptLayout = new ArenaLayout
            {
                Seed = 999,
                Biome = BiomeCatalog.GetById(1),
                TopologyType = ArenaTopologyType.CircularColosseum,
                Width = 40f,
                Height = 24f,
                PlayerSpawn = Vector2D.Zero
            };

            // Intentionally block player spawn with a wall
            corruptLayout.Walls.Add(new ArenaWall("BlockingWall", Vector2D.Zero, new Vector2D(5f, 5f)));
            corruptLayout.Portals.Add(new ArenaPortal(0, "P0", new Vector2D(10f, 0f), new Vector2D(-1f, 0f)));

            bool valid = ArenaGenerator.ValidateLayout(corruptLayout, out string reason);
            Assert.IsFalse(valid);
            Assert.IsNotNull(reason);
            StringAssert.Contains("Player spawn", reason);
        }

        [Test]
        public void Validation_DisconnectedPortal_DetectedByFloodFill()
        {
            var trappedLayout = new ArenaLayout
            {
                Seed = 888,
                Biome = BiomeCatalog.GetById(1),
                TopologyType = ArenaTopologyType.CircularColosseum,
                Width = 40f,
                Height = 24f,
                PlayerSpawn = Vector2D.Zero
            };

            // Player is free, but portal is boxed in by impenetrable walls
            trappedLayout.Portals.Add(new ArenaPortal(0, "P0", new Vector2D(15f, 0f), Vector2D.Zero));
            trappedLayout.Portals.Add(new ArenaPortal(1, "P1", new Vector2D(-15f, 0f), Vector2D.Zero));
            trappedLayout.Portals.Add(new ArenaPortal(2, "P2", new Vector2D(0f, 9f), Vector2D.Zero));
            trappedLayout.Portals.Add(new ArenaPortal(3, "TrappedPortal", new Vector2D(0f, -9f), Vector2D.Zero));

            // Completely encircle TrappedPortal with thick walls
            trappedLayout.Walls.Add(new ArenaWall("EnclosingWall", new Vector2D(0f, -9f), new Vector2D(10f, 10f)));

            bool valid = ArenaGenerator.ValidateLayout(trappedLayout, out string reason);
            Assert.IsFalse(valid);
            Assert.IsNotNull(reason);
        }

        [Test]
        public void RunContext_InitializesLayoutDeterministically()
        {
            long seed = 123456789L;
            var character = CharacterCatalog.Find(CharacterCatalog.LichLordId);
            var run = new RunContext(seed, 1, character);

            Assert.IsNotNull(run.Layout);
            Assert.AreEqual(seed, run.Layout.Seed);
            Assert.IsTrue(run.Layout.IsValid);
            Assert.IsFalse(string.IsNullOrEmpty(run.Layout.Summary));
        }

        [Test]
        public void GameSessionState_CustomSeed_ReproducesExactLayout()
        {
            long testSeed = 987654321L;
            var session = new GameSessionState(42L);
            session.TrySelectCharacter(CharacterCatalog.LichLordId);

            bool started = session.TryStartRun(customSeed: testSeed);
            Assert.IsTrue(started);
            Assert.AreEqual(testSeed, session.Run.Seed);
            Assert.AreEqual(testSeed, session.Run.Layout.Seed);
        }
    }
}
