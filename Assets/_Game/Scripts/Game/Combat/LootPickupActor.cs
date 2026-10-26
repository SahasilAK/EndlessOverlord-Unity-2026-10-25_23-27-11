using System;
using FinalBoss.Core;
using UnityEngine;

namespace FinalBoss.Game
{
    /// <summary>
    /// Physical in-world loot pickup (Gold or Equipment Item).
    /// Features proximity magnet attraction, single-collection atomic guarantee,
    /// and inventory capacity validation.
    /// </summary>
    public sealed class LootPickupActor : MonoBehaviour
    {
        private static long _pickupCounter = 1;

        public const float MagnetRadius = 3.5f;
        public const float CollectRadius = 0.8f;
        public const float MagnetSpeed = 8.5f;

        public long PickupId { get; private set; }
        public bool IsGold { get; private set; }
        public int GoldAmount { get; private set; }
        public ItemInstance Item { get; private set; }
        public bool IsCollected { get; private set; }

        private HudModel _hud;
        private PlayerInventory _inventory;
        private SpriteRenderer _sr;

        public static LootPickupActor SpawnGold(Vector2 position, int amount, HudModel hud)
        {
            var go = new GameObject($"Loot_Gold_{_pickupCounter}");
            go.transform.position = position;
            go.transform.localScale = new Vector3(0.35f, 0.35f, 1f);

            var pickup = go.AddComponent<LootPickupActor>();
            pickup.PickupId = _pickupCounter++;
            pickup.IsGold = true;
            pickup.GoldAmount = amount;
            pickup._hud = hud;

            var sr = go.AddComponent<SpriteRenderer>();
            sr.sprite = PlaceholderSprites.Square;
            sr.color = new Color(1f, 0.85f, 0.2f); // Gold coin yellow
            sr.sortingOrder = 8;
            pickup._sr = sr;

            return pickup;
        }

        public static LootPickupActor SpawnItem(Vector2 position, ItemInstance item, PlayerInventory inventory)
        {
            var go = new GameObject($"Loot_Item_{item.Definition.Id}_{_pickupCounter}");
            go.transform.position = position;
            go.transform.localScale = new Vector3(0.45f, 0.45f, 1f);

            var pickup = go.AddComponent<LootPickupActor>();
            pickup.PickupId = _pickupCounter++;
            pickup.IsGold = false;
            pickup.Item = item;
            pickup._inventory = inventory;

            var sr = go.AddComponent<SpriteRenderer>();
            sr.sprite = PlaceholderSprites.Square;
            sr.color = GetRarityColor(item.Definition.Rarity);
            sr.sortingOrder = 8;
            pickup._sr = sr;

            return pickup;
        }

        private void Update()
        {
            if (IsCollected)
                return;

            var player = ActorRegistry.Player;
            if (player == null || !player.Alive)
                return;

            Vector2 myPos = transform.position;
            Vector2 playerPos = player.Position;
            float distSqr = (playerPos - myPos).sqrMagnitude;

            // Magnet pull
            if (distSqr <= MagnetRadius * MagnetRadius)
            {
                transform.position = Vector3.MoveTowards(transform.position, playerPos, MagnetSpeed * Time.deltaTime);
            }

            // Collection
            if (distSqr <= CollectRadius * CollectRadius)
            {
                TryCollect();
            }
        }

        public bool TryCollect()
        {
            if (IsCollected)
                return false;

            if (IsGold)
            {
                IsCollected = true;
                if (_hud != null)
                {
                    _hud.AddGold(GoldAmount);
                }
                CueBus.Raise(CueIds.BoltHit, transform.position.x, transform.position.y);
                Destroy(gameObject);
                return true;
            }
            else if (Item != null)
            {
                if (_inventory != null && _inventory.CanAddItem)
                {
                    IsCollected = true;
                    _inventory.TryAddItem(Item);
                    CueBus.Raise(CueIds.PlayerDash, transform.position.x, transform.position.y);
                    Destroy(gameObject);
                    return true;
                }
                // Cannot collect if inventory is full: remains on ground
                return false;
            }

            return false;
        }

        public static Color GetRarityColor(ItemRarity rarity)
        {
            switch (rarity)
            {
                case ItemRarity.Common: return new Color(0.75f, 0.8f, 0.85f);
                case ItemRarity.Uncommon: return new Color(0.3f, 0.85f, 0.4f);
                case ItemRarity.Rare: return new Color(0.25f, 0.7f, 1.0f);
                case ItemRarity.Epic: return new Color(0.78f, 0.4f, 0.98f);
                case ItemRarity.Legendary: return new Color(1.0f, 0.82f, 0.2f);
                default: return Color.white;
            }
        }
    }
}
