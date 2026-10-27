using System;
using System.Collections.Generic;
using FinalBoss.Core;
using UnityEngine;

namespace FinalBoss.Game
{
    /// <summary>
    /// Invader entity supporting distinct combat archetypes:
    /// - Swordsman: Melee chaser, windup cleave, strike.
    /// - Archer: Ranged kiter, maintains distance, telegraphed shot, fires arrows.
    /// - Cleric: Support healer, seeks and channels heals on injured allies.
    /// - RoyalChampion: Multi-phase Boss with cleave, charge dash, enrage at 50% HP, summon guards, and triple shockwave!
    /// </summary>
    public sealed class InvaderActor : MonoBehaviour, IDamageable
    {
        private Rigidbody2D _rb;
        private SpriteRenderer _body;
        private SpriteRenderer _telegraph;
        private Transform _barFill;
        private Transform _barBgTransform;
        private Color _baseColor;
        private float _flash;
        private int _id;

        // Attack simulators and timers
        private MeleeAttackSim _melee;
        private float _actionCooldown;
        private float _actionWindup;
        private bool _isActionWinding;
        private Vector2 _aimDirection = Vector2.right;

        // Boss-specific states
        private bool _isEnraged;
        private float _bossDashTimer;
        private float _bossDashActive;
        private Vector2 _bossDashDir;
        private float _timeSinceDamageTaken;
        private bool _spawned70Reinforcements;
        private bool _spawned35Reinforcements;
        private float _bossAbilityCooldown = 2.0f;
        private float _bossAbilityWindup;
        private bool _isBossAbilityWinding;
        private BossAbilityType _windingAbility;
        private bool _useSecondaryAbilityNext;

        public Health Health { get; private set; }
        public InvaderArchetype Archetype { get; private set; } = InvaderArchetype.Swordsman;
        public InvaderDefinition Definition { get; private set; }
        public bool IsElite { get; private set; }
        public string BossId { get; private set; }
        public LegendaryBossDefinition BossDefinition { get; private set; }
        public BossModifier Modifiers { get; private set; }
        public bool IsBoss => !string.IsNullOrEmpty(BossId) || Archetype == InvaderArchetype.RoyalChampion;
        public float BarrierCurrent { get; private set; }
        public float BarrierMax { get; private set; }
        public bool HasBarrier => BarrierCurrent > 0f;

        private float _baseMoveSpeed;
        private float _slowMultiplier = 1f;
        private float _slowTimer;

        public float MoveSpeed => _baseMoveSpeed * _slowMultiplier;
        public float BaseMoveSpeed => _baseMoveSpeed;
        public float SlowMultiplier => _slowMultiplier;
        public bool IsSlowed => _slowTimer > 0f;
        public float Armor { get; private set; }
        public float SoulReward { get; private set; }
        public long ScoreReward { get; private set; }

        public event Action<InvaderActor> Killed;

        public int Id => _id;
        public Faction Team => Faction.Invader;
        public Vector2 Position => _rb != null ? _rb.position : (Vector2)transform.position;
        public float Radius { get; private set; } = CombatTuning.InvaderRadius;
        public bool Alive => Health != null && !Health.IsDead;
        public MeleeAttackSim Melee => _melee;

        public void ApplySlow(float slowPercent, float duration)
        {
            if (!Alive) return;
            float targetMult = Mathf.Clamp(1f - slowPercent, 0.15f, 1f);
            if (_slowTimer <= 0f || targetMult < _slowMultiplier)
            {
                _slowMultiplier = targetMult;
            }
            if (duration > _slowTimer)
            {
                _slowTimer = duration;
            }
        }

        public void ApplyKnockback(Vector2 impulse)
        {
            if (_rb != null && Alive)
            {
                _rb.linearVelocity += impulse;
            }
        }

        public static InvaderActor Spawn(
            Vector2 position,
            InvaderArchetype archetype = InvaderArchetype.Swordsman,
            bool isElite = false,
            string bossId = null,
            BossModifier modifiers = BossModifier.None)
        {
            string name = !string.IsNullOrEmpty(bossId)
                ? $"Boss_{bossId}"
                : (isElite ? $"Elite_{archetype}" : archetype.ToString());
            var go = new GameObject(name);
            go.transform.position = position;
            var actor = go.AddComponent<InvaderActor>();
            actor.Initialize(archetype, isElite, bossId, modifiers);
            return actor;
        }

