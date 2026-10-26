using System.Collections.Generic;
using FinalBoss.Core;
using UnityEngine;

namespace FinalBoss.Game
{
    public static class DungeonExtensions
    {
        public static Vector2 ToUnity(this Vector2D v) => new Vector2(v.X, v.Y);
        public static Vector3 ToUnity3(this Vector2D v, float z = 0f) => new Vector3(v.X, v.Y, z);
        public static Color ToUnityColor(this ColorData c) => new Color(c.R, c.G, c.B, c.A);
    }

    /// <summary>
    /// Renders procedural arena geometry into the active Unity scene.
    /// Creates the tinted biome floor, perimeter/interior walls with 2D colliders,
    /// obstacle clusters, glowing animated portals, and landmark markers.
    /// </summary>
    public static class DungeonPresenter
    {
        public static GameObject BuildArena(Transform parent, ArenaLayout layout)
        {
            var root = new GameObject("DungeonWorld");
            if (parent != null) root.transform.SetParent(parent, false);

            var biome = layout.Biome;
            Color floorCol = biome != null ? biome.FloorColor.ToUnityColor() : new Color(0.16f, 0.16f, 0.2f);
            Color wallCol = biome != null ? biome.WallColor.ToUnityColor() : new Color(0.3f, 0.25f, 0.35f);
            Color obsCol = biome != null ? biome.ObstacleColor.ToUnityColor() : new Color(0.45f, 0.4f, 0.5f);
            Color portalCol = biome != null ? biome.PortalColor.ToUnityColor() : new Color(0.2f, 0.8f, 0.9f);
            Color accentCol = biome != null ? biome.AccentColor.ToUnityColor() : Color.yellow;

            // 1. Floor
            var floor = CreateSpriteObject("ArenaFloor", root.transform, new Vector3(0f, 0f, 1f),
                new Vector2(layout.Width, layout.Height), floorCol, -10);

            // 2. Walls
            var wallsGroup = new GameObject("Walls");
            wallsGroup.transform.SetParent(root.transform, false);
            for (int i = 0; i < layout.Walls.Count; i++)
            {
                var w = layout.Walls[i];
                var wallObj = CreateSpriteObject(w.Name, wallsGroup.transform, w.Position.ToUnity3(),
                    w.Size.ToUnity(), wallCol, -5);
                var col = wallObj.AddComponent<BoxCollider2D>();
                col.size = Vector2.one;
            }

            // 3. Obstacles
            var obstaclesGroup = new GameObject("Obstacles");
            obstaclesGroup.transform.SetParent(root.transform, false);
            for (int i = 0; i < layout.Obstacles.Count; i++)
            {
                var ob = layout.Obstacles[i];
                var obObj = CreateSpriteObject(ob.Name, obstaclesGroup.transform, ob.Position.ToUnity3(),
                    ob.Size.ToUnity(), obsCol, -4);

                if (ob.Shape == ObstacleShape.Circle)
                {
                    var circleCol = obObj.AddComponent<CircleCollider2D>();
                    circleCol.radius = 0.5f;
                }
                else
                {
                    var boxCol = obObj.AddComponent<BoxCollider2D>();
                    boxCol.size = Vector2.one;
                }
            }

            // 4. Portals
            var portalsGroup = new GameObject("Portals");
            portalsGroup.transform.SetParent(root.transform, false);
            for (int i = 0; i < layout.Portals.Count; i++)
            {
                var p = layout.Portals[i];
                var portalObj = CreateSpriteObject(p.Name, portalsGroup.transform, p.Position.ToUnity3(),
                    new Vector2(1.6f, 1.6f), portalCol, -3);

                // Portal pulsing visual indicator component
                portalObj.AddComponent<PortalMarker>().Initialize(p, portalCol);
            }

            // 5. Landmark
            if (!string.IsNullOrEmpty(layout.LandmarkName))
            {
                var landmarkObj = CreateSpriteObject("Landmark_" + layout.LandmarkName, root.transform,
                    layout.LandmarkPosition.ToUnity3(), new Vector2(2.5f, 2.5f), accentCol * 0.7f, -6);

                var labelGo = new GameObject("LandmarkLabel");
                labelGo.transform.SetParent(landmarkObj.transform, false);
                labelGo.transform.localPosition = new Vector3(0f, 0f, -0.1f);
                var text = labelGo.AddComponent<TextMesh>();
                text.text = layout.LandmarkName;
                text.fontSize = 24;
                text.characterSize = 0.08f;
                text.anchor = TextAnchor.MiddleCenter;
                text.alignment = TextAlignment.Center;
                text.color = Color.white;
            }

            return root;
        }

        private static GameObject CreateSpriteObject(string name, Transform parent, Vector3 pos, Vector2 size, Color color, int sortingOrder)
        {
            var go = new GameObject(name);
            go.transform.SetParent(parent, false);
            go.transform.position = pos;
            go.transform.localScale = new Vector3(size.x, size.y, 1f);

            var sr = go.AddComponent<SpriteRenderer>();
            sr.sprite = PlaceholderSprites.Square;
            sr.color = color;
            sr.sortingOrder = sortingOrder;

            return go;
        }
    }

    /// <summary>
    /// Visual component attached to active enemy portals for animation and text identification.
    /// </summary>
    public sealed class PortalMarker : MonoBehaviour
    {
        private ArenaPortal _portal;
        private Color _baseColor;
        private SpriteRenderer _sr;
        private float _phase;

        public ArenaPortal Portal => _portal;

        public void Initialize(ArenaPortal portal, Color portalColor)
        {
            _portal = portal;
            _baseColor = portalColor;
            _sr = GetComponent<SpriteRenderer>();

            var textGo = new GameObject("Label");
            textGo.transform.SetParent(transform, false);
            textGo.transform.localPosition = new Vector3(0f, 0f, -0.2f);
            var text = textGo.AddComponent<TextMesh>();
            text.text = "PORTAL";
            text.fontSize = 22;
            text.characterSize = 0.07f;
            text.anchor = TextAnchor.MiddleCenter;
            text.alignment = TextAlignment.Center;
            text.color = Color.white;
        }

        private void Update()
        {
            if (_sr == null) return;
            _phase += Time.deltaTime * 3f;
            float pulse = 0.8f + 0.2f * Mathf.Sin(_phase);
            _sr.color = new Color(_baseColor.r * pulse, _baseColor.g * pulse, _baseColor.b * pulse, 0.9f);
        }
    }
}
