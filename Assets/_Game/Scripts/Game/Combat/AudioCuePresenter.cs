using System.Collections.Generic;
using FinalBoss.Core;
using UnityEngine;

namespace FinalBoss.Game
{
    /// <summary>
    /// Presentation audio listener that maps CueBus requests to spatial sound effects.
    /// Operates purely on presentation events without affecting simulation truth.
    /// </summary>
    public sealed class AudioCuePresenter : MonoBehaviour
    {
        private AudioSource _source;
        private readonly Dictionary<string, AudioClip> _clipCache = new Dictionary<string, AudioClip>();

        private void Awake()
        {
            _source = gameObject.AddComponent<AudioSource>();
            _source.playOnAwake = false;
            _source.spatialBlend = 0f; // 2D flat mix for top-down retro audio

            GenerateSyntheticClips();
            CueBus.Requested += OnCueRequested;
        }

        private void OnDestroy()
        {
            CueBus.Requested -= OnCueRequested;
        }

        private void OnCueRequested(CueRequest req)
        {
            if (string.IsNullOrEmpty(req.Id) || _source == null) return;

            if (_clipCache.TryGetValue(req.Id, out var clip) && clip != null)
            {
                // Pitch variance to avoid audio fatigue
                _source.pitch = Random.Range(0.92f, 1.08f);
                _source.PlayOneShot(clip, 0.45f);
            }
        }

        private void GenerateSyntheticClips()
        {
            // Procedural synthesized chiptune retro sound effects
            _clipCache[CueIds.BoltFire] = CreateToneClip("Sfx_BoltFire", 440f, 220f, 0.08f);
            _clipCache[CueIds.PlayerDash] = CreateToneClip("Sfx_PlayerDash", 600f, 300f, 0.12f, isNoise: true);
            _clipCache[CueIds.PlayerHit] = CreateToneClip("Sfx_PlayerHit", 180f, 90f, 0.15f);
            _clipCache[CueIds.PlayerDeath] = CreateToneClip("Sfx_PlayerDeath", 240f, 60f, 0.45f);
            _clipCache[CueIds.InvaderDeath] = CreateToneClip("Sfx_InvaderDeath", 320f, 120f, 0.10f);
            _clipCache[CueIds.ShrineActivated] = CreateToneClip("Sfx_Shrine", 523f, 1046f, 0.35f);
            _clipCache[CueIds.CrusadeEvent] = CreateToneClip("Sfx_Crusade", 330f, 660f, 0.40f);
            _clipCache[CueIds.SpellCastFire] = CreateToneClip("Sfx_SpellFire", 400f, 180f, 0.14f, isNoise: true);
            _clipCache[CueIds.SpellCastIce] = CreateToneClip("Sfx_SpellIce", 880f, 440f, 0.12f);
            _clipCache[CueIds.SpellCastLightning] = CreateToneClip("Sfx_SpellLightning", 950f, 200f, 0.10f, isNoise: true);
        }

        private static AudioClip CreateToneClip(string name, float startFreq, float endFreq, float duration, bool isNoise = false)
        {
            const int sampleRate = 22050;
            int sampleCount = (int)(sampleRate * duration);
            float[] samples = new float[sampleCount];

            float phase = 0f;
            for (int i = 0; i < sampleCount; i++)
            {
                float t = (float)i / sampleCount;
                float freq = Mathf.Lerp(startFreq, endFreq, t);
                phase += 2f * Mathf.PI * freq / sampleRate;

                float envelope = 1f - t; // Linear decay
                float sampleVal;

                if (isNoise)
                {
                    sampleVal = (Random.value * 2f - 1f) * 0.4f + Mathf.Sin(phase) * 0.6f;
                }
                else
                {
                    sampleVal = Mathf.Sin(phase);
                }

                samples[i] = sampleVal * envelope * 0.5f;
            }

            var clip = AudioClip.Create(name, sampleCount, 1, sampleRate, false);
            clip.SetData(samples, 0);
            return clip;
        }
    }
}
