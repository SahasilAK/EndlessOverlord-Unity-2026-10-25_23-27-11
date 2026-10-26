using System;
using System.Collections.Generic;
using FinalBoss.Core;
using UnityEngine;

namespace FinalBoss.Game
{
    /// <summary>
    /// Core active ability loadout controller managing the 5 equipped ability slots (slots 0..4).
    /// Handles:
    /// - 5-slot activation via hotkeys 1-5 (and slot 0 alternate E/RMB)
    /// - Cooldown tracking per slot
    /// - Autocast toggles and target-in-range evaluation
    /// - Definition-driven modular execution nodes (Summon, Projectile, MeleeArc, RadialAoE, Dash, Shield, CC, Heal, Hazard, Gravity)
    /// - Blocked-state messaging
    /// - Useful-action mastery XP awarding with anti-spam thresholds
    /// </summary>
    public class AbilityLoadoutController : MonoBehaviour
    {
        public const int MaxSlots = AbilityLoadout.SlotCount;

        protected PlayerActor _player;
        protected PlayerInputAdapter _input;
        protected RunContext _run;

        private readonly float[] _slotCooldowns = new float[MaxSlots];
        private readonly AbilityDefinition[] _slotAbilities = new AbilityDefinition[MaxSlots];
        private readonly int[] _slotRanks = new int[MaxSlots];
        private readonly int[] _slotAscensions = new int[MaxSlots];
        private readonly bool[] _autocastEnabled = new bool[MaxSlots];

        private readonly List<IDamageable> _hitBuffer = new List<IDamageable>();
        private readonly List<TargetCandidate> _targetBuffer = new List<TargetCandidate>();

        public event Action<int, string> AbilityBlocked;
        public event Action<int, AbilityExecutionResult> AbilityExecuted;
        public event Action LoadoutChanged;

        // Slot 0 backwards-compatible properties for SignatureAbilityController
        public float CooldownRemaining => GetSlotCooldown(0);
        public bool IsReady => IsSlotReady(0);
        public string AbilityName => GetSlotAbilityName(0);
        public float ResourceCost => GetSlotResourceCost(0);
        public bool HasSufficientResource => HasSufficientResourceForSlot(0);

        public void Initialize(PlayerActor player, PlayerInputAdapter input, RunContext run)
        {
            _player = player;
            _input = input;
            _run = run;

            if (_input != null)
            {
                _input.SkillPressed += OnSkillPressed;
            }

            SyncLoadoutFromRun();
        }

        protected virtual void OnDestroy()
        {
            if (_input != null)
            {
                _input.SkillPressed -= OnSkillPressed;
            }
        }

        public void SyncLoadoutFromRun()
        {
            if (_run == null) return;

            CharacterRecord charRec = default(CharacterRecord);
            bool hasProfile = SaveStorage.CurrentProfile != null;
            if (hasProfile)
            {
                charRec = SaveStorage.CurrentProfile.GetOrAddCharacter(_run.Character.Id);
            }

            for (int i = 0; i < MaxSlots; i++)
            {
                string abilityId = _run.Loadout != null ? _run.Loadout.GetSlot(i) : string.Empty;

                // Default slot 0 to signature ability if empty
                if (i == 0 && string.IsNullOrEmpty(abilityId) && _run.Character != null)
                {
                    abilityId = _run.Character.SignatureAbilityId;
                    if (_run.Loadout != null)
                    {
                        _run.Loadout.TryEquip(0, abilityId, null, out _);
                    }
                }

                _slotAbilities[i] = AbilityRegistry.Get(abilityId);
                if (_slotAbilities[i] != null)
                {
                    int rank = hasProfile ? CharacterSkillTree.GetAbilityRank(charRec, abilityId) : 1;
                    _slotRanks[i] = Math.Max(1, rank);
                    var prog = hasProfile ? CharacterSkillTree.GetAbilityProgress(charRec, abilityId) : default(AbilityProgressRecord);
                    _slotAscensions[i] = prog.AscensionRank;
                }
                else
                {
                    _slotRanks[i] = 0;
                    _slotAscensions[i] = 0;
                }

                _autocastEnabled[i] = _run.Loadout != null && _run.Loadout.IsAutocastEnabled(i);
            }

            LoadoutChanged?.Invoke();
        }

        public AbilityDefinition GetSlotAbility(int slot)
        {
            if (slot < 0 || slot >= MaxSlots) return null;
            return _slotAbilities[slot];
        }

        public float GetSlotCooldown(int slot)
        {
            if (slot < 0 || slot >= MaxSlots) return 0f;
            return _slotCooldowns[slot];
        }

        public float GetSlotMaxCooldown(int slot)
        {
            if (slot < 0 || slot >= MaxSlots || _slotAbilities[slot] == null) return 0f;
            return AbilityProgressionMath.CalculateEffectiveCooldown(_slotAbilities[slot], _slotRanks[slot], _slotAscensions[slot]);
        }

        public string GetSlotAbilityName(int slot)
        {
            if (slot < 0 || slot >= MaxSlots || _slotAbilities[slot] == null) return "Empty";
            return _slotAbilities[slot].Name;
        }

        public float GetSlotResourceCost(int slot)
        {
            if (slot < 0 || slot >= MaxSlots || _slotAbilities[slot] == null) return 0f;
            return AbilityProgressionMath.CalculateEffectiveResourceCost(_slotAbilities[slot], _slotRanks[slot], _slotAscensions[slot]);
        }

        public bool IsSlotReady(int slot)
        {
            if (slot < 0 || slot >= MaxSlots || _slotAbilities[slot] == null) return false;
            return _slotCooldowns[slot] <= 0f && HasSufficientResourceForSlot(slot);
        }

        public bool HasSufficientResourceForSlot(int slot)
        {
            if (_run == null || _run.Hud == null) return false;
            float cost = GetSlotResourceCost(slot);
            return _run.Hud.Resource >= cost;
        }

        public bool IsAutocastEnabled(int slot)
        {
            if (slot < 0 || slot >= MaxSlots) return false;
            return _autocastEnabled[slot];
        }

        public void ToggleAutocast(int slot)
        {
            if (slot < 0 || slot >= MaxSlots) return;
            if (_slotAbilities[slot] == null || !_slotAbilities[slot].EligibleForAutocast) return;

            _autocastEnabled[slot] = !_autocastEnabled[slot];
            if (_run != null && _run.Loadout != null)
            {
                _run.Loadout.SetAutocast(slot, _autocastEnabled[slot]);
            }

            if (SaveStorage.CurrentProfile != null && _run != null && _run.Character != null)
            {
                var charRec = SaveStorage.CurrentProfile.GetOrAddCharacter(_run.Character.Id);
                charRec.EnsureInitialized();
                if (charRec.AutocastSettings != null && slot < charRec.AutocastSettings.Count)
                {
                    charRec.AutocastSettings[slot] = _autocastEnabled[slot];
                    SaveStorage.CurrentProfile.SaveCharacter(charRec);
                    SaveStorage.SaveCurrentProfile();
                }
            }

            LoadoutChanged?.Invoke();
        }

        private void Update()
        {
            // Cooldown ticking
            for (int i = 0; i < MaxSlots; i++)
            {
                if (_slotCooldowns[i] > 0f)
                {
                    _slotCooldowns[i] -= Time.deltaTime;
                    if (_slotCooldowns[i] < 0f) _slotCooldowns[i] = 0f;
                }
            }

            // Autocast ticking (strictly gated by gameplay state)
            if (GameSession.Machine.Current == RunState.PlayingWave && _player != null && _player.Alive)
            {
                TickAutocast();
            }
        }

        private void TickAutocast()
        {
            for (int i = 0; i < MaxSlots; i++)
            {
                if (!_autocastEnabled[i]) continue;
                if (_slotAbilities[i] == null) continue;
                if (_slotCooldowns[i] > 0f) continue;

                // Resource guard: do not spin or cast if player cannot afford it
                if (!HasSufficientResourceForSlot(i)) continue;

                // Target check: do not spin into thin air if no enemies in range
                float range = Math.Max(4f, _slotAbilities[i].Range);
                if (!HasEnemyInRange(range)) continue;

                TryActivateSlot(i);
            }
        }

        private bool HasEnemyInRange(float range)
        {
            var all = ActorRegistry.AllActors;
            Vector2 pos = _player != null ? _player.Position : Vector2.zero;
            for (int i = 0; i < all.Count; i++)
            {
                var a = all[i];
                if (a.Team == Faction.Invader && a.Alive)
                {
                    if (Vector2.Distance(pos, a.Position) <= range)
                        return true;
                }
            }
            return false;
        }

        private void OnSkillPressed(int slot)
        {
            TryActivateSlot(slot);
        }

        public bool TryActivate()
        {
            return TryActivateSlot(0);
        }

        public bool TryActivateSlot(int slotIndex)
        {
            if (slotIndex < 0 || slotIndex >= MaxSlots)
                return false;

            var def = _slotAbilities[slotIndex];
            if (def == null)
            {
                NotifyBlocked(slotIndex, "Slot is empty.");
                return false;
            }

            // State guard: pause and death check
            if (GameSession.Machine.Current != RunState.PlayingWave)
            {
                NotifyBlocked(slotIndex, "Cannot cast: Game is paused or in menu.");
                return false;
            }

            if (_player == null || !_player.Alive)
            {
                NotifyBlocked(slotIndex, "Cannot cast: Player is defeated.");
                return false;
            }

            // Cooldown check
            if (_slotCooldowns[slotIndex] > 0f)
            {
                NotifyBlocked(slotIndex, $"On cooldown ({_slotCooldowns[slotIndex]:0.0}s).");
                return false;
            }

            int rank = _slotRanks[slotIndex];
            int ascension = _slotAscensions[slotIndex];
            float effectiveCost = AbilityProgressionMath.CalculateEffectiveResourceCost(def, rank, ascension);
            float effectiveCooldown = AbilityProgressionMath.CalculateEffectiveCooldown(def, rank, ascension);

            // Resource check
            if (_run == null || _run.Hud == null || !_run.Hud.TrySpendResource(effectiveCost))
            {
                string resName = _run?.Hud?.ResourceName ?? "Resource";
                NotifyBlocked(slotIndex, $"Not enough {resName} (Requires {effectiveCost:0}).");
                return false;
            }

            // Set cooldown
            _slotCooldowns[slotIndex] = effectiveCooldown;

            // Execute modular nodes
            var result = ExecuteAbilityEffects(def, rank, ascension);
            AbilityExecuted?.Invoke(slotIndex, result);

            // Award useful-action mastery XP with anti-spam check
            if (result.IsUseful && SaveStorage.CurrentProfile != null && _run != null && _run.Character != null)
            {
                var charRec = SaveStorage.CurrentProfile.GetOrAddCharacter(_run.Character.Id);
                bool rankedUp = CharacterSkillTree.AwardMasteryXp(def.Id, ref charRec, xpAmount: 15);
                SaveStorage.CurrentProfile.SaveCharacter(charRec);
                SaveStorage.SaveCurrentProfile();

                if (rankedUp)
                {
                    SyncLoadoutFromRun();
                }
            }

            return true;
        }

        private void NotifyBlocked(int slotIndex, string message)
        {
            AbilityBlocked?.Invoke(slotIndex, message);
        }

