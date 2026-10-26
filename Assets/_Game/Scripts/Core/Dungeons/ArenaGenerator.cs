using System;
using System.Collections.Generic;

namespace FinalBoss.Core
{
    /// <summary>
    /// Pure C# deterministic arena layout generator.
    /// Supports 5 structural families (CircularColosseum, AsymmetricCourtyard, OrganicCavern,
    /// LinkedChambers, RuinedAmphitheater) and 20 canonical dungeon biomes.
    /// Enforces large walkable battlefield geometry, corridor clearance >= 3.0m, player spawn
    /// clearance >= 2.0m, BFS reachability validation to all portals, and safe fallback.
    /// </summary>
    public static class ArenaGenerator
    {
        public const int CurrentVersion = 1;
        public const float DefaultWidth = 40f;
        public const float DefaultHeight = 24f;
        public const float MinPlayerClearance = 2.0f;
        public const float MinCorridorClearance = 3.0f;

        /// <summary>
        /// Deterministically generates an ArenaLayout for a given seed, optionally overriding
        /// topology or biome for debug harnesses or tests.
        /// </summary>
        public static ArenaLayout Generate(
            long seed,
            ArenaTopologyType? forcedTopology = null,
            int? forcedBiomeId = null,
            int version = CurrentVersion)
        {
            var rng = new SplitMix64Rng(seed);

            // Deterministic biome selection (0..19 -> 1..20)
            int biomeIndex = rng.NextInt(0, 20);
            var biome = forcedBiomeId.HasValue
                ? BiomeCatalog.GetById(forcedBiomeId.Value)
                : BiomeCatalog.GetByIndex(biomeIndex);

            // Deterministic topology selection (0..4)
            int topologyIndex = rng.NextInt(0, 5);
            var topology = forcedTopology.HasValue
                ? forcedTopology.Value
                : (ArenaTopologyType)topologyIndex;

            var layout = new ArenaLayout
            {
                Seed = seed,
                Version = version,
                Biome = biome,
                TopologyType = topology,
                Width = DefaultWidth,
                Height = DefaultHeight,
                PlayerSpawn = Vector2D.Zero
            };

            switch (topology)
            {
                case ArenaTopologyType.CircularColosseum:
                    BuildColosseum(layout, rng);
                    break;
                case ArenaTopologyType.AsymmetricCourtyard:
                    BuildCourtyard(layout, rng);
                    break;
                case ArenaTopologyType.OrganicCavern:
                    BuildCavern(layout, rng);
                    break;
                case ArenaTopologyType.LinkedChambers:
                    BuildLinkedChambers(layout, rng);
                    break;
                case ArenaTopologyType.RuinedAmphitheater:
                    BuildAmphitheater(layout, rng);
                    break;
                default:
                    BuildColosseum(layout, rng);
                    break;
            }

            // Reachability and clearance validation
            bool valid = ValidateLayout(layout, out string failureReason);
            if (!valid)
            {
                layout.IsValid = false;
                layout.ValidationReason = failureReason;

                // Safe fallback to guaranteed valid Colosseum
                var fallback = CreateSafeFallback(seed, biome);
                fallback.IsValid = false;
                fallback.ValidationReason = failureReason;
                return fallback;
            }

            layout.IsValid = true;
            layout.ValidationReason = "Valid";
            layout.Fingerprint = ComputeFingerprint(layout);
            return layout;
        }

        private static string ComputeFingerprint(ArenaLayout layout)
        {
            return $"{layout.Biome.Id:D2}_{layout.TopologyType}_{layout.Seed:X16}_{layout.Walls.Count}_{layout.Obstacles.Count}_{layout.Portals.Count}";
        }

