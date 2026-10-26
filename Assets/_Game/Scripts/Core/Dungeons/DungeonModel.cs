using System;
using System.Collections.Generic;

namespace FinalBoss.Core
{
    [Serializable]
    public struct Vector2D
    {
        public float X;
        public float Y;

        public Vector2D(float x, float y)
        {
            X = x;
            Y = y;
        }

        public static Vector2D Zero => new Vector2D(0f, 0f);
        public static Vector2D One => new Vector2D(1f, 1f);

        public float SqrMagnitude => X * X + Y * Y;
        public float Magnitude => (float)Math.Sqrt(X * X + Y * Y);

        public static float Distance(Vector2D a, Vector2D b)
        {
            float dx = a.X - b.X;
            float dy = a.Y - b.Y;
            return (float)Math.Sqrt(dx * dx + dy * dy);
        }

        public static Vector2D operator +(Vector2D a, Vector2D b) => new Vector2D(a.X + b.X, a.Y + b.Y);
        public static Vector2D operator -(Vector2D a, Vector2D b) => new Vector2D(a.X - b.X, a.Y - b.Y);
        public static Vector2D operator *(Vector2D a, float d) => new Vector2D(a.X * d, a.Y * d);
        public static Vector2D operator /(Vector2D a, float d) => new Vector2D(a.X / d, a.Y / d);

        public override string ToString() => $"({X:F1}, {Y:F1})";
    }

    [Serializable]
    public struct ColorData
    {
        public float R;
        public float G;
        public float B;
        public float A;

        public ColorData(float r, float g, float b, float a = 1f)
        {
            R = r; G = g; B = b; A = a;
        }

        public static ColorData FromHex(string hex)
        {
            if (string.IsNullOrEmpty(hex)) return new ColorData(1f, 1f, 1f, 1f);
            if (hex.StartsWith("#")) hex = hex.Substring(1);
            if (hex.Length == 6)
            {
                byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
                byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
                byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
                return new ColorData(r / 255f, g / 255f, b / 255f, 1f);
            }
            return new ColorData(1f, 1f, 1f, 1f);
        }
    }

    public enum ArenaTopologyType
    {
        CircularColosseum = 0,
        AsymmetricCourtyard = 1,
        OrganicCavern = 2,
        LinkedChambers = 3,
        RuinedAmphitheater = 4
    }

    public enum ObstacleShape
    {
        Box = 0,
        Circle = 1
    }

    [Serializable]
    public struct ArenaWall
    {
        public string Name;
        public Vector2D Position;
        public Vector2D Size;

        public ArenaWall(string name, Vector2D position, Vector2D size)
        {
            Name = name;
            Position = position;
            Size = size;
        }

        public float MinX => Position.X - Size.X * 0.5f;
        public float MaxX => Position.X + Size.X * 0.5f;
        public float MinY => Position.Y - Size.Y * 0.5f;
        public float MaxY => Position.Y + Size.Y * 0.5f;

        public bool Contains(Vector2D point, float padding = 0f)
        {
            return point.X >= MinX - padding && point.X <= MaxX + padding &&
                   point.Y >= MinY - padding && point.Y <= MaxY + padding;
        }
    }

    [Serializable]
    public struct ArenaObstacle
    {
        public string Name;
        public Vector2D Position;
        public Vector2D Size;
        public ObstacleShape Shape;

        public ArenaObstacle(string name, Vector2D position, Vector2D size, ObstacleShape shape = ObstacleShape.Box)
        {
            Name = name;
            Position = position;
            Size = size;
            Shape = shape;
        }

        public float MinX => Position.X - Size.X * 0.5f;
        public float MaxX => Position.X + Size.X * 0.5f;
        public float MinY => Position.Y - Size.Y * 0.5f;
        public float MaxY => Position.Y + Size.Y * 0.5f;

        public bool Contains(Vector2D point, float padding = 0f)
        {
            if (Shape == ObstacleShape.Circle)
            {
                float radius = Size.X * 0.5f + padding;
                return Vector2D.Distance(point, Position) <= radius;
            }
            return point.X >= MinX - padding && point.X <= MaxX + padding &&
                   point.Y >= MinY - padding && point.Y <= MaxY + padding;
        }
    }

    [Serializable]
    public struct ArenaPortal
    {
        public int Id;
        public string Name;
        public Vector2D Position;
        public Vector2D Facing;

        public ArenaPortal(int id, string name, Vector2D position, Vector2D facing)
        {
            Id = id;
            Name = name;
            Position = position;
            Facing = facing;
        }
    }

    public sealed class DungeonBiome
    {
        public int Id { get; }
        public string Name { get; }
        public string SourceDescription { get; }
        public ColorData FloorColor { get; }
        public ColorData WallColor { get; }
        public ColorData ObstacleColor { get; }
        public ColorData PortalColor { get; }
        public ColorData AccentColor { get; }

