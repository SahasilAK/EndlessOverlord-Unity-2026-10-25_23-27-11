using FinalBoss.Core;
using UnityEngine;

namespace FinalBoss.Game
{
    /// <summary>
    /// Visual presenter for playable boss characters.
    /// Drives pixel-art sprite animation across Idle, Walk, Cast, Hit, and Death states.
    /// Supports all 5 overlord classes using their generated sprite sheets.
    /// </summary>
    public sealed class LichLordVisualPresenter : MonoBehaviour
    {
        private PlayerActor _player;
        private SpriteRenderer _bodyRenderer;
        private string _characterId;

        private Sprite _spriteIdle;
        private Sprite _spriteWalk1;
        private Sprite _spriteWalk2;
        private Sprite _spriteCast;
        private Sprite _spriteHit;
        private Sprite _spriteDeath;

        private float _animTimer;
        private int _walkFrame;
        private float _castVisualTimer;

        public bool IsLoaded => _spriteIdle != null;

        public void Initialize(PlayerActor player, SpriteRenderer bodyRenderer, string characterId = null)
        {
            _player = player;
            _bodyRenderer = bodyRenderer;
            _characterId = characterId ?? CharacterCatalog.LichLordId;

            LoadSprites();
        }

        private void LoadSprites()
        {
            string sheetRelPath = $"_Game/Art/Characters/{_characterId}/char_{_characterId}_sheet.png";
            string path = System.IO.Path.Combine(Application.dataPath, sheetRelPath);

            if (!System.IO.File.Exists(path))
            {
                path = System.IO.Path.Combine(Application.dataPath, "_Game/Art/Characters/LichLord.png");
            }

            if (System.IO.File.Exists(path))
            {
                byte[] bytes = System.IO.File.ReadAllBytes(path);
                var tex = new Texture2D(192, 32, TextureFormat.RGBA32, false)
                {
                    filterMode = FilterMode.Point,
                    wrapMode = TextureWrapMode.Clamp
                };

                if (tex.LoadImage(bytes))
                {
                    Vector2 pivot = new Vector2(0.5f, 0.1f);
                    const float ppu = 32f;
                    string prefix = (_characterId == CharacterCatalog.LichLordId || _characterId == "LichLord") ? "LichLord" : _characterId;

                    _spriteIdle = Sprite.Create(tex, new Rect(0, 0, 32, 32), pivot, ppu);
                    _spriteIdle.name = $"{prefix}_Idle";

                    _spriteWalk1 = Sprite.Create(tex, new Rect(32, 0, 32, 32), pivot, ppu);
                    _spriteWalk1.name = $"{prefix}_Walk1";

                    _spriteWalk2 = Sprite.Create(tex, new Rect(64, 0, 32, 32), pivot, ppu);
                    _spriteWalk2.name = $"{prefix}_Walk2";

                    _spriteCast = Sprite.Create(tex, new Rect(96, 0, 32, 32), pivot, ppu);
                    _spriteCast.name = $"{prefix}_Cast";

                    _spriteHit = Sprite.Create(tex, new Rect(128, 0, 32, 32), pivot, ppu);
                    _spriteHit.name = $"{prefix}_Hit";

                    _spriteDeath = Sprite.Create(tex, new Rect(160, 0, 32, 32), pivot, ppu);
                    _spriteDeath.name = $"{prefix}_Death";

                    if (_bodyRenderer != null)
                    {
                        _bodyRenderer.sprite = _spriteIdle;
                    }
                }
            }
        }

        public void TriggerCastVisual(float duration = 0.25f)
        {
            _castVisualTimer = duration;
        }

        private void Update()
        {
            if (_player == null || _bodyRenderer == null || !IsLoaded) return;

            // Handle death state
            if (_player.Health != null && _player.Health.IsDead)
            {
                _bodyRenderer.sprite = _spriteDeath;
                return;
            }

            // Facing direction flip
            if (_player.AimDirection.x < -0.05f)
            {
                _bodyRenderer.flipX = true;
            }
            else if (_player.AimDirection.x > 0.05f)
            {
                _bodyRenderer.flipX = false;
            }

            // Hit state reaction
            if (_player.FlashTimer > 0f)
            {
                _bodyRenderer.sprite = _spriteHit;
                return;
            }

            // Cast state reaction
            if (_castVisualTimer > 0f)
            {
                _castVisualTimer -= Time.deltaTime;
                _bodyRenderer.sprite = _spriteCast;
                return;
            }

            // Movement vs Idle
            Vector2 vel = _player.Velocity;
            if (vel.sqrMagnitude > 0.05f)
            {
                _animTimer += Time.deltaTime * 8f;
                if (_animTimer >= 1f)
                {
                    _animTimer -= 1f;
                    _walkFrame = 1 - _walkFrame;
                }
                _bodyRenderer.sprite = _walkFrame == 0 ? _spriteWalk1 : _spriteWalk2;
            }
            else
            {
                _bodyRenderer.sprite = _spriteIdle;
                _animTimer = 0f;
            }
        }
    }
}
