using System;
using System.Collections.Generic;

namespace FinalBoss.Core
{
    /// <summary>
    /// Minimal character descriptor for the graybox. IDs, names and resources come from
    /// registries/character_roster.json. <see cref="Playable"/> is true only for characters whose
    /// gameplay is actually implemented. Base values are placeholder tuning, not final balance.
    /// </summary>
    public sealed class CharacterInfo
    {
        public string Id { get; }
        public string DisplayName { get; }
        public string ResourceName { get; }
        public bool Playable { get; }
        public float BaseMaxHealth { get; }
        public float BaseMaxResource { get; }

        public CharacterInfo(string id, string displayName, string resourceName, bool playable,
                             float baseMaxHealth = 100f, float baseMaxResource = 100f)
        {
            Id = id;
            DisplayName = displayName;
            ResourceName = resourceName;
            Playable = playable;
            BaseMaxHealth = baseMaxHealth;
            BaseMaxResource = baseMaxResource;
        }
    }

    public static class CharacterCatalog
    {
        public const string LichLordId = "lich_lord";

        private static readonly CharacterInfo[] All =
        {
            new CharacterInfo(LichLordId,       "Lich Lord",       "Soul Essence",   playable: true),
            new CharacterInfo("demon_monarch",  "Demon Monarch",   "Infernal Fury",  playable: false),
            new CharacterInfo("vampire_queen",  "Vampire Queen",   "Blood Essence",  playable: false),
            new CharacterInfo("shadow_reaper",  "Shadow Reaper",   "Reaper's Marks", playable: false),
            new CharacterInfo("cursed_golem",   "Cursed Golem",    "Runic Charge",   playable: false),
        };

        public static IReadOnlyList<CharacterInfo> Characters { get { return All; } }

        public static CharacterInfo Find(string id)
        {
            for (int i = 0; i < All.Length; i++)
                if (string.Equals(All[i].Id, id, StringComparison.Ordinal))
                    return All[i];
            return null;
        }
    }
}