        public DungeonBiome(
            int id,
            string name,
            string desc,
            ColorData floor,
            ColorData wall,
            ColorData obstacle,
            ColorData portal,
            ColorData accent)
        {
            Id = id;
            Name = name;
            SourceDescription = desc;
            FloorColor = floor;
            WallColor = wall;
            ObstacleColor = obstacle;
            PortalColor = portal;
            AccentColor = accent;
        }
    }

    public static class BiomeCatalog
    {
        private static readonly DungeonBiome[] Biomes = new[]
        {
            new DungeonBiome(1, "Forgotten Necropolis",
                "Ancient graveyards, cracked tombstones, burial chambers, skeletal statues, crypt entrances, and drifting ghostly mist.",
                ColorData.FromHex("2A2E30"), ColorData.FromHex("3E4447"), ColorData.FromHex("6B7275"), ColorData.FromHex("4DEEEA"), ColorData.FromHex("A5B4FC")),

            new DungeonBiome(2, "Infernal Citadel",
                "Lava rivers, black volcanic rock, demonic architecture, burning braziers, chains, molten floors, and towering infernal statues.",
                ColorData.FromHex("1F1515"), ColorData.FromHex("331C1C"), ColorData.FromHex("592828"), ColorData.FromHex("FF5722"), ColorData.FromHex("F97316")),

            new DungeonBiome(3, "Frozen Cathedral",
                "Frost-covered stone floors, frozen fountains, enormous ice crystals, ruined religious architecture, and drifting snow.",
                ColorData.FromHex("202B3B"), ColorData.FromHex("1E293B"), ColorData.FromHex("7DD3FC"), ColorData.FromHex("38BDF8"), ColorData.FromHex("BAE6FD")),

            new DungeonBiome(4, "Vampire's Crimson Palace",
                "Elegant gothic flooring, red carpets, marble pillars, grand chandeliers, stained glass, crimson curtains, and royal vampire decorations.",
                ColorData.FromHex("2D151E"), ColorData.FromHex("1E161B"), ColorData.FromHex("7F1D1D"), ColorData.FromHex("E11D48"), ColorData.FromHex("FDA4AF")),

            new DungeonBiome(5, "Shadow Realm",
                "Floating ruins, dark stone platforms, surreal architecture, strange glowing plants, spectral fog, and mysterious shadow anomalies.",
                ColorData.FromHex("181424"), ColorData.FromHex("231C38"), ColorData.FromHex("4C1D95"), ColorData.FromHex("A855F7"), ColorData.FromHex("C084FC")),

            new DungeonBiome(6, "Ancient Golem Sanctuary",
                "Massive stone structures, ancient runic engravings, carved pillars, magical crystals, moss-covered statues, and glowing mechanical runes.",
                ColorData.FromHex("1E2621"), ColorData.FromHex("2A382F"), ColorData.FromHex("4D7C0F"), ColorData.FromHex("10B981"), ColorData.FromHex("6EE7B7")),

            new DungeonBiome(7, "Corrupted Enchanted Forest",
                "Twisted trees, glowing mushrooms, magical roots, overgrown ruins, strange flowers, and mysterious forest spirits.",
                ColorData.FromHex("1E231B"), ColorData.FromHex("2D3B27"), ColorData.FromHex("4338CA"), ColorData.FromHex("8B5CF6"), ColorData.FromHex("A7F3D0")),

            new DungeonBiome(8, "Sunken Temple",
                "Flooded chambers, shallow water, coral-covered ruins, ancient mosaics, broken fountains, seaweed, and underwater magical effects.",
                ColorData.FromHex("16272C"), ColorData.FromHex("1E3A42"), ColorData.FromHex("0D9488"), ColorData.FromHex("06B6D4"), ColorData.FromHex("67E8F9")),

            new DungeonBiome(9, "Abandoned Dwarven Forge",
                "Massive anvils, machinery, metal flooring, conveyor mechanisms, molten forges, enormous chains, and underground workshops.",
                ColorData.FromHex("2B2625"), ColorData.FromHex("3D3432"), ColorData.FromHex("B45309"), ColorData.FromHex("F59E0B"), ColorData.FromHex("FCD34D")),

            new DungeonBiome(10, "Arcane Library",
                "Enchanted bookshelves, floating books, magical books, crystal lanterns, runic floors, arcane laboratories, and mysterious magical devices.",
                ColorData.FromHex("1E1B2E"), ColorData.FromHex("2E2548"), ColorData.FromHex("6366F1"), ColorData.FromHex("3B82F6"), ColorData.FromHex("93C5FD")),

            new DungeonBiome(11, "Plague Kingdom",
                "Abandoned settlements, broken carts, corrupted vegetation, toxic pools, decaying architecture, and sickly atmospheric fog.",
                ColorData.FromHex("252B1E"), ColorData.FromHex("333D29"), ColorData.FromHex("65A30D"), ColorData.FromHex("84CC16"), ColorData.FromHex("BEF264")),

            new DungeonBiome(12, "Crystal Caverns",
                "Enormous glowing crystal clusters, underground lakes, reflective mineral surfaces, sparkling rocks, and colorful natural cave formations.",
                ColorData.FromHex("182230"), ColorData.FromHex("223348"), ColorData.FromHex("06B6D4"), ColorData.FromHex("EC4899"), ColorData.FromHex("F472B6")),

            new DungeonBiome(13, "Cursed Royal Castle",
                "Beautiful ruined royal halls, fractured marble, throne rooms, ancient banners, large pillars, royal statues, and fallen chandeliers.",
                ColorData.FromHex("2A2730"), ColorData.FromHex("3B3745"), ColorData.FromHex("854D0E"), ColorData.FromHex("EAB308"), ColorData.FromHex("FDE047")),

            new DungeonBiome(14, "Celestial Ruins",
                "Floating stone pathways, ancient divine structures, glowing inscriptions, ethereal clouds, celestial gardens, and golden magical light.",
                ColorData.FromHex("252D3D"), ColorData.FromHex("3E4C66"), ColorData.FromHex("FBBF24"), ColorData.FromHex("FDE047"), ColorData.FromHex("FEF08A")),

            new DungeonBiome(15, "Abyssal Prison",
                "Ancient prison corridors, iron gates, dark cells, magical seals, hanging chains, and supernatural prison mechanisms.",
                ColorData.FromHex("191C22"), ColorData.FromHex("262B34"), ColorData.FromHex("475569"), ColorData.FromHex("2563EB"), ColorData.FromHex("60A5FA")),

            new DungeonBiome(16, "Desert Tomb",
                "Sand-covered temples, ancient hieroglyphs, broken sandstone columns, burial chambers, glowing relics, and animated drifting sand.",
                ColorData.FromHex("2E271D"), ColorData.FromHex("42372A"), ColorData.FromHex("D97706"), ColorData.FromHex("F59E0B"), ColorData.FromHex("FDE68A")),

            new DungeonBiome(17, "Mushroom Underworld",
                "Bioluminescent mushrooms, enormous fungal structures, glowing spores, overgrown stone passages, and colorful subterranean ecosystems.",
                ColorData.FromHex("1B2420"), ColorData.FromHex("27362E"), ColorData.FromHex("059669"), ColorData.FromHex("14B8A6"), ColorData.FromHex("5EEAD4")),

            new DungeonBiome(18, "Ancient Clockwork Fortress",
                "Massive gears, moving machinery, brass floors, rotating mechanisms, mechanical pillars, and magical clockwork devices.",
                ColorData.FromHex("2B231D"), ColorData.FromHex("3D322A"), ColorData.FromHex("B45309"), ColorData.FromHex("F97316"), ColorData.FromHex("FDBA74")),

            new DungeonBiome(19, "Haunted Village",
                "Abandoned fantasy houses, crooked streets, old wells, wooden fences, broken market stalls, fog, and mysterious glowing windows.",
                ColorData.FromHex("232326"), ColorData.FromHex("36363B"), ColorData.FromHex("52525B"), ColorData.FromHex("2DD4BF"), ColorData.FromHex("99F6E4")),

            new DungeonBiome(20, "Primordial Worldstone Chamber",
                "Ancient monolithic stone structures, floating crystals, magical mineral formations, enormous runes, and shifting underground energy.",
                ColorData.FromHex("1A1E2B"), ColorData.FromHex("262E42"), ColorData.FromHex("3B82F6"), ColorData.FromHex("8B5CF6"), ColorData.FromHex("C4B5FD"))
        };