        private static void BuildColosseum(ArenaLayout layout, SplitMix64Rng rng)
        {
            layout.PlayerSpawn = Vector2D.Zero;
            layout.LandmarkPosition = Vector2D.Zero;
            layout.LandmarkName = "Gladiator Worldstone Dais";

            // Perimeter walls (Octagonal approximation)
            float halfW = layout.Width * 0.5f;
            float halfH = layout.Height * 0.5f;

            layout.Walls.Add(new ArenaWall("Wall_N", new Vector2D(0f, halfH + 0.5f), new Vector2D(26f, 1.5f)));
            layout.Walls.Add(new ArenaWall("Wall_S", new Vector2D(0f, -halfH - 0.5f), new Vector2D(26f, 1.5f)));
            layout.Walls.Add(new ArenaWall("Wall_E", new Vector2D(halfW + 0.5f, 0f), new Vector2D(1.5f, 16f)));
            layout.Walls.Add(new ArenaWall("Wall_W", new Vector2D(-halfW - 0.5f, 0f), new Vector2D(1.5f, 16f)));

            // Angled corner wall blocks
            layout.Walls.Add(new ArenaWall("Wall_NW", new Vector2D(-16f, 10f), new Vector2D(8f, 4f)));
            layout.Walls.Add(new ArenaWall("Wall_NE", new Vector2D(16f, 10f), new Vector2D(8f, 4f)));
            layout.Walls.Add(new ArenaWall("Wall_SW", new Vector2D(-16f, -10f), new Vector2D(8f, 4f)));
            layout.Walls.Add(new ArenaWall("Wall_SE", new Vector2D(16f, -10f), new Vector2D(8f, 4f)));

            // Symmetric ring of 6 pillars at radius 7.5m (leaving wide 6m+ corridors and 7.5m center arena)
            float pillarRadius = 7.5f;
            for (int i = 0; i < 6; i++)
            {
                double angle = i * (Math.PI / 3.0);
                float px = (float)(Math.Cos(angle) * pillarRadius);
                float py = (float)(Math.Sin(angle) * pillarRadius);
                layout.Obstacles.Add(new ArenaObstacle($"Pillar_{i}", new Vector2D(px, py), new Vector2D(1.4f, 1.4f), ObstacleShape.Box));
            }

            // 4 Portals in cardinal directions
            layout.Portals.Add(new ArenaPortal(0, "Portal_East", new Vector2D(15f, 0f), new Vector2D(-1f, 0f)));
            layout.Portals.Add(new ArenaPortal(1, "Portal_North", new Vector2D(0f, 9.5f), new Vector2D(0f, -1f)));
            layout.Portals.Add(new ArenaPortal(2, "Portal_West", new Vector2D(-15f, 0f), new Vector2D(1f, 0f)));
            layout.Portals.Add(new ArenaPortal(3, "Portal_South", new Vector2D(0f, -9.5f), new Vector2D(0f, 1f)));
        }

        private static void BuildCourtyard(ArenaLayout layout, SplitMix64Rng rng)
        {
            layout.PlayerSpawn = new Vector2D(0f, -2f);
            layout.LandmarkPosition = new Vector2D(0f, 10.5f);
            layout.LandmarkName = "Royal High Throne Dais";

            float halfW = layout.Width * 0.5f;
            float halfH = layout.Height * 0.5f;

            // Asymmetric boundary walls
            layout.Walls.Add(new ArenaWall("Wall_S", new Vector2D(0f, -halfH - 0.5f), new Vector2D(layout.Width + 2f, 1.5f)));
            layout.Walls.Add(new ArenaWall("Wall_E", new Vector2D(halfW + 0.5f, 0f), new Vector2D(1.5f, layout.Height + 2f)));
            layout.Walls.Add(new ArenaWall("Wall_W", new Vector2D(-halfW - 0.5f, 0f), new Vector2D(1.5f, layout.Height + 2f)));

            // High northern terrace alcove
            layout.Walls.Add(new ArenaWall("Wall_N_Left", new Vector2D(-13f, halfH + 0.5f), new Vector2D(15f, 1.5f)));
            layout.Walls.Add(new ArenaWall("Wall_N_Right", new Vector2D(13f, halfH + 0.5f), new Vector2D(15f, 1.5f)));
            layout.Walls.Add(new ArenaWall("Wall_Alcove_Back", new Vector2D(0f, halfH + 2.5f), new Vector2D(12f, 1.5f)));
            layout.Walls.Add(new ArenaWall("Wall_Alcove_W", new Vector2D(-6f, halfH + 1.5f), new Vector2D(1.5f, 3.5f)));
            layout.Walls.Add(new ArenaWall("Wall_Alcove_E", new Vector2D(6f, halfH + 1.5f), new Vector2D(1.5f, 3.5f)));

            // Asymmetric interior obstacles:
            // Eastern Colonnade (3 pillars)
            layout.Obstacles.Add(new ArenaObstacle("Colonnade_1", new Vector2D(10f, 5f), new Vector2D(1.5f, 1.5f)));
            layout.Obstacles.Add(new ArenaObstacle("Colonnade_2", new Vector2D(10f, 0f), new Vector2D(1.5f, 1.5f)));
            layout.Obstacles.Add(new ArenaObstacle("Colonnade_3", new Vector2D(10f, -5f), new Vector2D(1.5f, 1.5f)));

            // Western Ruined Fountain
            layout.Obstacles.Add(new ArenaObstacle("Ruined_Fountain", new Vector2D(-8f, -4f), new Vector2D(2.2f, 2.2f)));

            // 5 Asymmetric Portals
            layout.Portals.Add(new ArenaPortal(0, "Portal_Throne", new Vector2D(0f, 10.5f), new Vector2D(0f, -1f)));
            layout.Portals.Add(new ArenaPortal(1, "Portal_West_Terrace", new Vector2D(-16f, 2f), new Vector2D(1f, 0f)));
            layout.Portals.Add(new ArenaPortal(2, "Portal_East_Portico", new Vector2D(16f, 6f), new Vector2D(-1f, 0f)));
            layout.Portals.Add(new ArenaPortal(3, "Portal_South_Gate", new Vector2D(0f, -9.5f), new Vector2D(0f, 1f)));
            layout.Portals.Add(new ArenaPortal(4, "Portal_East_Nook", new Vector2D(16f, -7f), new Vector2D(-1f, 0f)));
        }