        private void Initialize(InvaderArchetype archetype, bool isElite, string bossId = null, BossModifier modifiers = BossModifier.None)
        {
            Archetype = archetype;
            IsElite = isElite;
            BossId = bossId;
            Modifiers = modifiers;
            _id = ActorRegistry.NextActorId();
            Definition = InvaderCatalog.Get(archetype);

            float hp = Definition.BaseHealth;
            float armor = Definition.BaseArmor;
            float speed = Definition.BaseMoveSpeed;
            Radius = Definition.Radius;
            _baseColor = new Color(Definition.ColorR, Definition.ColorG, Definition.ColorB);

            if (!string.IsNullOrEmpty(bossId))
            {
                BossDefinition = LegendaryBossCatalog.Get(bossId);
                if (BossDefinition != null)
                {
                    hp = BossDefinition.BaseHealth;
                    armor = BossDefinition.BaseArmor;
                    speed = BossDefinition.BaseMoveSpeed;
                    Radius = BossDefinition.Radius;
                    _baseColor = new Color(BossDefinition.ColorR, BossDefinition.ColorG, BossDefinition.ColorB);
                }
            }
            else if (archetype == InvaderArchetype.RoyalChampion)
            {
                BossDefinition = LegendaryBossCatalog.Get("boss_alden");
                Modifiers |= BossModifier.Enrage;
            }

            if (IsBoss)
            {
                _melee = new MeleeAttackSim(CombatTuning.BossCleaveRange, CombatTuning.BossCleaveWindup, CombatTuning.BossCleaveRecover);
                _bossDashTimer = CombatTuning.BossDashCooldown;
                _bossAbilityCooldown = 2.0f;
            }
            else if (Definition.Role == InvaderCombatRole.Melee || Definition.Role == InvaderCombatRole.Tank || Definition.Role == InvaderCombatRole.Assassin)
            {
                _melee = new MeleeAttackSim(Definition.AttackRange, Definition.AttackWindup, Math.Max(0.4f, Definition.AttackCooldown * 0.5f));
            }
            else
            {
                _actionCooldown = 1.0f;
            }

            if ((Modifiers & BossModifier.Barrier) != 0)
            {
                BarrierMax = hp * 0.25f;
                BarrierCurrent = BarrierMax;
            }

            if (isElite)
            {
                hp *= CombatTuning.EliteHealthMultiplier;
                armor += 5f;
                speed *= CombatTuning.EliteSpeedMultiplier;
                _baseColor = Color.Lerp(_baseColor, new Color(1f, 0.85f, 0.1f), 0.5f); // Golden sheen
            }

            _baseMoveSpeed = speed;
            Armor = armor;
            SoulReward = IsBoss ? CombatTuning.BossSoulReward : (isElite ? CombatTuning.SoulPerKill * 2.5f : CombatTuning.SoulPerKill);
            ScoreReward = IsBoss ? CombatTuning.BossScoreReward : (isElite ? (long)(CombatTuning.ScorePerKill * CombatTuning.EliteScoreMultiplier) : CombatTuning.ScorePerKill);

            Health = new Health(hp);

            _rb = gameObject.AddComponent<Rigidbody2D>();
            _rb.gravityScale = 0f;
            _rb.freezeRotation = true;
            _rb.mass = IsBoss ? 15f : (isElite ? 5f : 3f);
            _rb.interpolation = RigidbodyInterpolation2D.Interpolate;

            var col = gameObject.AddComponent<CircleCollider2D>();
            col.radius = 0.5f;

            transform.localScale = new Vector3(Radius * 2f, Radius * 2f, 1f);

            _body = gameObject.AddComponent<SpriteRenderer>();

            Sprite customSprite = null;
            if (IsBoss && !string.IsNullOrEmpty(BossId))
            {
                if (BossId.Equals("boss_valerius", StringComparison.OrdinalIgnoreCase))
                {
                    customSprite = SpriteSheetRegistry.GetIdleSprite("_Game/Art/Characters/Bosses/boss_01_valerius_sheet.png", 48, 48, 6, new Vector2(0.5f, 0.08f), 32f);
                }
                else if (BossId.Equals("boss_cassandra", StringComparison.OrdinalIgnoreCase))
                {
                    customSprite = SpriteSheetRegistry.GetIdleSprite("_Game/Art/Characters/Bosses/boss_02_cassandra_sheet.png", 48, 48, 6, new Vector2(0.5f, 0.08f), 32f);
                }
                else if (BossId.Equals("boss_ignis", StringComparison.OrdinalIgnoreCase))
                {
                    customSprite = SpriteSheetRegistry.GetIdleSprite("_Game/Art/Characters/Bosses/boss_03_ignis_sheet.png", 48, 48, 6, new Vector2(0.5f, 0.08f), 32f);
                }
                else if (BossId.Equals("boss_malakor", StringComparison.OrdinalIgnoreCase))
                {
                    customSprite = SpriteSheetRegistry.GetIdleSprite("_Game/Art/Characters/Bosses/boss_04_malakor_sheet.png", 48, 48, 6, new Vector2(0.5f, 0.08f), 32f);
                }
                else if (BossId.Equals("boss_kraag", StringComparison.OrdinalIgnoreCase))
                {
                    customSprite = SpriteSheetRegistry.GetIdleSprite("_Game/Art/Characters/Bosses/boss_05_kraag_sheet.png", 48, 48, 6, new Vector2(0.5f, 0.08f), 32f);
                }
                else if (BossId.Equals("boss_elara", StringComparison.OrdinalIgnoreCase))
                {
                    customSprite = SpriteSheetRegistry.GetIdleSprite("_Game/Art/Characters/Bosses/boss_06_elara_sheet.png", 48, 48, 6, new Vector2(0.5f, 0.08f), 32f);
                }
                else if (BossId.Equals("boss_sylas", StringComparison.OrdinalIgnoreCase))
                {
                    customSprite = SpriteSheetRegistry.GetIdleSprite("_Game/Art/Characters/Bosses/boss_07_sylas_sheet.png", 48, 48, 6, new Vector2(0.5f, 0.08f), 32f);
                }
                else if (BossId.Equals("boss_eisen", StringComparison.OrdinalIgnoreCase))
                {
                    customSprite = SpriteSheetRegistry.GetIdleSprite("_Game/Art/Characters/Bosses/boss_08_eisen_sheet.png", 48, 48, 6, new Vector2(0.5f, 0.08f), 32f);
                }
                else if (BossId.Equals("boss_vaelin", StringComparison.OrdinalIgnoreCase))
                {
                    customSprite = SpriteSheetRegistry.GetIdleSprite("_Game/Art/Characters/Bosses/boss_09_vaelin_sheet.png", 48, 48, 6, new Vector2(0.5f, 0.08f), 32f);
                }
                else if (BossId.Equals("boss_alden", StringComparison.OrdinalIgnoreCase))
                {
                    customSprite = SpriteSheetRegistry.GetIdleSprite("_Game/Art/Characters/Bosses/boss_10_alden_sheet.png", 48, 48, 6, new Vector2(0.5f, 0.08f), 32f);
                }
            }
            else
            {
                switch (Archetype)
                {
                    case InvaderArchetype.Swordsman:
                        customSprite = SpriteSheetRegistry.GetIdleSprite("_Game/Art/Characters/Invaders/inv_01_swordsman_sheet.png", 32, 32, 6, new Vector2(0.5f, 0.1f), 32f);
                        break;
                    case InvaderArchetype.Archer:
                        customSprite = SpriteSheetRegistry.GetIdleSprite("_Game/Art/Characters/Invaders/inv_02_archer_sheet.png", 32, 32, 6, new Vector2(0.5f, 0.1f), 32f);
                        break;
                    case InvaderArchetype.Crossbowman:
                        customSprite = SpriteSheetRegistry.GetIdleSprite("_Game/Art/Characters/Invaders/inv_03_crossbowman_sheet.png", 32, 32, 6, new Vector2(0.5f, 0.1f), 32f);
                        break;
                    case InvaderArchetype.Spearman:
                        customSprite = SpriteSheetRegistry.GetIdleSprite("_Game/Art/Characters/Invaders/inv_04_spearman_sheet.png", 32, 32, 6, new Vector2(0.5f, 0.1f), 32f);
                        break;
                    case InvaderArchetype.Berserker:
                        customSprite = SpriteSheetRegistry.GetIdleSprite("_Game/Art/Characters/Invaders/inv_03_berserker_sheet.png", 32, 32, 6, new Vector2(0.5f, 0.1f), 32f);
                        break;
                    case InvaderArchetype.Rogue:
                        customSprite = SpriteSheetRegistry.GetIdleSprite("_Game/Art/Characters/Invaders/inv_04_rogue_sheet.png", 32, 32, 6, new Vector2(0.5f, 0.1f), 32f);
                        break;
                    case InvaderArchetype.Knight:
                        customSprite = SpriteSheetRegistry.GetIdleSprite("_Game/Art/Characters/Invaders/inv_05_knight_sheet.png", 32, 32, 6, new Vector2(0.5f, 0.1f), 32f);
                        break;
                    case InvaderArchetype.Paladin:
                        customSprite = SpriteSheetRegistry.GetIdleSprite("_Game/Art/Characters/Invaders/inv_09_paladin_sheet.png", 32, 32, 6, new Vector2(0.5f, 0.1f), 32f);
                        break;
                    case InvaderArchetype.Cleric:
                        customSprite = SpriteSheetRegistry.GetIdleSprite("_Game/Art/Characters/Invaders/inv_10_cleric_sheet.png", 32, 32, 6, new Vector2(0.5f, 0.1f), 32f);
                        break;
                    case InvaderArchetype.Priest:
                        customSprite = SpriteSheetRegistry.GetIdleSprite("_Game/Art/Characters/Invaders/inv_11_priest_sheet.png", 32, 32, 6, new Vector2(0.5f, 0.1f), 32f);
                        break;
                    case InvaderArchetype.FireMage:
                        customSprite = SpriteSheetRegistry.GetIdleSprite("_Game/Art/Characters/Invaders/inv_08_fire_mage_sheet.png", 32, 32, 6, new Vector2(0.5f, 0.1f), 32f);
                        break;
                    case InvaderArchetype.IceMage:
                        customSprite = SpriteSheetRegistry.GetIdleSprite("_Game/Art/Characters/Invaders/inv_13_ice_mage_sheet.png", 32, 32, 6, new Vector2(0.5f, 0.1f), 32f);
                        break;
                    case InvaderArchetype.LightningMage:
                        customSprite = SpriteSheetRegistry.GetIdleSprite("_Game/Art/Characters/Invaders/inv_14_lightning_mage_sheet.png", 32, 32, 6, new Vector2(0.5f, 0.1f), 32f);
                        break;
                    case InvaderArchetype.ArcaneWizard:
                        customSprite = SpriteSheetRegistry.GetIdleSprite("_Game/Art/Characters/Invaders/inv_15_arcane_wizard_sheet.png", 32, 32, 6, new Vector2(0.5f, 0.1f), 32f);
                        break;
                    case InvaderArchetype.WitchHunter:
                        customSprite = SpriteSheetRegistry.GetIdleSprite("_Game/Art/Characters/Invaders/inv_16_witch_hunter_sheet.png", 32, 32, 6, new Vector2(0.5f, 0.1f), 32f);
                        break;
                    case InvaderArchetype.MonsterHunter:
                        customSprite = SpriteSheetRegistry.GetIdleSprite("_Game/Art/Characters/Invaders/inv_17_monster_hunter_sheet.png", 32, 32, 6, new Vector2(0.5f, 0.1f), 32f);
                        break;
                    case InvaderArchetype.Ranger:
                        customSprite = SpriteSheetRegistry.GetIdleSprite("_Game/Art/Characters/Invaders/inv_18_ranger_sheet.png", 32, 32, 6, new Vector2(0.5f, 0.1f), 32f);
                        break;
                    case InvaderArchetype.Druid:
                        customSprite = SpriteSheetRegistry.GetIdleSprite("_Game/Art/Characters/Invaders/inv_19_druid_sheet.png", 32, 32, 6, new Vector2(0.5f, 0.1f), 32f);
                        break;
                    case InvaderArchetype.Alchemist:
                        customSprite = SpriteSheetRegistry.GetIdleSprite("_Game/Art/Characters/Invaders/inv_20_alchemist_sheet.png", 32, 32, 6, new Vector2(0.5f, 0.1f), 32f);
                        break;
                    case InvaderArchetype.ShieldGuardian:
                        customSprite = SpriteSheetRegistry.GetIdleSprite("_Game/Art/Characters/Invaders/inv_23_shield_guardian_sheet.png", 32, 32, 6, new Vector2(0.5f, 0.1f), 32f);
                        break;
                    case InvaderArchetype.BattleMonk:
                        customSprite = SpriteSheetRegistry.GetIdleSprite("_Game/Art/Characters/Invaders/inv_24_battle_monk_sheet.png", 32, 32, 6, new Vector2(0.5f, 0.1f), 32f);
                        break;
                }
            }

            if (customSprite != null)
            {
                _body.sprite = customSprite;
                _body.color = _baseColor;
            }
            else
            {
                _body.sprite = PlaceholderSprites.Square;
                _body.color = _baseColor;
            }
            _body.sortingOrder = IsBoss ? 8 : 5;

            // Telegraph
            var tg = new GameObject("Telegraph");
            tg.transform.SetParent(transform, false);
            _telegraph = tg.AddComponent<SpriteRenderer>();
            _telegraph.sprite = PlaceholderSprites.Square;
            _telegraph.color = (Definition != null && Definition.Role == InvaderCombatRole.HealerSupport)
                ? new Color(0.2f, 1f, 0.4f, 0.35f) // Green/Gold for heal
                : new Color(1f, 0.15f, 0.1f, 0.35f); // Red for attack
            _telegraph.sortingOrder = 4;
            _telegraph.enabled = false;

            // Health bar
            var bg = new GameObject("BarBg");
            bg.transform.SetParent(transform, false);
            var bgr = bg.AddComponent<SpriteRenderer>();
            bgr.sprite = PlaceholderSprites.Square;
            bgr.color = new Color(0f, 0f, 0f, 0.75f);
            bgr.sortingOrder = 20;

            var fill = new GameObject("BarFill");
            fill.transform.SetParent(transform, false);
            var fr = fill.AddComponent<SpriteRenderer>();
            fr.sprite = PlaceholderSprites.Square;
            fr.color = IsBoss ? new Color(0.9f, 0.2f, 0.8f) : (isElite ? new Color(1f, 0.85f, 0.1f) : new Color(0.3f, 0.9f, 0.3f));
            fr.sortingOrder = 21;
            _barFill = fill.transform;

            float barY = IsBoss ? 1.15f : 0.85f;
            bg.transform.localPosition = new Vector3(0f, barY, 0f);
            fill.transform.localPosition = new Vector3(0f, barY, 0f);
            _barBgTransform = bg.transform;

            Health.Changed += OnHealthChanged;
            ActorRegistry.Register(this);
            OnHealthChanged();
        }