        private AbilityExecutionResult ExecuteAbilityEffects(AbilityDefinition def, int rank, int ascension)
        {
            var result = new AbilityExecutionResult { Success = true };
            float damage = AbilityProgressionMath.CalculateEffectiveDamage(def, rank, ascension);
            float radius = AbilityProgressionMath.CalculateEffectiveRadius(def, rank);
            int summonCount = AbilityProgressionMath.CalculateEffectiveSummonCount(def, rank);
            Vector2 playerPos = _player.Position;
            Vector2 aimDir = _player.AimDirection.normalized;

            switch (def.Id)
            {
                // -------------------------------------------------------------
                // LICH LORD
                // -------------------------------------------------------------
                case "lich_lord_active_01": // Bone Sentinel
                    for (int i = 0; i < summonCount; i++)
                    {
                        Vector2 offset = aimDir * (1f + i * 0.8f);
                        BoneSentinelActor.Spawn(playerPos + offset);
                        result.SummonsCreated++;
                    }
                    CueBus.Raise(CueIds.BoltFire, playerPos.x, playerPos.y);
                    SpawnVisualBurst(playerPos + aimDir, 1.2f, new Color(0.7f, 1f, 0.9f, 0.8f), 0.25f);
                    break;

                case "lich_lord_active_02": // Crypt Archer
                    for (int i = 0; i < summonCount; i++)
                    {
                        Vector2 offset = -aimDir * (1.2f + i * 0.8f);
                        CryptArcherActor.Spawn(playerPos + offset, damage: damage, pierce: 1);
                        result.SummonsCreated++;
                    }
                    CueBus.Raise(CueIds.BoltFire, playerPos.x, playerPos.y);
                    SpawnVisualBurst(playerPos, 1.2f, new Color(0.6f, 0.85f, 1f, 0.85f), 0.25f);
                    break;

                case "lich_lord_active_03": // Grave Spearman
                    for (int i = 0; i < summonCount; i++)
                    {
                        Vector2 offset = aimDir * (1.2f + i * 0.8f);
                        UndeadMinionActor.Spawn(UndeadMinionType.Spearman, playerPos + offset, damage);
                        result.SummonsCreated++;
                    }
                    CueBus.Raise(CueIds.BoltFire, playerPos.x, playerPos.y);
                    SpawnVisualBurst(playerPos + aimDir, 1.2f, new Color(0.7f, 0.9f, 0.85f, 0.8f), 0.25f);
                    break;

                case "lich_lord_active_04": // Grave Hound
                    for (int i = 0; i < summonCount; i++)
                    {
                        Vector2 offset = aimDir * (1.0f + i * 0.7f);
                        UndeadMinionActor.Spawn(UndeadMinionType.Hound, playerPos + offset, damage);
                        result.SummonsCreated++;
                    }
                    CueBus.Raise(CueIds.BoltFire, playerPos.x, playerPos.y);
                    SpawnVisualBurst(playerPos + aimDir, 1.0f, new Color(0.55f, 0.6f, 0.65f, 0.8f), 0.25f);
                    break;

                case "lich_lord_active_05": // Bone Mage
                    for (int i = 0; i < summonCount; i++)
                    {
                        Vector2 offset = -aimDir * (1.5f + i * 0.8f);
                        UndeadMinionActor.Spawn(UndeadMinionType.Mage, playerPos + offset, damage);
                        result.SummonsCreated++;
                    }
                    CueBus.Raise(CueIds.BoltFire, playerPos.x, playerPos.y);
                    SpawnVisualBurst(playerPos, 1.2f, new Color(0.6f, 0.5f, 0.95f, 0.8f), 0.25f);
                    break;

                case "lich_lord_active_06": // Ossuary Guardian
                    for (int i = 0; i < summonCount; i++)
                    {
                        Vector2 offset = aimDir * (1.5f + i * 0.8f);
                        UndeadMinionActor.Spawn(UndeadMinionType.Guardian, playerPos + offset, damage);
                        result.SummonsCreated++;
                    }
                    CueBus.Raise(CueIds.BoltFire, playerPos.x, playerPos.y);
                    SpawnVisualBurst(playerPos + aimDir, 1.5f, new Color(0.85f, 0.85f, 0.92f, 0.85f), 0.3f);
                    break;

                case "lich_lord_active_07": // Wight Captain
                    for (int i = 0; i < summonCount; i++)
                    {
                        Vector2 offset = aimDir * (1.3f + i * 0.8f);
                        UndeadMinionActor.Spawn(UndeadMinionType.Captain, playerPos + offset, damage);
                        result.SummonsCreated++;
                    }
                    CueBus.Raise(CueIds.BoltFire, playerPos.x, playerPos.y);
                    SpawnVisualBurst(playerPos, 2.0f, new Color(1.0f, 0.9f, 0.5f, 0.85f), 0.35f);
                    break;

                case "lich_lord_active_08": // Corpse Collector
                    for (int i = 0; i < summonCount; i++)
                    {
                        Vector2 offset = aimDir * (1.0f + i * 0.7f);
                        UndeadMinionActor.Spawn(UndeadMinionType.Scavenger, playerPos + offset, damage);
                        result.SummonsCreated++;
                    }
                    CueBus.Raise(CueIds.BoltFire, playerPos.x, playerPos.y);
                    SpawnVisualBurst(playerPos, 1.2f, new Color(0.5f, 0.9f, 0.5f, 0.8f), 0.25f);
                    break;

                case "lich_lord_active_09": // Dread Abomination
                    for (int i = 0; i < summonCount; i++)
                    {
                        Vector2 offset = aimDir * (2.0f + i * 1.0f);
                        UndeadMinionActor.Spawn(UndeadMinionType.Abomination, playerPos + offset, damage);
                        result.SummonsCreated++;
                    }
                    CueBus.Raise(CueIds.BoltFire, playerPos.x, playerPos.y);
                    SpawnVisualBurst(playerPos + aimDir * 2f, 2.5f, new Color(0.4f, 0.7f, 0.4f, 0.9f), 0.4f);
                    break;

                case "lich_lord_active_10": // Skeletal Dragon
                    for (int i = 0; i < summonCount; i++)
                    {
                        Vector2 offset = aimDir * (2.5f + i * 1.2f);
                        UndeadMinionActor.Spawn(UndeadMinionType.Dragon, playerPos + offset, damage);
                        result.SummonsCreated++;
                    }
                    CueBus.Raise(CueIds.BoltFire, playerPos.x, playerPos.y);
                    SpawnVisualBurst(playerPos + aimDir * 2.5f, 3.0f, new Color(0.35f, 0.2f, 0.65f, 0.9f), 0.5f);
                    break;

                case "lich_lord_active_11": // Frost Nova
                    result.DamageDealt += DamageArea(playerPos, radius, damage, knockbackForce: 1.5f, slowPercent: 0.5f, slowDuration: 3f);
                    result.TargetsHit += _hitBuffer.Count;
                    SpawnVisualBurst(playerPos, radius * 2f, new Color(0.4f, 0.8f, 1f, 0.85f), 0.35f);
                    CueBus.Raise(CueIds.BoltFire, playerPos.x, playerPos.y);
                    break;

                case "lich_lord_active_12": // Permafrost Sigil
                    Vector2 sigilPos = playerPos + aimDir * Math.Min(3.5f, def.Range > 0 ? def.Range : 3.5f);
                    float sigilDur = def.Duration > 0f ? def.Duration : 5f;
                    HazardZoneActor.Spawn(sigilPos, radius, sigilDur, damagePerTick: damage * 0.35f, tickInterval: 0.5f, slowPercent: 0.5f, slowDuration: 1.5f, color: new Color(0.25f, 0.75f, 1f, 0.45f), label: "[SIGIL]");
                    result.DamageDealt += DamageArea(sigilPos, radius, damage * 0.4f, slowPercent: 0.5f, slowDuration: 2f);
                    result.TargetsHit += _hitBuffer.Count;
                    SpawnVisualBurst(sigilPos, radius * 2f, new Color(0.3f, 0.8f, 1f, 0.7f), 0.3f);
                    CueBus.Raise(CueIds.BoltFire, sigilPos.x, sigilPos.y);
                    break;

                case "lich_lord_active_13": // Chill Lance
                    int lancePierce = 2 + (rank >= 10 ? 2 : 0);
                    if (ProjectileManager.Instance != null)
                    {
                        ProjectileManager.Instance.FireCustom(
                            Faction.Player,
                            playerPos,
                            aimDir,
                            speed: 18f,
                            radius: 0.28f,
                            damage: damage,
                            lifetime: 1.5f,
                            color: new Color(0.35f, 0.85f, 1f, 0.95f),
                            cueId: CueIds.BoltFire,
                            pierce: lancePierce
                        );
                    }
                    result.DamageDealt += DamageArea(playerPos + aimDir * 2f, 1.2f, damage * 0.5f, slowPercent: 0.6f, slowDuration: 2f);
                    result.TargetsHit += _hitBuffer.Count;
                    SpawnVisualBurst(playerPos + aimDir * 0.8f, 1.0f, new Color(0.5f, 0.9f, 1f, 0.8f), 0.2f);
                    CueBus.Raise(CueIds.BoltFire, playerPos.x, playerPos.y);
                    break;

                case "lich_lord_active_14": // Wailing Blizzard
                    Vector2 blizzardVel = aimDir * 2.2f;
                    float blizzardDur = def.Duration > 0f ? def.Duration : 4f;
                    HazardZoneActor.Spawn(playerPos, radius, blizzardDur, damagePerTick: damage * 0.3f, tickInterval: 0.4f, slowPercent: 0.6f, slowDuration: 1.5f, velocity: blizzardVel, color: new Color(0.4f, 0.7f, 1f, 0.4f), label: "[BLIZZARD]");
                    result.DamageDealt += DamageArea(playerPos, radius, damage * 0.35f, slowPercent: 0.6f, slowDuration: 2.5f);
                    result.TargetsHit += _hitBuffer.Count;
                    SpawnVisualBurst(playerPos, radius * 2f, new Color(0.5f, 0.8f, 1f, 0.7f), 0.4f);
                    CueBus.Raise(CueIds.BoltFire, playerPos.x, playerPos.y);
                    break;

                case "lich_lord_active_15": // Glacial Tomb
                    IDamageable tombTarget = null;
                    float closestTombDist = float.MaxValue;
                    var allActors = ActorRegistry.AllActors;
                    for (int i = 0; i < allActors.Count; i++)
                    {
                        var a = allActors[i];
                        if (a != null && a.Team == Faction.Invader && a.Alive)
                        {
                            float d = Vector2.Distance(playerPos, a.Position);
                            if (d <= (def.Range > 0 ? def.Range : 7f) && d < closestTombDist)
                            {
                                closestTombDist = d;
                                tombTarget = a;
                            }
                        }
                    }
                    if (tombTarget != null)
                    {
                        float dealt = tombTarget.ReceiveDamage(damage);
                        result.DamageDealt += dealt;
                        result.TargetsHit++;
                        if (tombTarget is InvaderActor inv)
                        {
                            inv.ApplySlow(0.85f, 3.5f);
                        }
                        SpawnVisualBurst(tombTarget.Position, 2.0f, new Color(0.2f, 0.9f, 1f, 0.95f), 0.5f);
                        CueBus.Raise(CueIds.InvaderStrike, tombTarget.Position.x, tombTarget.Position.y);
                    }
                    else
                    {
                        Vector2 fallbackPos = playerPos + aimDir * 3f;
                        result.DamageDealt += DamageArea(fallbackPos, radius, damage, slowPercent: 0.85f, slowDuration: 3f);
                        result.TargetsHit += _hitBuffer.Count;
                        SpawnVisualBurst(fallbackPos, radius * 2f, new Color(0.2f, 0.9f, 1f, 0.9f), 0.4f);
                        CueBus.Raise(CueIds.BoltFire, fallbackPos.x, fallbackPos.y);
                    }
                    break;

                case "lich_lord_active_16": // Shiver Pulse
                    result.DamageDealt += DamageArea(playerPos, radius, damage, knockbackForce: 6.5f, slowPercent: 0.65f, slowDuration: 3.5f);
                    result.TargetsHit += _hitBuffer.Count;
                    SpawnVisualBurst(playerPos, radius * 2f, new Color(0.6f, 0.95f, 1f, 0.85f), 0.35f);
                    CueBus.Raise(CueIds.BoltFire, playerPos.x, playerPos.y);
                    break;

                case "lich_lord_active_17": // Rime Chain
                    var chainHits = new List<IDamageable>();
                    var candidateActors = ActorRegistry.AllActors;
                    Vector2 searchFrom = playerPos;
                    float chainRange = def.Range > 0 ? def.Range : 7.5f;
                    int maxJumps = 4;
                    for (int jump = 0; jump < maxJumps; jump++)
                    {
                        IDamageable nextTarget = null;
                        float bestDist = float.MaxValue;
                        for (int i = 0; i < candidateActors.Count; i++)
                        {
                            var a = candidateActors[i];
                            if (a != null && a.Team == Faction.Invader && a.Alive && !chainHits.Contains(a))
                            {
                                float d = Vector2.Distance(searchFrom, a.Position);
                                if (d <= chainRange && d < bestDist)
                                {
                                    bestDist = d;
                                    nextTarget = a;
                                }
                            }
                        }
                        if (nextTarget == null) break;
                        chainHits.Add(nextTarget);
                        searchFrom = nextTarget.Position;
                        chainRange = 5.0f;
                    }

                    if (chainHits.Count > 0)
                    {
                        for (int i = 0; i < chainHits.Count; i++)
                        {
                            var target = chainHits[i];
                            float dealt = target.ReceiveDamage(damage * (1f - i * 0.15f));
                            result.DamageDealt += dealt;
                            result.TargetsHit++;
                            if (target is InvaderActor inv)
                            {
                                inv.ApplySlow(0.6f, 2.5f);
                            }
                            SpawnVisualBurst(target.Position, 1.2f, new Color(0.4f, 0.85f, 1f, 0.9f), 0.25f);
                        }
                        CueBus.Raise(CueIds.BoltFire, playerPos.x, playerPos.y);
                    }
                    else
                    {
                        Vector2 chainFallback = playerPos + aimDir * 2.5f;
                        result.DamageDealt += DamageArea(chainFallback, radius, damage, slowPercent: 0.5f, slowDuration: 2f);
                        result.TargetsHit += _hitBuffer.Count;
                        SpawnVisualBurst(chainFallback, radius * 2f, new Color(0.4f, 0.85f, 1f, 0.8f), 0.25f);
                        CueBus.Raise(CueIds.BoltFire, chainFallback.x, chainFallback.y);
                    }
                    break;

                case "lich_lord_active_18": // Frozen Lantern
                    int lanternCount = 1 + (rank >= 10 ? 1 : 0);
                    float lanternDur = def.Duration > 0f ? def.Duration : 15f;
                    for (int l = 0; l < lanternCount; l++)
                    {
                        float angle = l * (360f / lanternCount);
                        FrozenLanternActor.Spawn(_player, initialAngle: angle, damage: damage * 0.45f, lifetime: lanternDur);
                        result.SummonsCreated++;
                    }
                    SpawnVisualBurst(playerPos, 1.5f, new Color(0.4f, 0.85f, 1f, 0.8f), 0.3f);
                    CueBus.Raise(CueIds.BoltFire, playerPos.x, playerPos.y);
                    break;

                case "lich_lord_active_19": // Hail Crown
                    float hailDur = def.Duration > 0f ? def.Duration : 4.5f;
                    HazardZoneActor.Spawn(playerPos, radius, hailDur, damagePerTick: damage * 0.25f, tickInterval: 0.25f, slowPercent: 0.55f, slowDuration: 1.5f, color: new Color(0.35f, 0.65f, 1f, 0.5f), label: "[HAIL_CROWN]");
                    result.DamageDealt += DamageArea(playerPos, radius, damage * 0.35f, slowPercent: 0.55f, slowDuration: 2.5f);
                    result.TargetsHit += _hitBuffer.Count;
                    SpawnVisualBurst(playerPos, radius * 2f, new Color(0.6f, 0.9f, 1f, 0.85f), 0.4f);
                    CueBus.Raise(CueIds.BoltFire, playerPos.x, playerPos.y);
                    break;

                case "lich_lord_active_20": // Winter of the Dead
                    result.DamageDealt += DamageArea(playerPos, radius, damage, knockbackForce: 4f, slowPercent: 0.8f, slowDuration: 8f);
                    result.TargetsHit += _hitBuffer.Count;
                    var minions = ActorRegistry.AllActors;
                    for (int i = 0; i < minions.Count; i++)
                    {
                        var minion = minions[i];
                        if (minion != null && minion.Team == Faction.Player && (object)minion != _player && minion.Alive)
                        {
                            if (minion is UndeadMinionActor undead) undead.Health?.Heal(25f);
                            else if (minion is BoneSentinelActor sentinel) sentinel.Health?.Heal(25f);
                            else if (minion is CryptArcherActor archer) archer.Health?.Heal(25f);
                        }
                    }
                    SpawnVisualBurst(playerPos, radius * 2f, new Color(0.2f, 0.7f, 1f, 0.9f), 0.7f);
                    CueBus.Raise(CueIds.BoltFire, playerPos.x, playerPos.y);
                    break;

                case "lich_lord_active_21": // Witchfire Orb
                    int orbCount = 1 + (rank >= 20 ? 1 : 0);
                    for (int o = 0; o < orbCount; o++)
                    {
                        float spread = (o - (orbCount - 1) * 0.5f) * 15f * Mathf.Deg2Rad;
                        Vector2 oDir = new Vector2(
                            aimDir.x * Mathf.Cos(spread) - aimDir.y * Mathf.Sin(spread),
                            aimDir.x * Mathf.Sin(spread) + aimDir.y * Mathf.Cos(spread)
                        );
                        if (ProjectileManager.Instance != null)
                        {
                            ProjectileManager.Instance.FireCustom(
                                Faction.Player,
                                playerPos,
                                oDir,
                                speed: 12f,
                                radius: 0.25f,
                                damage: damage,
                                lifetime: 1.5f,
                                color: new Color(0.2f, 0.95f, 0.4f, 0.95f),
                                cueId: CueIds.BoltFire,
                                pierce: 0
                            );
                        }
                    }
                    result.DamageDealt += DamageArea(playerPos + aimDir * 2.5f, radius, damage * 0.4f);
                    result.TargetsHit += _hitBuffer.Count;
                    SpawnVisualBurst(playerPos + aimDir * 2.5f, radius * 2f, new Color(0.2f, 0.95f, 0.4f, 0.85f), 0.3f);
                    break;

                case "lich_lord_active_22": // Graveflame Pillar
                    Vector2 pillarPos = playerPos + aimDir * Math.Min(3.5f, def.Range > 0 ? def.Range : 3.5f);
                    float pillarDur = def.Duration > 0f ? def.Duration : 2.5f;
                    HazardZoneActor.Spawn(pillarPos, radius, pillarDur, damagePerTick: damage * 0.3f, tickInterval: 0.35f, color: new Color(0.9f, 0.3f, 0.1f, 0.5f), label: "[GRAVEFLAME]");
                    result.DamageDealt += DamageArea(pillarPos, radius, damage, knockbackForce: 3f);
                    result.TargetsHit += _hitBuffer.Count;
                    SpawnVisualBurst(pillarPos, radius * 2f, new Color(1.0f, 0.4f, 0.1f, 0.9f), 0.4f);
                    CueBus.Raise(CueIds.InvaderStrike, pillarPos.x, pillarPos.y);
                    break;

                case "lich_lord_active_23": // Ashen Gust
                    result.DamageDealt += DamageArc(playerPos, aimDir, radius, 120f, damage, knockbackForce: 7.0f);
                    result.TargetsHit += _hitBuffer.Count;
                    SpawnVisualArc(playerPos, aimDir, radius, new Color(0.75f, 0.75f, 0.75f, 0.7f), 0.25f);
                    CueBus.Raise(CueIds.PlayerDash, playerPos.x, playerPos.y);
                    break;

                case "lich_lord_active_24": // Static Cadaver
                    Vector2 trapPos = playerPos + aimDir * Math.Min(3.5f, def.Range > 0 ? def.Range : 3.5f);
                    float cadaverDur = def.Duration > 0f ? def.Duration : 4f;
                    HazardZoneActor.Spawn(trapPos, radius, cadaverDur, damagePerTick: damage * 0.25f, tickInterval: 0.3f, slowPercent: 0.3f, slowDuration: 1.0f, color: new Color(0.4f, 0.6f, 1f, 0.45f), label: "[STATIC_CADAVER]");
                    result.DamageDealt += DamageArea(trapPos, radius, damage * 0.4f);
                    result.TargetsHit += _hitBuffer.Count;
                    SpawnVisualBurst(trapPos, radius * 2f, new Color(0.4f, 0.8f, 1f, 0.85f), 0.35f);
                    CueBus.Raise(CueIds.BoltFire, trapPos.x, trapPos.y);
                    break;

                case "lich_lord_active_25": // Deathbolt
                    int boltPierce = 3 + (rank >= 5 ? 1 : 0);
                    if (ProjectileManager.Instance != null)
                    {
                        ProjectileManager.Instance.FireCustom(
                            Faction.Player,
                            playerPos,
                            aimDir,
                            speed: 20f,
                            radius: 0.22f,
                            damage: damage,
                            lifetime: 1.2f,
                            color: new Color(0.9f, 0.95f, 0.3f, 0.95f),
                            cueId: CueIds.BoltFire,
                            pierce: boltPierce
                        );
                    }
                    result.DamageDealt += DamageArea(playerPos + aimDir * 3f, 1.5f, damage * 0.6f, knockbackForce: 2f);
                    result.TargetsHit += _hitBuffer.Count;
                    SpawnVisualBurst(playerPos + aimDir * 1.5f, 1.2f, new Color(1f, 1f, 0.4f, 0.85f), 0.25f);
                    CueBus.Raise(CueIds.BoltFire, playerPos.x, playerPos.y);
                    break;

                case "lich_lord_active_26": // Corpse Spark
                    var sparkHits = new List<IDamageable>();
                    var sparkCandidates = ActorRegistry.AllActors;
                    Vector2 sparkOrigin = playerPos;
                    float sparkRange = def.Range > 0 ? def.Range : 8.0f;
                    int maxSparkJumps = 4 + (rank >= 5 ? 1 : 0);
                    for (int jump = 0; jump < maxSparkJumps; jump++)
                    {
                        IDamageable nextSpark = null;
                        float bestSparkDist = float.MaxValue;
                        for (int i = 0; i < sparkCandidates.Count; i++)
                        {
                            var a = sparkCandidates[i];
                            if (a != null && a.Team == Faction.Invader && a.Alive && !sparkHits.Contains(a))
                            {
                                float d = Vector2.Distance(sparkOrigin, a.Position);
                                if (d <= sparkRange && d < bestSparkDist)
                                {
                                    bestSparkDist = d;
                                    nextSpark = a;
                                }
                            }
                        }
                        if (nextSpark == null) break;
                        sparkHits.Add(nextSpark);
                        sparkOrigin = nextSpark.Position;
                        sparkRange = 5.5f;
                    }

                    if (sparkHits.Count > 0)
                    {
                        for (int i = 0; i < sparkHits.Count; i++)
                        {
                            var target = sparkHits[i];
                            float dealt = target.ReceiveDamage(damage * (1f - i * 0.12f));
                            result.DamageDealt += dealt;
                            result.TargetsHit++;
                            SpawnVisualBurst(target.Position, 1.2f, new Color(0.8f, 0.9f, 1f, 0.9f), 0.2f);
                        }
                        CueBus.Raise(CueIds.BoltFire, playerPos.x, playerPos.y);
                    }
                    else
                    {
                        Vector2 sparkFallback = playerPos + aimDir * 2.5f;
                        result.DamageDealt += DamageArea(sparkFallback, radius, damage);
                        result.TargetsHit += _hitBuffer.Count;
                        SpawnVisualBurst(sparkFallback, radius * 2f, new Color(0.7f, 0.85f, 1f, 0.8f), 0.25f);
                        CueBus.Raise(CueIds.BoltFire, sparkFallback.x, sparkFallback.y);
                    }
                    break;

                case "lich_lord_active_27": // Storm of Ash
                    Vector2 ashVel = aimDir * 1.8f;
                    float ashDur = def.Duration > 0f ? def.Duration : 5f;
                    HazardZoneActor.Spawn(playerPos, radius, ashDur, damagePerTick: damage * 0.25f, tickInterval: 0.35f, slowPercent: 0.4f, slowDuration: 1.5f, velocity: ashVel, color: new Color(0.55f, 0.4f, 0.35f, 0.45f), label: "[ASH_STORM]");
                    result.DamageDealt += DamageArea(playerPos, radius, damage * 0.35f, slowPercent: 0.4f, slowDuration: 2f);
                    result.TargetsHit += _hitBuffer.Count;
                    SpawnVisualBurst(playerPos, radius * 2f, new Color(0.6f, 0.45f, 0.35f, 0.7f), 0.4f);
                    CueBus.Raise(CueIds.BoltFire, playerPos.x, playerPos.y);
                    break;

                case "lich_lord_active_28": // Phantom Cyclone
                    Vector2 cyclonePos = playerPos + aimDir * Math.Min(3.5f, def.Range > 0 ? def.Range : 3.5f);
                    float cycloneDur = def.Duration > 0f ? def.Duration : 4f;
                    HazardZoneActor.Spawn(cyclonePos, radius, cycloneDur, damagePerTick: damage * 0.25f, tickInterval: 0.3f, color: new Color(0.3f, 0.85f, 0.8f, 0.45f), label: "[CYCLONE]");
                    result.DamageDealt += DamageArea(cyclonePos, radius, damage * 0.4f, pullTowardCenter: true);
                    result.TargetsHit += _hitBuffer.Count;
                    SpawnVisualBurst(cyclonePos, radius * 2f, new Color(0.35f, 0.9f, 0.85f, 0.8f), 0.4f);
                    CueBus.Raise(CueIds.PlayerDash, cyclonePos.x, cyclonePos.y);
                    break;

                case "lich_lord_active_29": // Ember Ossuary
                    float emberDur = def.Duration > 0f ? def.Duration : 3.5f;
                    HazardZoneActor.Spawn(playerPos, radius, emberDur, damagePerTick: damage * 0.3f, tickInterval: 0.4f, color: new Color(1.0f, 0.45f, 0.15f, 0.5f), label: "[EMBER_OSSUARY]");
                    result.DamageDealt += DamageArea(playerPos, radius, damage * 0.6f, knockbackForce: 4f);
                    result.TargetsHit += _hitBuffer.Count;
                    SpawnVisualBurst(playerPos, radius * 2f, new Color(1f, 0.5f, 0.2f, 0.9f), 0.5f);
                    CueBus.Raise(CueIds.InvaderStrike, playerPos.x, playerPos.y);
                    break;

                case "lich_lord_active_30": // Thunder Coffin
                    IDamageable coffinTarget = null;
                    float closestCoffinDist = float.MaxValue;
                    var coffinActors = ActorRegistry.AllActors;
                    for (int i = 0; i < coffinActors.Count; i++)
                    {
                        var a = coffinActors[i];
                        if (a != null && a.Team == Faction.Invader && a.Alive)
                        {
                            float d = Vector2.Distance(playerPos, a.Position);
                            if (d <= (def.Range > 0 ? def.Range : 8.5f) && d < closestCoffinDist)
                            {
                                closestCoffinDist = d;
                                coffinTarget = a;
                            }
                        }
                    }
                    if (coffinTarget != null)
                    {
                        coffinTarget.ReceiveDamage(damage * 0.4f);
                        if (coffinTarget is InvaderActor inv) inv.ApplySlow(0.9f, 2.5f);
                        Vector2 center = coffinTarget.Position;
                        HazardZoneActor.Spawn(center, radius, duration: 2.5f, damagePerTick: damage * 0.3f, tickInterval: 0.3f, slowPercent: 0.9f, slowDuration: 1.0f, color: new Color(0.3f, 0.6f, 1f, 0.55f), label: "[THUNDER_COFFIN]");
                        result.DamageDealt += DamageArea(center, radius, damage * 0.6f, knockbackForce: 5.5f);
                        result.TargetsHit += _hitBuffer.Count;
                        SpawnVisualBurst(center, radius * 2f, new Color(0.5f, 0.8f, 1f, 0.95f), 0.6f);
                        CueBus.Raise(CueIds.BoltFire, center.x, center.y);
                    }
                    else
                    {
                        Vector2 coffinFallback = playerPos + aimDir * 3f;
                        result.DamageDealt += DamageArea(coffinFallback, radius, damage, knockbackForce: 5f);
                        result.TargetsHit += _hitBuffer.Count;
                        SpawnVisualBurst(coffinFallback, radius * 2f, new Color(0.5f, 0.8f, 1f, 0.9f), 0.5f);
                        CueBus.Raise(CueIds.BoltFire, coffinFallback.x, coffinFallback.y);
                    }
                    break;

                case "lich_lord_active_31": // Soul Siphon
                    Vector2 siphonPos = playerPos + aimDir * 2.5f;
                    result.DamageDealt += DamageArea(siphonPos, radius, damage);
                    result.TargetsHit += _hitBuffer.Count;
                    if (result.DamageDealt > 0f)
                    {
                        float heal = result.DamageDealt * (0.35f + (rank >= 5 ? 0.15f : 0f));
                        _player.Health?.Heal(heal);
                        result.HealingDone += heal;
                    }
                    if (rank >= 20 && _run?.Hud != null)
                    {
                        _run.Hud.AddResource(15f);
                    }
                    SpawnVisualBurst(siphonPos, radius * 2f, new Color(0.6f, 0.1f, 0.7f, 0.85f), 0.3f);
                    CueBus.Raise(CueIds.InvaderStrike, siphonPos.x, siphonPos.y);
                    break;

                case "lich_lord_active_32": // Hex of Frailty
                    Vector2 hexPos = playerPos + aimDir * Math.Min(3.5f, def.Range > 0 ? def.Range : 3.5f);
                    result.DamageDealt += DamageArea(hexPos, radius, damage, slowPercent: 0.35f, slowDuration: def.Duration > 0 ? def.Duration : 4f);
                    result.TargetsHit += _hitBuffer.Count;
                    SpawnVisualBurst(hexPos, radius * 2f, new Color(0.5f, 0.05f, 0.65f, 0.85f), 0.4f);
                    CueBus.Raise(CueIds.BoltFire, hexPos.x, hexPos.y);
                    break;

                case "lich_lord_active_33": // Withering Field
                    Vector2 witheringPos = playerPos + aimDir * Math.Min(3.5f, def.Range > 0 ? def.Range : 3.5f);
                    float witheringDur = def.Duration > 0f ? def.Duration : 4.5f;
                    HazardZoneActor.Spawn(witheringPos, radius, witheringDur, damagePerTick: damage * 0.25f, tickInterval: 0.35f, slowPercent: 0.4f, slowDuration: 1.0f, color: new Color(0.35f, 0.1f, 0.45f, 0.5f), label: "[WITHERING]");
                    result.DamageDealt += DamageArea(witheringPos, radius, damage * 0.35f, slowPercent: 0.4f, slowDuration: 2f);
                    result.TargetsHit += _hitBuffer.Count;
                    SpawnVisualBurst(witheringPos, radius * 2f, new Color(0.4f, 0.1f, 0.5f, 0.75f), 0.35f);
                    CueBus.Raise(CueIds.BoltFire, witheringPos.x, witheringPos.y);
                    break;

                case "lich_lord_active_34": // Black Sun
                    Vector2 sunPos = playerPos + aimDir * Math.Min(3.5f, def.Range > 0 ? def.Range : 3.5f);
                    float sunDur = def.Duration > 0f ? def.Duration : 4f;
                    HazardZoneActor.Spawn(sunPos, radius, sunDur, damagePerTick: damage * 0.3f, tickInterval: 0.3f, color: new Color(0.15f, 0.02f, 0.25f, 0.6f), label: "[BLACK_SUN]");
                    result.DamageDealt += DamageArea(sunPos, radius, damage * 0.4f, pullTowardCenter: true);
                    result.TargetsHit += _hitBuffer.Count;
                    SpawnVisualBurst(sunPos, radius * 2f, new Color(0.2f, 0.05f, 0.35f, 0.95f), 0.5f);
                    CueBus.Raise(CueIds.PlayerDash, sunPos.x, sunPos.y);
                    break;

                case "lich_lord_active_35": // Spirit Leech
                    IDamageable leechTarget = null;
                    float closestLeechDist = float.MaxValue;
                    var leechActors = ActorRegistry.AllActors;
                    for (int i = 0; i < leechActors.Count; i++)
                    {
                        var a = leechActors[i];
                        if (a != null && a.Team == Faction.Invader && a.Alive)
                        {
                            float d = Vector2.Distance(playerPos, a.Position);
                            if (d <= (def.Range > 0 ? def.Range : 8f) && d < closestLeechDist)
                            {
                                closestLeechDist = d;
                                leechTarget = a;
                            }
                        }
                    }
                    if (leechTarget != null)
                    {
                        float dealt = leechTarget.ReceiveDamage(damage);
                        result.DamageDealt += dealt;
                        result.TargetsHit++;
                        float heal = dealt * 0.45f;
                        _player.Health?.Heal(heal);
                        result.HealingDone += heal;
                        SpawnVisualBurst(leechTarget.Position, 1.5f, new Color(0.5f, 0.1f, 0.6f, 0.9f), 0.3f);
                        CueBus.Raise(CueIds.InvaderStrike, leechTarget.Position.x, leechTarget.Position.y);
                    }
                    else
                    {
                        result.DamageDealt += DamageArea(playerPos + aimDir * 3f, radius, damage * 0.5f);
                        result.TargetsHit += _hitBuffer.Count;
                        SpawnVisualBurst(playerPos + aimDir * 3f, radius * 2f, new Color(0.5f, 0.1f, 0.6f, 0.8f), 0.3f);
                    }
                    break;

                case "lich_lord_active_36": // Doom Bind
                    Vector2 bindPos = playerPos + aimDir * Math.Min(3.5f, def.Range > 0 ? def.Range : 3.5f);
                    result.DamageDealt += DamageArea(bindPos, radius, damage, slowPercent: 0.90f, slowDuration: def.Duration > 0 ? def.Duration : 3f);
                    result.TargetsHit += _hitBuffer.Count;
                    SpawnVisualBurst(bindPos, radius * 2f, new Color(0.25f, 0.05f, 0.4f, 0.9f), 0.4f);
                    CueBus.Raise(CueIds.InvaderStrike, bindPos.x, bindPos.y);
                    break;

                case "lich_lord_active_37": // Ebon Grasp
                    Vector2 graspPos = playerPos + aimDir * Math.Min(3.5f, def.Range > 0 ? def.Range : 3.5f);
                    float graspDur = def.Duration > 0f ? def.Duration : 4f;
                    HazardZoneActor.Spawn(graspPos, radius, graspDur, damagePerTick: damage * 0.25f, tickInterval: 0.35f, slowPercent: 0.65f, slowDuration: 1.0f, color: new Color(0.3f, 0.15f, 0.35f, 0.5f), label: "[EBON_GRASP]");
                    result.DamageDealt += DamageArea(graspPos, radius, damage * 0.35f, slowPercent: 0.65f, slowDuration: 2.5f);
                    result.TargetsHit += _hitBuffer.Count;
                    SpawnVisualBurst(graspPos, radius * 2f, new Color(0.35f, 0.1f, 0.45f, 0.8f), 0.35f);
                    CueBus.Raise(CueIds.BoltFire, graspPos.x, graspPos.y);
                    break;

                case "lich_lord_active_38": // Curse Spread
                    var curseHits = new List<IDamageable>();
                    var curseCandidates = ActorRegistry.AllActors;
                    Vector2 curseOrigin = playerPos;
                    float curseRange = def.Range > 0 ? def.Range : 8.0f;
                    int maxCurseJumps = 4 + (rank >= 5 ? 1 : 0);
                    for (int jump = 0; jump < maxCurseJumps; jump++)
                    {
                        IDamageable nextCurse = null;
                        float bestCurseDist = float.MaxValue;
                        for (int i = 0; i < curseCandidates.Count; i++)
                        {
                            var a = curseCandidates[i];
                            if (a != null && a.Team == Faction.Invader && a.Alive && !curseHits.Contains(a))
                            {
                                float d = Vector2.Distance(curseOrigin, a.Position);
                                if (d <= curseRange && d < bestCurseDist)
                                {
                                    bestCurseDist = d;
                                    nextCurse = a;
                                }
                            }
                        }
                        if (nextCurse == null) break;
                        curseHits.Add(nextCurse);
                        curseOrigin = nextCurse.Position;
                        curseRange = 5.5f;
                    }

                    if (curseHits.Count > 0)
                    {
                        for (int i = 0; i < curseHits.Count; i++)
                        {
                            var target = curseHits[i];
                            float dealt = target.ReceiveDamage(damage * (1f - i * 0.12f));
                            result.DamageDealt += dealt;
                            result.TargetsHit++;
                            if (target is InvaderActor inv) inv.ApplySlow(0.4f, 3.0f);
                            SpawnVisualBurst(target.Position, 1.2f, new Color(0.6f, 0.1f, 0.7f, 0.9f), 0.2f);
                        }
                        CueBus.Raise(CueIds.BoltFire, playerPos.x, playerPos.y);
                    }
                    else
                    {
                        Vector2 curseFallback = playerPos + aimDir * 2.5f;
                        result.DamageDealt += DamageArea(curseFallback, radius, damage);
                        result.TargetsHit += _hitBuffer.Count;
                        SpawnVisualBurst(curseFallback, radius * 2f, new Color(0.5f, 0.1f, 0.65f, 0.8f), 0.25f);
                        CueBus.Raise(CueIds.BoltFire, curseFallback.x, curseFallback.y);
                    }
                    break;

                case "lich_lord_active_39": // Death Sentence
                    IDamageable sentenceTarget = null;
                    float closestSentenceDist = float.MaxValue;
                    var sentenceActors = ActorRegistry.AllActors;
                    for (int i = 0; i < sentenceActors.Count; i++)
                    {
                        var a = sentenceActors[i];
                        if (a != null && a.Team == Faction.Invader && a.Alive)
                        {
                            float d = Vector2.Distance(playerPos, a.Position);
                            if (d <= (def.Range > 0 ? def.Range : 8.5f) && d < closestSentenceDist)
                            {
                                closestSentenceDist = d;
                                sentenceTarget = a;
                            }
                        }
                    }
                    if (sentenceTarget != null)
                    {
                        float dealt = sentenceTarget.ReceiveDamage(damage);
                        result.DamageDealt += dealt;
                        result.TargetsHit++;
                        if (sentenceTarget is InvaderActor inv) inv.ApplySlow(0.85f, 3f);
                        SpawnVisualBurst(sentenceTarget.Position, 2f, new Color(0.8f, 0.05f, 0.2f, 0.95f), 0.5f);
                        CueBus.Raise(CueIds.InvaderStrike, sentenceTarget.Position.x, sentenceTarget.Position.y);
                    }
                    else
                    {
                        Vector2 sentenceFallback = playerPos + aimDir * 3f;
                        result.DamageDealt += DamageArea(sentenceFallback, radius, damage, slowPercent: 0.85f, slowDuration: 3f);
                        result.TargetsHit += _hitBuffer.Count;
                        SpawnVisualBurst(sentenceFallback, radius * 2f, new Color(0.8f, 0.05f, 0.2f, 0.9f), 0.4f);
                        CueBus.Raise(CueIds.BoltFire, sentenceFallback.x, sentenceFallback.y);
                    }
                    break;

                case "lich_lord_active_40": // Umbral Nova
                    result.DamageDealt += DamageArea(playerPos, radius, damage, knockbackForce: 5.5f, slowPercent: 0.7f, slowDuration: 4f);
                    result.TargetsHit += _hitBuffer.Count;
                    if (rank >= 20 && _player.Health != null && _run?.Hud != null)
                    {
                        _player.Health.Heal(25f);
                        _run.Hud.AddResource(20f);
                    }
                    SpawnVisualBurst(playerPos, radius * 2f, new Color(0.15f, 0.02f, 0.25f, 0.95f), 0.6f);
                    CueBus.Raise(CueIds.BossEnrage, playerPos.x, playerPos.y);
                    break;

                case "lich_lord_active_41": // Reanimate Fallen
                    int reanimateCount = 1 + (rank >= 20 ? 1 : 0);
                    for (int i = 0; i < reanimateCount; i++)
                    {
                        Vector2 offset = aimDir * (1.2f + i * 0.8f);
                        UndeadMinionActor.Spawn(UndeadMinionType.Spearman, playerPos + offset, overrideDamage: damage, overrideMaxHp: 60f + (rank >= 10 ? 25f : 0f), lifetime: def.Duration > 0 ? def.Duration : 8.0f);
                        result.SummonsCreated++;
                    }
                    result.DamageDealt += DamageArea(playerPos + aimDir * 2f, radius, damage);
                    result.TargetsHit += _hitBuffer.Count;
                    SpawnVisualBurst(playerPos + aimDir * 2f, radius * 2f, new Color(0.2f, 0.8f, 0.5f, 0.8f), 0.35f);
                    CueBus.Raise(CueIds.BoltFire, playerPos.x, playerPos.y);
                    break;

                case "lich_lord_active_42": // Tomb Relay
                    Vector2 relayTarget = playerPos + aimDir * Math.Min(6.0f, def.Range > 0 ? def.Range : 6.0f);
                    var allUndead = UnityEngine.Object.FindObjectsByType<UndeadMinionActor>(FindObjectsSortMode.None);
                    int relayedCount = 0;
                    for (int i = 0; i < allUndead.Length; i++)
                    {
                        if (allUndead[i].Alive)
                        {
                            float angle = relayedCount * (Mathf.PI * 2f / Mathf.Max(1, allUndead.Length));
                            Vector2 dest = relayTarget + new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * 1.2f;
                            allUndead[i].transform.position = dest;
                            var rb = allUndead[i].GetComponent<Rigidbody2D>();
                            if (rb != null) rb.position = dest;
                            relayedCount++;
                        }
                    }
                    var sentinels = UnityEngine.Object.FindObjectsByType<BoneSentinelActor>(FindObjectsSortMode.None);
                    for (int i = 0; i < sentinels.Length; i++)
                    {
                        sentinels[i].transform.position = relayTarget + new Vector2(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f));
                    }
                    result.DamageDealt += DamageArea(relayTarget, radius, damage, knockbackForce: 3.5f);
                    result.TargetsHit += _hitBuffer.Count;
                    SpawnVisualBurst(relayTarget, radius * 2f, new Color(0.3f, 0.9f, 0.7f, 0.85f), 0.4f);
                    CueBus.Raise(CueIds.BoltFire, relayTarget.x, relayTarget.y);
                    break;

                case "lich_lord_active_43": // Legion Formation
                    var formationMinions = UnityEngine.Object.FindObjectsByType<UndeadMinionActor>(FindObjectsSortMode.None);
                    for (int i = 0; i < formationMinions.Length; i++)
                    {
                        if (formationMinions[i].Alive)
                        {
                            formationMinions[i].Health.Heal(rank >= 20 ? 15f : 5f);
                        }
                    }
                    _player.Health?.Heal(10f);
                    SpawnVisualBurst(playerPos, radius * 2f, new Color(0.4f, 0.85f, 0.4f, 0.75f), 0.5f);
                    CueBus.Raise(CueIds.InvaderHit, playerPos.x, playerPos.y);
                    break;

                case "lich_lord_active_44": // Soul Furnace
                    Vector2 furnacePos = playerPos + aimDir * Math.Min(4.0f, def.Range > 0 ? def.Range : 4.0f);
                    float furnaceDur = def.Duration > 0 ? def.Duration : 3.0f;
                    HazardZoneActor.Spawn(furnacePos, radius, furnaceDur, damagePerTick: damage * 0.35f, tickInterval: 0.3f, slowPercent: 0.3f, slowDuration: 0.6f, color: new Color(0.85f, 0.3f, 0.1f, 0.6f), label: "[SOUL_FURNACE]");
                    result.DamageDealt += DamageArea(furnacePos, radius, damage, knockbackForce: 3.0f);
                    result.TargetsHit += _hitBuffer.Count;
                    SpawnVisualBurst(furnacePos, radius * 2f, new Color(0.9f, 0.35f, 0.1f, 0.9f), 0.5f);
                    CueBus.Raise(CueIds.BoltFire, furnacePos.x, furnacePos.y);
                    break;

                case "lich_lord_active_45": // Boneshield
                    float shieldHp = 50f + (rank >= 5 ? 20f : 0f);
                    float shieldDur = def.Duration > 0 ? def.Duration : (rank >= 15 ? 12.0f : 8.0f);
                    _player.SetBarrier(shieldHp, shieldDur);
                    result.DamageDealt += DamageArea(playerPos, radius, damage);
                    result.TargetsHit += _hitBuffer.Count;
                    SpawnVisualBurst(playerPos, radius * 2f, new Color(0.85f, 0.9f, 0.95f, 0.85f), 0.4f);
                    CueBus.Raise(CueIds.BoltFire, playerPos.x, playerPos.y);
                    break;

                case "lich_lord_active_46": // Corpse Explosion
                    Vector2 explodePos = playerPos + aimDir * Math.Min(5.0f, def.Range > 0 ? def.Range : 5.0f);
                    float nearestDist = float.MaxValue;
                    var allExplodeTargets = ActorRegistry.AllActors;
                    for (int i = 0; i < allExplodeTargets.Count; i++)
                    {
                        var a = allExplodeTargets[i];
                        if (a.Team == Faction.Invader && a.Alive)
                        {
                            float d = Vector2.Distance(playerPos, a.Position);
                            if (d < nearestDist && d <= (def.Range > 0 ? def.Range : 8.5f))
                            {
                                nearestDist = d;
                                explodePos = a.Position;
                            }
                        }
                    }
                    result.DamageDealt += DamageArea(explodePos, radius, damage, knockbackForce: 4.5f);
                    result.TargetsHit += _hitBuffer.Count;
                    SpawnVisualBurst(explodePos, radius * 2f, new Color(0.8f, 0.15f, 0.15f, 0.9f), 0.45f);
                    CueBus.Raise(CueIds.BossEnrage, explodePos.x, explodePos.y);
                    break;

                case "lich_lord_active_47": // Soul Anchor
                    Vector2 anchorPos = playerPos + aimDir * Math.Min(4.0f, def.Range > 0 ? def.Range : 4.0f);
                    float anchorDur = def.Duration > 0 ? def.Duration : 6.0f;
                    HazardZoneActor.Spawn(anchorPos, radius, anchorDur, damagePerTick: damage * 0.25f, tickInterval: 0.5f, slowPercent: 0.4f, slowDuration: 1.0f, color: new Color(0.2f, 0.8f, 0.7f, 0.55f), label: "[SOUL_ANCHOR]");
                    _player.Health?.Heal(20f + (rank >= 5 ? 10f : 0f));
                    SpawnVisualBurst(anchorPos, radius * 2f, new Color(0.25f, 0.85f, 0.75f, 0.8f), 0.4f);
                    CueBus.Raise(CueIds.BoltFire, anchorPos.x, anchorPos.y);
                    break;

                case "lich_lord_active_48": // Death Pact
                    var sacrificeCandidates = UnityEngine.Object.FindObjectsByType<UndeadMinionActor>(FindObjectsSortMode.None);
                    int sacrificed = 0;
                    for (int i = 0; i < sacrificeCandidates.Length && sacrificed < 2; i++)
                    {
                        if (sacrificeCandidates[i].Alive)
                        {
                            sacrificeCandidates[i].ReceiveDamage(9999f);
                            sacrificed++;
                        }
                    }
                    float healRatio = 0.25f + (rank >= 5 ? 0.10f : 0f);
                    if (_player.Health != null)
                    {
                        _player.Health.Heal(_player.Health.Max * healRatio);
                    }
                    if (_run?.Hud != null)
                    {
                        _run.Hud.AddResource(20f + (rank >= 10 ? 15f : 0f));
                    }
                    result.DamageDealt += DamageArea(playerPos, radius, damage, knockbackForce: 3.5f);
                    result.TargetsHit += _hitBuffer.Count;
                    SpawnVisualBurst(playerPos, radius * 2f, new Color(0.6f, 0.1f, 0.3f, 0.85f), 0.4f);
                    CueBus.Raise(CueIds.InvaderHit, playerPos.x, playerPos.y);
                    break;

                case "lich_lord_active_49": // Necropolis Gate
                    Vector2 gatePos = playerPos + aimDir * Math.Min(4.0f, def.Range > 0 ? def.Range : 4.0f);
                    float gateDur = def.Duration > 0 ? def.Duration : 6.0f;
                    HazardZoneActor.Spawn(gatePos, radius, gateDur, damagePerTick: damage * 0.25f, tickInterval: 0.5f, slowPercent: 0.35f, slowDuration: 1.0f, color: new Color(0.25f, 0.1f, 0.35f, 0.6f), label: "[NECROPOLIS_GATE]");
                    int gateSummonCount = 1 + (rank >= 5 ? 1 : 0);
                    for (int i = 0; i < gateSummonCount; i++)
                    {
                        BoneSentinelActor.Spawn(gatePos + new Vector2(UnityEngine.Random.Range(-0.8f, 0.8f), UnityEngine.Random.Range(-0.8f, 0.8f)));
                        result.SummonsCreated++;
                    }
                    if (rank >= 20)
                    {
                        UndeadMinionActor.Spawn(UndeadMinionType.Captain, gatePos + Vector2.up * 0.5f, overrideDamage: damage);
                        result.SummonsCreated++;
                    }
                    SpawnVisualBurst(gatePos, radius * 2f, new Color(0.35f, 0.15f, 0.45f, 0.85f), 0.45f);
                    CueBus.Raise(CueIds.BoltFire, gatePos.x, gatePos.y);
                    break;

                case "lich_lord_active_50": // Crown of Silence
                    result.DamageDealt += DamageArea(playerPos, radius, damage, knockbackForce: 4.5f, slowPercent: 0.85f, slowDuration: def.Duration > 0 ? def.Duration : 3.5f);
                    result.TargetsHit += _hitBuffer.Count;
                    SpawnVisualBurst(playerPos, radius * 2f, new Color(0.4f, 0.15f, 0.55f, 0.95f), 0.6f);
                    CueBus.Raise(CueIds.BossEnrage, playerPos.x, playerPos.y);
                    break;

                case "lich_lord_ultimate_50": // Army of the Damned
                    for (int i = 0; i < summonCount + 1; i++)
                    {
                        float angle = i * (Mathf.PI * 2f / (summonCount + 1));
                        Vector2 spawnPos = playerPos + new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * 1.8f;
                        BoneSentinelActor.Spawn(spawnPos);
                        result.SummonsCreated++;
                    }
                    result.DamageDealt += DamageArea(playerPos, radius, damage, knockbackForce: 4f);
                    result.TargetsHit += _hitBuffer.Count;
                    SpawnVisualBurst(playerPos, radius * 2f, new Color(0.2f, 0.9f, 0.7f, 0.9f), 0.5f);
                    CueBus.Raise(CueIds.BoltFire, playerPos.x, playerPos.y);
                    break;

                case "lich_lord_ultimate_100": // Ossuary Dragon
                    Vector2 dragonPos = playerPos + aimDir * 2.5f;
                    UndeadMinionActor.Spawn(UndeadMinionType.Dragon, dragonPos, damage, overrideMaxHp: 250f);
                    result.SummonsCreated++;
                    result.DamageDealt += DamageArea(dragonPos, radius, damage, knockbackForce: 4.5f);
                    result.TargetsHit += _hitBuffer.Count;
                    SpawnVisualBurst(dragonPos, radius * 2f, new Color(0.4f, 0.15f, 0.7f, 0.9f), 0.6f);
                    CueBus.Raise(CueIds.BoltFire, playerPos.x, playerPos.y);
                    break;

                case "lich_lord_ultimate_150": // Eternal Winter Court
                    result.DamageDealt += DamageArea(playerPos, radius, damage, knockbackForce: 2f, slowPercent: 0.7f, slowDuration: 5f);
                    result.TargetsHit += _hitBuffer.Count;
                    for (int i = 0; i < 2; i++)
                    {
                        float side = i == 0 ? -1.5f : 1.5f;
                        Vector2 gPos = playerPos + new Vector2(side, 0f);
                        UndeadMinionActor.Spawn(UndeadMinionType.Guardian, gPos, damage * 0.4f, overrideMaxHp: 120f);
                        result.SummonsCreated++;
                    }
                    SpawnVisualBurst(playerPos, radius * 2f, new Color(0.3f, 0.75f, 1f, 0.9f), 0.6f);
                    CueBus.Raise(CueIds.BoltFire, playerPos.x, playerPos.y);
                    break;

                case "lich_lord_ultimate_200": // Black Star Eclipse
                    result.DamageDealt += DamageArea(playerPos, radius, damage, knockbackForce: 5f);
                    result.TargetsHit += _hitBuffer.Count;
                    if (_run != null && _run.Hud != null)
                    {
                        _run.Hud.AddResource(40f);
                    }
                    SpawnVisualBurst(playerPos, radius * 2f, new Color(0.15f, 0.05f, 0.35f, 0.95f), 0.7f);
                    CueBus.Raise(CueIds.BoltFire, playerPos.x, playerPos.y);
                    break;

                case "lich_lord_ultimate_250": // World Grave Awakening
                    result.DamageDealt += DamageArea(playerPos, radius, damage, knockbackForce: 6f);
                    result.TargetsHit += _hitBuffer.Count;
                    for (int i = 0; i < 4; i++)
                    {
                        float angle = i * (Mathf.PI * 0.5f);
                        BoneSentinelActor.Spawn(playerPos + new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * 2f);
                        result.SummonsCreated++;
                    }
                    for (int i = 0; i < 2; i++)
                    {
                        float side = i == 0 ? -2.5f : 2.5f;
                        CryptArcherActor.Spawn(playerPos + new Vector2(side, -1f));
                        result.SummonsCreated++;
                    }
                    UndeadMinionActor.Spawn(UndeadMinionType.Captain, playerPos + aimDir * 2f);
                    result.SummonsCreated++;
                    SpawnVisualBurst(playerPos, radius * 2f, new Color(0.2f, 0.85f, 0.4f, 0.95f), 0.8f);
                    CueBus.Raise(CueIds.BoltFire, playerPos.x, playerPos.y);
                    break;

                // -------------------------------------------------------------
                // DEMON MONARCH
                // -------------------------------------------------------------
                // Branch A — Infernal Swordsmanship (Actives 01-10)
                case "demon_monarch_active_01": // Infernal Cleave
                    result.DamageDealt += DamageArc(playerPos, aimDir, radius, 140f, damage, knockbackForce: 2.5f);
                    result.TargetsHit += _hitBuffer.Count;
                    SpawnVisualArc(playerPos, aimDir, radius, new Color(1f, 0.4f, 0.1f, 0.85f), 0.25f);
                    CueBus.Raise(CueIds.BossCleave, playerPos.x, playerPos.y);
                    break;

                case "demon_monarch_active_02": // Hellbrand Thrust
                    Vector2 thrustPos = playerPos + aimDir * 2.2f;
                    result.DamageDealt += DamageArea(thrustPos, radius, damage, knockbackForce: 3.5f);
                    result.TargetsHit += _hitBuffer.Count;
                    SpawnVisualBurst(thrustPos, radius * 1.8f, new Color(1f, 0.35f, 0.1f, 0.9f), 0.3f);
                    CueBus.Raise(CueIds.InvaderStrike, thrustPos.x, thrustPos.y);
                    break;

                case "demon_monarch_active_03": // Flaming Arc
                    if (ProjectileManager.Instance != null)
                    {
                        int waves = rank >= 20 ? 3 : 1;
                        for (int w = 0; w < waves; w++)
                        {
                            float angleOffset = (w - (waves - 1) * 0.5f) * 18f * Mathf.Deg2Rad;
                            Vector2 wDir = new Vector2(
                                aimDir.x * Mathf.Cos(angleOffset) - aimDir.y * Mathf.Sin(angleOffset),
                                aimDir.x * Mathf.Sin(angleOffset) + aimDir.y * Mathf.Cos(angleOffset)
                            );
                            ProjectileManager.Instance.FireCustom(
                                Faction.Player,
                                playerPos,
                                wDir,
                                speed: 9f,
                                radius: 0.45f,
                                damage: damage * 0.7f,
                                lifetime: 1.2f,
                                color: new Color(1f, 0.45f, 0.1f),
                                cueId: CueIds.InvaderStrike,
                                pierce: 3
                            );
                        }
                    }
                    Vector2 arcBurstPos = playerPos + aimDir * 2f;
                    result.DamageDealt += DamageArc(playerPos, aimDir, radius, 120f, damage * 0.5f, knockbackForce: 2f);
                    result.TargetsHit += _hitBuffer.Count;
                    SpawnVisualArc(playerPos, aimDir, radius, new Color(1f, 0.5f, 0.15f, 0.9f), 0.3f);
                    CueBus.Raise(CueIds.InvaderStrike, playerPos.x, playerPos.y);
                    break;

                case "demon_monarch_active_04": // Ember Uppercut
                    Vector2 uppercutPos = playerPos + aimDir * 1.8f;
                    result.DamageDealt += DamageArea(uppercutPos, radius, damage, knockbackForce: 6.5f);
                    result.TargetsHit += _hitBuffer.Count;
                    SpawnVisualBurst(uppercutPos, radius * 2f, new Color(1f, 0.6f, 0.2f, 0.9f), 0.35f);
                    CueBus.Raise(CueIds.BossCleave, uppercutPos.x, uppercutPos.y);
                    break;

                case "demon_monarch_active_05": // Blazing Riposte
                    float riposteBarrier = 60f * (1f + (rank >= 5 ? 0.30f : 0f));
                    _player.SetBarrier(riposteBarrier, duration: def.Duration);
                    result.BarrierGranted += riposteBarrier;
                    result.DamageDealt += DamageArea(playerPos, radius, damage, knockbackForce: 4f);
                    result.TargetsHit += _hitBuffer.Count;
                    SpawnVisualBurst(playerPos, radius * 2.2f, new Color(1f, 0.3f, 0.1f, 0.9f), 0.4f);
                    CueBus.Raise(CueIds.BossCleave, playerPos.x, playerPos.y);
                    break;

                case "demon_monarch_active_06": // Molten Lunge
                    Vector2 lungeStart = playerPos;
                    Vector2 lungeEnd = ValidateLandingDestination(lungeStart, lungeStart + aimDir * (def.Range > 0 ? def.Range : 5f));
                    _player.Teleport(lungeEnd);
                    result.DamageDealt += DamageArea(lungeEnd, radius, damage, knockbackForce: 5f);
                    result.TargetsHit += _hitBuffer.Count;
                    HazardZoneActor.Spawn(lungeStart, radius * 0.8f, duration: 2.5f, damagePerTick: damage * 0.25f, tickInterval: 0.4f, slowPercent: 0.3f, slowDuration: 1f, color: new Color(1f, 0.35f, 0.1f, 0.6f), label: "[MOLTEN_TRACK]");
                    SpawnVisualBurst(lungeEnd, radius * 2f, new Color(1f, 0.45f, 0.1f, 0.9f), 0.35f);
                    CueBus.Raise(CueIds.PlayerDash, lungeEnd.x, lungeEnd.y);
                    break;

                case "demon_monarch_active_07": // Burning Whirl
                    result.DamageDealt += DamageArea(playerPos, radius, damage, knockbackForce: 4f);
                    result.TargetsHit += _hitBuffer.Count;
                    if (ProjectileManager.Instance != null && rank >= 20)
                    {
                        for (int p = 0; p < 4; p++)
                        {
                            float ang = p * 90f * Mathf.Deg2Rad;
                            Vector2 bDir = new Vector2(Mathf.Cos(ang), Mathf.Sin(ang));
                            ProjectileManager.Instance.FireCustom(
                                Faction.Player,
                                playerPos,
                                bDir,
                                speed: 8f,
                                radius: 0.3f,
                                damage: damage * 0.35f,
                                lifetime: 0.8f,
                                color: new Color(1f, 0.35f, 0.15f),
                                cueId: CueIds.InvaderStrike,
                                pierce: 2
                            );
                        }
                    }
                    SpawnVisualBurst(playerPos, radius * 2.2f, new Color(1f, 0.4f, 0.15f, 0.9f), 0.45f);
                    CueBus.Raise(CueIds.BossCleave, playerPos.x, playerPos.y);
                    break;

                case "demon_monarch_active_08": // Volcanic Slash
                    Vector2 slashPos = playerPos + aimDir * 2.5f;
                    result.DamageDealt += DamageArc(playerPos, aimDir, radius, 120f, damage, knockbackForce: 3f);
                    result.TargetsHit += _hitBuffer.Count;
                    HazardZoneActor.Spawn(slashPos, radius, duration: 3.0f, damagePerTick: damage * 0.3f, tickInterval: 0.4f, slowPercent: 0.35f, slowDuration: 1.0f, color: new Color(1f, 0.25f, 0.05f, 0.65f), label: "[VOLCANIC_TRENCH]");
                    SpawnVisualBurst(slashPos, radius * 2.2f, new Color(1f, 0.35f, 0.1f, 0.95f), 0.5f);
                    CueBus.Raise(CueIds.BossCleave, slashPos.x, slashPos.y);
                    break;

                case "demon_monarch_active_09": // Sundering Edge
                    Vector2 sunderPos = playerPos + aimDir * 2.5f;
                    result.DamageDealt += DamageArea(sunderPos, radius, damage, knockbackForce: 5.5f);
                    result.TargetsHit += _hitBuffer.Count;
                    for (int i = 0; i < _hitBuffer.Count; i++)
                    {
                        if (_hitBuffer[i] is InvaderActor invader && invader.Alive)
                        {
                            invader.ApplySlow(0.40f, 3.0f);
                        }
                    }
                    SpawnVisualBurst(sunderPos, radius * 2.2f, new Color(0.95f, 0.2f, 0.1f, 0.95f), 0.45f);
                    CueBus.Raise(CueIds.BossCleave, sunderPos.x, sunderPos.y);
                    break;

                case "demon_monarch_active_10": // Royal Execution
                    Vector2 execPos = playerPos + aimDir * 2.5f;
                    result.DamageDealt += DamageArea(execPos, radius, damage, knockbackForce: 7f);
                    result.TargetsHit += _hitBuffer.Count;
                    SpawnVisualBurst(execPos, radius * 2.5f, new Color(1f, 0.2f, 0.05f, 0.95f), 0.6f);
                    CueBus.Raise(CueIds.BossCleave, execPos.x, execPos.y);
                    break;

                // Branch B — Hellfire Magic (Actives 11-20)
                case "demon_monarch_active_11": // Hellfire Wave
                    Vector2 wavePos = playerPos + aimDir * 3.0f;
                    result.DamageDealt += DamageArea(wavePos, radius, damage, knockbackForce: 4f);
                    result.TargetsHit += _hitBuffer.Count;
                    HazardZoneActor.Spawn(wavePos, radius, duration: 2.0f, damagePerTick: damage * 0.25f, tickInterval: 0.35f, slowPercent: 0.3f, slowDuration: 1.0f, color: new Color(1f, 0.35f, 0.1f, 0.6f), label: "[HELLFIRE_WAVE]");
                    SpawnVisualBurst(wavePos, radius * 2.2f, new Color(1f, 0.4f, 0.1f, 0.9f), 0.45f);
                    CueBus.Raise(CueIds.InvaderStrike, wavePos.x, wavePos.y);
                    break;

                case "demon_monarch_active_12": // Magma Fissure
                    Vector2 fissurePos = playerPos + aimDir * 3.0f;
                    result.DamageDealt += DamageArea(fissurePos, radius, damage, knockbackForce: 4.5f);
                    result.TargetsHit += _hitBuffer.Count;
                    HazardZoneActor.Spawn(fissurePos, radius, duration: 3.0f, damagePerTick: damage * 0.3f, tickInterval: 0.4f, slowPercent: 0.35f, slowDuration: 1.0f, color: new Color(1f, 0.25f, 0.05f, 0.65f), label: "[MAGMA_FISSURE]");
                    SpawnVisualBurst(fissurePos, radius * 2.2f, new Color(1f, 0.3f, 0.05f, 0.95f), 0.5f);
                    CueBus.Raise(CueIds.InvaderStrike, fissurePos.x, fissurePos.y);
                    break;

                case "demon_monarch_active_13": // Cinder Rain
                    Vector2 cinderPos = playerPos + aimDir * 3.5f;
                    result.DamageDealt += DamageArea(cinderPos, radius, damage, knockbackForce: 3f);
                    result.TargetsHit += _hitBuffer.Count;
                    HazardZoneActor.Spawn(cinderPos, radius, duration: 3.5f, damagePerTick: damage * 0.25f, tickInterval: 0.4f, slowPercent: 0.40f, slowDuration: 1.5f, color: new Color(1f, 0.45f, 0.15f, 0.6f), label: "[CINDER_RAIN]");
                    SpawnVisualBurst(cinderPos, radius * 2.4f, new Color(1f, 0.45f, 0.15f, 0.9f), 0.55f);
                    CueBus.Raise(CueIds.InvaderStrike, cinderPos.x, cinderPos.y);
                    break;

                case "demon_monarch_active_14": // Inferno Spiral
                    float spiralBarrier = rank >= 5 ? 50f : 0f;
                    if (spiralBarrier > 0f)
                    {
                        _player.SetBarrier(spiralBarrier, duration: def.Duration);
                        result.BarrierGranted += spiralBarrier;
                    }
                    result.DamageDealt += DamageArea(playerPos, radius, damage, knockbackForce: 3.5f);
                    result.TargetsHit += _hitBuffer.Count;
                    SpawnVisualBurst(playerPos, radius * 2f, new Color(1f, 0.4f, 0.1f, 0.9f), 0.45f);
                    CueBus.Raise(CueIds.BossCleave, playerPos.x, playerPos.y);
                    break;

                case "demon_monarch_active_15": // Pyre Ring
                    float pyreKb = rank >= 5 ? 7f : 5f;
                    result.DamageDealt += DamageArea(playerPos, radius, damage, knockbackForce: pyreKb);
                    result.TargetsHit += _hitBuffer.Count;
                    SpawnVisualBurst(playerPos, radius * 2.2f, new Color(1f, 0.35f, 0.05f, 0.95f), 0.45f);
                    CueBus.Raise(CueIds.InvaderStrike, playerPos.x, playerPos.y);
                    break;

                case "demon_monarch_active_16": // Flame Vortex
                    Vector2 vortexPos = playerPos + aimDir * 3.0f;
                    result.DamageDealt += DamageArea(vortexPos, radius, damage, knockbackForce: -4.5f, pullTowardCenter: true);
                    result.TargetsHit += _hitBuffer.Count;
                    HazardZoneActor.Spawn(vortexPos, radius, duration: 3.5f, damagePerTick: damage * 0.3f, tickInterval: 0.35f, slowPercent: 0.5f, slowDuration: 1.5f, color: new Color(1f, 0.3f, 0.1f, 0.65f), label: "[FLAME_VORTEX]");
                    SpawnVisualBurst(vortexPos, radius * 2.2f, new Color(1f, 0.3f, 0.1f, 0.95f), 0.55f);
                    CueBus.Raise(CueIds.InvaderStrike, vortexPos.x, vortexPos.y);
                    break;

                case "demon_monarch_active_17": // Molten Chains
                    Vector2 chainsPos = playerPos + aimDir * 3.0f;
                    result.DamageDealt += DamageArea(chainsPos, radius, damage, knockbackForce: 2f);
                    result.TargetsHit += _hitBuffer.Count;
                    float chainsSlow = rank >= 5 ? 0.70f : 0.45f;
                    for (int i = 0; i < _hitBuffer.Count; i++)
                    {
                        if (_hitBuffer[i] is InvaderActor invader && invader.Alive)
                        {
                            invader.ApplySlow(chainsSlow, 2.5f);
                        }
                    }
                    SpawnVisualBurst(chainsPos, radius * 2f, new Color(0.95f, 0.35f, 0.1f, 0.9f), 0.45f);
                    CueBus.Raise(CueIds.InvaderStrike, chainsPos.x, chainsPos.y);
                    break;

                case "demon_monarch_active_18": // Ember Lance
                    if (ProjectileManager.Instance != null)
                    {
                        ProjectileManager.Instance.FireCustom(
                            Faction.Player,
                            playerPos,
                            aimDir,
                            speed: 12f,
                            radius: 0.35f,
                            damage: damage,
                            lifetime: 1.0f,
                            color: new Color(1f, 0.45f, 0.1f),
                            cueId: CueIds.InvaderStrike,
                            pierce: 6
                        );
                    }
                    Vector2 lancePos = playerPos + aimDir * 3f;
                    result.DamageDealt += DamageArea(lancePos, radius * 0.6f, damage * 0.5f, knockbackForce: 4f);
                    result.TargetsHit += _hitBuffer.Count;
                    SpawnVisualBurst(lancePos, radius * 1.6f, new Color(1f, 0.5f, 0.15f, 0.9f), 0.35f);
                    CueBus.Raise(CueIds.InvaderStrike, playerPos.x, playerPos.y);
                    break;

                case "demon_monarch_active_19": // Searing Brand
                    Vector2 brandPos = playerPos + aimDir * 3.0f;
                    result.DamageDealt += DamageArea(brandPos, radius, damage, knockbackForce: 3f);
                    result.TargetsHit += _hitBuffer.Count;
                    for (int i = 0; i < _hitBuffer.Count; i++)
                    {
                        if (_hitBuffer[i] is InvaderActor invader && invader.Alive)
                        {
                            invader.ApplySlow(0.35f, 3.0f);
                        }
                    }
                    SpawnVisualBurst(brandPos, radius * 2f, new Color(1f, 0.2f, 0.05f, 0.95f), 0.5f);
                    CueBus.Raise(CueIds.InvaderStrike, brandPos.x, brandPos.y);
                    break;

                case "demon_monarch_active_20": // Cataclysm Fire
                    Vector2 cataPos = playerPos + aimDir * 3.5f;
                    result.DamageDealt += DamageArea(cataPos, radius, damage, knockbackForce: 7.5f);
                    result.TargetsHit += _hitBuffer.Count;
                    HazardZoneActor.Spawn(cataPos, radius, duration: 4.5f, damagePerTick: damage * 0.35f, tickInterval: 0.4f, slowPercent: 0.5f, slowDuration: 2.0f, color: new Color(1f, 0.2f, 0.05f, 0.7f), label: "[CATACLYSM_CRATER]");
                    SpawnVisualBurst(cataPos, radius * 2.8f, new Color(1f, 0.25f, 0.05f, 0.95f), 0.8f);
                    CueBus.Raise(CueIds.InvaderStrike, cataPos.x, cataPos.y);
                    break;

                // Branch C — Demonic Powers (Actives 21-30)
                case "demon_monarch_active_21": // Throne of Embers
                    HazardZoneActor.Spawn(playerPos, radius, duration: def.Duration > 0 ? def.Duration : 3.5f, damagePerTick: damage * 0.3f, tickInterval: 0.5f, color: new Color(1f, 0.3f, 0.1f, 0.6f), label: "[THRONE_OF_EMBERS]");
                    result.DamageDealt += DamageArea(playerPos, radius, damage * 0.7f, knockbackForce: 2f);
                    result.TargetsHit += _hitBuffer.Count;
                    _player.SetBarrier(25f + (rank >= 20 ? 25f : 0f), duration: 4.0f);
                    SpawnVisualBurst(playerPos, radius * 2.2f, new Color(1f, 0.4f, 0.1f, 0.9f), 0.5f);
                    CueBus.Raise(CueIds.InvaderStrike, playerPos.x, playerPos.y);
                    break;

                case "demon_monarch_active_22": // Hellstep
                    Vector2 startPos = playerPos;
                    Vector2 destPos = ValidateLandingDestination(startPos, startPos + aimDir * def.Range);
                    _player.Teleport(destPos);
                    result.DamageDealt += DamageArea(startPos, radius, damage);
                    result.DamageDealt += DamageArea(destPos, radius, damage);
                    result.TargetsHit += _hitBuffer.Count;
                    SpawnVisualBurst(startPos, radius * 2f, new Color(1f, 0.35f, 0.1f, 0.85f), 0.3f);
                    SpawnVisualBurst(destPos, radius * 2f, new Color(1f, 0.35f, 0.1f, 0.85f), 0.3f);
                    CueBus.Raise(CueIds.PlayerDash, destPos.x, destPos.y);
                    break;

                case "demon_monarch_active_23": // Ash Doppelganger
                    Vector2 doppelPos = playerPos + aimDir * 2.0f;
                    result.DamageDealt += DamageArea(doppelPos, radius, damage * 0.6f, knockbackForce: 3f);
                    result.TargetsHit += _hitBuffer.Count;
                    UndeadMinionActor.Spawn(UndeadMinionType.Captain, doppelPos, overrideDamage: damage * 0.8f, overrideMaxHp: 120f, lifetime: def.Duration > 0 ? def.Duration : 6.0f);
                    SpawnVisualBurst(doppelPos, radius * 2.2f, new Color(0.8f, 0.2f, 0.1f, 0.95f), 0.5f);
                    CueBus.Raise(CueIds.InvaderStrike, doppelPos.x, doppelPos.y);
                    break;

                case "demon_monarch_active_24": // Bloodforged Blade
                    _player.Health?.TakeDamage(10f); // Demonic blood sacrifice
                    Vector2 bloodSlashPos = playerPos + aimDir * 2.5f;
                    result.DamageDealt += DamageArea(bloodSlashPos, radius, damage * 1.3f, knockbackForce: 4.5f);
                    result.TargetsHit += _hitBuffer.Count;
                    if (result.DamageDealt > 0f)
                    {
                        float lifestealAmount = result.DamageDealt * (0.25f + (rank >= 10 ? 0.15f : 0f));
                        _player.Health?.Heal(lifestealAmount);
                        result.HealingDone += lifestealAmount;
                    }
                    _player.SetBarrier(30f, duration: 4.0f);
                    SpawnVisualBurst(bloodSlashPos, radius * 2.2f, new Color(0.9f, 0.05f, 0.15f, 0.95f), 0.4f);
                    CueBus.Raise(CueIds.InvaderStrike, bloodSlashPos.x, bloodSlashPos.y);
                    break;

                case "demon_monarch_active_25": // Furnace Heart
                    result.DamageDealt += DamageArea(playerPos, radius, damage, knockbackForce: 5.0f);
                    result.TargetsHit += _hitBuffer.Count;
                    _player.SetBarrier(45f, duration: 3.5f);
                    SpawnVisualBurst(playerPos, radius * 2.5f, new Color(1f, 0.45f, 0.05f, 0.95f), 0.6f);
                    CueBus.Raise(CueIds.PlayerDash, playerPos.x, playerPos.y);
                    break;

                case "demon_monarch_active_26": // Terror Roar
                    result.DamageDealt += DamageArea(playerPos, radius, damage, knockbackForce: 8.5f);
                    result.TargetsHit += _hitBuffer.Count;
                    for (int i = 0; i < _hitBuffer.Count; i++)
                    {
                        if (_hitBuffer[i] is InvaderActor invader && invader.Alive)
                        {
                            invader.ApplySlow(0.60f, def.Duration > 0 ? def.Duration : 2.5f);
                        }
                    }
                    SpawnVisualBurst(playerPos, radius * 2.6f, new Color(0.9f, 0.15f, 0.1f, 0.9f), 0.5f);
                    CueBus.Raise(CueIds.InvaderStrike, playerPos.x, playerPos.y);
                    break;

                case "demon_monarch_active_27": // Infernal Grasp
                    Vector2 graspCenter = playerPos + aimDir * 3.5f;
                    result.DamageDealt += DamageArea(graspCenter, radius, damage, knockbackForce: -6.0f); // Inward pull
                    result.TargetsHit += _hitBuffer.Count;
                    for (int i = 0; i < _hitBuffer.Count; i++)
                    {
                        if (_hitBuffer[i] is InvaderActor invader && invader.Alive)
                        {
                            invader.ApplySlow(0.5f, 1.5f);
                        }
                    }
                    SpawnVisualBurst(graspCenter, radius * 2.4f, new Color(0.85f, 0.1f, 0.2f, 0.95f), 0.5f);
                    CueBus.Raise(CueIds.InvaderStrike, graspCenter.x, graspCenter.y);
                    break;

                case "demon_monarch_active_28": // Blasphemous Banner
                    Vector2 bannerPos = playerPos + aimDir * 2.5f;
                    HazardZoneActor.Spawn(bannerPos, radius, duration: def.Duration > 0 ? def.Duration : 5.0f, damagePerTick: damage * 0.35f, tickInterval: 0.5f, color: new Color(0.9f, 0.1f, 0.1f, 0.65f), label: "[BLASPHEMOUS_BANNER]");
                    result.DamageDealt += DamageArea(bannerPos, radius, damage * 0.6f, knockbackForce: 3f);
                    result.TargetsHit += _hitBuffer.Count;
                    _player.SetBarrier(35f, duration: 4.0f);
                    SpawnVisualBurst(bannerPos, radius * 2.4f, new Color(1f, 0.2f, 0.1f, 0.95f), 0.6f);
                    CueBus.Raise(CueIds.InvaderStrike, bannerPos.x, bannerPos.y);
                    break;

                case "demon_monarch_active_29": // Wrath Magnet
                    result.DamageDealt += DamageArea(playerPos, radius, damage, knockbackForce: -8.0f); // Strong inward pull
                    result.TargetsHit += _hitBuffer.Count;
                    SpawnVisualBurst(playerPos, radius * 2.8f, new Color(1f, 0.3f, 0.05f, 0.95f), 0.6f);
                    CueBus.Raise(CueIds.InvaderStrike, playerPos.x, playerPos.y);
                    break;

                case "demon_monarch_active_30": // Demonic Standard
                    Vector2 guardPos = playerPos + aimDir * 2.0f;
                    result.DamageDealt += DamageArea(playerPos, radius, damage, knockbackForce: 7.0f);
                    result.TargetsHit += _hitBuffer.Count;
                    HazardZoneActor.Spawn(playerPos, radius, duration: 4.0f, damagePerTick: damage * 0.3f, tickInterval: 0.5f, color: new Color(1f, 0.15f, 0.05f, 0.65f), label: "[DEMONIC_STANDARD_AURA]");
                    UndeadMinionActor.Spawn(UndeadMinionType.Captain, guardPos, overrideDamage: damage * 0.9f, overrideMaxHp: 160f, lifetime: def.Duration > 0 ? def.Duration : 6.0f);
                    if (rank >= 20)
                    {
                        UndeadMinionActor.Spawn(UndeadMinionType.Guardian, playerPos - aimDir * 1.5f, overrideDamage: damage * 0.7f, overrideMaxHp: 200f, lifetime: def.Duration > 0 ? def.Duration : 6.0f);
                    }
                    SpawnVisualBurst(playerPos, radius * 3.0f, new Color(1f, 0.2f, 0.05f, 0.95f), 0.8f);
                    CueBus.Raise(CueIds.InvaderStrike, playerPos.x, playerPos.y);
                    break;

                // Branch D — Devastating Physical Attacks (Actives 31-40)
                case "demon_monarch_active_31": // Brutal Smash
                    result.DamageDealt += DamageArea(playerPos, radius, damage, knockbackForce: 4.5f);
                    result.TargetsHit += _hitBuffer.Count;
                    SpawnVisualBurst(playerPos, radius * 2.2f, new Color(0.9f, 0.2f, 0.05f, 0.95f), 0.3f);
                    CueBus.Raise(CueIds.InvaderStrike, playerPos.x, playerPos.y);
                    break;

                case "demon_monarch_active_32": // Titanic Swing
                    Vector2 swingPos = playerPos + aimDir * 2.5f;
                    result.DamageDealt += DamageArea(swingPos, radius, damage, knockbackForce: 6.5f);
                    result.TargetsHit += _hitBuffer.Count;
                    SpawnVisualBurst(swingPos, radius * 2.4f, new Color(1f, 0.3f, 0.1f, 0.95f), 0.4f);
                    CueBus.Raise(CueIds.InvaderStrike, swingPos.x, swingPos.y);
                    break;

                case "demon_monarch_active_33": // Rupture Stomp
                    result.DamageDealt += DamageArea(playerPos, radius, damage, knockbackForce: 5.5f);
                    result.TargetsHit += _hitBuffer.Count;
                    HazardZoneActor.Spawn(playerPos, radius, duration: def.Duration > 0 ? def.Duration : 2.0f, damagePerTick: damage * 0.25f, tickInterval: 0.5f, slowPercent: 0.4f, slowDuration: 1.5f, color: new Color(0.85f, 0.3f, 0.1f, 0.55f), label: "[RUPTURE_GRAVEL]");
                    SpawnVisualBurst(playerPos, radius * 2.4f, new Color(0.95f, 0.25f, 0.05f, 0.95f), 0.4f);
                    CueBus.Raise(CueIds.InvaderStrike, playerPos.x, playerPos.y);
                    break;

                case "demon_monarch_active_34": // Colossal Crush
                    Vector2 crushPos = playerPos + aimDir * 2.2f;
                    result.DamageDealt += DamageArea(crushPos, radius, damage * 1.25f, knockbackForce: 7.0f);
                    result.TargetsHit += _hitBuffer.Count;
                    for (int i = 0; i < _hitBuffer.Count; i++)
                    {
                        if (_hitBuffer[i] is InvaderActor invader && invader.Alive)
                        {
                            invader.ApplySlow(0.5f, 2.0f);
                        }
                    }
                    SpawnVisualBurst(crushPos, radius * 2.2f, new Color(1f, 0.2f, 0.05f, 0.95f), 0.45f);
                    CueBus.Raise(CueIds.InvaderStrike, crushPos.x, crushPos.y);
                    break;

                case "demon_monarch_active_35": // Iron Sledge
                    Vector2 sledgeStart = playerPos;
                    Vector2 sledgeDest = ValidateLandingDestination(sledgeStart, sledgeStart + aimDir * def.Range);
                    _player.Teleport(sledgeDest);
                    result.DamageDealt += DamageArea(sledgeDest, radius, damage, knockbackForce: 6.0f);
                    result.TargetsHit += _hitBuffer.Count;
                    _player.SetBarrier(40f, duration: 4.0f);
                    SpawnVisualBurst(sledgeStart, radius * 1.8f, new Color(0.8f, 0.3f, 0.1f, 0.8f), 0.3f);
                    SpawnVisualBurst(sledgeDest, radius * 2.2f, new Color(1f, 0.4f, 0.1f, 0.95f), 0.4f);
                    CueBus.Raise(CueIds.PlayerDash, sledgeDest.x, sledgeDest.y);
                    break;

                case "demon_monarch_active_36": // Devastating Quake
                    result.DamageDealt += DamageArea(playerPos, radius, damage, knockbackForce: 8.0f);
                    result.TargetsHit += _hitBuffer.Count;
                    for (int i = 0; i < _hitBuffer.Count; i++)
                    {
                        if (_hitBuffer[i] is InvaderActor invader && invader.Alive)
                        {
                            invader.ApplySlow(0.7f, 2.5f);
                        }
                    }
                    SpawnVisualBurst(playerPos, radius * 2.8f, new Color(1f, 0.35f, 0.05f, 0.95f), 0.6f);
                    CueBus.Raise(CueIds.InvaderStrike, playerPos.x, playerPos.y);
                    break;

                case "demon_monarch_active_37": // Bone Pulverizer
                    Vector2 bonePos = playerPos + aimDir * 2.5f;
                    result.DamageDealt += DamageArea(bonePos, radius, damage * 1.3f, knockbackForce: 6.5f);
                    result.TargetsHit += _hitBuffer.Count;
                    SpawnVisualBurst(bonePos, radius * 2.2f, new Color(0.95f, 0.2f, 0.15f, 0.95f), 0.4f);
                    CueBus.Raise(CueIds.InvaderStrike, bonePos.x, bonePos.y);
                    break;

                case "demon_monarch_active_38": // Brutal Cleaver
                    Vector2 cleaverPos = playerPos + aimDir * 2.5f;
                    result.DamageDealt += DamageArea(cleaverPos, radius, damage, knockbackForce: 5.5f);
                    result.TargetsHit += _hitBuffer.Count;
                    if (result.DamageDealt > 0f)
                    {
                        float heal = result.DamageDealt * (0.15f + (rank >= 20 ? 0.15f : 0f));
                        _player.Health?.Heal(heal);
                        result.HealingDone += heal;
                    }
                    SpawnVisualBurst(cleaverPos, radius * 2.5f, new Color(1f, 0.15f, 0.1f, 0.95f), 0.5f);
                    CueBus.Raise(CueIds.InvaderStrike, cleaverPos.x, cleaverPos.y);
                    break;

                case "demon_monarch_active_39": // Sunder the Earth
                    Vector2 sunderEarthPos = playerPos + aimDir * 3.5f;
                    result.DamageDealt += DamageArea(sunderEarthPos, radius, damage, knockbackForce: 7.5f);
                    result.TargetsHit += _hitBuffer.Count;
                    HazardZoneActor.Spawn(sunderEarthPos, radius, duration: def.Duration > 0 ? def.Duration : 3.0f, damagePerTick: damage * 0.35f, tickInterval: 0.5f, slowPercent: 0.45f, slowDuration: 1.5f, color: new Color(1f, 0.25f, 0.05f, 0.65f), label: "[SUNDERED_EARTH]");
                    SpawnVisualBurst(sunderEarthPos, radius * 2.8f, new Color(1f, 0.3f, 0.05f, 0.95f), 0.7f);
                    CueBus.Raise(CueIds.InvaderStrike, sunderEarthPos.x, sunderEarthPos.y);
                    break;

                case "demon_monarch_active_40": // Wrathful Guillotine
                    Vector2 guillotinePos = playerPos + aimDir * 3.5f;
                    result.DamageDealt += DamageArea(guillotinePos, radius, damage * 1.5f, knockbackForce: 9.0f);
                    result.TargetsHit += _hitBuffer.Count;
                    for (int i = 0; i < _hitBuffer.Count; i++)
                    {
                        if (_hitBuffer[i] is InvaderActor invader && invader.Alive)
                        {
                            invader.ApplySlow(0.75f, 2.5f);
                        }
                    }
                    _player.SetBarrier(50f, duration: 4.0f);
                    SpawnVisualBurst(guillotinePos, radius * 3.2f, new Color(1f, 0.2f, 0.05f, 0.95f), 0.8f);
                    CueBus.Raise(CueIds.InvaderStrike, guillotinePos.x, guillotinePos.y);
                    break;

                // Branch E — Infernal Ascension (Actives 41-50)
                case "demon_monarch_active_41": // Crown of Flames
                    result.DamageDealt += DamageArea(playerPos, radius, damage * 0.8f, knockbackForce: 3.0f);
                    result.TargetsHit += _hitBuffer.Count;
                    _player.SetBarrier(30f + (rank >= 20 ? 20f : 0f), duration: def.Duration > 0 ? def.Duration : 4.0f);
                    SpawnVisualBurst(playerPos, radius * 2.2f, new Color(1f, 0.45f, 0.1f, 0.95f), 0.5f);
                    CueBus.Raise(CueIds.InvaderStrike, playerPos.x, playerPos.y);
                    break;

                case "demon_monarch_active_42": // Wing Buffet
                    result.DamageDealt += DamageArea(playerPos, radius, damage, knockbackForce: 7.5f);
                    result.TargetsHit += _hitBuffer.Count;
                    SpawnVisualBurst(playerPos, radius * 2.5f, new Color(1f, 0.35f, 0.1f, 0.9f), 0.4f);
                    CueBus.Raise(CueIds.PlayerDash, playerPos.x, playerPos.y);
                    break;

                case "demon_monarch_active_43": // Demonic Ascendance
                    result.DamageDealt += DamageArea(playerPos, radius, damage * 1.1f, knockbackForce: 6.0f);
                    result.TargetsHit += _hitBuffer.Count;
                    _player.SetBarrier(45f, duration: 4.0f);
                    SpawnVisualBurst(playerPos, radius * 2.6f, new Color(1f, 0.4f, 0.05f, 0.95f), 0.5f);
                    CueBus.Raise(CueIds.InvaderStrike, playerPos.x, playerPos.y);
                    break;

                case "demon_monarch_active_44": // Molten Halo
                    Vector2 haloPos = playerPos + aimDir * 3.5f;
                    result.DamageDealt += DamageArea(haloPos, radius, damage, knockbackForce: 4.5f);
                    result.TargetsHit += _hitBuffer.Count;
                    SpawnVisualBurst(haloPos, radius * 2.2f, new Color(1f, 0.5f, 0.1f, 0.95f), 0.4f);
                    CueBus.Raise(CueIds.BoltFire, haloPos.x, haloPos.y);
                    break;

                case "demon_monarch_active_45": // Sovereign Glare
                    Vector2 glarePos = playerPos + aimDir * 3.0f;
                    result.DamageDealt += DamageArea(glarePos, radius, damage, knockbackForce: 3.5f);
                    result.TargetsHit += _hitBuffer.Count;
                    for (int i = 0; i < _hitBuffer.Count; i++)
                    {
                        if (_hitBuffer[i] is InvaderActor invader && invader.Alive)
                        {
                            invader.ApplySlow(0.6f, def.Duration > 0 ? def.Duration : 2.0f);
                        }
                    }
                    SpawnVisualBurst(glarePos, radius * 2.4f, new Color(0.95f, 0.1f, 0.15f, 0.95f), 0.5f);
                    CueBus.Raise(CueIds.InvaderStrike, glarePos.x, glarePos.y);
                    break;

                case "demon_monarch_active_46": // Overlord's Wrath
                    Vector2 wrathPos = playerPos + aimDir * 3.0f;
                    result.DamageDealt += DamageArea(wrathPos, radius, damage * 1.25f, knockbackForce: 6.5f);
                    result.TargetsHit += _hitBuffer.Count;
                    SpawnVisualBurst(wrathPos, radius * 2.6f, new Color(1f, 0.25f, 0.05f, 0.95f), 0.6f);
                    CueBus.Raise(CueIds.InvaderStrike, wrathPos.x, wrathPos.y);
                    break;

                case "demon_monarch_active_47": // Hellfire Nova
                    result.DamageDealt += DamageArea(playerPos, radius, damage * 1.3f, knockbackForce: 8.5f);
                    result.TargetsHit += _hitBuffer.Count;
                    for (int i = 0; i < _hitBuffer.Count; i++)
                    {
                        if (_hitBuffer[i] is InvaderActor invader && invader.Alive)
                        {
                            invader.ApplySlow(0.7f, 1.5f);
                        }
                    }
                    SpawnVisualBurst(playerPos, radius * 2.8f, new Color(1f, 0.35f, 0.1f, 0.95f), 0.7f);
                    CueBus.Raise(CueIds.InvaderStrike, playerPos.x, playerPos.y);
                    break;

                case "demon_monarch_active_48": // Pillar of Damnation
                    Vector2 damnpillarPos = playerPos + aimDir * 3.5f;
                    result.DamageDealt += DamageArea(damnpillarPos, radius, damage * 1.35f, knockbackForce: 7.0f);
                    result.TargetsHit += _hitBuffer.Count;
                    HazardZoneActor.Spawn(damnpillarPos, radius, duration: def.Duration > 0 ? def.Duration : 3.5f, damagePerTick: damage * 0.35f, tickInterval: 0.5f, color: new Color(0.9f, 0.15f, 0.05f, 0.65f), label: "[PILLAR_OF_DAMNATION]");
                    SpawnVisualBurst(damnpillarPos, radius * 2.8f, new Color(1f, 0.2f, 0.1f, 0.95f), 0.7f);
                    CueBus.Raise(CueIds.InvaderStrike, damnpillarPos.x, damnpillarPos.y);
                    break;

                case "demon_monarch_active_49": // Crown of the Abyss
                    result.DamageDealt += DamageArea(playerPos, radius, damage * 1.4f, knockbackForce: 8.0f);
                    result.TargetsHit += _hitBuffer.Count;
                    _player.SetBarrier(60f, duration: def.Duration > 0 ? def.Duration : 4.5f);
                    SpawnVisualBurst(playerPos, radius * 3.0f, new Color(0.85f, 0.1f, 0.2f, 0.95f), 0.8f);
                    CueBus.Raise(CueIds.InvaderStrike, playerPos.x, playerPos.y);
                    break;

                case "demon_monarch_active_50": // Infernal Ascension
                    result.DamageDealt += DamageArea(playerPos, radius, damage * 1.6f, knockbackForce: 9.5f);
                    result.TargetsHit += _hitBuffer.Count;
                    _player.SetBarrier(80f, duration: 6.0f);
                    HazardZoneActor.Spawn(playerPos, radius, duration: 5.0f, damagePerTick: damage * 0.4f, tickInterval: 0.5f, color: new Color(1f, 0.25f, 0.05f, 0.7f), label: "[INFERNAL_ASCENSION_AURA]");
                    Vector2 dragonOffset = playerPos + aimDir * 2.5f;
                    UndeadMinionActor.Spawn(UndeadMinionType.Dragon, dragonOffset, overrideDamage: damage * 1.2f, overrideMaxHp: 280f, lifetime: def.Duration > 0 ? def.Duration : 6.0f);
                    SpawnVisualBurst(playerPos, radius * 3.5f, new Color(1f, 0.2f, 0.05f, 0.98f), 1.0f);
                    CueBus.Raise(CueIds.InvaderStrike, playerPos.x, playerPos.y);
                    break;

                // Milestone Ultimates
                case "demon_monarch_ultimate_50": // Hellblade Judgment
                    Vector2 bladeImpact = playerPos + aimDir * 3f;
                    result.DamageDealt += DamageArea(bladeImpact, radius, damage, knockbackForce: 5f);
                    result.TargetsHit += _hitBuffer.Count;
                    SpawnVisualBurst(bladeImpact, radius * 2f, new Color(1f, 0.25f, 0.05f, 0.95f), 0.6f);
                    CueBus.Raise(CueIds.InvaderStrike, bladeImpact.x, bladeImpact.y);
                    break;

                case "demon_monarch_ultimate_100": // Demon King's Awakening
                    _player.SetBarrier(140f, duration: def.Duration);
                    result.BarrierGranted += 140f;
                    result.DamageDealt += DamageArea(playerPos, radius, damage, knockbackForce: 5.5f);
                    result.TargetsHit += _hitBuffer.Count;
                    SpawnVisualBurst(playerPos, radius * 2.4f, new Color(1f, 0.2f, 0.1f, 0.95f), 0.7f);
                    CueBus.Raise(CueIds.BossCleave, playerPos.x, playerPos.y);
                    break;

                case "demon_monarch_ultimate_150": // Rain of a Thousand Blades
                    Vector2 bladeRainPos = playerPos + aimDir * 3.5f;
                    result.DamageDealt += DamageArea(bladeRainPos, radius, damage, knockbackForce: 6f);
                    result.TargetsHit += _hitBuffer.Count;
                    HazardZoneActor.Spawn(bladeRainPos, radius, duration: 4.5f, damagePerTick: damage * 0.35f, tickInterval: 0.4f, slowPercent: 0.4f, slowDuration: 1.5f, color: new Color(1f, 0.3f, 0.1f, 0.65f), label: "[BLADE_RAIN]");
                    SpawnVisualBurst(bladeRainPos, radius * 2.5f, new Color(1f, 0.35f, 0.1f, 0.95f), 0.75f);
                    CueBus.Raise(CueIds.InvaderStrike, bladeRainPos.x, bladeRainPos.y);
                    break;

                case "demon_monarch_ultimate_200": // Infernal Apocalypse
                    result.DamageDealt += DamageArea(playerPos, radius, damage, knockbackForce: 7.5f);
                    result.TargetsHit += _hitBuffer.Count;
                    HazardZoneActor.Spawn(playerPos, radius, duration: 5.0f, damagePerTick: damage * 0.4f, tickInterval: 0.45f, slowPercent: 0.5f, slowDuration: 2.0f, color: new Color(1f, 0.2f, 0.05f, 0.7f), label: "[APOCALYPSE_LAVA]");
                    SpawnVisualBurst(playerPos, radius * 2.8f, new Color(1f, 0.25f, 0.05f, 0.95f), 0.8f);
                    CueBus.Raise(CueIds.InvaderStrike, playerPos.x, playerPos.y);
                    break;

                case "demon_monarch_ultimate_250": // Throne of Eternal Flame
                    _player.SetBarrier(250f, duration: def.Duration);
                    result.BarrierGranted += 250f;
                    result.DamageDealt += DamageArea(playerPos, radius, damage, knockbackForce: 8.5f);
                    result.TargetsHit += _hitBuffer.Count;
                    HazardZoneActor.Spawn(playerPos, radius, duration: 6.0f, damagePerTick: damage * 0.45f, tickInterval: 0.4f, slowPercent: 0.55f, slowDuration: 2.5f, color: new Color(1f, 0.15f, 0.05f, 0.75f), label: "[ETERNAL_FLAME]");
                    SpawnVisualBurst(playerPos, radius * 3.2f, new Color(1f, 0.35f, 0.1f, 0.95f), 0.9f);
                    CueBus.Raise(CueIds.BossCleave, playerPos.x, playerPos.y);
                    break;

                // -------------------------------------------------------------
                // VAMPIRE QUEEN
                // -------------------------------------------------------------
                // Branch A — Blood Sorcery (Actives 01-10)
                case "vampire_queen_active_01": // Sanguine Burst
                    result.DamageDealt += DamageArea(playerPos, radius, damage);
                    result.TargetsHit += _hitBuffer.Count;
                    if (result.DamageDealt > 0f)
                    {
                        float lifesteal = result.DamageDealt * (0.35f + (rank >= 10 ? 0.15f : 0f));
                        _player.Health.Heal(lifesteal);
                        result.HealingDone += lifesteal;
                        CueBus.Raise(CueIds.ClericHeal, playerPos.x, playerPos.y, lifesteal);
                    }
                    SpawnVisualBurst(playerPos, radius * 2f, new Color(0.9f, 0.1f, 0.25f, 0.85f), 0.35f);
                    CueBus.Raise(CueIds.InvaderStrike, playerPos.x, playerPos.y);
                    break;

                case "vampire_queen_active_02": // Crimson Lance
                    Vector2 vqLancePos = playerPos + aimDir * 3.5f;
                    result.DamageDealt += DamageArea(vqLancePos, radius, damage, knockbackForce: 4.5f);
                    result.TargetsHit += _hitBuffer.Count;
                    if (result.DamageDealt > 0f)
                    {
                        float lanceHeal = result.DamageDealt * 0.25f;
                        _player.Health.Heal(lanceHeal);
                        result.HealingDone += lanceHeal;
                    }
                    SpawnVisualBurst(vqLancePos, radius * 2.2f, new Color(0.95f, 0.05f, 0.2f, 0.95f), 0.35f);
                    CueBus.Raise(CueIds.BoltFire, vqLancePos.x, vqLancePos.y);
                    break;

                case "vampire_queen_active_03": // Blood Well
                    Vector2 wellPos = playerPos + aimDir * 3.0f;
                    HazardZoneActor.Spawn(wellPos, radius, duration: def.Duration > 0 ? def.Duration : 3.5f, damagePerTick: damage * 0.3f, tickInterval: 0.5f, color: new Color(0.85f, 0.05f, 0.15f, 0.65f), label: "[BLOOD_WELL]");
                    result.DamageDealt += DamageArea(wellPos, radius, damage * 0.6f);
                    result.TargetsHit += _hitBuffer.Count;
                    if (result.DamageDealt > 0f)
                    {
                        float wellHeal = result.DamageDealt * 0.3f;
                        _player.Health.Heal(wellHeal);
                        result.HealingDone += wellHeal;
                    }
                    SpawnVisualBurst(wellPos, radius * 2.2f, new Color(0.9f, 0.1f, 0.2f, 0.9f), 0.45f);
                    CueBus.Raise(CueIds.InvaderStrike, wellPos.x, wellPos.y);
                    break;

                case "vampire_queen_active_04": // Transfusion
                    result.DamageDealt += DamageArea(playerPos, radius, damage);
                    result.TargetsHit += _hitBuffer.Count;
                    if (result.DamageDealt > 0f)
                    {
                        float transHeal = result.DamageDealt * 0.45f;
                        _player.Health.Heal(transHeal);
                        result.HealingDone += transHeal;
                        CueBus.Raise(CueIds.ClericHeal, playerPos.x, playerPos.y, transHeal);
                    }
                    SpawnVisualBurst(playerPos, radius * 2.2f, new Color(1f, 0.1f, 0.3f, 0.9f), 0.4f);
                    break;

                case "vampire_queen_active_05": // Sanguine Ward
                    result.DamageDealt += DamageArea(playerPos, radius, damage * 0.7f, knockbackForce: 3.5f);
                    result.TargetsHit += _hitBuffer.Count;
                    _player.SetBarrier(35f + (rank >= 5 ? 15f : 0f), duration: def.Duration > 0 ? def.Duration : 4.0f);
                    SpawnVisualBurst(playerPos, radius * 2.2f, new Color(0.9f, 0.15f, 0.25f, 0.95f), 0.5f);
                    CueBus.Raise(CueIds.InvaderStrike, playerPos.x, playerPos.y);
                    break;

                case "vampire_queen_active_06": // Blood Boil
                    result.DamageDealt += DamageArea(playerPos, radius, damage, knockbackForce: 5.0f);
                    result.TargetsHit += _hitBuffer.Count;
                    for (int i = 0; i < _hitBuffer.Count; i++)
                    {
                        if (_hitBuffer[i] is InvaderActor invader && invader.Alive)
                        {
                            invader.ApplySlow(0.5f, 2.0f);
                        }
                    }
                    if (result.DamageDealt > 0f)
                    {
                        float boilHeal = result.DamageDealt * 0.3f;
                        _player.Health.Heal(boilHeal);
                        result.HealingDone += boilHeal;
                    }
                    SpawnVisualBurst(playerPos, radius * 2.5f, new Color(1f, 0.05f, 0.15f, 0.95f), 0.5f);
                    CueBus.Raise(CueIds.InvaderStrike, playerPos.x, playerPos.y);
                    break;

                case "vampire_queen_active_07": // Exsanguinate
                    Vector2 exsanPos = playerPos + aimDir * 2.5f;
                    result.DamageDealt += DamageArea(exsanPos, radius, damage * 1.3f, knockbackForce: 4.5f);
                    result.TargetsHit += _hitBuffer.Count;
                    if (result.DamageDealt > 0f)
                    {
                        float exsanHeal = result.DamageDealt * 0.6f;
                        _player.Health.Heal(exsanHeal);
                        result.HealingDone += exsanHeal;
                        CueBus.Raise(CueIds.ClericHeal, playerPos.x, playerPos.y, exsanHeal);
                    }
                    SpawnVisualBurst(exsanPos, radius * 2.4f, new Color(0.85f, 0.05f, 0.2f, 0.95f), 0.45f);
                    CueBus.Raise(CueIds.InvaderStrike, exsanPos.x, exsanPos.y);
                    break;

                case "vampire_queen_active_08": // Crimson Tempest
                    Vector2 tempestPos = playerPos + aimDir * 3.0f;
                    result.DamageDealt += DamageArea(tempestPos, radius, damage, knockbackForce: -4.0f); // Inward pull
                    result.TargetsHit += _hitBuffer.Count;
                    SpawnVisualBurst(tempestPos, radius * 2.6f, new Color(0.95f, 0.1f, 0.25f, 0.9f), 0.6f);
                    CueBus.Raise(CueIds.InvaderStrike, tempestPos.x, tempestPos.y);
                    break;

                case "vampire_queen_active_09": // Haemomancy
                    result.DamageDealt += DamageArea(playerPos, radius, damage * 1.3f, knockbackForce: 6.0f);
                    result.TargetsHit += _hitBuffer.Count;
                    HazardZoneActor.Spawn(playerPos, radius, duration: 3.0f, damagePerTick: damage * 0.3f, tickInterval: 0.5f, color: new Color(0.9f, 0.05f, 0.15f, 0.6f), label: "[HAEMOMANCY_POOL]");
                    SpawnVisualBurst(playerPos, radius * 2.8f, new Color(1f, 0.05f, 0.2f, 0.95f), 0.6f);
                    CueBus.Raise(CueIds.InvaderStrike, playerPos.x, playerPos.y);
                    break;

                case "vampire_queen_active_10": // Heartstopper
                    Vector2 heartPos = playerPos + aimDir * 3.0f;
                    result.DamageDealt += DamageArea(heartPos, radius, damage * 1.5f, knockbackForce: 8.0f);
                    result.TargetsHit += _hitBuffer.Count;
                    if (result.DamageDealt > 0f)
                    {
                        float heartHeal = result.DamageDealt * 0.5f;
                        _player.Health.Heal(heartHeal);
                        result.HealingDone += heartHeal;
                        CueBus.Raise(CueIds.ClericHeal, playerPos.x, playerPos.y, heartHeal);
                    }
                    SpawnVisualBurst(heartPos, radius * 3.0f, new Color(1f, 0.02f, 0.1f, 0.98f), 0.7f);
                    CueBus.Raise(CueIds.InvaderStrike, heartPos.x, heartPos.y);
                    break;

                // Branch B — Precision & Rapier (Actives 11-20)
                case "vampire_queen_active_11": // Rapier Thrust
                    Vector2 vqThrustPos = playerPos + aimDir * 2.2f;
                    result.DamageDealt += DamageArea(vqThrustPos, radius, damage, knockbackForce: 4.0f);
                    result.TargetsHit += _hitBuffer.Count;
                    if (result.DamageDealt > 0f)
                    {
                        float thrustHeal = result.DamageDealt * (0.20f + (rank >= 20 ? 0.15f : 0f));
                        _player.Health.Heal(thrustHeal);
                        result.HealingDone += thrustHeal;
                    }
                    SpawnVisualBurst(vqThrustPos, radius * 2.0f, new Color(0.95f, 0.1f, 0.2f, 0.95f), 0.3f);
                    CueBus.Raise(CueIds.InvaderStrike, vqThrustPos.x, vqThrustPos.y);
                    break;

                case "vampire_queen_active_12": // Crimson Needle
                case "vampire_queen_active_needle":
                    int needleCount = 1 + (rank >= 5 ? 1 : 0) + (rank >= 20 ? 3 : 0);
                    for (int n = 0; n < needleCount; n++)
                    {
                        float spreadAngle = (n - (needleCount - 1) * 0.5f) * 12f * Mathf.Deg2Rad;
                        Vector2 nDir = new Vector2(
                            aimDir.x * Mathf.Cos(spreadAngle) - aimDir.y * Mathf.Sin(spreadAngle),
                            aimDir.x * Mathf.Sin(spreadAngle) + aimDir.y * Mathf.Cos(spreadAngle)
                        );
                        if (ProjectileManager.Instance != null)
                        {
                            ProjectileManager.Instance.FireCustom(
                                Faction.Player,
                                playerPos,
                                nDir,
                                speed: 16f,
                                radius: 0.18f,
                                damage: damage,
                                lifetime: 0.9f,
                                color: new Color(0.95f, 0.15f, 0.3f),
                                cueId: CueIds.BoltFire,
                                pierce: 2
                            );
                        }
                    }
                    result.TargetsHit += needleCount;
                    CueBus.Raise(CueIds.BoltFire, playerPos.x, playerPos.y);
                    break;

                case "vampire_queen_active_13": // Riposte
                    Vector2 ripostePos = playerPos + aimDir * 2.0f;
                    result.DamageDealt += DamageArea(ripostePos, radius, damage * 1.2f, knockbackForce: 5.5f);
                    result.TargetsHit += _hitBuffer.Count;
                    _player.SetBarrier(35f, duration: 3.5f);
                    SpawnVisualBurst(ripostePos, radius * 2.2f, new Color(1f, 0.2f, 0.3f, 0.95f), 0.35f);
                    CueBus.Raise(CueIds.InvaderStrike, ripostePos.x, ripostePos.y);
                    break;

                case "vampire_queen_active_14": // Fencing Lunge
                    Vector2 vqLungeStart = playerPos;
                    Vector2 vqLungeDest = ValidateLandingDestination(vqLungeStart, vqLungeStart + aimDir * def.Range);
                    _player.Teleport(vqLungeDest);
                    result.DamageDealt += DamageArea(vqLungeDest, radius, damage, knockbackForce: 5.0f);
                    result.TargetsHit += _hitBuffer.Count;
                    SpawnVisualBurst(vqLungeStart, radius * 1.8f, new Color(0.9f, 0.1f, 0.25f, 0.8f), 0.25f);
                    SpawnVisualBurst(vqLungeDest, radius * 2.2f, new Color(1f, 0.2f, 0.35f, 0.95f), 0.35f);
                    CueBus.Raise(CueIds.PlayerDash, vqLungeDest.x, vqLungeDest.y);
                    break;

                case "vampire_queen_active_15": // Needle Rain
                    Vector2 rainPos = playerPos + aimDir * 3.5f;
                    result.DamageDealt += DamageArea(rainPos, radius, damage, knockbackForce: 3.5f);
                    result.TargetsHit += _hitBuffer.Count;
                    HazardZoneActor.Spawn(rainPos, radius, duration: def.Duration > 0 ? def.Duration : 3.0f, damagePerTick: damage * 0.3f, tickInterval: 0.4f, slowPercent: 0.35f, slowDuration: 1.5f, color: new Color(0.95f, 0.1f, 0.2f, 0.6f), label: "[NEEDLE_RAIN]");
                    SpawnVisualBurst(rainPos, radius * 2.5f, new Color(1f, 0.15f, 0.3f, 0.95f), 0.5f);
                    CueBus.Raise(CueIds.BoltFire, rainPos.x, rainPos.y);
                    break;

                case "vampire_queen_active_16": // Arterial Flurry
                    Vector2 flurryPos = playerPos + aimDir * 2.5f;
                    result.DamageDealt += DamageArea(flurryPos, radius, damage * 1.25f, knockbackForce: 4.5f);
                    result.TargetsHit += _hitBuffer.Count;
                    if (result.DamageDealt > 0f)
                    {
                        float flurryHeal = result.DamageDealt * 0.30f;
                        _player.Health.Heal(flurryHeal);
                        result.HealingDone += flurryHeal;
                        CueBus.Raise(CueIds.ClericHeal, playerPos.x, playerPos.y, flurryHeal);
                    }
                    SpawnVisualBurst(flurryPos, radius * 2.4f, new Color(0.95f, 0.05f, 0.15f, 0.95f), 0.45f);
                    CueBus.Raise(CueIds.InvaderStrike, flurryPos.x, flurryPos.y);
                    break;

                case "vampire_queen_active_17": // Crystal Volley
                    Vector2 volleyPos = playerPos + aimDir * 3.5f;
                    result.DamageDealt += DamageArea(volleyPos, radius, damage, knockbackForce: 4.0f);
                    result.TargetsHit += _hitBuffer.Count;
                    SpawnVisualBurst(volleyPos, radius * 2.4f, new Color(1f, 0.2f, 0.4f, 0.95f), 0.4f);
                    CueBus.Raise(CueIds.BoltFire, volleyPos.x, volleyPos.y);
                    break;

                case "vampire_queen_active_18": // Peerless Feint
                    Vector2 feintStart = playerPos;
                    Vector2 feintDest = ValidateLandingDestination(feintStart, feintStart + aimDir * def.Range);
                    _player.Teleport(feintDest);
                    result.DamageDealt += DamageArea(feintDest, radius, damage * 1.35f, knockbackForce: 6.0f);
                    result.TargetsHit += _hitBuffer.Count;
                    for (int i = 0; i < _hitBuffer.Count; i++)
                    {
                        if (_hitBuffer[i] is InvaderActor invader && invader.Alive)
                        {
                            invader.ApplySlow(0.7f, 1.5f);
                        }
                    }
                    SpawnVisualBurst(feintStart, radius * 2f, new Color(0.85f, 0.05f, 0.2f, 0.85f), 0.3f);
                    SpawnVisualBurst(feintDest, radius * 2.5f, new Color(1f, 0.15f, 0.3f, 0.95f), 0.4f);
                    CueBus.Raise(CueIds.PlayerDash, feintDest.x, feintDest.y);
                    break;

                case "vampire_queen_active_19": // Grand Battement
                    Vector2 battementPos = playerPos + aimDir * 3.5f;
                    result.DamageDealt += DamageArea(battementPos, radius, damage * 1.3f, knockbackForce: 6.5f);
                    result.TargetsHit += _hitBuffer.Count;
                    for (int i = 0; i < _hitBuffer.Count; i++)
                    {
                        if (_hitBuffer[i] is InvaderActor invader && invader.Alive)
                        {
                            invader.ApplySlow(0.6f, 2.0f);
                        }
                    }
                    SpawnVisualBurst(battementPos, radius * 2.8f, new Color(1f, 0.1f, 0.25f, 0.95f), 0.5f);
                    CueBus.Raise(CueIds.InvaderStrike, battementPos.x, battementPos.y);
                    break;

                case "vampire_queen_active_20": // Aristocratic Execution
                    result.DamageDealt += DamageArea(playerPos, radius, damage * 1.5f, knockbackForce: 8.5f);
                    result.TargetsHit += _hitBuffer.Count;
                    if (result.DamageDealt > 0f)
                    {
                        float execHeal = result.DamageDealt * 0.45f;
                        _player.Health.Heal(execHeal);
                        result.HealingDone += execHeal;
                        CueBus.Raise(CueIds.ClericHeal, playerPos.x, playerPos.y, execHeal);
                    }
                    _player.SetBarrier(50f, duration: 4.0f);
                    SpawnVisualBurst(playerPos, radius * 3.2f, new Color(1f, 0.05f, 0.2f, 0.98f), 0.7f);
                    CueBus.Raise(CueIds.InvaderStrike, playerPos.x, playerPos.y);
                    break;

                case "vampire_queen_active_21": // Bat Swarm
                    Vector2 vqSwarmPos = playerPos + aimDir * Math.Min(3.5f, def.Range > 0 ? def.Range : 3.5f);
                    result.DamageDealt += DamageArea(vqSwarmPos, radius, damage, slowPercent: 0.40f, slowDuration: 2.5f);
                    result.TargetsHit += _hitBuffer.Count;
                    if (result.DamageDealt > 0f)
                    {
                        float vqSwarmHeal = result.DamageDealt * (rank >= 20 ? 1.0f : 0.35f);
                        _player.Health.Heal(vqSwarmHeal);
                        result.HealingDone += vqSwarmHeal;
                        CueBus.Raise(CueIds.ClericHeal, playerPos.x, playerPos.y, vqSwarmHeal);
                    }
                    SpawnVisualBurst(vqSwarmPos, radius * 2.2f, new Color(0.8f, 0.05f, 0.25f, 0.85f), 0.4f);
                    CueBus.Raise(CueIds.InvaderStrike, vqSwarmPos.x, vqSwarmPos.y);
                    break;

                case "vampire_queen_active_22": // Blood Thrall
                    Vector2 vqThrallPos = playerPos + aimDir * Math.Min(3.0f, def.Range > 0 ? def.Range : 3.0f);
                    UndeadMinionActor.Spawn(UndeadMinionType.Hound, vqThrallPos, overrideDamage: damage * 0.9f, overrideMaxHp: 80f + (rank >= 5 ? 24f : 0f), lifetime: def.Duration > 0 ? def.Duration : 6.0f);
                    if (rank >= 20)
                    {
                        UndeadMinionActor.Spawn(UndeadMinionType.Hound, playerPos - aimDir * 1.5f, overrideDamage: damage * 0.9f, overrideMaxHp: 80f, lifetime: def.Duration > 0 ? def.Duration : 6.0f);
                    }
                    result.DamageDealt += DamageArea(vqThrallPos, radius, damage * 0.5f);
                    result.TargetsHit += _hitBuffer.Count;
                    SpawnVisualBurst(vqThrallPos, radius * 2.0f, new Color(0.85f, 0.1f, 0.2f, 0.9f), 0.4f);
                    CueBus.Raise(CueIds.InvaderStrike, vqThrallPos.x, vqThrallPos.y);
                    break;

                case "vampire_queen_active_23": // Leech Infestation
                    Vector2 vqLeechPos = playerPos + aimDir * Math.Min(4.5f, def.Range > 0 ? def.Range : 4.5f);
                    float vqLeechDur = def.Duration > 0 ? def.Duration : 3.0f;
                    HazardZoneActor.Spawn(vqLeechPos, radius, vqLeechDur, damagePerTick: damage * 0.25f, tickInterval: 0.4f, slowPercent: 0.5f, slowDuration: 1.0f, color: new Color(0.8f, 0.05f, 0.2f, 0.55f), label: "[LEECH_INFESTATION]");
                    result.DamageDealt += DamageArea(vqLeechPos, radius, damage * 0.4f, slowPercent: 0.5f, slowDuration: 2.0f);
                    result.TargetsHit += _hitBuffer.Count;
                    if (result.DamageDealt > 0f)
                    {
                        float vqLeechHeal = result.DamageDealt * (rank >= 20 ? 0.50f : 0.35f);
                        _player.Health.Heal(vqLeechHeal);
                        result.HealingDone += vqLeechHeal;
                        CueBus.Raise(CueIds.ClericHeal, playerPos.x, playerPos.y, vqLeechHeal);
                    }
                    SpawnVisualBurst(vqLeechPos, radius * 2.2f, new Color(0.75f, 0.05f, 0.25f, 0.85f), 0.4f);
                    CueBus.Raise(CueIds.InvaderStrike, vqLeechPos.x, vqLeechPos.y);
                    break;

                case "vampire_queen_active_24": // Vampiric Command
                    result.DamageDealt += DamageArea(playerPos, radius, damage, knockbackForce: 5.0f);
                    result.TargetsHit += _hitBuffer.Count;
                    _player.SetBarrier(35f + (rank >= 20 ? 15f : 0f), duration: def.Duration > 0 ? def.Duration : 4.0f);
                    SpawnVisualBurst(playerPos, radius * 2.5f, new Color(0.9f, 0.05f, 0.2f, 0.9f), 0.5f);
                    CueBus.Raise(CueIds.InvaderStrike, playerPos.x, playerPos.y);
                    break;

                case "vampire_queen_active_25": // Shroud of Night
                    Vector2 vqShroudOffset = aimDir * Math.Min(3.5f, def.Range > 0 ? def.Range : 3.5f);
                    Vector2 vqShroudDest = playerPos + vqShroudOffset;
                    _player.Teleport(vqShroudDest);
                    result.DamageDealt += DamageArea(vqShroudDest, radius, damage, slowPercent: 0.35f, slowDuration: 1.5f);
                    result.TargetsHit += _hitBuffer.Count;
                    _player.SetBarrier(30f, duration: def.Duration > 0 ? def.Duration : 2.5f);
                    SpawnVisualBurst(vqShroudDest, radius * 2.4f, new Color(0.4f, 0.05f, 0.35f, 0.9f), 0.45f);
                    CueBus.Raise(CueIds.InvaderStrike, vqShroudDest.x, vqShroudDest.y);
                    break;

                case "vampire_queen_active_26": // Dominion Choke
                    Vector2 vqChokeTargetPos = playerPos + aimDir * Math.Min(4.5f, def.Range > 0 ? def.Range : 4.5f);
                    var vqAllActors = ActorRegistry.AllActors;
                    float vqBestDist = float.MaxValue;
                    for (int i = 0; i < vqAllActors.Count; i++)
                    {
                        var a = vqAllActors[i];
                        if (a != null && a.Team == Faction.Invader && a.Alive)
                        {
                            float d = Vector2.Distance(playerPos, a.Position);
                            if (d <= (def.Range > 0 ? def.Range : 6.5f) && d < vqBestDist)
                            {
                                vqBestDist = d;
                                vqChokeTargetPos = a.Position;
                            }
                        }
                    }
                    result.DamageDealt += DamageArea(vqChokeTargetPos, radius, damage * 1.35f, slowPercent: 0.70f, slowDuration: 2.5f);
                    result.TargetsHit += _hitBuffer.Count;
                    float vqChokeHeal = 25f;
                    _player.Health.Heal(vqChokeHeal);
                    result.HealingDone += vqChokeHeal;
                    CueBus.Raise(CueIds.ClericHeal, playerPos.x, playerPos.y, vqChokeHeal);
                    SpawnVisualBurst(vqChokeTargetPos, radius * 2.2f, new Color(0.7f, 0.05f, 0.2f, 0.95f), 0.5f);
                    CueBus.Raise(CueIds.InvaderStrike, vqChokeTargetPos.x, vqChokeTargetPos.y);
                    break;

                case "vampire_queen_active_27": // Hive Shriek
                    result.DamageDealt += DamageArea(playerPos, radius, damage, knockbackForce: 7.5f, slowPercent: 0.45f, slowDuration: 2.5f);
                    result.TargetsHit += _hitBuffer.Count;
                    SpawnVisualBurst(playerPos, radius * 2.8f, new Color(0.85f, 0.1f, 0.35f, 0.95f), 0.4f);
                    CueBus.Raise(CueIds.InvaderStrike, playerPos.x, playerPos.y);
                    break;

                case "vampire_queen_active_28": // Crimson Gargoyle
                    Vector2 vqGargoylePos = playerPos + aimDir * Math.Min(4.0f, def.Range > 0 ? def.Range : 4.0f);
                    UndeadMinionActor.Spawn(UndeadMinionType.Guardian, vqGargoylePos, overrideDamage: damage * 1.1f, overrideMaxHp: 200f, lifetime: def.Duration > 0 ? def.Duration : 6.0f);
                    result.DamageDealt += DamageArea(vqGargoylePos, radius, damage * 0.7f, knockbackForce: 6.0f);
                    result.TargetsHit += _hitBuffer.Count;
                    SpawnVisualBurst(vqGargoylePos, radius * 2.5f, new Color(0.9f, 0.1f, 0.15f, 0.95f), 0.5f);
                    CueBus.Raise(CueIds.InvaderStrike, vqGargoylePos.x, vqGargoylePos.y);
                    break;

                case "vampire_queen_active_29": // Apex Colony
                    Vector2 vqColonyPos = playerPos + aimDir * Math.Min(4.5f, def.Range > 0 ? def.Range : 4.5f);
                    float vqColonyDur = def.Duration > 0 ? def.Duration : 4.0f;
                    HazardZoneActor.Spawn(vqColonyPos, radius, vqColonyDur, damagePerTick: damage * 0.30f, tickInterval: 0.35f, slowPercent: 0.45f, slowDuration: 1.2f, color: new Color(0.85f, 0.05f, 0.15f, 0.6f), label: "[APEX_COLONY]");
                    result.DamageDealt += DamageArea(vqColonyPos, radius, damage * 0.5f);
                    result.TargetsHit += _hitBuffer.Count;
                    if (result.DamageDealt > 0f)
                    {
                        float vqColonyHeal = result.DamageDealt * 0.40f;
                        _player.Health.Heal(vqColonyHeal);
                        result.HealingDone += vqColonyHeal;
                        CueBus.Raise(CueIds.ClericHeal, playerPos.x, playerPos.y, vqColonyHeal);
                    }
                    SpawnVisualBurst(vqColonyPos, radius * 2.8f, new Color(0.9f, 0.05f, 0.2f, 0.95f), 0.55f);
                    CueBus.Raise(CueIds.InvaderStrike, vqColonyPos.x, vqColonyPos.y);
                    break;

                case "vampire_queen_active_30": // Queen's Dominion
                    result.DamageDealt += DamageArea(playerPos, radius, damage * 1.4f, knockbackForce: 8.0f);
                    result.TargetsHit += _hitBuffer.Count;
                    _player.SetBarrier(60f, duration: def.Duration > 0 ? def.Duration : 6.0f);
                    Vector2 vqDomMinionPos = playerPos + aimDir * 2.0f;
                    UndeadMinionActor.Spawn(UndeadMinionType.Captain, vqDomMinionPos, overrideDamage: damage * 0.85f, overrideMaxHp: 180f, lifetime: def.Duration > 0 ? def.Duration : 6.0f);
                    if (result.DamageDealt > 0f)
                    {
                        float vqDomHeal = result.DamageDealt * 0.35f;
                        _player.Health.Heal(vqDomHeal);
                        result.HealingDone += vqDomHeal;
                        CueBus.Raise(CueIds.ClericHeal, playerPos.x, playerPos.y, vqDomHeal);
                    }
                    SpawnVisualBurst(playerPos, radius * 3.2f, new Color(0.95f, 0.05f, 0.25f, 0.98f), 0.7f);
                    CueBus.Raise(CueIds.InvaderStrike, playerPos.x, playerPos.y);
                    break;

                case "vampire_queen_active_31": // Crimson Mist
                    Vector2 vqMistOffset = aimDir * Math.Min(3.5f, def.Range > 0 ? def.Range : 3.5f);
                    Vector2 vqMistDest = playerPos + vqMistOffset;
                    _player.Teleport(vqMistDest);
                    result.DamageDealt += DamageArea(vqMistDest, radius, damage, slowPercent: 0.35f, slowDuration: 1.5f);
                    result.TargetsHit += _hitBuffer.Count;
                    _player.SetBarrier(30f, duration: def.Duration > 0 ? def.Duration : 2.0f);
                    SpawnVisualBurst(vqMistDest, radius * 2.2f, new Color(0.9f, 0.1f, 0.3f, 0.85f), 0.4f);
                    CueBus.Raise(CueIds.InvaderStrike, vqMistDest.x, vqMistDest.y);
                    break;

                case "vampire_queen_active_32": // Hypnotic Gaze
                    Vector2 vqGazePos = playerPos + aimDir * Math.Min(4.5f, def.Range > 0 ? def.Range : 4.5f);
                    var vqGazeCandidates = ActorRegistry.AllActors;
                    float vqBestGazeDist = float.MaxValue;
                    for (int i = 0; i < vqGazeCandidates.Count; i++)
                    {
                        var a = vqGazeCandidates[i];
                        if (a != null && a.Team == Faction.Invader && a.Alive)
                        {
                            float d = Vector2.Distance(playerPos, a.Position);
                            if (d <= (def.Range > 0 ? def.Range : 6.5f) && d < vqBestGazeDist)
                            {
                                vqBestGazeDist = d;
                                vqGazePos = a.Position;
                            }
                        }
                    }
                    result.DamageDealt += DamageArea(vqGazePos, radius, damage, slowPercent: 0.60f, slowDuration: 2.5f);
                    result.TargetsHit += _hitBuffer.Count;
                    SpawnVisualBurst(vqGazePos, radius * 2.2f, new Color(0.7f, 0.05f, 0.4f, 0.9f), 0.4f);
                    CueBus.Raise(CueIds.InvaderStrike, vqGazePos.x, vqGazePos.y);
                    break;

                case "vampire_queen_active_33": // Noble Command
                    result.DamageDealt += DamageArea(playerPos, radius, damage, knockbackForce: 6.0f, slowPercent: 0.50f, slowDuration: 2.0f);
                    result.TargetsHit += _hitBuffer.Count;
                    SpawnVisualBurst(playerPos, radius * 2.5f, new Color(0.8f, 0.05f, 0.3f, 0.9f), 0.45f);
                    CueBus.Raise(CueIds.InvaderStrike, playerPos.x, playerPos.y);
                    break;

                case "vampire_queen_active_34": // Veil of Night
                    Vector2 vqVeilPos = playerPos + aimDir * Math.Min(4.5f, def.Range > 0 ? def.Range : 4.5f);
                    float vqVeilDur = def.Duration > 0 ? def.Duration : 4.0f;
                    HazardZoneActor.Spawn(vqVeilPos, radius, vqVeilDur, damagePerTick: damage * 0.25f, tickInterval: 0.35f, slowPercent: 0.40f, slowDuration: 1.2f, color: new Color(0.2f, 0.05f, 0.25f, 0.65f), label: "[VEIL_OF_NIGHT]");
                    result.DamageDealt += DamageArea(vqVeilPos, radius, damage * 0.45f);
                    result.TargetsHit += _hitBuffer.Count;
                    float vqVeilHeal = 35f;
                    _player.Health.Heal(vqVeilHeal);
                    result.HealingDone += vqVeilHeal;
                    CueBus.Raise(CueIds.ClericHeal, playerPos.x, playerPos.y, vqVeilHeal);
                    SpawnVisualBurst(vqVeilPos, radius * 2.5f, new Color(0.3f, 0.05f, 0.35f, 0.9f), 0.5f);
                    CueBus.Raise(CueIds.InvaderStrike, vqVeilPos.x, vqVeilPos.y);
                    break;

                case "vampire_queen_active_35": // Blood Illusion
                    Vector2 vqIllusionPos = playerPos + aimDir * Math.Min(3.0f, def.Range > 0 ? def.Range : 3.0f);
                    UndeadMinionActor.Spawn(UndeadMinionType.Hound, vqIllusionPos, overrideDamage: damage * 0.8f, overrideMaxHp: 100f, lifetime: def.Duration > 0 ? def.Duration : 3.5f);
                    result.DamageDealt += DamageArea(vqIllusionPos, radius, damage * 0.6f);
                    result.TargetsHit += _hitBuffer.Count;
                    _player.SetBarrier(35f, duration: 3.5f);
                    SpawnVisualBurst(vqIllusionPos, radius * 2.2f, new Color(0.85f, 0.05f, 0.2f, 0.9f), 0.4f);
                    CueBus.Raise(CueIds.InvaderStrike, vqIllusionPos.x, vqIllusionPos.y);
                    break;

                case "vampire_queen_active_36": // Phantom Waltz
                    Vector2 vqWaltzDest = playerPos + aimDir * Math.Min(4.5f, def.Range > 0 ? def.Range : 4.5f);
                    _player.Teleport(vqWaltzDest);
                    result.DamageDealt += DamageArea(vqWaltzDest, radius, damage, knockbackForce: 4.0f);
                    result.TargetsHit += _hitBuffer.Count;
                    if (result.DamageDealt > 0f)
                    {
                        float vqWaltzHeal = result.DamageDealt * 0.35f;
                        _player.Health.Heal(vqWaltzHeal);
                        result.HealingDone += vqWaltzHeal;
                        CueBus.Raise(CueIds.ClericHeal, playerPos.x, playerPos.y, vqWaltzHeal);
                    }
                    SpawnVisualBurst(vqWaltzDest, radius * 2.4f, new Color(0.95f, 0.1f, 0.25f, 0.95f), 0.45f);
                    CueBus.Raise(CueIds.InvaderStrike, vqWaltzDest.x, vqWaltzDest.y);
                    break;

                case "vampire_queen_active_37": // Scarlet Prison
                    Vector2 vqPrisonPos = playerPos + aimDir * Math.Min(4.5f, def.Range > 0 ? def.Range : 4.5f);
                    float vqPrisonDur = def.Duration > 0 ? def.Duration : 3.0f;
                    HazardZoneActor.Spawn(vqPrisonPos, radius, vqPrisonDur, damagePerTick: damage * 0.28f, tickInterval: 0.35f, slowPercent: 0.75f, slowDuration: 1.5f, color: new Color(0.7f, 0.02f, 0.18f, 0.65f), label: "[SCARLET_PRISON]");
                    result.DamageDealt += DamageArea(vqPrisonPos, radius, damage * 0.55f, slowPercent: 0.75f, slowDuration: 2.5f);
                    result.TargetsHit += _hitBuffer.Count;
                    SpawnVisualBurst(vqPrisonPos, radius * 2.5f, new Color(0.8f, 0.05f, 0.2f, 0.95f), 0.5f);
                    CueBus.Raise(CueIds.InvaderStrike, vqPrisonPos.x, vqPrisonPos.y);
                    break;

                case "vampire_queen_active_38": // Royal Decree
                    result.DamageDealt += DamageArea(playerPos, radius, damage, knockbackForce: 7.0f, slowPercent: 0.50f, slowDuration: 3.0f);
                    result.TargetsHit += _hitBuffer.Count;
                    SpawnVisualBurst(playerPos, radius * 2.8f, new Color(0.85f, 0.1f, 0.3f, 0.95f), 0.5f);
                    CueBus.Raise(CueIds.InvaderStrike, playerPos.x, playerPos.y);
                    break;

                case "vampire_queen_active_39": // Midnight Embrace
                    Vector2 vqEmbracePos = playerPos + aimDir * Math.Min(4.5f, def.Range > 0 ? def.Range : 4.5f);
                    result.DamageDealt += DamageArea(vqEmbracePos, radius, damage * 1.3f, slowPercent: 0.65f, slowDuration: 2.5f);
                    result.TargetsHit += _hitBuffer.Count;
                    if (result.DamageDealt > 0f)
                    {
                        float vqEmbraceHeal = result.DamageDealt * 0.45f;
                        _player.Health.Heal(vqEmbraceHeal);
                        result.HealingDone += vqEmbraceHeal;
                        CueBus.Raise(CueIds.ClericHeal, playerPos.x, playerPos.y, vqEmbraceHeal);
                    }
                    SpawnVisualBurst(vqEmbracePos, radius * 2.6f, new Color(0.35f, 0.05f, 0.35f, 0.95f), 0.55f);
                    CueBus.Raise(CueIds.InvaderStrike, vqEmbracePos.x, vqEmbracePos.y);
                    break;

                case "vampire_queen_active_40": // Dark Ballroom
                    float vqBallroomDur = def.Duration > 0 ? def.Duration : 5.0f;
                    HazardZoneActor.Spawn(playerPos, radius, vqBallroomDur, damagePerTick: damage * 0.30f, tickInterval: 0.35f, slowPercent: 0.45f, slowDuration: 1.5f, color: new Color(0.25f, 0.02f, 0.28f, 0.65f), label: "[DARK_BALLROOM]");
                    result.DamageDealt += DamageArea(playerPos, radius, damage * 1.4f, knockbackForce: 8.5f);
                    result.TargetsHit += _hitBuffer.Count;
                    _player.SetBarrier(65f, duration: vqBallroomDur);
                    if (result.DamageDealt > 0f)
                    {
                        float vqBallroomHeal = result.DamageDealt * 0.35f;
                        _player.Health.Heal(vqBallroomHeal);
                        result.HealingDone += vqBallroomHeal;
                        CueBus.Raise(CueIds.ClericHeal, playerPos.x, playerPos.y, vqBallroomHeal);
                    }
                    SpawnVisualBurst(playerPos, radius * 3.2f, new Color(0.85f, 0.05f, 0.3f, 0.98f), 0.7f);
                    CueBus.Raise(CueIds.InvaderStrike, playerPos.x, playerPos.y);
                    break;

                case "vampire_queen_active_41": // Blood Covenant
                    result.DamageDealt += DamageArea(playerPos, radius, damage, knockbackForce: 4.5f);
                    result.TargetsHit += _hitBuffer.Count;
                    _player.SetBarrier(35f + (rank >= 20 ? 15f : 0f), duration: def.Duration > 0 ? def.Duration : 3.5f);
                    SpawnVisualBurst(playerPos, radius * 2.2f, new Color(0.9f, 0.05f, 0.2f, 0.9f), 0.4f);
                    CueBus.Raise(CueIds.InvaderStrike, playerPos.x, playerPos.y);
                    break;

                case "vampire_queen_active_42": // Crimson Chalice
                    result.DamageDealt += DamageArea(playerPos, radius, damage * 0.8f);
                    result.TargetsHit += _hitBuffer.Count;
                    float vqChaliceHeal = 35f;
                    _player.Health.Heal(vqChaliceHeal);
                    result.HealingDone += vqChaliceHeal;
                    CueBus.Raise(CueIds.ClericHeal, playerPos.x, playerPos.y, vqChaliceHeal);
                    _player.SetBarrier(25f, duration: 3.0f);
                    SpawnVisualBurst(playerPos, radius * 2.4f, new Color(1f, 0.1f, 0.25f, 0.95f), 0.45f);
                    CueBus.Raise(CueIds.InvaderStrike, playerPos.x, playerPos.y);
                    break;

                case "vampire_queen_active_43": // Scarlet Resurrection
                    result.DamageDealt += DamageArea(playerPos, radius, damage * 1.1f, knockbackForce: 7.0f);
                    result.TargetsHit += _hitBuffer.Count;
                    float vqResHeal = 40f;
                    _player.Health.Heal(vqResHeal);
                    result.HealingDone += vqResHeal;
                    CueBus.Raise(CueIds.ClericHeal, playerPos.x, playerPos.y, vqResHeal);
                    _player.SetBarrier(40f, duration: 4.0f);
                    SpawnVisualBurst(playerPos, radius * 2.6f, new Color(1f, 0.05f, 0.15f, 0.98f), 0.5f);
                    CueBus.Raise(CueIds.InvaderStrike, playerPos.x, playerPos.y);
                    break;

                case "vampire_queen_active_44": // Throne of Thorns
                    float vqThroneDur = def.Duration > 0 ? def.Duration : 4.0f;
                    HazardZoneActor.Spawn(playerPos, radius, vqThroneDur, damagePerTick: damage * 0.28f, tickInterval: 0.35f, slowPercent: 0.60f, slowDuration: 1.2f, color: new Color(0.85f, 0.05f, 0.15f, 0.6f), label: "[THRONE_OF_THORNS]");
                    result.DamageDealt += DamageArea(playerPos, radius, damage * 0.5f, slowPercent: 0.60f, slowDuration: 2.0f);
                    result.TargetsHit += _hitBuffer.Count;
                    SpawnVisualBurst(playerPos, radius * 2.5f, new Color(0.9f, 0.1f, 0.2f, 0.95f), 0.5f);
                    CueBus.Raise(CueIds.InvaderStrike, playerPos.x, playerPos.y);
                    break;

                case "vampire_queen_active_45": // Royal Feast
                    result.DamageDealt += DamageArea(playerPos, radius, damage * 1.2f, knockbackForce: 5.0f);
                    result.TargetsHit += _hitBuffer.Count;
                    if (result.DamageDealt > 0f)
                    {
                        float vqFeastHeal = result.DamageDealt * 0.50f;
                        _player.Health.Heal(vqFeastHeal);
                        result.HealingDone += vqFeastHeal;
                        CueBus.Raise(CueIds.ClericHeal, playerPos.x, playerPos.y, vqFeastHeal);
                    }
                    SpawnVisualBurst(playerPos, radius * 2.8f, new Color(0.95f, 0.05f, 0.25f, 0.95f), 0.5f);
                    CueBus.Raise(CueIds.InvaderStrike, playerPos.x, playerPos.y);
                    break;

                case "vampire_queen_active_46": // Queen's Masquerade
                    Vector2 vqMasqPos1 = playerPos + aimDir * Math.Min(3.0f, def.Range > 0 ? def.Range : 3.0f);
                    Vector2 vqMasqPos2 = playerPos - aimDir * 2.0f;
                    UndeadMinionActor.Spawn(UndeadMinionType.Hound, vqMasqPos1, overrideDamage: damage * 0.85f, overrideMaxHp: 120f, lifetime: def.Duration > 0 ? def.Duration : 4.0f);
                    UndeadMinionActor.Spawn(UndeadMinionType.Hound, vqMasqPos2, overrideDamage: damage * 0.85f, overrideMaxHp: 120f, lifetime: def.Duration > 0 ? def.Duration : 4.0f);
                    result.DamageDealt += DamageArea(vqMasqPos1, radius, damage * 0.5f);
                    result.TargetsHit += _hitBuffer.Count;
                    SpawnVisualBurst(playerPos, radius * 2.8f, new Color(0.8f, 0.05f, 0.35f, 0.9f), 0.5f);
                    CueBus.Raise(CueIds.InvaderStrike, playerPos.x, playerPos.y);
                    break;

                case "vampire_queen_active_47": // Blood Seraph
                    Vector2 vqSeraphPos = playerPos + aimDir * Math.Min(3.5f, def.Range > 0 ? def.Range : 3.5f);
                    result.DamageDealt += DamageArea(vqSeraphPos, radius, damage * 1.25f, knockbackForce: 6.5f);
                    result.TargetsHit += _hitBuffer.Count;
                    _player.SetBarrier(40f, duration: 4.0f);
                    SpawnVisualBurst(vqSeraphPos, radius * 2.6f, new Color(1f, 0.05f, 0.2f, 0.95f), 0.5f);
                    CueBus.Raise(CueIds.InvaderStrike, vqSeraphPos.x, vqSeraphPos.y);
                    break;

                case "vampire_queen_active_48": // Sanguine Dominion
                    Vector2 vqDomPos = playerPos + aimDir * Math.Min(4.5f, def.Range > 0 ? def.Range : 4.5f);
                    float vqDomDur = def.Duration > 0 ? def.Duration : 4.5f;
                    HazardZoneActor.Spawn(vqDomPos, radius, vqDomDur, damagePerTick: damage * 0.28f, tickInterval: 0.35f, slowPercent: 0.55f, slowDuration: 1.5f, color: new Color(0.75f, 0.02f, 0.22f, 0.65f), label: "[SANGUINE_DOMINION]");
                    result.DamageDealt += DamageArea(vqDomPos, radius, damage * 0.5f, slowPercent: 0.55f, slowDuration: 2.5f);
                    result.TargetsHit += _hitBuffer.Count;
                    float vqDomHeal2 = 30f;
                    _player.Health.Heal(vqDomHeal2);
                    result.HealingDone += vqDomHeal2;
                    CueBus.Raise(CueIds.ClericHeal, playerPos.x, playerPos.y, vqDomHeal2);
                    SpawnVisualBurst(vqDomPos, radius * 2.8f, new Color(0.85f, 0.05f, 0.25f, 0.95f), 0.55f);
                    CueBus.Raise(CueIds.InvaderStrike, vqDomPos.x, vqDomPos.y);
                    break;

                case "vampire_queen_active_49": // Eternal Thirst
                    result.DamageDealt += DamageArea(playerPos, radius, damage * 1.35f, knockbackForce: 6.0f);
                    result.TargetsHit += _hitBuffer.Count;
                    if (result.DamageDealt > 0f)
                    {
                        float vqThirstHeal = result.DamageDealt * 0.60f;
                        _player.Health.Heal(vqThirstHeal);
                        result.HealingDone += vqThirstHeal;
                        CueBus.Raise(CueIds.ClericHeal, playerPos.x, playerPos.y, vqThirstHeal);
                    }
                    _player.SetBarrier(45f, duration: 4.0f);
                    SpawnVisualBurst(playerPos, radius * 3.0f, new Color(1f, 0.02f, 0.15f, 0.98f), 0.6f);
                    CueBus.Raise(CueIds.InvaderStrike, playerPos.x, playerPos.y);
                    break;

                case "vampire_queen_active_50": // Crimson Coronation
                    result.DamageDealt += DamageArea(playerPos, radius, damage * 1.6f, knockbackForce: 9.5f);
                    result.TargetsHit += _hitBuffer.Count;
                    _player.SetBarrier(80f, duration: def.Duration > 0 ? def.Duration : 6.0f);
                    Vector2 vqCoroPos1 = playerPos + aimDir * 2.0f;
                    Vector2 vqCoroPos2 = playerPos - aimDir * 2.0f;
                    UndeadMinionActor.Spawn(UndeadMinionType.Captain, vqCoroPos1, overrideDamage: damage * 0.9f, overrideMaxHp: 200f, lifetime: def.Duration > 0 ? def.Duration : 6.0f);
                    UndeadMinionActor.Spawn(UndeadMinionType.Captain, vqCoroPos2, overrideDamage: damage * 0.9f, overrideMaxHp: 200f, lifetime: def.Duration > 0 ? def.Duration : 6.0f);
                    if (result.DamageDealt > 0f)
                    {
                        float vqCoroHeal = result.DamageDealt * 0.40f;
                        _player.Health.Heal(vqCoroHeal);
                        result.HealingDone += vqCoroHeal;
                        CueBus.Raise(CueIds.ClericHeal, playerPos.x, playerPos.y, vqCoroHeal);
                    }
                    SpawnVisualBurst(playerPos, radius * 3.5f, new Color(1f, 0.05f, 0.2f, 1f), 0.8f);
                    CueBus.Raise(CueIds.InvaderStrike, playerPos.x, playerPos.y);
                    break;

                // Milestone Ultimates
                case "vampire_queen_ultimate_50": // Crimson Masquerade
                    result.DamageDealt += DamageArea(playerPos, radius, damage);
                    result.TargetsHit += _hitBuffer.Count;
                    if (result.DamageDealt > 0f)
                    {
                        float drain = result.DamageDealt * 0.60f;
                        _player.Health.Heal(drain);
                        result.HealingDone += drain;
                        CueBus.Raise(CueIds.ClericHeal, playerPos.x, playerPos.y, drain);
                    }
                    SpawnVisualBurst(playerPos, radius * 2f, new Color(1f, 0.05f, 0.2f, 0.9f), 0.6f);
                    CueBus.Raise(CueIds.InvaderStrike, playerPos.x, playerPos.y);
                    break;

                case "vampire_queen_ultimate_100": // Blood Moon Ascension
                    HazardZoneActor.Spawn(playerPos, radius, duration: def.Duration > 0 ? def.Duration : 6.0f, damagePerTick: damage * 0.35f, tickInterval: 0.5f, slowPercent: 0.4f, slowDuration: 1.5f, color: new Color(0.95f, 0.05f, 0.15f, 0.65f), label: "[BLOOD_MOON]");
                    result.DamageDealt += DamageArea(playerPos, radius, damage * 0.8f);
                    result.TargetsHit += _hitBuffer.Count;
                    if (result.DamageDealt > 0f)
                    {
                        float moonDrain = result.DamageDealt * 0.4f;
                        _player.Health.Heal(moonDrain);
                        result.HealingDone += moonDrain;
                    }
                    SpawnVisualBurst(playerPos, radius * 2.8f, new Color(1f, 0.1f, 0.2f, 0.95f), 0.8f);
                    CueBus.Raise(CueIds.InvaderStrike, playerPos.x, playerPos.y);
                    break;

                case "vampire_queen_ultimate_150": // Sovereign Feast
                    result.DamageDealt += DamageArea(playerPos, radius, damage * 1.3f, knockbackForce: 7.0f);
                    result.TargetsHit += _hitBuffer.Count;
                    if (result.DamageDealt > 0f)
                    {
                        float feastHeal = result.DamageDealt * 0.75f;
                        _player.Health.Heal(feastHeal);
                        result.HealingDone += feastHeal;
                        CueBus.Raise(CueIds.ClericHeal, playerPos.x, playerPos.y, feastHeal);
                    }
                    _player.SetBarrier(100f, duration: 4.0f);
                    SpawnVisualBurst(playerPos, radius * 2.8f, new Color(1f, 0.02f, 0.15f, 0.95f), 0.8f);
                    CueBus.Raise(CueIds.InvaderStrike, playerPos.x, playerPos.y);
                    break;

                case "vampire_queen_ultimate_200": // Grand Vampire Court
                    result.DamageDealt += DamageArea(playerPos, radius, damage * 1.1f, knockbackForce: 6.0f);
                    result.TargetsHit += _hitBuffer.Count;
                    Vector2 thrallOffset1 = playerPos + aimDir * 2.0f;
                    Vector2 thrallOffset2 = playerPos - aimDir * 2.0f;
                    UndeadMinionActor.Spawn(UndeadMinionType.Captain, thrallOffset1, overrideDamage: damage * 0.9f, overrideMaxHp: 160f, lifetime: def.Duration > 0 ? def.Duration : 8.0f);
                    UndeadMinionActor.Spawn(UndeadMinionType.Guardian, thrallOffset2, overrideDamage: damage * 0.7f, overrideMaxHp: 200f, lifetime: def.Duration > 0 ? def.Duration : 8.0f);
                    _player.SetBarrier(80f, duration: 5.0f);
                    SpawnVisualBurst(playerPos, radius * 3.0f, new Color(0.9f, 0.05f, 0.25f, 0.95f), 0.8f);
                    CueBus.Raise(CueIds.InvaderStrike, playerPos.x, playerPos.y);
                    break;

                case "vampire_queen_ultimate_250": // Apocalypse of Scarlet
                    result.DamageDealt += DamageArea(playerPos, radius, damage * 1.6f, knockbackForce: 10.0f);
                    result.TargetsHit += _hitBuffer.Count;
                    if (result.DamageDealt > 0f)
                    {
                        float apocHeal = result.DamageDealt * 0.85f;
                        _player.Health.Heal(apocHeal);
                        result.HealingDone += apocHeal;
                        CueBus.Raise(CueIds.ClericHeal, playerPos.x, playerPos.y, apocHeal);
                    }
                    _player.SetBarrier(150f, duration: 6.0f);
                    HazardZoneActor.Spawn(playerPos, radius, duration: 5.0f, damagePerTick: damage * 0.4f, tickInterval: 0.5f, color: new Color(1f, 0.05f, 0.15f, 0.7f), label: "[SCARLET_APOCALYPSE]");
                    SpawnVisualBurst(playerPos, radius * 3.5f, new Color(1f, 0.02f, 0.1f, 0.98f), 1.0f);
                    CueBus.Raise(CueIds.InvaderStrike, playerPos.x, playerPos.y);
                    break;

                // -------------------------------------------------------------
                // SHADOW REAPER
                // -------------------------------------------------------------
                case "shadow_reaper_active_01": // Crescent Reap
                    result.DamageDealt += DamageArc(playerPos, aimDir, radius, 150f, damage, knockbackForce: 2.5f);
                    result.TargetsHit += _hitBuffer.Count;
                    SpawnVisualArc(playerPos, aimDir, radius, new Color(0.7f, 0.3f, 0.95f, 0.9f), 0.25f);
                    CueBus.Raise(CueIds.InvaderStrike, playerPos.x, playerPos.y);
                    break;

                case "shadow_reaper_active_02": // Soul Sever
                    Vector2 srSeverPos = playerPos + aimDir * Math.Min(2.5f, def.Range > 0 ? def.Range : 2.5f);
                    result.DamageDealt += DamageArea(srSeverPos, radius, damage * 1.25f, slowPercent: 0.50f, slowDuration: 2.0f);
                    result.TargetsHit += _hitBuffer.Count;
                    SpawnVisualBurst(srSeverPos, radius * 2.2f, new Color(0.75f, 0.2f, 1f, 0.95f), 0.35f);
                    CueBus.Raise(CueIds.InvaderStrike, srSeverPos.x, srSeverPos.y);
                    break;

                case "shadow_reaper_active_03": // Grim Spin
                    result.DamageDealt += DamageArea(playerPos, radius, damage, knockbackForce: 5.0f);
                    result.TargetsHit += _hitBuffer.Count;
                    _player.SetBarrier(25f, duration: 3.5f);
                    SpawnVisualBurst(playerPos, radius * 2.5f, new Color(0.65f, 0.15f, 0.9f, 0.9f), 0.4f);
                    CueBus.Raise(CueIds.InvaderStrike, playerPos.x, playerPos.y);
                    break;

                case "shadow_reaper_active_04": // Phantom Slice
                    Vector2 srSlicePos = playerPos + aimDir * Math.Min(4.5f, def.Range > 0 ? def.Range : 4.5f);
                    result.DamageDealt += DamageArea(srSlicePos, radius, damage * 1.1f);
                    result.TargetsHit += _hitBuffer.Count;
                    SpawnVisualBurst(srSlicePos, radius * 2.2f, new Color(0.6f, 0.25f, 0.85f, 0.9f), 0.35f);
                    CueBus.Raise(CueIds.InvaderStrike, srSlicePos.x, srSlicePos.y);
                    break;

                case "shadow_reaper_active_05": // Executioner's Hook
                    Vector2 srHookPos = playerPos + aimDir * Math.Min(4.0f, def.Range > 0 ? def.Range : 4.0f);
                    result.DamageDealt += DamageArea(srHookPos, radius, damage, slowPercent: 0.70f, slowDuration: 3.0f);
                    result.TargetsHit += _hitBuffer.Count;
                    SpawnVisualBurst(srHookPos, radius * 2.4f, new Color(0.8f, 0.2f, 0.95f, 0.9f), 0.4f);
                    CueBus.Raise(CueIds.InvaderStrike, srHookPos.x, srHookPos.y);
                    break;

                case "shadow_reaper_active_06": // Twin Crescent
                    Vector2 srTwinPos = playerPos + aimDir * Math.Min(3.5f, def.Range > 0 ? def.Range : 3.5f);
                    result.DamageDealt += DamageArea(srTwinPos, radius, damage * 1.35f, knockbackForce: 4.5f);
                    result.TargetsHit += _hitBuffer.Count;
                    SpawnVisualBurst(srTwinPos, radius * 2.6f, new Color(0.7f, 0.15f, 1f, 0.95f), 0.45f);
                    CueBus.Raise(CueIds.InvaderStrike, srTwinPos.x, srTwinPos.y);
                    break;

                case "shadow_reaper_active_07": // Gravewheel
                    Vector2 srWheelPos = playerPos + aimDir * Math.Min(4.0f, def.Range > 0 ? def.Range : 4.0f);
                    float srWheelDur = def.Duration > 0 ? def.Duration : 2.0f;
                    HazardZoneActor.Spawn(srWheelPos, radius, srWheelDur, damagePerTick: damage * 0.25f, tickInterval: 0.35f, color: new Color(0.6f, 0.15f, 0.8f, 0.6f), label: "[GRAVEWHEEL]");
                    result.DamageDealt += DamageArea(srWheelPos, radius, damage * 0.5f, knockbackForce: -3.0f);
                    result.TargetsHit += _hitBuffer.Count;
                    SpawnVisualBurst(srWheelPos, radius * 2.4f, new Color(0.7f, 0.2f, 0.9f, 0.9f), 0.4f);
                    CueBus.Raise(CueIds.InvaderStrike, srWheelPos.x, srWheelPos.y);
                    break;

                case "shadow_reaper_active_08": // Silent Decapitation
                    Vector2 srDecapPos = playerPos + aimDir * Math.Min(3.0f, def.Range > 0 ? def.Range : 3.0f);
                    result.DamageDealt += DamageArea(srDecapPos, radius, damage * 1.4f, knockbackForce: 6.0f);
                    result.TargetsHit += _hitBuffer.Count;
                    _player.SetBarrier(35f, duration: 4.0f);
                    SpawnVisualBurst(srDecapPos, radius * 2.6f, new Color(0.85f, 0.1f, 1f, 0.98f), 0.45f);
                    CueBus.Raise(CueIds.InvaderStrike, srDecapPos.x, srDecapPos.y);
                    break;

                case "shadow_reaper_active_09": // Harvesting Dance
                    result.DamageDealt += DamageArea(playerPos, radius, damage * 1.35f, knockbackForce: 5.5f);
                    result.TargetsHit += _hitBuffer.Count;
                    _player.SetBarrier(40f, duration: 4.0f);
                    SpawnVisualBurst(playerPos, radius * 2.8f, new Color(0.75f, 0.15f, 0.95f, 0.95f), 0.5f);
                    CueBus.Raise(CueIds.InvaderStrike, playerPos.x, playerPos.y);
                    break;

                case "shadow_reaper_active_10": // Reaper's Guillotine
                    Vector2 srGuillotinePos = playerPos + aimDir * Math.Min(3.5f, def.Range > 0 ? def.Range : 3.5f);
                    result.DamageDealt += DamageArea(srGuillotinePos, radius, damage * 1.6f, knockbackForce: 8.5f, slowPercent: 0.80f, slowDuration: 2.0f);
                    result.TargetsHit += _hitBuffer.Count;
                    _player.SetBarrier(50f, duration: 5.0f);
                    SpawnVisualBurst(srGuillotinePos, radius * 3.2f, new Color(0.9f, 0.1f, 1f, 1f), 0.6f);
                    CueBus.Raise(CueIds.InvaderStrike, srGuillotinePos.x, srGuillotinePos.y);
                    break;

                case "shadow_reaper_active_shadowstep": // Legacy shadowstep alias
                    Vector2 reaperStart = playerPos;
                    Vector2 reaperDest = ValidateLandingDestination(reaperStart, reaperStart + aimDir * def.Range);
                    _player.Teleport(reaperDest);
                    result.DamageDealt += DamageArea(reaperDest, radius, damage);
                    result.TargetsHit += _hitBuffer.Count;
                    SpawnVisualBurst(reaperDest, radius * 2f, new Color(0.6f, 0.2f, 0.9f, 0.85f), 0.3f);
                    CueBus.Raise(CueIds.InvaderStrike, reaperDest.x, reaperDest.y);
                    break;

                case "shadow_reaper_active_11": // Umbral Shroud (legacy placeholder)
                    result.DamageDealt += DamageArea(playerPos, radius, damage, slowPercent: 0.5f, slowDuration: 4f);
                    result.TargetsHit += _hitBuffer.Count;
                    SpawnVisualBurst(playerPos, radius * 2f, new Color(0.3f, 0.1f, 0.5f, 0.8f), def.Duration);
                    CueBus.Raise(CueIds.InvaderStrike, playerPos.x, playerPos.y);
                    break;

                // Milestone Ultimates
                case "shadow_reaper_ultimate_50": // Dance of Ten Thousand Cuts
                    result.DamageDealt += DamageArea(playerPos, radius, damage, knockbackForce: 5.0f);
                    result.TargetsHit += _hitBuffer.Count;
                    _player.SetBarrier(60f, duration: 4.0f);
                    SpawnVisualBurst(playerPos, radius * 2.6f, new Color(0.85f, 0.2f, 1f, 0.95f), 0.6f);
                    CueBus.Raise(CueIds.InvaderStrike, playerPos.x, playerPos.y);
                    break;

                case "shadow_reaper_ultimate_100": // Night Without Footsteps
                    result.DamageDealt += DamageArea(playerPos, radius, damage * 1.25f, knockbackForce: 6.0f);
                    result.TargetsHit += _hitBuffer.Count;
                    _player.SetBarrier(70f, duration: 5.0f);
                    SpawnVisualBurst(playerPos, radius * 3.0f, new Color(0.7f, 0.1f, 0.95f, 0.95f), 0.7f);
                    CueBus.Raise(CueIds.InvaderStrike, playerPos.x, playerPos.y);
                    break;

                case "shadow_reaper_ultimate_150": // Umbral Labyrinth
                    float srLabDur = def.Duration > 0 ? def.Duration : 6.0f;
                    HazardZoneActor.Spawn(playerPos, radius, srLabDur, damagePerTick: damage * 0.30f, tickInterval: 0.35f, slowPercent: 0.60f, slowDuration: 1.5f, color: new Color(0.4f, 0.05f, 0.6f, 0.65f), label: "[UMBRAL_LABYRINTH]");
                    result.DamageDealt += DamageArea(playerPos, radius, damage * 1.35f, knockbackForce: 7.0f);
                    result.TargetsHit += _hitBuffer.Count;
                    _player.SetBarrier(80f, duration: srLabDur);
                    SpawnVisualBurst(playerPos, radius * 3.2f, new Color(0.8f, 0.15f, 1f, 0.98f), 0.75f);
                    CueBus.Raise(CueIds.InvaderStrike, playerPos.x, playerPos.y);
                    break;

                case "shadow_reaper_ultimate_200": // Harvest of Names
                    result.DamageDealt += DamageArea(playerPos, radius, damage * 1.5f, knockbackForce: 8.5f, slowPercent: 0.65f, slowDuration: 3.0f);
                    result.TargetsHit += _hitBuffer.Count;
                    _player.SetBarrier(90f, duration: 6.0f);
                    SpawnVisualBurst(playerPos, radius * 3.5f, new Color(0.9f, 0.1f, 1f, 1f), 0.8f);
                    CueBus.Raise(CueIds.InvaderStrike, playerPos.x, playerPos.y);
                    break;

                case "shadow_reaper_ultimate_250": // Eclipse Reaper
                    result.DamageDealt += DamageArea(playerPos, radius, damage * 1.8f, knockbackForce: 10.0f, slowPercent: 0.75f, slowDuration: 4.0f);
                    result.TargetsHit += _hitBuffer.Count;
                    _player.SetBarrier(120f, duration: 8.0f);
                    Vector2 srEclipseMinion1 = playerPos + aimDir * 2.0f;
                    Vector2 srEclipseMinion2 = playerPos - aimDir * 2.0f;
                    UndeadMinionActor.Spawn(UndeadMinionType.Captain, srEclipseMinion1, overrideDamage: damage * 0.9f, overrideMaxHp: 220f, lifetime: 8.0f);
                    UndeadMinionActor.Spawn(UndeadMinionType.Captain, srEclipseMinion2, overrideDamage: damage * 0.9f, overrideMaxHp: 220f, lifetime: 8.0f);
                    SpawnVisualBurst(playerPos, radius * 4.0f, new Color(1f, 0.05f, 1f, 1f), 0.9f);
                    CueBus.Raise(CueIds.InvaderStrike, playerPos.x, playerPos.y);
                    break;

                // -------------------------------------------------------------
                // CURSED GOLEM
                // -------------------------------------------------------------
                // Branch A — Colossal Physical Combat (Actives 01-10)
                case "cursed_golem_active_01": // Titan Punch
                    Vector2 punchCenter = playerPos + aimDir * 1.5f;
                    float punchKb = 4.5f * (1f + (rank >= 15 ? 0.50f : 0f));
                    result.DamageDealt += DamageArea(punchCenter, radius, damage, knockbackForce: punchKb);
                    result.TargetsHit += _hitBuffer.Count;
                    if (rank >= 20)
                    {
                        result.DamageDealt += DamageArea(punchCenter + aimDir * 1.0f, radius * 1.2f, damage * 0.5f, knockbackForce: punchKb * 1.2f);
                        result.TargetsHit += _hitBuffer.Count;
                    }
                    SpawnVisualBurst(punchCenter, radius * 2f, new Color(0.85f, 0.65f, 0.3f, 0.9f), 0.35f);
                    CueBus.Raise(CueIds.InvaderStrike, playerPos.x, playerPos.y);
                    break;

                case "cursed_golem_active_02": // Stonebreaker
                    Vector2 stonebreakerPos = playerPos + aimDir * 1.5f;
                    result.DamageDealt += DamageArea(stonebreakerPos, radius, damage, knockbackForce: 3.5f);
                    result.TargetsHit += _hitBuffer.Count;
                    SpawnVisualBurst(stonebreakerPos, radius * 2f, new Color(0.75f, 0.55f, 0.25f, 0.9f), 0.4f);
                    CueBus.Raise(CueIds.InvaderStrike, playerPos.x, playerPos.y);
                    break;

                case "cursed_golem_active_03": // Boulder Uppercut
                    Vector2 upperPos = playerPos + aimDir * 1.5f;
                    float upperKb = 6f * (1f + (rank >= 5 ? 0.35f : 0f));
                    result.DamageDealt += DamageArea(upperPos, radius, damage, knockbackForce: upperKb);
                    result.TargetsHit += _hitBuffer.Count;
                    if (rank >= 20)
                    {
                        result.DamageDealt += DamageArea(upperPos, radius * 1.5f, 25f, knockbackForce: 4f);
                        result.TargetsHit += _hitBuffer.Count;
                    }
                    SpawnVisualBurst(upperPos, radius * 2.2f, new Color(0.8f, 0.7f, 0.5f, 0.9f), 0.45f);
                    CueBus.Raise(CueIds.InvaderStrike, playerPos.x, playerPos.y);
                    break;

                case "cursed_golem_active_04": // Mountain Slam
                    result.DamageDealt += DamageArea(playerPos, radius, damage, knockbackForce: 5f);
                    result.TargetsHit += _hitBuffer.Count;
                    if (rank >= 20)
                    {
                        for (int i = 0; i < _hitBuffer.Count; i++)
                        {
                            if (_hitBuffer[i] is InvaderActor invader && invader.Alive)
                            {
                                invader.ApplySlow(0.40f, 3.0f);
                            }
                        }
                    }
                    SpawnVisualBurst(playerPos, radius * 2f, new Color(0.7f, 0.5f, 0.3f, 0.9f), 0.5f);
                    CueBus.Raise(CueIds.InvaderStrike, playerPos.x, playerPos.y);
                    break;

                case "cursed_golem_active_05": // Granite Sweep
                    Vector2 sweepPos = playerPos + aimDir * 1.8f;
                    float sweepKb = rank >= 15 ? 8f : 4f;
                    result.DamageDealt += DamageArea(sweepPos, radius, damage, knockbackForce: sweepKb);
                    result.TargetsHit += _hitBuffer.Count;
                    if (rank >= 20)
                    {
                        result.DamageDealt += DamageArea(sweepPos, radius, damage * 0.75f, knockbackForce: sweepKb);
                        result.TargetsHit += _hitBuffer.Count;
                    }
                    SpawnVisualBurst(sweepPos, radius * 2.2f, new Color(0.65f, 0.6f, 0.55f, 0.9f), 0.4f);
                    CueBus.Raise(CueIds.InvaderStrike, playerPos.x, playerPos.y);
                    break;

                case "cursed_golem_active_06": // Crushing Charge
                    Vector2 chargeDir = aimDir.sqrMagnitude > 0.01f ? aimDir.normalized : Vector2.right;
                    float rushDist = 3.5f * (1f + (rank >= 5 ? 0.30f : 0f));
                    if (_player != null)
                    {
                        _player.transform.position += (Vector3)(chargeDir * rushDist);
                    }
                    Vector2 chargePos = _player != null ? (Vector2)_player.transform.position : playerPos + chargeDir * rushDist;
                    if (rank >= 20 && _player != null)
                    {
                        _player.SetBarrier(40f, 4f);
                        result.BarrierGranted += 40f;
                    }
                    result.DamageDealt += DamageArea(chargePos, radius, damage, knockbackForce: 5.5f);
                    result.TargetsHit += _hitBuffer.Count;
                    SpawnVisualBurst(chargePos, radius * 2f, new Color(0.9f, 0.6f, 0.2f, 0.9f), 0.4f);
                    CueBus.Raise(CueIds.InvaderStrike, chargePos.x, chargePos.y);
                    break;

                case "cursed_golem_active_07": // Bedrock Hammer
                    Vector2 hammerPos = playerPos + aimDir * 2.0f;
                    result.DamageDealt += DamageArea(hammerPos, radius, damage, knockbackForce: 5f);
                    result.TargetsHit += _hitBuffer.Count;
                    if (rank >= 20)
                    {
                        result.DamageDealt += DamageArea(hammerPos, radius * 1.5f, 25f, knockbackForce: 3f);
                        result.TargetsHit += _hitBuffer.Count;
                    }
                    SpawnVisualBurst(hammerPos, radius * 2f, new Color(0.6f, 0.5f, 0.4f, 0.95f), 0.45f);
                    CueBus.Raise(CueIds.InvaderStrike, playerPos.x, playerPos.y);
                    break;

                case "cursed_golem_active_08": // Colossus Grip
                    Vector2 gripPos = playerPos + aimDir * 2.5f;
                    result.DamageDealt += DamageArea(gripPos, radius, damage, knockbackForce: 2f, pullTowardCenter: true);
                    result.TargetsHit += _hitBuffer.Count;
                    SpawnVisualBurst(gripPos, radius * 2f, new Color(0.5f, 0.6f, 0.7f, 0.9f), 0.45f);
                    CueBus.Raise(CueIds.InvaderStrike, playerPos.x, playerPos.y);
                    break;

                case "cursed_golem_active_09": // Double Fist Impact
                    result.DamageDealt += DamageArea(playerPos, radius, damage, knockbackForce: 6f);
                    result.TargetsHit += _hitBuffer.Count;
                    if (rank >= 20)
                    {
                        result.DamageDealt += DamageArea(playerPos, radius * 1.3f, damage * 0.5f, knockbackForce: 4f);
                        result.TargetsHit += _hitBuffer.Count;
                    }
                    SpawnVisualBurst(playerPos, radius * 2.2f, new Color(0.85f, 0.55f, 0.2f, 0.95f), 0.55f);
                    CueBus.Raise(CueIds.InvaderStrike, playerPos.x, playerPos.y);
                    break;

                case "cursed_golem_active_10": // Worldbreaker Punch
                    Vector2 worldbreakerPos = playerPos + aimDir * 2.5f;
                    result.DamageDealt += DamageArea(worldbreakerPos, radius, damage, knockbackForce: 7f);
                    result.TargetsHit += _hitBuffer.Count;
                    if (ProjectileManager.Instance != null)
                    {
                        ProjectileManager.Instance.FireCustom(
                            Faction.Player,
                            playerPos,
                            aimDir,
                            speed: 12f,
                            radius: 0.5f,
                            damage: damage * 0.5f,
                            lifetime: 1.0f,
                            color: new Color(0.9f, 0.6f, 0.2f),
                            cueId: CueIds.InvaderStrike,
                            pierce: 5
                        );
                    }
                    SpawnVisualBurst(worldbreakerPos, radius * 2.5f, new Color(1f, 0.7f, 0.2f, 1f), 0.6f);
                    CueBus.Raise(CueIds.InvaderStrike, playerPos.x, playerPos.y);
                    break;

                // Branch B — Earth and Stone Magic (Actives 11-20)
                case "cursed_golem_active_11": // Stone Spike
                    Vector2 spikePos = playerPos + aimDir * 2.5f;
                    result.DamageDealt += DamageArea(spikePos, radius, damage, knockbackForce: 4f);
                    result.TargetsHit += _hitBuffer.Count;
                    for (int i = 0; i < _hitBuffer.Count; i++)
                    {
                        if (_hitBuffer[i] is InvaderActor invader && invader.Alive)
                        {
                            invader.ApplySlow(0.35f, 2.5f);
                        }
                    }
                    HazardZoneActor.Spawn(spikePos, radius, duration: 2.0f, damagePerTick: damage * 0.25f, tickInterval: 0.5f, slowPercent: 0.35f, slowDuration: 1.5f, color: new Color(0.6f, 0.5f, 0.4f, 0.5f), label: "[STONE_SPIKES]");
                    SpawnVisualBurst(spikePos, radius * 2f, new Color(0.7f, 0.6f, 0.45f, 0.9f), 0.4f);
                    CueBus.Raise(CueIds.InvaderStrike, spikePos.x, spikePos.y);
                    break;

                case "cursed_golem_active_12": // Boulder Throw
                    if (ProjectileManager.Instance != null)
                    {
                        ProjectileManager.Instance.FireCustom(
                            Faction.Player,
                            playerPos,
                            aimDir,
                            speed: 11f,
                            radius: 0.45f,
                            damage: damage,
                            lifetime: 1.2f,
                            color: new Color(0.7f, 0.6f, 0.45f),
                            cueId: CueIds.InvaderStrike,
                            pierce: rank >= 15 ? 2 : 1
                        );
                    }
                    result.TargetsHit += 1;
                    CueBus.Raise(CueIds.InvaderStrike, playerPos.x, playerPos.y);
                    break;

                case "cursed_golem_active_13": // Rockfall
                    Vector2 rockfallPos = playerPos + aimDir * 3.0f;
                    result.DamageDealt += DamageArea(rockfallPos, radius, damage, knockbackForce: 4.5f);
                    result.TargetsHit += _hitBuffer.Count;
                    HazardZoneActor.Spawn(rockfallPos, radius, duration: 2.5f, damagePerTick: damage * 0.25f, tickInterval: 0.4f, slowPercent: 0.3f, slowDuration: 1.0f, color: new Color(0.6f, 0.5f, 0.35f, 0.7f), label: "[ROCKFALL]");
                    SpawnVisualBurst(rockfallPos, radius * 2.2f, new Color(0.65f, 0.55f, 0.4f, 0.9f), 0.5f);
                    CueBus.Raise(CueIds.InvaderStrike, rockfallPos.x, rockfallPos.y);
                    break;

                case "cursed_golem_active_14": // Stone Wall
                    float wallBarrier = 50f * (1f + (rank >= 5 ? 0.30f : 0f));
                    _player.SetBarrier(wallBarrier, duration: def.Duration);
                    result.BarrierGranted += wallBarrier;
                    result.DamageDealt += DamageArea(playerPos, radius, damage, knockbackForce: 4f);
                    result.TargetsHit += _hitBuffer.Count;
                    SpawnVisualBurst(playerPos, radius * 2f, new Color(0.55f, 0.5f, 0.45f, 0.85f), 0.45f);
                    CueBus.Raise(CueIds.InvaderStrike, playerPos.x, playerPos.y);
                    break;

                case "cursed_golem_active_15": // Earth Pillar
                    Vector2 earthPillarPos = playerPos + aimDir * 2.5f;
                    result.DamageDealt += DamageArea(earthPillarPos, radius, damage, knockbackForce: 6f);
                    result.TargetsHit += _hitBuffer.Count;
                    if (rank >= 20)
                    {
                        result.DamageDealt += DamageArea(earthPillarPos + aimDir * 1.5f, radius, damage * 0.6f, knockbackForce: 6f);
                        result.TargetsHit += _hitBuffer.Count;
                    }
                    SpawnVisualBurst(earthPillarPos, radius * 2f, new Color(0.6f, 0.5f, 0.35f, 0.95f), 0.45f);
                    CueBus.Raise(CueIds.InvaderStrike, earthPillarPos.x, earthPillarPos.y);
                    break;

                case "cursed_golem_active_16": // Rolling Boulder
                    if (ProjectileManager.Instance != null)
                    {
                        ProjectileManager.Instance.FireCustom(
                            Faction.Player,
                            playerPos,
                            aimDir,
                            speed: 9f,
                            radius: 0.6f,
                            damage: damage,
                            lifetime: 1.5f,
                            color: new Color(0.65f, 0.55f, 0.4f),
                            cueId: CueIds.InvaderStrike,
                            pierce: 8
                        );
                    }
                    Vector2 rollImpact = playerPos + aimDir * 3f;
                    result.DamageDealt += DamageArea(rollImpact, radius, damage * 0.5f, knockbackForce: 5f);
                    result.TargetsHit += _hitBuffer.Count;
                    SpawnVisualBurst(rollImpact, radius * 2f, new Color(0.7f, 0.6f, 0.5f, 0.9f), 0.45f);
                    CueBus.Raise(CueIds.InvaderStrike, playerPos.x, playerPos.y);
                    break;

                case "cursed_golem_active_17": // Crystal Eruption
                    result.DamageDealt += DamageArea(playerPos, radius, damage, knockbackForce: 5f);
                    result.TargetsHit += _hitBuffer.Count;
                    if (ProjectileManager.Instance != null)
                    {
                        int shards = rank >= 20 ? 8 : 4;
                        for (int s = 0; s < shards; s++)
                        {
                            float angle = s * (360f / shards) * Mathf.Deg2Rad;
                            Vector2 sDir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
                            ProjectileManager.Instance.FireCustom(
                                Faction.Player,
                                playerPos,
                                sDir,
                                speed: 10f,
                                radius: 0.25f,
                                damage: damage * 0.3f,
                                lifetime: 0.8f,
                                color: new Color(0.4f, 0.9f, 0.8f),
                                cueId: CueIds.InvaderStrike,
                                pierce: 2
                            );
                        }
                    }
                    SpawnVisualBurst(playerPos, radius * 2.2f, new Color(0.35f, 0.85f, 0.75f, 0.9f), 0.5f);
                    CueBus.Raise(CueIds.InvaderStrike, playerPos.x, playerPos.y);
                    break;

                case "cursed_golem_active_18": // Tremor Field
                    result.DamageDealt += DamageArea(playerPos, radius, damage, knockbackForce: 3f, pullTowardCenter: rank >= 20);
                    result.TargetsHit += _hitBuffer.Count;
                    for (int i = 0; i < _hitBuffer.Count; i++)
                    {
                        if (_hitBuffer[i] is InvaderActor invader && invader.Alive)
                        {
                            invader.ApplySlow(0.40f, 3.0f);
                        }
                    }
                    HazardZoneActor.Spawn(playerPos, radius, duration: 4.0f, damagePerTick: damage * 0.25f, tickInterval: 0.5f, slowPercent: 0.4f, slowDuration: 1.5f, color: new Color(0.75f, 0.6f, 0.35f, 0.6f), label: "[TREMOR_FIELD]");
                    SpawnVisualBurst(playerPos, radius * 2.2f, new Color(0.75f, 0.6f, 0.35f, 0.9f), 0.55f);
                    CueBus.Raise(CueIds.InvaderStrike, playerPos.x, playerPos.y);
                    break;

                case "cursed_golem_active_19": // Living Stone
                    Vector2 stoneMinionPos = playerPos + aimDir * 2f;
                    UndeadMinionActor.Spawn(UndeadMinionType.Hound, stoneMinionPos, overrideDamage: damage * 0.8f);
                    if (rank >= 20)
                    {
                        UndeadMinionActor.Spawn(UndeadMinionType.Hound, stoneMinionPos + Vector2.up * 1f, overrideDamage: damage * 0.8f);
                    }
                    result.DamageDealt += DamageArea(stoneMinionPos, radius, damage * 0.5f, knockbackForce: 4f);
                    result.TargetsHit += _hitBuffer.Count;
                    SpawnVisualBurst(stoneMinionPos, radius * 2f, new Color(0.6f, 0.55f, 0.5f, 0.9f), 0.45f);
                    CueBus.Raise(CueIds.InvaderStrike, stoneMinionPos.x, stoneMinionPos.y);
                    break;

                case "cursed_golem_active_20": // Mountain's Wrath
                    result.DamageDealt += DamageArea(playerPos, radius, damage, knockbackForce: 7f);
                    result.TargetsHit += _hitBuffer.Count;
                    for (int i = 0; i < _hitBuffer.Count; i++)
                    {
                        if (_hitBuffer[i] is InvaderActor invader && invader.Alive)
                        {
                            invader.ApplySlow(0.50f, 3.5f);
                        }
                    }
                    HazardZoneActor.Spawn(playerPos, radius, duration: 3.0f, damagePerTick: damage * 0.35f, tickInterval: 0.4f, slowPercent: 0.5f, slowDuration: 2.0f, color: new Color(0.85f, 0.5f, 0.2f, 0.7f), label: "[MOUNTAINS_WRATH]");
                    SpawnVisualBurst(playerPos, radius * 2.5f, new Color(0.9f, 0.55f, 0.2f, 0.95f), 0.7f);
                    CueBus.Raise(CueIds.InvaderStrike, playerPos.x, playerPos.y);
                    break;

                // Branch C — Ancient Runic Magic (Actives 21-30)
                case "cursed_golem_active_21": // Rune of Crushing
                    Vector2 crushingPos = playerPos + aimDir * 1.5f;
                    result.DamageDealt += DamageArea(crushingPos, radius, damage, knockbackForce: 4.5f);
                    result.TargetsHit += _hitBuffer.Count;
                    if (rank >= 20)
                    {
                        result.DamageDealt += DamageArea(crushingPos + aimDir * 1f, radius * 1.2f, 20f, knockbackForce: 4f);
                        result.TargetsHit += _hitBuffer.Count;
                    }
                    SpawnVisualBurst(crushingPos, radius * 2f, new Color(0.2f, 0.8f, 0.7f, 0.9f), 0.35f);
                    CueBus.Raise(CueIds.InvaderStrike, crushingPos.x, crushingPos.y);
                    break;

                case "cursed_golem_active_22": // Rune of Protection
                    float barrierHp = 60f * (1f + (rank >= 5 ? 0.30f : 0f));
                    _player.SetBarrier(barrierHp, duration: def.Duration);
                    result.BarrierGranted += barrierHp;
                    result.DamageDealt += DamageArea(playerPos, radius, damage, knockbackForce: 3.5f);
                    result.TargetsHit += _hitBuffer.Count;
                    SpawnVisualBurst(playerPos, radius * 2f, new Color(0.1f, 0.8f, 0.7f, 0.85f), 0.4f);
                    CueBus.Raise(CueIds.InvaderStrike, playerPos.x, playerPos.y);
                    break;

                case "cursed_golem_active_23": // Rune of Binding
                    Vector2 runeBindPos = playerPos + aimDir * 2.5f;
                    result.DamageDealt += DamageArea(runeBindPos, radius, damage, knockbackForce: 0f);
                    result.TargetsHit += _hitBuffer.Count;
                    for (int i = 0; i < _hitBuffer.Count; i++)
                    {
                        if (_hitBuffer[i] is InvaderActor invader && invader.Alive)
                        {
                            invader.ApplySlow(0.80f, 2.0f * (1f + (rank >= 5 ? 0.30f : 0f)));
                        }
                    }
                    SpawnVisualBurst(runeBindPos, radius * 2f, new Color(0.3f, 0.85f, 0.75f, 0.9f), 0.5f);
                    CueBus.Raise(CueIds.InvaderStrike, runeBindPos.x, runeBindPos.y);
                    break;

                case "cursed_golem_active_24": // Rune of Reflection
                    float reflectBarrier = 65f * (1f + (rank >= 5 ? 0.30f : 0f));
                    _player.SetBarrier(reflectBarrier, duration: def.Duration);
                    result.BarrierGranted += reflectBarrier;
                    result.DamageDealt += DamageArea(playerPos, radius, damage, knockbackForce: 4f);
                    result.TargetsHit += _hitBuffer.Count;
                    SpawnVisualBurst(playerPos, radius * 2f, new Color(0.4f, 0.95f, 0.85f, 0.9f), 0.45f);
                    CueBus.Raise(CueIds.InvaderStrike, playerPos.x, playerPos.y);
                    break;

                case "cursed_golem_active_25": // Rune of Detonation
                    Vector2 detPos = playerPos + aimDir * 2.5f;
                    result.DamageDealt += DamageArea(detPos, radius, damage, knockbackForce: 5.5f);
                    result.TargetsHit += _hitBuffer.Count;
                    HazardZoneActor.Spawn(detPos, radius, duration: 3.0f, damagePerTick: damage * 0.3f, tickInterval: 0.4f, slowPercent: 0.3f, slowDuration: 1.0f, color: new Color(0.2f, 0.9f, 0.6f, 0.6f), label: "[DETONATION_RUNE]");
                    SpawnVisualBurst(detPos, radius * 2.2f, new Color(0.2f, 0.9f, 0.6f, 0.95f), 0.5f);
                    CueBus.Raise(CueIds.InvaderStrike, detPos.x, detPos.y);
                    break;

                case "cursed_golem_active_26": // Rune of Reinforcement
                    float reinfBarrier = 75f * (1f + (rank >= 5 ? 0.30f : 0f));
                    _player.SetBarrier(reinfBarrier, duration: def.Duration);
                    result.BarrierGranted += reinfBarrier;
                    float reinfHeal = rank >= 15 ? 30f : 15f;
                    _player.Health?.Heal(reinfHeal);
                    result.HealingDone += reinfHeal;
                    result.DamageDealt += DamageArea(playerPos, radius, damage, knockbackForce: 3f);
                    result.TargetsHit += _hitBuffer.Count;
                    SpawnVisualBurst(playerPos, radius * 2f, new Color(0.25f, 0.8f, 0.9f, 0.85f), 0.5f);
                    CueBus.Raise(CueIds.InvaderStrike, playerPos.x, playerPos.y);
                    break;

                case "cursed_golem_active_27": // Rune of Reversal
                    float revKb = rank >= 5 ? 8f : 5f;
                    result.DamageDealt += DamageArea(playerPos, radius, damage, knockbackForce: revKb);
                    result.TargetsHit += _hitBuffer.Count;
                    if (ProjectileManager.Instance != null && rank >= 20)
                    {
                        for (int p = 0; p < 6; p++)
                        {
                            float ang = p * 60f * Mathf.Deg2Rad;
                            Vector2 pDir = new Vector2(Mathf.Cos(ang), Mathf.Sin(ang));
                            ProjectileManager.Instance.FireCustom(
                                Faction.Player,
                                playerPos,
                                pDir,
                                speed: 10f,
                                radius: 0.3f,
                                damage: damage * 0.4f,
                                lifetime: 0.8f,
                                color: new Color(0.2f, 0.85f, 0.7f),
                                cueId: CueIds.InvaderStrike,
                                pierce: 2
                            );
                        }
                    }
                    SpawnVisualBurst(playerPos, radius * 2.2f, new Color(0.3f, 0.9f, 0.8f, 0.9f), 0.45f);
                    CueBus.Raise(CueIds.InvaderStrike, playerPos.x, playerPos.y);
                    break;

                case "cursed_golem_active_28": // Rune of Resonance
                    Vector2 resPos = playerPos + aimDir * 2.5f;
                    result.DamageDealt += DamageArea(resPos, radius, damage, knockbackForce: 4f);
                    result.TargetsHit += _hitBuffer.Count;
                    SpawnVisualBurst(resPos, radius * 2.2f, new Color(0.4f, 0.95f, 0.75f, 0.9f), 0.5f);
                    CueBus.Raise(CueIds.InvaderStrike, resPos.x, resPos.y);
                    break;

                case "cursed_golem_active_29": // Ancient Rune Circle
                    _player.SetBarrier(60f, duration: 5.0f);
                    result.BarrierGranted += 60f;
                    result.DamageDealt += DamageArea(playerPos, radius, damage, knockbackForce: 3f, pullTowardCenter: rank >= 20);
                    result.TargetsHit += _hitBuffer.Count;
                    for (int i = 0; i < _hitBuffer.Count; i++)
                    {
                        if (_hitBuffer[i] is InvaderActor invader && invader.Alive)
                        {
                            invader.ApplySlow(0.40f, 3.0f);
                        }
                    }
                    HazardZoneActor.Spawn(playerPos, radius, duration: 5.0f, damagePerTick: damage * 0.3f, tickInterval: 0.5f, slowPercent: 0.4f, slowDuration: 1.5f, color: new Color(0.15f, 0.85f, 0.7f, 0.5f), label: "[ANCIENT_RUNE_CIRCLE]");
                    SpawnVisualBurst(playerPos, radius * 2.2f, new Color(0.15f, 0.85f, 0.7f, 0.9f), 0.6f);
                    CueBus.Raise(CueIds.InvaderStrike, playerPos.x, playerPos.y);
                    break;

                case "cursed_golem_active_30": // Runeheart Awakening
                    float awakeningBarrier = rank >= 15 ? 160f : 100f;
                    _player.SetBarrier(awakeningBarrier, duration: def.Duration);
                    result.BarrierGranted += awakeningBarrier;
                    result.DamageDealt += DamageArea(playerPos, radius, damage, knockbackForce: 7f);
                    result.TargetsHit += _hitBuffer.Count;
                    HazardZoneActor.Spawn(playerPos, radius, duration: 4.0f, damagePerTick: damage * 0.35f, tickInterval: 0.4f, slowPercent: 0.5f, slowDuration: 2.0f, color: new Color(0.3f, 0.95f, 0.85f, 0.6f), label: "[RUNEHEART]");
                    SpawnVisualBurst(playerPos, radius * 2.5f, new Color(0.3f, 0.95f, 0.85f, 0.95f), 0.8f);
                    CueBus.Raise(CueIds.InvaderStrike, playerPos.x, playerPos.y);
                    break;

                // Branch D — Fortress and Defense (Actives 31-40)
                case "cursed_golem_active_31": // Granite Skin
                    float graniteBarrier = (rank >= 20 ? 120f : 60f) * (1f + (rank >= 5 ? 0.25f : 0f));
                    _player.SetBarrier(graniteBarrier, duration: def.Duration);
                    result.BarrierGranted += graniteBarrier;
                    result.DamageDealt += DamageArea(playerPos, radius, damage, knockbackForce: 3f);
                    result.TargetsHit += _hitBuffer.Count;
                    SpawnVisualBurst(playerPos, radius * 1.8f, new Color(0.6f, 0.6f, 0.65f, 0.9f), 0.35f);
                    CueBus.Raise(CueIds.InvaderStrike, playerPos.x, playerPos.y);
                    break;

                case "cursed_golem_active_32": // Living Fortress
                    float livingFortressBarrier = 75f * (1f + (rank >= 5 ? 0.30f : 0f));
                    _player.SetBarrier(livingFortressBarrier, duration: def.Duration);
                    result.BarrierGranted += livingFortressBarrier;
                    result.DamageDealt += DamageArea(playerPos, radius, damage, knockbackForce: 4.5f);
                    result.TargetsHit += _hitBuffer.Count;
                    for (int i = 0; i < _hitBuffer.Count; i++)
                    {
                        if (_hitBuffer[i] is InvaderActor invader && invader.Alive)
                        {
                            invader.ApplySlow(0.45f, 2.5f);
                        }
                    }
                    SpawnVisualBurst(playerPos, radius * 2f, new Color(0.55f, 0.5f, 0.45f, 0.9f), 0.45f);
                    CueBus.Raise(CueIds.InvaderStrike, playerPos.x, playerPos.y);
                    break;

                case "cursed_golem_active_33": // Cursed Regeneration
                    float regenHealth = rank >= 5 ? 54f : 40f;
                    _player.Health?.Heal(regenHealth);
                    result.HealingDone += regenHealth;
                    float regenBarrier = rank >= 10 ? 50f : 30f;
                    _player.SetBarrier(regenBarrier, duration: def.Duration);
                    result.BarrierGranted += regenBarrier;
                    result.DamageDealt += DamageArea(playerPos, radius, damage, knockbackForce: 2f);
                    result.TargetsHit += _hitBuffer.Count;
                    SpawnVisualBurst(playerPos, radius * 1.8f, new Color(0.4f, 0.75f, 0.4f, 0.9f), 0.4f);
                    CueBus.Raise(CueIds.InvaderStrike, playerPos.x, playerPos.y);
                    break;

                case "cursed_golem_active_34": // Obsidian Shell
                    float obsidianBarrier = 85f * (1f + (rank >= 5 ? 0.30f : 0f));
                    _player.SetBarrier(obsidianBarrier, duration: def.Duration);
                    result.BarrierGranted += obsidianBarrier;
                    result.DamageDealt += DamageArea(playerPos, radius, damage, knockbackForce: 3.5f);
                    result.TargetsHit += _hitBuffer.Count;
                    if (ProjectileManager.Instance != null)
                    {
                        int obsShards = rank >= 20 ? 8 : 4;
                        for (int s = 0; s < obsShards; s++)
                        {
                            float angle = s * (360f / obsShards) * Mathf.Deg2Rad;
                            Vector2 sDir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
                            ProjectileManager.Instance.FireCustom(
                                Faction.Player,
                                playerPos,
                                sDir,
                                speed: 9.5f,
                                radius: 0.25f,
                                damage: damage * 0.35f,
                                lifetime: 0.75f,
                                color: new Color(0.2f, 0.15f, 0.25f),
                                cueId: CueIds.InvaderStrike,
                                pierce: 2
                            );
                        }
                    }
                    SpawnVisualBurst(playerPos, radius * 2f, new Color(0.3f, 0.2f, 0.4f, 0.9f), 0.45f);
                    CueBus.Raise(CueIds.InvaderStrike, playerPos.x, playerPos.y);
                    break;

                case "cursed_golem_active_35": // Retaliation Quake
                    float retKb = rank >= 20 ? 8f : 5.5f;
                    result.DamageDealt += DamageArea(playerPos, radius, damage, knockbackForce: retKb);
                    result.TargetsHit += _hitBuffer.Count;
                    SpawnVisualBurst(playerPos, radius * 2.2f, new Color(0.7f, 0.55f, 0.35f, 0.9f), 0.45f);
                    CueBus.Raise(CueIds.InvaderStrike, playerPos.x, playerPos.y);
                    break;

                case "cursed_golem_active_36": // Stoneheart Guard
                    float stoneheartBarrier = 95f * (1f + (rank >= 5 ? 0.30f : 0f));
                    _player.SetBarrier(stoneheartBarrier, duration: def.Duration);
                    result.BarrierGranted += stoneheartBarrier;
                    result.DamageDealt += DamageArea(playerPos, radius, damage, knockbackForce: 3f);
                    result.TargetsHit += _hitBuffer.Count;
                    float stoneheartSlow = rank >= 20 ? 0.60f : 0.35f;
                    for (int i = 0; i < _hitBuffer.Count; i++)
                    {
                        if (_hitBuffer[i] is InvaderActor invader && invader.Alive)
                        {
                            invader.ApplySlow(stoneheartSlow, 2.5f);
                        }
                    }
                    SpawnVisualBurst(playerPos, radius * 2f, new Color(0.65f, 0.5f, 0.6f, 0.9f), 0.5f);
                    CueBus.Raise(CueIds.InvaderStrike, playerPos.x, playerPos.y);
                    break;

                case "cursed_golem_active_37": // Immovable Titan
                    float titanBarrier = 90f * (1f + (rank >= 5 ? 0.35f : 0f));
                    _player.SetBarrier(titanBarrier, duration: def.Duration);
                    result.BarrierGranted += titanBarrier;
                    result.DamageDealt += DamageArea(playerPos, radius, damage, knockbackForce: 6f);
                    result.TargetsHit += _hitBuffer.Count;
                    SpawnVisualBurst(playerPos, radius * 2.4f, new Color(0.5f, 0.5f, 0.55f, 0.95f), 0.55f);
                    CueBus.Raise(CueIds.InvaderStrike, playerPos.x, playerPos.y);
                    break;

                case "cursed_golem_active_38": // Fortress Roar
                    result.DamageDealt += DamageArea(playerPos, radius, damage, knockbackForce: 4.5f);
                    result.TargetsHit += _hitBuffer.Count;
                    float roarSlow = rank >= 20 ? 0.70f : 0.45f;
                    for (int i = 0; i < _hitBuffer.Count; i++)
                    {
                        if (_hitBuffer[i] is InvaderActor invader && invader.Alive)
                        {
                            invader.ApplySlow(roarSlow, 3.0f);
                        }
                    }
                    SpawnVisualBurst(playerPos, radius * 2.2f, new Color(0.85f, 0.65f, 0.45f, 0.9f), 0.5f);
                    CueBus.Raise(CueIds.InvaderStrike, playerPos.x, playerPos.y);
                    break;

                case "cursed_golem_active_39": // Ancient Reassembly
                    float reassemblyHeal = rank >= 5 ? 84f : 60f;
                    _player.Health?.Heal(reassemblyHeal);
                    result.HealingDone += reassemblyHeal;
                    float reassemblyBarrier = rank >= 10 ? 120f : 80f;
                    _player.SetBarrier(reassemblyBarrier, duration: def.Duration);
                    result.BarrierGranted += reassemblyBarrier;
                    result.DamageDealt += DamageArea(playerPos, radius, damage, knockbackForce: 3f);
                    result.TargetsHit += _hitBuffer.Count;
                    SpawnVisualBurst(playerPos, radius * 2f, new Color(0.45f, 0.8f, 0.6f, 0.9f), 0.55f);
                    CueBus.Raise(CueIds.InvaderStrike, playerPos.x, playerPos.y);
                    break;

                case "cursed_golem_active_40": // Adamantine Form
                    float adamantineBarrier = rank >= 5 ? 200f : 150f;
                    _player.SetBarrier(adamantineBarrier, duration: def.Duration);
                    result.BarrierGranted += adamantineBarrier;
                    result.DamageDealt += DamageArea(playerPos, radius, damage, knockbackForce: 7.5f);
                    result.TargetsHit += _hitBuffer.Count;
                    HazardZoneActor.Spawn(playerPos, radius, duration: 4.5f, damagePerTick: damage * 0.35f, tickInterval: 0.45f, slowPercent: 0.5f, slowDuration: 2.0f, color: new Color(0.4f, 0.7f, 0.8f, 0.6f), label: "[ADAMANTINE_ZONE]");
                    SpawnVisualBurst(playerPos, radius * 2.6f, new Color(0.4f, 0.75f, 0.9f, 0.95f), 0.75f);
                    CueBus.Raise(CueIds.InvaderStrike, playerPos.x, playerPos.y);
                    break;

                // Branch E — Gravity and Cataclysm (Actives 41-50)
                case "cursed_golem_active_41": // Gravity Pull
                    Vector2 pullPos = playerPos + aimDir * 3.0f;
                    result.DamageDealt += DamageArea(pullPos, radius, damage, knockbackForce: -5f, pullTowardCenter: true);
                    result.TargetsHit += _hitBuffer.Count;
                    SpawnVisualBurst(pullPos, radius * 2f, new Color(0.5f, 0.2f, 0.6f, 0.9f), 0.4f);
                    CueBus.Raise(CueIds.InvaderStrike, pullPos.x, pullPos.y);
                    break;

                case "cursed_golem_active_42": // Crushing Field
                    Vector2 fieldPos = playerPos + aimDir * 3.0f;
                    result.DamageDealt += DamageArea(fieldPos, radius, damage, knockbackForce: 0f);
                    result.TargetsHit += _hitBuffer.Count;
                    for (int i = 0; i < _hitBuffer.Count; i++)
                    {
                        if (_hitBuffer[i] is InvaderActor invader && invader.Alive)
                        {
                            invader.ApplySlow(0.50f, 3.0f);
                        }
                    }
                    HazardZoneActor.Spawn(fieldPos, radius, duration: 3.5f, damagePerTick: damage * 0.3f, tickInterval: 0.4f, slowPercent: 0.50f, slowDuration: 1.5f, color: new Color(0.45f, 0.15f, 0.55f, 0.6f), label: "[CRUSHING_FIELD]");
                    SpawnVisualBurst(fieldPos, radius * 2f, new Color(0.45f, 0.15f, 0.55f, 0.9f), 0.5f);
                    CueBus.Raise(CueIds.InvaderStrike, fieldPos.x, fieldPos.y);
                    break;

                case "cursed_golem_active_43": // Meteoric Fist
                    Vector2 meteoricPos = playerPos + aimDir * 3.0f;
                    result.DamageDealt += DamageArea(meteoricPos, radius, damage, knockbackForce: 6f);
                    result.TargetsHit += _hitBuffer.Count;
                    SpawnVisualBurst(meteoricPos, radius * 2.2f, new Color(0.85f, 0.35f, 0.15f, 0.95f), 0.5f);
                    CueBus.Raise(CueIds.InvaderStrike, meteoricPos.x, meteoricPos.y);
                    break;

                case "cursed_golem_active_44": // Gravitic Pulse
                    float pulseKb = rank >= 5 ? 9f : 6f;
                    result.DamageDealt += DamageArea(playerPos, radius, damage, knockbackForce: pulseKb);
                    result.TargetsHit += _hitBuffer.Count;
                    SpawnVisualBurst(playerPos, radius * 2.4f, new Color(0.6f, 0.3f, 0.75f, 0.9f), 0.45f);
                    CueBus.Raise(CueIds.InvaderStrike, playerPos.x, playerPos.y);
                    break;

                case "cursed_golem_active_45": // Stone Orbit
                    float orbitBarrier = 70f * (1f + (rank >= 5 ? 0.30f : 0f));
                    _player.SetBarrier(orbitBarrier, duration: def.Duration);
                    result.BarrierGranted += orbitBarrier;
                    result.DamageDealt += DamageArea(playerPos, radius, damage, knockbackForce: 3.5f);
                    result.TargetsHit += _hitBuffer.Count;
                    if (ProjectileManager.Instance != null)
                    {
                        int orbitProjectiles = rank >= 20 ? 6 : 4;
                        for (int p = 0; p < orbitProjectiles; p++)
                        {
                            float ang = p * (360f / orbitProjectiles) * Mathf.Deg2Rad;
                            Vector2 oDir = new Vector2(Mathf.Cos(ang), Mathf.Sin(ang));
                            ProjectileManager.Instance.FireCustom(
                                Faction.Player,
                                playerPos,
                                oDir,
                                speed: 8f,
                                radius: 0.3f,
                                damage: damage * 0.35f,
                                lifetime: 0.9f,
                                color: new Color(0.55f, 0.45f, 0.6f),
                                cueId: CueIds.InvaderStrike,
                                pierce: 3
                            );
                        }
                    }
                    SpawnVisualBurst(playerPos, radius * 2f, new Color(0.55f, 0.45f, 0.6f, 0.9f), 0.5f);
                    CueBus.Raise(CueIds.InvaderStrike, playerPos.x, playerPos.y);
                    break;

                case "cursed_golem_active_46": // Gravity Prison
                    Vector2 prisonPos = playerPos + aimDir * 3.0f;
                    result.DamageDealt += DamageArea(prisonPos, radius, damage, knockbackForce: -4f, pullTowardCenter: true);
                    result.TargetsHit += _hitBuffer.Count;
                    for (int i = 0; i < _hitBuffer.Count; i++)
                    {
                        if (_hitBuffer[i] is InvaderActor invader && invader.Alive)
                        {
                            invader.ApplySlow(0.65f, 3.0f);
                        }
                    }
                    HazardZoneActor.Spawn(prisonPos, radius, duration: 4.0f, damagePerTick: damage * 0.35f, tickInterval: 0.4f, slowPercent: 0.65f, slowDuration: 1.5f, color: new Color(0.35f, 0.1f, 0.45f, 0.65f), label: "[GRAVITY_PRISON]");
                    SpawnVisualBurst(prisonPos, radius * 2.2f, new Color(0.4f, 0.15f, 0.5f, 0.9f), 0.55f);
                    CueBus.Raise(CueIds.InvaderStrike, prisonPos.x, prisonPos.y);
                    break;

                case "cursed_golem_active_47": // Falling Mountain
                    Vector2 mtnPos = playerPos + aimDir * 3.5f;
                    result.DamageDealt += DamageArea(mtnPos, radius, damage, knockbackForce: 7f);
                    result.TargetsHit += _hitBuffer.Count;
                    HazardZoneActor.Spawn(mtnPos, radius, duration: 3.5f, damagePerTick: damage * 0.3f, tickInterval: 0.45f, slowPercent: 0.45f, slowDuration: 1.5f, color: new Color(0.6f, 0.45f, 0.35f, 0.6f), label: "[FALLING_MOUNTAIN]");
                    SpawnVisualBurst(mtnPos, radius * 2.5f, new Color(0.65f, 0.5f, 0.35f, 0.95f), 0.65f);
                    CueBus.Raise(CueIds.InvaderStrike, mtnPos.x, mtnPos.y);
                    break;

                case "cursed_golem_active_48": // Worldstone Core
                    float worldstoneBarrier = 110f * (1f + (rank >= 5 ? 0.35f : 0f));
                    _player.SetBarrier(worldstoneBarrier, duration: def.Duration);
                    result.BarrierGranted += worldstoneBarrier;
                    result.DamageDealt += DamageArea(playerPos, radius, damage, knockbackForce: 4f);
                    result.TargetsHit += _hitBuffer.Count;
                    SpawnVisualBurst(playerPos, radius * 2.2f, new Color(0.65f, 0.35f, 0.8f, 0.9f), 0.55f);
                    CueBus.Raise(CueIds.InvaderStrike, playerPos.x, playerPos.y);
                    break;

                case "cursed_golem_active_49": // Titan's Judgment
                    Vector2 judgmentPos = playerPos + aimDir * 3.5f;
                    result.DamageDealt += DamageArea(judgmentPos, radius, damage, knockbackForce: 8f);
                    result.TargetsHit += _hitBuffer.Count;
                    HazardZoneActor.Spawn(judgmentPos, radius, duration: 4.0f, damagePerTick: damage * 0.4f, tickInterval: 0.4f, slowPercent: 0.55f, slowDuration: 2.0f, color: new Color(0.7f, 0.3f, 0.2f, 0.65f), label: "[TITANS_JUDGMENT]");
                    SpawnVisualBurst(judgmentPos, radius * 2.6f, new Color(0.8f, 0.35f, 0.2f, 0.95f), 0.7f);
                    CueBus.Raise(CueIds.InvaderStrike, judgmentPos.x, judgmentPos.y);
                    break;

                case "cursed_golem_active_50": // Colossus Ascension
                    float ascensionBarrier = rank >= 5 ? 220f : 160f;
                    _player.SetBarrier(ascensionBarrier, duration: def.Duration);
                    result.BarrierGranted += ascensionBarrier;
                    result.DamageDealt += DamageArea(playerPos, radius, damage, knockbackForce: 9f);
                    result.TargetsHit += _hitBuffer.Count;
                    HazardZoneActor.Spawn(playerPos, radius, duration: 5.0f, damagePerTick: damage * 0.4f, tickInterval: 0.45f, slowPercent: 0.6f, slowDuration: 2.5f, color: new Color(0.55f, 0.25f, 0.7f, 0.65f), label: "[COLOSSUS_ASCENSION]");
                    SpawnVisualBurst(playerPos, radius * 3f, new Color(0.6f, 0.25f, 0.75f, 0.95f), 0.85f);
                    CueBus.Raise(CueIds.InvaderStrike, playerPos.x, playerPos.y);
                    break;

                // Milestone Ultimates
                case "cursed_golem_ultimate_50": // Colossus Awakening
                    _player.SetBarrier(120f, duration: def.Duration);
                    result.BarrierGranted += 120f;
                    result.DamageDealt += DamageArea(playerPos, radius, damage, knockbackForce: 5f);
                    result.TargetsHit += _hitBuffer.Count;
                    SpawnVisualBurst(playerPos, radius * 2f, new Color(0.9f, 0.75f, 0.3f, 0.95f), 0.6f);
                    CueBus.Raise(CueIds.InvaderStrike, playerPos.x, playerPos.y);
                    break;

                case "cursed_golem_ultimate_100": // Faultline Dominion
                    result.DamageDealt += DamageArea(playerPos, radius, damage, knockbackForce: 6f);
                    result.TargetsHit += _hitBuffer.Count;
                    for (int i = 0; i < _hitBuffer.Count; i++)
                    {
                        if (_hitBuffer[i] is InvaderActor invader && invader.Alive)
                        {
                            invader.ApplySlow(0.50f, 4.0f);
                        }
                    }
                    SpawnVisualBurst(playerPos, radius * 2.2f, new Color(0.8f, 0.5f, 0.2f, 0.95f), 0.7f);
                    CueBus.Raise(CueIds.InvaderStrike, playerPos.x, playerPos.y);
                    break;

                case "cursed_golem_ultimate_150": // Heart of the Mountain
                    float mountainBarrier = 160f * (1f + (rank >= 15 ? 0.3125f : 0f));
                    _player.SetBarrier(mountainBarrier, duration: def.Duration);
                    result.BarrierGranted += mountainBarrier;
                    float healAmt = rank >= 20 ? 80f : 40f;
                    _player.Health?.Heal(healAmt);
                    result.HealingDone += healAmt;
                    result.DamageDealt += DamageArea(playerPos, radius, damage, knockbackForce: 4f);
                    result.TargetsHit += _hitBuffer.Count;
                    SpawnVisualBurst(playerPos, radius * 2.2f, new Color(0.3f, 0.9f, 0.6f, 0.95f), 0.7f);
                    CueBus.Raise(CueIds.InvaderStrike, playerPos.x, playerPos.y);
                    break;

                case "cursed_golem_ultimate_200": // Gravitic Cataclysm
                    result.DamageDealt += DamageArea(playerPos, radius, damage, knockbackForce: 2f, pullTowardCenter: true);
                    result.TargetsHit += _hitBuffer.Count;
                    SpawnVisualBurst(playerPos, radius * 2.5f, new Color(0.5f, 0.2f, 0.9f, 0.95f), 0.8f);
                    CueBus.Raise(CueIds.InvaderStrike, playerPos.x, playerPos.y);
                    break;

                case "cursed_golem_ultimate_250": // Worldstone Avatar
                    float avatarBarrier = rank >= 20 ? 300f : 200f;
                    _player.SetBarrier(avatarBarrier, duration: def.Duration);
                    result.BarrierGranted += avatarBarrier;
                    result.DamageDealt += DamageArea(playerPos, radius, damage, knockbackForce: 8f);
                    result.TargetsHit += _hitBuffer.Count;
                    SpawnVisualBurst(playerPos, radius * 2.5f, new Color(1f, 0.85f, 0.3f, 1f), 1.0f);
                    CueBus.Raise(CueIds.InvaderStrike, playerPos.x, playerPos.y);
                    break;

                default:
                    // Generic fallback execution
                    if (def.Effects.HasFlag(AbilityEffectType.RadialAoE))
                    {
                        result.DamageDealt += DamageArea(playerPos, radius, damage);
                        result.TargetsHit += _hitBuffer.Count;
                        SpawnVisualBurst(playerPos, radius * 2f, Color.white, 0.3f);
                    }
                    break;
            }