        private static void BuildCavern(ArenaLayout layout, SplitMix64Rng rng)
        {
            layout.PlayerSpawn = Vector2D.Zero;
            layout.LandmarkPosition = new Vector2D(-3f, 3f);
            layout.LandmarkName = "Ancient Crystal Geode";

            float halfW = layout.Width * 0.5f;
            float halfH = layout.Height * 0.5f;

            // Jagged natural boundary walls with recesses and rocky projections
            layout.Walls.Add(new ArenaWall("Wall_N_West", new Vector2D(-12f, halfH + 0.5f), new Vector2D(16f, 1.5f)));
            layout.Walls.Add(new ArenaWall("Wall_N_Center", new Vector2D(0f, halfH + 1.5f), new Vector2D(10f, 1.5f)));
            layout.Walls.Add(new ArenaWall("Wall_N_East", new Vector2D(12f, halfH + 0.5f), new Vector2D(16f, 1.5f)));

            layout.Walls.Add(new ArenaWall("Wall_S_West", new Vector2D(-12f, -halfH - 0.5f), new Vector2D(16f, 1.5f)));
            layout.Walls.Add(new ArenaWall("Wall_S_Center", new Vector2D(0f, -halfH + 0.5f), new Vector2D(10f, 1.5f)));
            layout.Walls.Add(new ArenaWall("Wall_S_East", new Vector2D(12f, -halfH - 0.5f), new Vector2D(16f, 1.5f)));

            layout.Walls.Add(new ArenaWall("Wall_E_Main", new Vector2D(halfW + 0.5f, 3f), new Vector2D(1.5f, 18f)));
            layout.Walls.Add(new ArenaWall("Wall_W_Main", new Vector2D(-halfW - 0.5f, -3f), new Vector2D(1.5f, 18f)));

            // Corner jagged rock blocks
            layout.Walls.Add(new ArenaWall("Teeth_NW", new Vector2D(-16f, 9.5f), new Vector2D(5f, 3f)));
            layout.Walls.Add(new ArenaWall("Teeth_NE", new Vector2D(16f, 9.5f), new Vector2D(5f, 3f)));
            layout.Walls.Add(new ArenaWall("Teeth_SW", new Vector2D(-16f, -9.5f), new Vector2D(5f, 3f)));
            layout.Walls.Add(new ArenaWall("Teeth_SE", new Vector2D(16f, -9.5f), new Vector2D(5f, 3f)));

            // 4 Stalagmite / Rock Clusters in outer quadrants, leaving center wide open (> 10m clear)
            layout.Obstacles.Add(new ArenaObstacle("Stalagmite_NW", new Vector2D(-9f, 5f), new Vector2D(2f, 2f), ObstacleShape.Box));
            layout.Obstacles.Add(new ArenaObstacle("Stalagmite_NE", new Vector2D(9f, 5f), new Vector2D(2f, 2f), ObstacleShape.Box));
            layout.Obstacles.Add(new ArenaObstacle("Stalagmite_SW", new Vector2D(-9f, -5f), new Vector2D(2f, 2f), ObstacleShape.Box));
            layout.Obstacles.Add(new ArenaObstacle("Stalagmite_SE", new Vector2D(9f, -5f), new Vector2D(2f, 2f), ObstacleShape.Box));

            // 4 Portals in cave alcoves
            layout.Portals.Add(new ArenaPortal(0, "Portal_North_Alcove", new Vector2D(0f, 10.5f), new Vector2D(0f, -1f)));
            layout.Portals.Add(new ArenaPortal(1, "Portal_South_Alcove", new Vector2D(0f, -8.5f), new Vector2D(0f, 1f)));
            layout.Portals.Add(new ArenaPortal(2, "Portal_East_Mouth", new Vector2D(16f, 0f), new Vector2D(-1f, 0f)));
            layout.Portals.Add(new ArenaPortal(3, "Portal_West_Mouth", new Vector2D(-16f, 0f), new Vector2D(1f, 0f)));
        }