        private void OnDestroy()
        {
            if (Health != null)
                Health.Changed -= OnHealthChanged;
            ActorRegistry.Unregister(this);
        }

        private void OnHealthChanged()
        {
            if (_barFill == null || Health == null)
                return;

            float worldWidth = IsBoss ? 2.4f : (IsElite ? 1.5f : 1.2f);
            float worldHeight = IsBoss ? 0.22f : 0.14f;
            float sx = worldWidth / transform.localScale.x;
            float sy = worldHeight / transform.localScale.y;
            float frac = Health.Max > 0f ? Health.Current / Health.Max : 0f;

            _barBgTransform.localScale = new Vector3(sx, sy, 1f);
            _barFill.localScale = new Vector3(sx * frac, sy * 0.8f, 1f);
            _barFill.localPosition = new Vector3(-(sx * (1f - frac)) * 0.5f, _barBgTransform.localPosition.y, 0f);

            // Boss Enrage check (< 50% HP)
            if (IsBoss && !_isEnraged && Health.Current <= Health.Max * 0.5f)
            {
                TriggerBossEnrage();
            }
        }

        private void TriggerBossEnrage()
        {
            _isEnraged = true;
            _baseMoveSpeed = CombatTuning.BossEnragedSpeed;
            _baseColor = new Color(0.85f, 0.1f, 0.25f); // Enraged Crimson
            if (_body != null) _body.color = _baseColor;

            CueBus.Raise(CueIds.BossEnrage, Position.x, Position.y);

            // Boss summons 2 Swordsman reinforcements
            Spawn(Position + new Vector2(-1.8f, 0f), InvaderArchetype.Swordsman, false);
            Spawn(Position + new Vector2(1.8f, 0f), InvaderArchetype.Swordsman, false);
        }

