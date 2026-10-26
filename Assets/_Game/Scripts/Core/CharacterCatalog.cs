using System;
using System.Collections.Generic;

namespace FinalBoss.Core
{
    /// <summary>
    /// Character descriptor defining class identity, base attributes, resource mechanics,
    /// starting weapon, basic auto-attack, and signature active ability.
    /// Based on registries/character_roster.json and individual character specs.
    /// </summary>
    public sealed class CharacterInfo
    {
        public string Id { get; }
        public string DisplayName { get; }
        public string ResourceName { get; }
        public bool Playable { get; }
        public float BaseMaxHealth { get; }
        public float BaseMaxResource { get; }
        public float StartingResource { get; }
        public float BaseMoveSpeed { get; }
        public float BaseArmor { get; }
        public string StartingWeapon { get; }
        public string BasicAttackName { get; }
        public string SignatureAbilityId { get; }
        public string SignatureAbilityName { get; }
        public float SignatureResourceCost { get; }
        public float SignatureCooldown { get; }
        public ColorData ClassColor { get; }

        public CharacterInfo(
            string id,
            string displayName,
            string resourceName,
            bool playable,
            float baseMaxHealth,
            float baseMaxResource,
            float startingResource,
            float baseMoveSpeed,
            float baseArmor,
            string startingWeapon,
            string basicAttackName,
            string signatureAbilityId,
            string signatureAbilityName,
            float signatureResourceCost,
            float signatureCooldown,
            ColorData classColor)
        {
            Id = id;
            DisplayName = displayName;
            ResourceName = resourceName;
            Playable = playable;
            BaseMaxHealth = baseMaxHealth;
            BaseMaxResource = baseMaxResource;
            StartingResource = startingResource;
            BaseMoveSpeed = baseMoveSpeed;
            BaseArmor = baseArmor;
            StartingWeapon = startingWeapon;
            BasicAttackName = basicAttackName;
            SignatureAbilityId = signatureAbilityId;
            SignatureAbilityName = signatureAbilityName;
            SignatureResourceCost = signatureResourceCost;
            SignatureCooldown = signatureCooldown;
            ClassColor = classColor;
        }
    }

    public static class CharacterCatalog
    {
        public const string LichLordId = "lich_lord";
        public const string DemonMonarchId = "demon_monarch";
        public const string VampireQueenId = "vampire_queen";
        public const string ShadowReaperId = "shadow_reaper";
        public const string CursedGolemId = "cursed_golem";

        private static readonly CharacterInfo[] All =
        {
            new CharacterInfo(
                id: LichLordId,
                displayName: "Lich Lord",
                resourceName: "Soul Essence",
                playable: true,
                baseMaxHealth: 100f,
                baseMaxResource: 100f,
                startingResource: 50f,
                baseMoveSpeed: 6.0f,
                baseArmor: 0f,
                startingWeapon: "Staff of the Forgotten King",
                basicAttackName: "Necrotic Bolt",
                signatureAbilityId: "lich_lord_active_01",
                signatureAbilityName: "Bone Sentinel",
                signatureResourceCost: 25f,
                signatureCooldown: 3.0f,
                classColor: ColorData.FromHex("6EE7B7") // Pale Bone / Cyan
            ),

            new CharacterInfo(
                id: DemonMonarchId,
                displayName: "Demon Monarch",
                resourceName: "Infernal Fury",
                playable: true,
                baseMaxHealth: 130f,
                baseMaxResource: 100f,
                startingResource: 0f,
                baseMoveSpeed: 5.5f,
                baseArmor: 6f,
                startingWeapon: "Monarch's Hellblade",
                basicAttackName: "Infernal Cleave",
                signatureAbilityId: "demon_monarch_active_22",
                signatureAbilityName: "Hellstep",
                signatureResourceCost: 25f,
                signatureCooldown: 3.5f,
                classColor: ColorData.FromHex("EF4444") // Fiery Demonic Red
            ),

            new CharacterInfo(
                id: VampireQueenId,
                displayName: "Vampire Queen",
                resourceName: "Blood Essence",
                playable: true,
                baseMaxHealth: 90f,
                baseMaxResource: 100f,
                startingResource: 40f,
                baseMoveSpeed: 6.5f,
                baseArmor: 2f,
                startingWeapon: "Crimson Royal Rapier",
                basicAttackName: "Crimson Needle",
                signatureAbilityId: "vampire_queen_active_01",
                signatureAbilityName: "Sanguine Burst",
                signatureResourceCost: 25f,
                signatureCooldown: 4.0f,
                classColor: ColorData.FromHex("E11D48") // Royal Crimson
            ),

            new CharacterInfo(
                id: ShadowReaperId,
                displayName: "Shadow Reaper",
                resourceName: "Reaper's Marks",
                playable: true,
                baseMaxHealth: 85f,
                baseMaxResource: 10f, // 10 Marks capacity
                startingResource: 0f,
                baseMoveSpeed: 6.8f,
                baseArmor: 1f,
                startingWeapon: "Scythe of the Forgotten",
                basicAttackName: "Spectral Scythe",
                signatureAbilityId: "shadow_reaper_active_01",
                signatureAbilityName: "Crescent Reap",
                signatureResourceCost: 3f,
                signatureCooldown: 3.0f,
                classColor: ColorData.FromHex("A855F7") // Midnight Amethyst Violet
            ),

            new CharacterInfo(
                id: CursedGolemId,
                displayName: "Cursed Golem",
                resourceName: "Runic Charge",
                playable: true,
                baseMaxHealth: 180f,
                baseMaxResource: 100f,
                startingResource: 0f,
                baseMoveSpeed: 4.8f,
                baseArmor: 15f,
                startingWeapon: "Colossus Stone Fists",
                basicAttackName: "Colossus Fist",
                signatureAbilityId: "cursed_golem_active_22",
                signatureAbilityName: "Rune of Protection",
                signatureResourceCost: 25f,
                signatureCooldown: 5.0f,
                classColor: ColorData.FromHex("14B8A6") // Runic Granite Teal
            )
        };

        public static IReadOnlyList<CharacterInfo> Characters => All;

        public static CharacterInfo Find(string id)
        {
            for (int i = 0; i < All.Length; i++)
            {
                if (string.Equals(All[i].Id, id, StringComparison.OrdinalIgnoreCase))
                    return All[i];
            }
            return null;
        }

        public static CharacterInfo Get(string id) => Find(id);
    }
}