            return result;
        }

        public Vector2 ValidateLandingDestination(Vector2 start, Vector2 target)
        {
            float halfW = ArenaConfig.HalfWidth - 1.5f;
            float halfH = ArenaConfig.HalfHeight - 1.5f;
            target.x = Mathf.Clamp(target.x, -halfW, halfW);
            target.y = Mathf.Clamp(target.y, -halfH, halfH);

            var layout = _run?.Layout;
            if (layout == null) return target;

            bool insideObstacle = true;
            int steps = 10;
            Vector2 current = target;

            for (int s = 0; s < steps; s++)
            {
                var pt = new Vector2D(current.x, current.y);
                insideObstacle = false;

                for (int w = 0; w < layout.Walls.Count; w++)
                {
                    if (layout.Walls[w].Contains(pt, 0.8f))
                    {
                        insideObstacle = true;
                        break;
                    }
                }

                if (!insideObstacle)
                {
                    for (int ob = 0; ob < layout.Obstacles.Count; ob++)
                    {
                        if (layout.Obstacles[ob].Contains(pt, 0.8f))
                        {
                            insideObstacle = true;
                            break;
                        }
                    }
                }

                if (!insideObstacle)
                    return current;

                current = Vector2.Lerp(target, start, (s + 1) / (float)steps);
            }

            return start;
        }