        private void FixedUpdate()
        {
            if (!Alive)
            {
                if (_rb != null) _rb.linearVelocity = Vector2.zero;
                return;
            }

            float dt = Time.fixedDeltaTime;

            if (_slowTimer > 0f)
            {
                _slowTimer -= dt;
                if (_slowTimer <= 0f)
                {
                    _slowMultiplier = 1f;
                }
            }

            var player = ActorRegistry.Player;
            bool targetValid = player != null && player.Alive;
            Vector2 toPlayer = targetValid ? player.Position - Position : Vector2.zero;
            float distToPlayer = toPlayer.magnitude;

            if (IsBoss)
            {
                UpdateBoss(dt, player, targetValid, toPlayer, distToPlayer);
            }
            else if (Definition != null && Definition.Role == InvaderCombatRole.HealerSupport)
            {
                UpdateCleric(dt, player, targetValid, toPlayer, distToPlayer);
            }
            else if (Definition != null && (Definition.Role == InvaderCombatRole.Ranged || Definition.Role == InvaderCombatRole.Caster))
            {
                UpdateArcher(dt, player, targetValid, toPlayer, distToPlayer);
            }
            else
            {
                UpdateSwordsman(dt, player, targetValid, toPlayer, distToPlayer);
            }
        }

        private void UpdateSwordsman(float dt, PlayerActor player, bool targetValid, Vector2 toPlayer, float distToPlayer)
        {
            if (_melee == null)
            {
                float range = Definition != null ? Definition.AttackRange : CombatTuning.InvaderMeleeRange;
                float windup = Definition != null ? Definition.AttackWindup : CombatTuning.InvaderWindup;
                float recover = Definition != null ? Math.Max(0.4f, Definition.AttackCooldown * 0.5f) : CombatTuning.InvaderRecover;
                _melee = new MeleeAttackSim(range, windup, recover);
            }

            bool struck = _melee.Tick(dt, distToPlayer, targetValid);
            if (struck)
            {
                CueBus.Raise(CueIds.InvaderStrike, Position.x, Position.y);
                float baseDmg = Definition != null ? Definition.AttackDamage : CombatTuning.InvaderMeleeDamage;
                float dmg = baseDmg * (IsElite ? CombatTuning.EliteDamageMultiplier : 1f);
                if (targetValid && distToPlayer <= _melee.Range * 1.15f)
                    player.ReceiveDamage(dmg);
            }

            bool approach = targetValid && _melee.Phase == MeleePhase.Idle && distToPlayer > _melee.Range * 0.8f;
            _rb.linearVelocity = approach ? toPlayer / distToPlayer * MoveSpeed : Vector2.zero;
        }