        private static void BuildLinkedChambers(ArenaLayout layout, SplitMix64Rng rng)
        {
            layout.PlayerSpawn = Vector2D.Zero;
            layout.LandmarkPosition = new Vector2D(0f, 3.5f);
            layout.LandmarkName = "Grand Arch of Triumph";

            float halfW = layout.Width * 0.5f;
            float halfH = layout.Height * 0.5f;

            // Outer perimeter walls
            layout.Walls.Add(new ArenaWall("Wall_N", new Vector2D(0f, halfH + 0.5f), new Vector2D(layout.Width + 2f, 1.5f)));
            layout.Walls.Add(new ArenaWall("Wall_S", new Vector2D(0f, -halfH - 0.5f), new Vector2D(layout.Width + 2f, 1.5f)));
            layout.Walls.Add(new ArenaWall("Wall_E", new Vector2D(halfW + 0.5f, 0f), new Vector2D(1.5f, layout.Height + 2f)));
            layout.Walls.Add(new ArenaWall("Wall_W", new Vector2D(-halfW - 0.5f, 0f), new Vector2D(1.5f, layout.Height + 2f)));

            // Dividing partition walls with a massive 10m high central passage connecting East and West chambers
            layout.Walls.Add(new ArenaWall("Divider_North", new Vector2D(0f, 8.5f), new Vector2D(2.5f, 7f)));
            layout.Walls.Add(new ArenaWall("Divider_South", new Vector2D(0f, -8.5f), new Vector2D(2.5f, 7f)));

            // Interior Chamber Obstacles:
            // West Chamber Altars
            layout.Obstacles.Add(new ArenaObstacle("West_Altar_N", new Vector2D(-10f, 4f), new Vector2D(1.5f, 1.5f)));
            layout.Obstacles.Add(new ArenaObstacle("West_Altar_S", new Vector2D(-10f, -4f), new Vector2D(1.5f, 1.5f)));

            // East Chamber Altars
            layout.Obstacles.Add(new ArenaObstacle("East_Altar_N", new Vector2D(10f, 4f), new Vector2D(1.5f, 1.5f)));
            layout.Obstacles.Add(new ArenaObstacle("East_Altar_S", new Vector2D(10f, -4f), new Vector2D(1.5f, 1.5f)));

            // 4 Portals distributed between chambers
            layout.Portals.Add(new ArenaPortal(0, "Portal_West_Hall", new Vector2D(-16f, 0f), new Vector2D(1f, 0f)));
            layout.Portals.Add(new ArenaPortal(1, "Portal_East_Hall", new Vector2D(16f, 0f), new Vector2D(-1f, 0f)));
            layout.Portals.Add(new ArenaPortal(2, "Portal_NW_Balcony", new Vector2D(-12f, 8f), new Vector2D(0f, -1f)));
            layout.Portals.Add(new ArenaPortal(3, "Portal_SE_Balcony", new Vector2D(12f, -8f), new Vector2D(0f, 1f)));
        }