        private float DamageArea(
            Vector2 center,
            float radius,
            float damage,
            float knockbackForce = 0f,
            float slowPercent = 0f,
            float slowDuration = 0f,
            bool pullTowardCenter = false)
        {
            float totalDealt = 0f;
            _hitBuffer.Clear();

            var all = ActorRegistry.AllActors;
            for (int i = 0; i < all.Count; i++)
            {
                var a = all[i];
                if (a.Team == Faction.Invader && a.Alive)
                {
                    float dist = Vector2.Distance(center, a.Position);
                    if (dist <= radius)
                    {
                        _hitBuffer.Add(a);
                    }
                }
            }

            for (int i = 0; i < _hitBuffer.Count; i++)
            {
                var target = _hitBuffer[i];
                float dealt = target.ReceiveDamage(damage);
                totalDealt += dealt;

                if (target is InvaderActor invader)
                {
                    if (slowPercent > 0f && slowDuration > 0f)
                    {
                        invader.ApplySlow(slowPercent, slowDuration);
                    }

                    if (pullTowardCenter)
                    {
                        Vector2 pullDir = (center - invader.Position).normalized;
                        invader.ApplyKnockback(pullDir * 2.5f);
                    }
                    else if (knockbackForce > 0f)
                    {
                        Vector2 pushDir = (invader.Position - center).normalized;
                        invader.ApplyKnockback(pushDir * knockbackForce);
                    }
                }
            }

            return totalDealt;
        }