        private void UpdateArcher(float dt, PlayerActor player, bool targetValid, Vector2 toPlayer, float distToPlayer)
        {
            if (_actionCooldown > 0f)
                _actionCooldown -= dt;

            if (_isActionWinding)
            {
                _actionWindup -= dt;
                _rb.linearVelocity = Vector2.zero;

                if (targetValid && toPlayer.sqrMagnitude > 1e-4f)
                    _aimDirection = toPlayer.normalized;

                if (_actionWindup <= 0f)
                {
                    _isActionWinding = false;
                    _actionCooldown = Definition != null ? Definition.AttackCooldown : CombatTuning.ArcherCooldown;

                    // Fire projectile
                    float baseDmg = Definition != null ? Definition.AttackDamage : CombatTuning.ArcherArrowDamage;
                    float dmg = baseDmg * (IsElite ? CombatTuning.EliteDamageMultiplier : 1f);
                    if (ProjectileManager.Instance != null)
                    {
                        Color projColor = (Definition != null && Definition.Role == InvaderCombatRole.Caster)
                            ? new Color(Definition.ColorR, Definition.ColorG, Definition.ColorB)
                            : new Color(1f, 0.75f, 0.2f);
                        string cue = (Definition != null && Definition.Role == InvaderCombatRole.Caster)
                            ? CueIds.BoltFire
                            : CueIds.ArrowFire;

                        ProjectileManager.Instance.FireCustom(
                            Faction.Invader,
                            Position,
                            _aimDirection,
                            CombatTuning.ArcherArrowSpeed,
                            CombatTuning.ArcherArrowRadius,
                            dmg,
                            CombatTuning.ArcherArrowLifetime,
                            projColor,
                            cue);
                    }
                }
                return;
            }

            if (!targetValid)
            {
                _rb.linearVelocity = Vector2.zero;
                return;
            }

            float minRange = Definition != null ? Math.Max(2.5f, Definition.AttackRange * 0.55f) : CombatTuning.ArcherMinRange;
            float maxRange = Definition != null ? Definition.AttackRange : CombatTuning.ArcherMaxRange;

            // Kiting logic
            if (distToPlayer < minRange)
            {
                // Retreat away from player
                _rb.linearVelocity = (-toPlayer / distToPlayer) * MoveSpeed;
            }
            else if (distToPlayer > maxRange)
            {
                // Approach
                _rb.linearVelocity = (toPlayer / distToPlayer) * MoveSpeed;
            }
            else
            {
                // Inside sweet spot -> stop and prepare attack
                _rb.linearVelocity = Vector2.zero;
                _aimDirection = toPlayer / distToPlayer;

                if (_actionCooldown <= 0f)
                {
                    _isActionWinding = true;
                    _actionWindup = Definition != null ? Definition.AttackWindup : CombatTuning.ArcherWindup;
                    CueBus.Raise(CueIds.InvaderWindup, Position.x, Position.y);
                }
            }
        }