        public static int Count => Biomes.Length;

        public static DungeonBiome GetById(int id)
        {
            if (id < 1 || id > Biomes.Length) return Biomes[0];
            return Biomes[id - 1];
        }

        public static DungeonBiome GetByIndex(int index)
        {
            int clamped = Math.Max(0, Math.Min(Biomes.Length - 1, index));
            return Biomes[clamped];
        }

        public static IReadOnlyList<DungeonBiome> GetAll() => Biomes;
    }

    public sealed class ArenaLayout
    {
        public long Seed { get; set; }
        public int Version { get; set; } = 1;
        public ArenaTopologyType TopologyType { get; set; }
        public DungeonBiome Biome { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
        public Vector2D PlayerSpawn { get; set; }
        public List<ArenaWall> Walls { get; } = new List<ArenaWall>();
        public List<ArenaObstacle> Obstacles { get; } = new List<ArenaObstacle>();
        public List<ArenaPortal> Portals { get; } = new List<ArenaPortal>();
        public Vector2D LandmarkPosition { get; set; }
        public string LandmarkName { get; set; }
        public string Fingerprint { get; set; }
        public bool IsValid { get; set; }
        public string ValidationReason { get; set; }

        public string Summary => $"[{Biome?.Name ?? "Unknown"} ({Biome?.Id ?? 0}) | {TopologyType} | Seed: {Seed} | Portals: {Portals.Count} | Walls: {Walls.Count} | Obstacles: {Obstacles.Count}]";
    }
}
