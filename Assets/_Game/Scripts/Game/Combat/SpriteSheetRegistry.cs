using FinalBoss.Core;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace FinalBoss.Game
{
    /// <summary>
    /// Central sprite registry that loads and caches generated pixel-art sprite sheets from disk.
    /// Supports Playable Overlords, Summons, Invaders, and Legendary Bosses.
    /// Falls back cleanly to null if a sheet has not been generated yet.
    /// </summary>
    public static class SpriteSheetRegistry
    {
        private static readonly Dictionary<string, Sprite[]> _cache = new Dictionary<string, Sprite[]>();

        public static Sprite[] GetSpriteStrip(string relativePath, int frameWidth, int frameHeight, int frameCount, Vector2 pivot, float ppu = 32f)
        {
            if (string.IsNullOrEmpty(relativePath)) return null;

            if (_cache.TryGetValue(relativePath, out var existing))
            {
                return existing;
            }

            string fullPath = Path.Combine(Application.dataPath, relativePath);
            if (!File.Exists(fullPath))
            {
                return null;
            }

            byte[] bytes = File.ReadAllBytes(fullPath);
            var tex = new Texture2D(frameWidth * frameCount, frameHeight, TextureFormat.RGBA32, false)
            {
                filterMode = FilterMode.Point,
                wrapMode = TextureWrapMode.Clamp
            };

            if (!tex.LoadImage(bytes))
            {
                return null;
            }

            var sprites = new Sprite[frameCount];
            for (int i = 0; i < frameCount; i++)
            {
                var rect = new Rect(i * frameWidth, 0, frameWidth, frameHeight);
                sprites[i] = Sprite.Create(tex, rect, pivot, ppu);
                sprites[i].name = $"{Path.GetFileNameWithoutExtension(relativePath)}_{i}";
            }

            _cache[relativePath] = sprites;
            return sprites;
        }

        public static Sprite GetIdleSprite(string relativePath, int frameWidth, int frameHeight, int frameCount, Vector2 pivot, float ppu = 32f)
        {
            var strip = GetSpriteStrip(relativePath, frameWidth, frameHeight, frameCount, pivot, ppu);
            return (strip != null && strip.Length > 0) ? strip[0] : null;
        }
        public static Sprite GetItemSprite(ItemDefinition def)
        {
            if (def == null) return null;

            string relPath;
            if (def.Slot == ItemSlot.Weapon)
            {
                switch (def.WeaponCategory)
                {
                    case WeaponCategory.TwoHandedSword:
                        relPath = "_Game/Art/UI/Equipment/Weapons/icon_wpn_2h_colossal_claymore.png";
                        break;
                    case WeaponCategory.Dagger:
                        relPath = "_Game/Art/UI/Equipment/Weapons/icon_wpn_dag_shadow_fang.png";
                        break;
                    case WeaponCategory.Bow:
                    case WeaponCategory.Crossbow:
                        relPath = "_Game/Art/UI/Equipment/Weapons/icon_wpn_bow_composite_shortbow.png";
                        break;
                    case WeaponCategory.Staff:
                        relPath = "_Game/Art/UI/Equipment/Weapons/icon_wpn_stf_bone_staff.png";
                        break;
                    case WeaponCategory.Axe:
                        relPath = "_Game/Art/UI/Equipment/Weapons/icon_wpn_axe_crescent_waraxe.png";
                        break;
                    case WeaponCategory.Wand:
                    case WeaponCategory.MagicalOrb:
                        relPath = "_Game/Art/UI/Equipment/Weapons/icon_wpn_wand_apprentice.png";
                        break;
                    case WeaponCategory.Scythe:
                    case WeaponCategory.Claws:
                    case WeaponCategory.CursedRelic:
                        relPath = "_Game/Art/UI/Equipment/Weapons/icon_wpn_scythe_grim.png";
                        break;
                    case WeaponCategory.OneHandedSword:
                    default:
                        relPath = "_Game/Art/UI/Equipment/Weapons/icon_wpn_1h_iron_broadsword.png";
                        break;
                }
            }
            else if (def.Slot == ItemSlot.Armor)
            {
                string id = (def.Id ?? "").ToLowerInvariant();
                if (id.Contains("shroud"))
                    relPath = "_Game/Art/UI/Equipment/Armors/icon_item_necrotic_shroud.png";
                else if (id.Contains("plate"))
                    relPath = "_Game/Art/UI/Equipment/Armors/icon_item_infernal_plate.png";
                else if (id.Contains("cowl") || id.Contains("frost"))
                    relPath = "_Game/Art/UI/Equipment/Armors/icon_item_frost_cowl.png";
                else if (id.Contains("leather") || id.Contains("brigandine") || id.Contains("jerkin"))
                    relPath = "_Game/Art/UI/Equipment/Armors/icon_item_leather_brigandine.png";
                else if (id.Contains("mail") || id.Contains("iron"))
                    relPath = "_Game/Art/UI/Equipment/Armors/icon_item_iron_mail.png";
                else
                    relPath = "_Game/Art/UI/Equipment/Armors/icon_item_crypt_robes.png";
            }
            else
            {
                relPath = "_Game/Art/Props/loot_item_chest.png";
            }

            int size = relPath.Contains("chest") ? 24 : 32;
            return GetIdleSprite(relPath, size, size, 1, new Vector2(0.5f, 0.5f), 32f);
        }
    }
}