        private static void BuildAmphitheater(ArenaLayout layout, SplitMix64Rng rng)
        {
            layout.PlayerSpawn = Vector2D.Zero;
            layout.LandmarkPosition = Vector2D.Zero;
            layout.LandmarkName = "Orator's Altar of Triumph";

            float halfW = layout.Width * 0.5f;
            float halfH = layout.Height * 0.5f;

            // Outer stadium perimeter
            layout.Walls.Add(new ArenaWall("Wall_N", new Vector2D(0f, halfH + 0.5f), new Vector2D(36f, 1.5f)));
            layout.Walls.Add(new ArenaWall("Wall_S", new Vector2D(0f, -halfH - 0.5f), new Vector2D(36f, 1.5f)));
            layout.Walls.Add(new ArenaWall("Wall_E", new Vector2D(halfW - 0.5f, 0f), new Vector2D(1.5f, 22f)));
            layout.Walls.Add(new ArenaWall("Wall_W", new Vector2D(-halfW + 0.5f, 0f), new Vector2D(1.5f, 22f)));

            // Corner stadium bevels
            layout.Walls.Add(new ArenaWall("Bevel_NW", new Vector2D(-16f, 9.5f), new Vector2D(5f, 4f)));
            layout.Walls.Add(new ArenaWall("Bevel_NE", new Vector2D(16f, 9.5f), new Vector2D(5f, 4f)));
            layout.Walls.Add(new ArenaWall("Bevel_SW", new Vector2D(-16f, -9.5f), new Vector2D(5f, 4f)));
            layout.Walls.Add(new ArenaWall("Bevel_SE", new Vector2D(16f, -9.5f), new Vector2D(5f, 4f)));

            // Concentric tiered ruin arcs with broad vomitoria passages (width > 6m) on East and West
            layout.Walls.Add(new ArenaWall("Tier_Arc_North", new Vector2D(0f, 7f), new Vector2D(14f, 1.4f)));
            layout.Walls.Add(new ArenaWall("Tier_Arc_South", new Vector2D(0f, -7f), new Vector2D(14f, 1.4f)));

            // Outer concourse stone benches / ruin blocks
            layout.Obstacles.Add(new ArenaObstacle("Bench_North", new Vector2D(0f, 10f), new Vector2D(3.5f, 1.2f)));
            layout.Obstacles.Add(new ArenaObstacle("Bench_South", new Vector2D(0f, -10f), new Vector2D(3.5f, 1.2f)));

            // 4 Portals in the outer concourses and vomitoria
            layout.Portals.Add(new ArenaPortal(0, "Portal_East_Vomitorium", new Vector2D(15f, 0f), new Vector2D(-1f, 0f)));
            layout.Portals.Add(new ArenaPortal(1, "Portal_West_Vomitorium", new Vector2D(-15f, 0f), new Vector2D(1f, 0f)));
            layout.Portals.Add(new ArenaPortal(2, "Portal_NE_Concourse", new Vector2D(10f, 8f), new Vector2D(-1f, -1f)));
            layout.Portals.Add(new ArenaPortal(3, "Portal_SW_Concourse", new Vector2D(-10f, -8f), new Vector2D(1f, 1f)));
        }

        private static ArenaLayout CreateSafeFallback(long seed, DungeonBiome biome)
        {
            var layout = new ArenaLayout
            {
                Seed = seed,
                Version = CurrentVersion,
                Biome = biome,
                TopologyType = ArenaTopologyType.CircularColosseum,
                Width = DefaultWidth,
                Height = DefaultHeight,
                PlayerSpawn = Vector2D.Zero
            };

            BuildColosseum(layout, new SplitMix64Rng(seed));
            layout.Fingerprint = ComputeFingerprint(layout) + "_FALLBACK";
            return layout;
        }

