using System.IO;
using NUnit.Framework;
using UnityEngine;

namespace FinalBoss.Tests.EditMode
{
    [TestFixture]
    public sealed class ArtAndAudioHandoffTests
    {
        [Test]
        public void AssetCensus_FileExists_AndHasAllRequiredColumns()
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "docs/Development/ASSET_CENSUS.csv");
            Assert.IsTrue(File.Exists(path), "ASSET_CENSUS.csv must exist in docs/Development/");

            string[] lines = File.ReadAllLines(path);
            Assert.Greater(lines.Length, 100, "Asset census must list comprehensive rows (>100).");

            string header = lines[0];
            StringAssert.Contains("asset_id", header);
            StringAssert.Contains("owner_system", header);
            StringAssert.Contains("required_states", header);
            StringAssert.Contains("pixel_dimensions", header);
            StringAssert.Contains("ppu", header);
            StringAssert.Contains("quality_status", header);
        }

        [Test]
        public void PixelArtStyleGuide_FileExists_AndSpecifiesRequiredStandards()
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "docs/Development/PIXEL_ART_STYLE_GUIDE.md");
            Assert.IsTrue(File.Exists(path), "PIXEL_ART_STYLE_GUIDE.md must exist in docs/Development/");

            string content = File.ReadAllText(path);
            StringAssert.Contains("32x32", content);
            StringAssert.Contains("PPU", content);
            StringAssert.Contains("Lich Lord", content);
            StringAssert.Contains("Demon Monarch", content);
        }

        [Test]
        public void LichLordSpriteSheet_PNGExists_AndValidDimensions()
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "Assets/_Game/Art/Characters/LichLord.png");
            Assert.IsTrue(File.Exists(path), "LichLord.png spritesheet must exist in Assets/_Game/Art/Characters/");

            byte[] bytes = File.ReadAllBytes(path);
            var tex = new Texture2D(2, 2);
            bool loaded = tex.LoadImage(bytes);
            Assert.IsTrue(loaded);
            Assert.AreEqual(192, tex.width, "Lich Lord spritesheet must be 192px wide (6 frames of 32px).");
            Assert.AreEqual(32, tex.height, "Lich Lord spritesheet must be 32px high.");
        }
    }
}