        private float DamageArc(
            Vector2 origin,
            Vector2 direction,
            float radius,
            float angleDegrees,
            float damage,
            float knockbackForce = 0f)
        {
            float totalDealt = 0f;
            _hitBuffer.Clear();

            var all = ActorRegistry.AllActors;
            for (int i = 0; i < all.Count; i++)
            {
                var a = all[i];
                if (a.Team == Faction.Invader && a.Alive)
                {
                    Vector2 toTarget = a.Position - origin;
                    float dist = toTarget.magnitude;
                    if (dist <= radius)
                    {
                        float angle = Vector2.Angle(direction, toTarget);
                        if (angle <= angleDegrees * 0.5f)
                        {
                            _hitBuffer.Add(a);
                        }
                    }
                }
            }

            for (int i = 0; i < _hitBuffer.Count; i++)
            {
                var target = _hitBuffer[i];
                float dealt = target.ReceiveDamage(damage);
                totalDealt += dealt;

                if (knockbackForce > 0f && target is InvaderActor invader)
                {
                    Vector2 pushDir = (invader.Position - origin).normalized;
                    invader.ApplyKnockback(pushDir * knockbackForce);
                }
            }

            return totalDealt;
        }

        private static void SpawnVisualBurst(Vector2 pos, float size, Color color, float duration)
        {
            var go = new GameObject("BurstVisual");
            go.transform.position = pos;
            go.transform.localScale = new Vector3(size, size, 1f);

            var sr = go.AddComponent<SpriteRenderer>();
            sr.sprite = PlaceholderSprites.Square;
            sr.color = color;
            sr.sortingOrder = 9;

            go.AddComponent<TemporaryFade>().Initialize(duration);
        }