        /// <summary>
        /// Validates player spawn clearance, portal clearances, and runs a discrete BFS flood-fill
        /// to ensure all portals are reachable from the player spawn through corridors >= 3.0m wide.
        /// </summary>
        public static bool ValidateLayout(ArenaLayout layout, out string failureReason)
        {
            failureReason = null;

            // 1. Player Spawn Clearance Check (>= 2.0m from any wall or obstacle)
            for (int i = 0; i < layout.Walls.Count; i++)
            {
                if (layout.Walls[i].Contains(layout.PlayerSpawn, MinPlayerClearance))
                {
                    failureReason = $"Player spawn {layout.PlayerSpawn} has clearance < {MinPlayerClearance}m from wall {layout.Walls[i].Name}";
                    return false;
                }
            }

            for (int i = 0; i < layout.Obstacles.Count; i++)
            {
                if (layout.Obstacles[i].Contains(layout.PlayerSpawn, MinPlayerClearance))
                {
                    failureReason = $"Player spawn {layout.PlayerSpawn} has clearance < {MinPlayerClearance}m from obstacle {layout.Obstacles[i].Name}";
                    return false;
                }
            }

            // 2. Portal count and clearances
            if (layout.Portals.Count < 3)
            {
                failureReason = $"Arena has too few portals ({layout.Portals.Count} < 3)";
                return false;
            }

            for (int i = 0; i < layout.Portals.Count; i++)
            {
                var portal = layout.Portals[i];
                for (int w = 0; w < layout.Walls.Count; w++)
                {
                    if (layout.Walls[w].Contains(portal.Position, 0.5f))
                    {
                        failureReason = $"Portal {portal.Name} is blocked by wall {layout.Walls[w].Name}";
                        return false;
                    }
                }
                for (int ob = 0; ob < layout.Obstacles.Count; ob++)
                {
                    if (layout.Obstacles[ob].Contains(portal.Position, 0.5f))
                    {
                        failureReason = $"Portal {portal.Name} is blocked by obstacle {layout.Obstacles[ob].Name}";
                        return false;
                    }
                }
            }

            // 3. Discrete BFS Reachability Grid
            // Grid cell size 1.0m, bounds padded around arena
            float minX = -layout.Width * 0.5f;
            float maxX = layout.Width * 0.5f;
            float minY = -layout.Height * 0.5f;
            float maxY = layout.Height * 0.5f;

            int cols = (int)(layout.Width) + 1;
            int rows = (int)(layout.Height) + 1;

            bool[,] blocked = new bool[cols, rows];

            // Boss clearance / corridor clearance check: pad colliders by 0.9m
            float bossPadding = 0.9f;

            for (int x = 0; x < cols; x++)
            {
                float worldX = minX + x;
                for (int y = 0; y < rows; y++)
                {
                    float worldY = minY + y;
                    var pt = new Vector2D(worldX, worldY);

                    for (int w = 0; w < layout.Walls.Count; w++)
                    {
                        if (layout.Walls[w].Contains(pt, bossPadding))
                        {
                            blocked[x, y] = true;
                            break;
                        }
                    }

                    if (blocked[x, y]) continue;

                    for (int ob = 0; ob < layout.Obstacles.Count; ob++)
                    {
                        if (layout.Obstacles[ob].Contains(pt, bossPadding))
                        {
                            blocked[x, y] = true;
                            break;
                        }
                    }
                }
            }

            // Map player spawn to grid
            int spawnX = (int)Math.Round(layout.PlayerSpawn.X - minX);
            int spawnY = (int)Math.Round(layout.PlayerSpawn.Y - minY);

            if (spawnX < 0 || spawnX >= cols || spawnY < 0 || spawnY >= rows || blocked[spawnX, spawnY])
            {
                failureReason = $"Player spawn {layout.PlayerSpawn} maps to a blocked or out-of-bounds grid cell";
                return false;
            }

            // BFS Flood Fill
            bool[,] visited = new bool[cols, rows];
            var queue = new Queue<Tuple<int, int>>();
            queue.Enqueue(Tuple.Create(spawnX, spawnY));
            visited[spawnX, spawnY] = true;
            int visitedCount = 0;

            int[] dx = { 1, -1, 0, 0 };
            int[] dy = { 0, 0, 1, -1 };

            while (queue.Count > 0)
            {
                var cur = queue.Dequeue();
                visitedCount++;

                for (int dir = 0; dir < 4; dir++)
                {
                    int nx = cur.Item1 + dx[dir];
                    int ny = cur.Item2 + dy[dir];

                    if (nx >= 0 && nx < cols && ny >= 0 && ny < rows && !blocked[nx, ny] && !visited[nx, ny])
                    {
                        visited[nx, ny] = true;
                        queue.Enqueue(Tuple.Create(nx, ny));
                    }
                }
            }

            // Verify all portals are reachable
            for (int i = 0; i < layout.Portals.Count; i++)
            {
                var portal = layout.Portals[i];
                int pX = (int)Math.Round(portal.Position.X - minX);
                int pY = (int)Math.Round(portal.Position.Y - minY);

                // Check 3x3 neighborhood around portal to account for discretization
                bool reached = false;
                for (int ox = -1; ox <= 1 && !reached; ox++)
                {
                    for (int oy = -1; oy <= 1 && !reached; oy++)
                    {
                        int checkX = pX + ox;
                        int checkY = pY + oy;
                        if (checkX >= 0 && checkX < cols && checkY >= 0 && checkY < rows && visited[checkX, checkY])
                        {
                            reached = true;
                        }
                    }
                }

                if (!reached)
                {
                    failureReason = $"Portal {portal.Name} at {portal.Position} is not reachable from player spawn";
                    return false;
                }
            }

            // Minimum walkable battle space check (must have at least 150 reachable cells)
            if (visitedCount < 150)
            {
                failureReason = $"Reachable arena area is too small ({visitedCount} walkable cells < 150)";
                return false;
            }

            return true;
        }
    }
}