        private void UpdateCleric(float dt, PlayerActor player, bool targetValid, Vector2 toPlayer, float distToPlayer)
        {
            if (_actionCooldown > 0f)
                _actionCooldown -= dt;

            // Find most injured ally
            InvaderActor injuredAlly = null;
            float lowestHpPercent = 0.95f;

            var all = ActorRegistry.AllActors;
            for (int i = 0; i < all.Count; i++)
            {
                if (all[i] is InvaderActor inv && inv != this && inv.Alive)
                {
                    float p = inv.Health.Current / inv.Health.Max;
                    if (p < lowestHpPercent && (inv.Position - Position).sqrMagnitude <= CombatTuning.ClericHealRange * CombatTuning.ClericHealRange * 1.5f)
                    {
                        lowestHpPercent = p;
                        injuredAlly = inv;
                    }
                }
            }

            if (_isActionWinding)
            {
                _actionWindup -= dt;
                _rb.linearVelocity = Vector2.zero;

                if (_actionWindup <= 0f)
                {
                    _isActionWinding = false;
                    _actionCooldown = CombatTuning.ClericHealCooldown;

                    if (injuredAlly != null && injuredAlly.Alive)
                    {
                        injuredAlly.Health.Heal(CombatTuning.ClericHealAmount);
                        CueBus.Raise(CueIds.ClericHeal, injuredAlly.Position.x, injuredAlly.Position.y);
                    }
                    else
                    {
                        Health.Heal(CombatTuning.ClericHealAmount);
                        CueBus.Raise(CueIds.ClericHeal, Position.x, Position.y);
                    }
                }
                return;
            }

            if (injuredAlly != null)
            {
                Vector2 toAlly = injuredAlly.Position - Position;
                float distToAlly = toAlly.magnitude;
                if (distToAlly > CombatTuning.ClericHealRange * 0.7f)
                {
                    _rb.linearVelocity = (toAlly / distToAlly) * MoveSpeed;
                }
                else
                {
                    _rb.linearVelocity = Vector2.zero;
                    if (_actionCooldown <= 0f)
                    {
                        _isActionWinding = true;
                        _actionWindup = CombatTuning.ClericHealWindup;
                    }
                }
            }
            else if (targetValid)
            {
                // Support keeps safe distance from player (6-8 units)
                if (distToPlayer < 5.5f)
                    _rb.linearVelocity = (-toPlayer / distToPlayer) * MoveSpeed;
                else if (distToPlayer > 8f)
                    _rb.linearVelocity = (toPlayer / distToPlayer) * MoveSpeed;
                else
                    _rb.linearVelocity = Vector2.zero;
            }
        }

        private void UpdateBoss(float dt, PlayerActor player, bool targetValid, Vector2 toPlayer, float distToPlayer)
        {
            // Passive Regeneration modifier (2.5% max HP / sec if undamaged for 2.5s)
            _timeSinceDamageTaken += dt;
            if ((Modifiers & BossModifier.Regeneration) != 0 && _timeSinceDamageTaken >= 2.5f && Health.Current < Health.Max)
            {
                Health.Heal(Health.Max * 0.025f * dt);
            }

            // Dash charge timer
            if (_bossDashActive > 0f)
            {
                _bossDashActive -= dt;
                _rb.linearVelocity = _bossDashDir * CombatTuning.BossDashSpeed;
                if (targetValid && distToPlayer < Radius + player.Radius)
                {
                    player.ReceiveDamage(15f);
                }
                return;
            }

            // Boss signature ability cooldown & windup
            if (_bossAbilityCooldown > 0f)
                _bossAbilityCooldown -= dt;

            if (_isBossAbilityWinding)
            {
                _bossAbilityWindup -= dt;
                _rb.linearVelocity = Vector2.zero;
                if (_bossAbilityWindup <= 0f)
                {
                    _isBossAbilityWinding = false;
                    _bossAbilityCooldown = BossDefinition != null ? BossDefinition.AttackCooldown : 3.0f;
                    ExecuteBossAbility(_windingAbility, player, toPlayer, distToPlayer);
                }
                return;
            }

            if (_bossAbilityCooldown <= 0f && targetValid && distToPlayer <= 9.0f)
            {
                BossAbilityType ability = BossDefinition != null ? BossDefinition.PrimaryAbility : BossAbilityType.WhirlwindCharge;
                if (_isEnraged && BossDefinition != null)
                {
                    ability = _useSecondaryAbilityNext ? BossDefinition.SecondaryAbility : BossDefinition.PrimaryAbility;
                    _useSecondaryAbilityNext = !_useSecondaryAbilityNext;
                }
                _windingAbility = ability;
                _isBossAbilityWinding = true;
                _bossAbilityWindup = BossDefinition != null ? Math.Max(0.35f, BossDefinition.AttackWindup) : 0.5f;
                CueBus.Raise(CueIds.InvaderWindup, Position.x, Position.y);
                return;
            }

            _bossDashTimer -= dt;
            if (_bossDashTimer <= 0f && targetValid && distToPlayer > 4f)
            {
                _bossDashTimer = CombatTuning.BossDashCooldown;
                _bossDashActive = 0.35f;
                _bossDashDir = toPlayer.normalized;
                return;
            }

            bool struck = _melee.Tick(dt, distToPlayer, targetValid);
            if (struck)
            {
                CueBus.Raise(CueIds.BossCleave, Position.x, Position.y);
                float dmg = CombatTuning.BossCleaveDamage * (_isEnraged ? 1.25f : 1f);
                if (targetValid && distToPlayer <= _melee.Range * 1.15f)
                    player.ReceiveDamage(dmg);

                // Enraged boss cleave emits 3 shockwave bolts
                if (_isEnraged && ProjectileManager.Instance != null && targetValid)
                {
                    Vector2 baseDir = toPlayer.normalized;
                    float angle = Mathf.Atan2(baseDir.y, baseDir.x) * Mathf.Rad2Deg;

                    float[] offsets = { -22f, 0f, 22f };
                    for (int i = 0; i < offsets.Length; i++)
                    {
                        float rad = (angle + offsets[i]) * Mathf.Deg2Rad;
                        Vector2 dir = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
                        ProjectileManager.Instance.FireCustom(
                            Faction.Invader,
                            Position,
                            dir,
                            CombatTuning.ArcherArrowSpeed * 1.1f,
                            0.25f,
                            CombatTuning.BossShockwaveDamage,
                            1.5f,
                            new Color(0.9f, 0.1f, 0.2f),
                            CueIds.BossShockwave);
                    }
                }
            }

            bool approach = targetValid && _melee.Phase == MeleePhase.Idle && distToPlayer > _melee.Range * 0.8f;
            _rb.linearVelocity = approach ? toPlayer / distToPlayer * MoveSpeed : Vector2.zero;
        }