        private static void SpawnVisualArc(Vector2 pos, Vector2 dir, float radius, Color color, float duration)
        {
            var go = new GameObject("ArcVisual");
            go.transform.position = pos + dir * (radius * 0.5f);
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            go.transform.rotation = Quaternion.Euler(0f, 0f, angle);
            go.transform.localScale = new Vector3(radius, radius * 0.7f, 1f);

            var sr = go.AddComponent<SpriteRenderer>();
            sr.sprite = PlaceholderSprites.Square;
            sr.color = color;
            sr.sortingOrder = 9;

            go.AddComponent<TemporaryFade>().Initialize(duration);
        }
    }

    public sealed class TemporaryFade : MonoBehaviour
    {
        private float _duration;
        private float _elapsed;
        private SpriteRenderer _sr;
        private Color _initialColor;

        public void Initialize(float duration)
        {
            _duration = duration;
            _sr = GetComponent<SpriteRenderer>();
            if (_sr != null) _initialColor = _sr.color;
        }

        private void Update()
        {
            _elapsed += Time.deltaTime;
            if (_elapsed >= _duration)
            {
                Destroy(gameObject);
                return;
            }

            if (_sr != null)
            {
                float a = 1f - (_elapsed / _duration);
                _sr.color = new Color(_initialColor.r, _initialColor.g, _initialColor.b, _initialColor.a * a);
            }
        }
    }
}
