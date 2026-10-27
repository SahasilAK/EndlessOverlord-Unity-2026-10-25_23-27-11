# Swordsman (`InvaderArchetype.Swordsman`) — Invader Audio & SFX Prompt Specification

> **Asset Category:** Hero Invader Audio #01 (`03_Invaders`)
> **Catalog Enum:** `InvaderArchetype.Swordsman` (`InvaderCombatRole.Melee`)
> **Acoustic Identity:** Crisp steel blade/blunt weapon swings + medium chainmail/plate footsteps + gritty human warrior grunts.

---

## 1. Base Sound Identity & Mix Rules
- **Timbre Profile:** Crisp steel blade/blunt weapon swings + medium chainmail/plate footsteps + gritty human warrior grunts.
- **Crowd Mix Control:** Because up to 25 invaders can be active simultaneously, invader cues must be **short (`0.15s–0.35s`)**, band-passed between `300 Hz` and `5.5 kHz`, and capped at `-5 dBFS` so they never muddy the low-end reserved for the Player Boss.

---

## 2. Step-by-Step Audio Variations

1. **Portal Spawn Arrival (`sfx_inv_swordsman_spawn.wav` — `CueIds.InvaderSpawn` / `"invader_spawn"`):**
   > `Quick arcane portal rift pop followed by a subtle melee gear rattle for Swordsman, 0.28s, mono 44.1kHz WAV.`
2. **Attack Windup & Release (`sfx_inv_swordsman_attack.wav` — `CueIds.InvaderAttackWindup` / `"invader_attack_windup"`):**
   > `Distinct 0.5s telegraph cue and strike for Swordsman (Standard frontline melee infantry with balanced strikes.): Crisp steel blade/blunt weapon swings + medium chainmail/plate footsteps + gritty human warrior grunts., crisp attack transient, 0.35s, mono 44.1kHz WAV.`
3. **Damaged / Hit Reaction (`sfx_inv_swordsman_hit.wav` — `CueIds.InvaderHit` / `"invader_hit"`):**
   > `Punchy flesh-and-armor impact thud with brief melee stagger grunt, 0.18s, mono 44.1kHz WAV.`
4. **Defeat / Soul Drop (`sfx_inv_swordsman_death.wav` — `CueIds.InvaderDeath` / `"invader_death"`):**
   > `Defeated hero armor collapse and weapon clatter onto stone floor with a subtle soul essence release chime, 0.42s, mono 44.1kHz WAV.`

---

## 3. Unity `CueBus` Integration Table

| Action | `CueIds` Constant | Cue String | Target `.wav` Path | Pitch Range |
| :--- | :--- | :--- | :--- | :--- |
| **Spawn** | `CueIds.InvaderSpawn` | `"invader_spawn"` | `Assets/_Game/Audio/SFX/Invaders/sfx_inv_swordsman_spawn.wav` | `0.94 - 1.06` |
| **Attack** | `CueIds.InvaderAttackWindup` | `"invader_attack_windup"` | `Assets/_Game/Audio/SFX/Invaders/sfx_inv_swordsman_attack.wav` | `0.92 - 1.08` |
| **Hit** | `CueIds.InvaderHit` | `"invader_hit"` | `Assets/_Game/Audio/SFX/Invaders/sfx_inv_swordsman_hit.wav` | `0.90 - 1.12` |
| **Death** | `CueIds.InvaderDeath` | `"invader_death"` | `Assets/_Game/Audio/SFX/Invaders/sfx_inv_swordsman_death.wav` | `0.90 - 1.10` |
