using System;
using FinalBoss.Core;
using UnityEngine;

namespace FinalBoss.Game
{
    /// <summary>
    /// Backwards-compatible controller extending <see cref="AbilityLoadoutController"/>.
    /// Handles slot 0 signature ability while simultaneously empowering full 5-slot loadouts,
    /// autocasting, and mastery progression.
    /// </summary>
    public sealed class SignatureAbilityController : AbilityLoadoutController
    {
    }
}
