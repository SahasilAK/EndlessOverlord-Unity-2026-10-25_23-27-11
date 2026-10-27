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
    }
}