        private void ExecuteBossAbility(BossAbilityType ability, PlayerActor player, Vector2 toPlayer, float distToPlayer)
        {
            float baseDmg = BossDefinition != null ? BossDefinition.AttackDamage : 25f;
            float dmg = baseDmg * (_isEnraged ? 1.35f : 1f);
            if ((Modifiers & BossModifier.ElementalOvercharge) != 0)
                dmg *= 1.3f;

            switch (ability)
            {
                case BossAbilityType.ColossalCleave:
                    CueBus.Raise(CueIds.BossCleave, Position.x, Position.y);
                    if (player != null && distToPlayer <= 3.2f)
                        player.ReceiveDamage(dmg * 1.25f);
                    break;

                case BossAbilityType.ShockwaveSlam:
                    CueBus.Raise(CueIds.BossShockwave, Position.x, Position.y);
                    if (player != null && distToPlayer <= 4.0f)
                        player.ReceiveDamage(dmg);
                    break;

                case BossAbilityType.MultishotBarrage:
                    if (ProjectileManager.Instance != null && player != null)
                    {
                        Vector2 baseDir = toPlayer.sqrMagnitude > 1e-4f ? toPlayer.normalized : Vector2.right;
                        float baseAngle = Mathf.Atan2(baseDir.y, baseDir.x) * Mathf.Rad2Deg;
                        float[] angles = { -30f, -15f, 0f, 15f, 30f };
                        for (int i = 0; i < angles.Length; i++)
                        {
                            float rad = (baseAngle + angles[i]) * Mathf.Deg2Rad;
                            Vector2 dir = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
                            ProjectileManager.Instance.FireCustom(
                                Faction.Invader, Position, dir, 8f, 0.25f, dmg * 0.6f, 2.0f,
                                new Color(0.95f, 0.35f, 0.15f), CueIds.ArrowFire);
                        }
                    }
                    break;

                case BossAbilityType.PhantomBlinkStrike:
                    if (player != null)
                    {
                        Vector2 blinkPos = player.Position + (Position - player.Position).normalized * 1.5f;
                        transform.position = blinkPos;
                        if (_rb != null) _rb.position = blinkPos;
                        CueBus.Raise(CueIds.Dash, blinkPos.x, blinkPos.y);
                        player.ReceiveDamage(dmg);
                    }
                    break;

                case BossAbilityType.MoltenEruption:
                    if (player != null)
                    {
                        CueBus.Raise(CueIds.Explosion, player.Position.x, player.Position.y);
                        if (distToPlayer <= 3.5f)
                            player.ReceiveDamage(dmg * 1.1f);
                    }
                    break;

                case BossAbilityType.GlacialPrison:
                    if (player != null)
                    {
                        CueBus.Raise(CueIds.FrostNova, player.Position.x, player.Position.y);
                        player.ReceiveDamage(dmg * 0.8f);
                        player.ApplySlow(0.35f, 3.0f);
                    }
                    break;

                case BossAbilityType.ThunderStrike:
                    if (player != null)
                    {
                        CueBus.Raise(CueIds.LightningStrike, player.Position.x, player.Position.y);
                        player.ReceiveDamage(dmg * 1.2f);
                    }
                    break;

                case BossAbilityType.SanctuaryWard:
                    CueBus.Raise(CueIds.ShieldBlock, Position.x, Position.y);
                    if (BarrierMax > 0f)
                        BarrierCurrent = Math.Min(BarrierMax, BarrierCurrent + BarrierMax * 0.5f);
                    else
                        Health.Heal(30f);
                    break;

                case BossAbilityType.WhirlwindCharge:
                    _bossDashActive = 0.45f;
                    _bossDashDir = toPlayer.sqrMagnitude > 1e-4f ? toPlayer.normalized : Vector2.right;
                    CueBus.Raise(CueIds.BossShockwave, Position.x, Position.y);
                    break;

                case BossAbilityType.SummonVanguard:
                    CueBus.Raise(CueIds.BossEnrage, Position.x, Position.y);
                    Spawn(Position + new Vector2(-1.8f, 0.5f), InvaderArchetype.Knight, false);
                    Spawn(Position + new Vector2(1.8f, 0.5f), InvaderArchetype.Swordsman, false);
                    break;
            }

            if ((Modifiers & BossModifier.ElementalOvercharge) != 0 && ProjectileManager.Instance != null)
            {
                Vector2[] dirs = { Vector2.up, Vector2.down, Vector2.left, Vector2.right };
                for (int i = 0; i < dirs.Length; i++)
                {
                    ProjectileManager.Instance.FireCustom(
                        Faction.Invader, Position, dirs[i], 7f, 0.2f, dmg * 0.4f, 1.5f,
                        new Color(1f, 0.5f, 0f), CueIds.BoltFire);
                }
            }
        }

