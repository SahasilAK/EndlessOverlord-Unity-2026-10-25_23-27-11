using FinalBoss.Core;
using UnityEngine;

namespace FinalBoss.Game
{
    /// <summary>
    /// Interactive in-arena Magical Shrine entity.
    /// Emits visual aura and activates when the player steps within interaction range.
    /// </summary>
    public sealed class ShrineActor : MonoBehaviour
    {
        public const float ActivationRadius = 1.6f;

        private SpriteRenderer _renderer;
        private ShrineInstance _instance;

        public ShrineInstance Instance => _instance;
        public bool IsUsed => _instance != null && _instance.IsUsed;

        public static ShrineActor Spawn(Vector2 position, ShrineType type)
        {
            var def = InRunEventCatalog.GetShrine(type);
            var inst = new ShrineInstance(def, position.x, position.y);

            var go = new GameObject($"Shrine_{type}");
            go.transform.position = (Vector3)position;
            var actor = go.AddComponent<ShrineActor>();
            actor.Initialize(inst);
            return actor;
        }

        public void Initialize(ShrineInstance instance)
        {
            _instance = instance;
            _renderer = gameObject.GetComponent<SpriteRenderer>();
            if (_renderer == null)
            {
                _renderer = gameObject.AddComponent<SpriteRenderer>();
            }

            _renderer.sprite = PlaceholderSprites.Square;
            _renderer.sortingOrder = 5;
            transform.localScale = new Vector3(0.9f, 1.4f, 1f);

            if (ColorUtility.TryParseHtmlString(_instance.Definition.ColorHex, out var col))
            {
                _renderer.color = col;
            }
            else
            {
                _renderer.color = Color.magenta;
            }
        }

        private void Update()
        {
            if (_instance == null || _instance.IsUsed) return;

            var player = ActorRegistry.Player;
            if (player == null || !player.Alive) return;

            float distSqr = ((Vector2)transform.position - player.Position).sqrMagnitude;
            if (distSqr <= ActivationRadius * ActivationRadius)
            {
                if (player.ActivateShrine(_instance))
                {
                    OnActivated();
                }
            }
        }

        private void OnActivated()
        {
            if (_renderer != null)
            {
                _renderer.color = new Color(0.4f, 0.4f, 0.4f, 0.5f);
            }
        }
    }
}