        private void Update()
        {
            // Telegraph visuals
            if (_telegraph != null)
            {
                bool isWinding = false;
                float progress = 0f;
                float range = 1.3f;

                if (IsBoss)
                {
                    if (_isBossAbilityWinding)
                    {
                        isWinding = Alive;
                        float total = BossDefinition != null ? Math.Max(0.35f, BossDefinition.AttackWindup) : 0.5f;
                        progress = 1f - (_bossAbilityWindup / total);
                        range = 2.8f;
                    }
                    else if (_melee != null && _melee.Phase == MeleePhase.Windup)
                    {
                        isWinding = Alive;
                        progress = _melee.WindupProgress;
                        range = _melee.Range;
                    }
                }
                else if (Archetype == InvaderArchetype.Swordsman)
                {
                    isWinding = Alive && _melee != null && _melee.Phase == MeleePhase.Windup;
                    progress = _melee != null ? _melee.WindupProgress : 0f;
                    range = _melee != null ? _melee.Range : 1.3f;
                }
                else if (Archetype == InvaderArchetype.Archer)
                {
                    isWinding = Alive && _isActionWinding;
                    progress = 1f - (_actionWindup / CombatTuning.ArcherWindup);
                    range = CombatTuning.ArcherMinRange;
                }
                else if (Archetype == InvaderArchetype.Cleric)
                {
                    isWinding = Alive && _isActionWinding;
                    progress = 1f - (_actionWindup / CombatTuning.ClericHealWindup);
                    range = CombatTuning.ClericHealRange;
                }

                _telegraph.enabled = isWinding;
                if (isWinding)
                {
                    float d = range * 2f * Mathf.Max(0.2f, progress);
                    _telegraph.transform.localScale = new Vector3(d / transform.localScale.x, d / transform.localScale.y, 1f);
                }
            }

            if (_flash > 0f) _flash -= Time.deltaTime;
            _body.color = _flash > 0f ? Color.white : _baseColor;
        }

        public float ReceiveDamage(float rawDamage)
        {
            if (!Alive)
                return 0f;

            _timeSinceDamageTaken = 0f;
            float computed = DamageCalculator.Compute(rawDamage, Armor);

            if (BarrierCurrent > 0f)
            {
                float absorbed = Math.Min(BarrierCurrent, computed);
                BarrierCurrent -= absorbed;
                computed -= absorbed;
                CueBus.Raise(CueIds.ShieldBlock, Position.x, Position.y);
                if (computed <= 0f)
                {
                    _flash = 0.08f;
                    return absorbed;
                }
            }

            float dealt = Health.TakeDamage(computed);
            if (dealt <= 0f)
                return 0f;

            if ((Modifiers & BossModifier.Reinforcements) != 0 && Alive)
            {
                if (!_spawned70Reinforcements && Health.Current <= Health.Max * 0.70f)
                {
                    _spawned70Reinforcements = true;
                    Spawn(Position + new Vector2(-1.5f, 0.5f), InvaderArchetype.Swordsman, false);
                    Spawn(Position + new Vector2(1.5f, 0.5f), InvaderArchetype.Archer, false);
                }
                else if (!_spawned35Reinforcements && Health.Current <= Health.Max * 0.35f)
                {
                    _spawned35Reinforcements = true;
                    Spawn(Position + new Vector2(-1.5f, -0.5f), InvaderArchetype.Knight, false);
                    Spawn(Position + new Vector2(1.5f, -0.5f), InvaderArchetype.Priest, false);
                }
            }

            _flash = 0.08f;
            CueBus.Raise(CueIds.InvaderHit, Position.x, Position.y, dealt);
            if (Health.IsDead)
                Die();

            return dealt;
        }

        private void Die()
        {
            _melee?.Cancel();
            ActorRegistry.Unregister(this);
            CueBus.Raise(CueIds.InvaderDeath, Position.x, Position.y);

            SpawnLoot();

            var handler = Killed;
            Killed = null;
            handler?.Invoke(this);

            Destroy(gameObject);
        }

        private void SpawnLoot()
        {
            var session = GameSession.State;
            if (session == null || session.Run == null)
                return;

            var hud = session.Run.Hud;
            var inv = session.Run.Inventory;
            Vector2 dropPos = Position;

            if (IsBoss)
            {
                LootPickupActor.SpawnGold(dropPos + new Vector2(-0.4f, 0f), 50, hud);
                var rareItems = new[] { ItemCatalog.DeathScytheId, ItemCatalog.NecroticShroudId, ItemCatalog.AmuletOfTormentId };
                var pickedId = rareItems[UnityEngine.Random.Range(0, rareItems.Length)];
                var def = ItemCatalog.GetById(pickedId);
                if (def != null)
                {
                    LootPickupActor.SpawnItem(dropPos + new Vector2(0.4f, 0f), new ItemInstance(def), inv);
                }
            }
            else if (IsElite)
            {
                LootPickupActor.SpawnGold(dropPos, 15, hud);
                if (UnityEngine.Random.value < 0.5f)
                {
                    var allDefs = new List<ItemDefinition>(ItemCatalog.All);
                    var def = allDefs[UnityEngine.Random.Range(0, allDefs.Count)];
                    LootPickupActor.SpawnItem(dropPos + new Vector2(0.3f, 0.2f), new ItemInstance(def), inv);
                }
            }
            else
            {
                if (UnityEngine.Random.value < 0.75f)
                {
                    LootPickupActor.SpawnGold(dropPos, 5, hud);
                }
                else if (UnityEngine.Random.value < 0.2f)
                {
                    var allDefs = new List<ItemDefinition>(ItemCatalog.All);
                    var def = allDefs[UnityEngine.Random.Range(0, allDefs.Count)];
                    LootPickupActor.SpawnItem(dropPos, new ItemInstance(def), inv);
                }
            }
        }
    }
}
