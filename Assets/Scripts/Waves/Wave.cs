using System;
using UnityEngine;

namespace ReefRush
{
    /// <summary>
    /// The settings for one wave of fish. [Serializable] lets the Inspector show it,
    /// so the waves can be designed and tuned in the Inspector without changing any code.
    /// </summary>
    [Serializable]
    public class Wave
    {
        [Min(1)] public int fishCount = 10;
        [Tooltip("Seconds between one fish and the next.")]
        [Min(0.05f)] public float spawnInterval = 0.8f;
        [Tooltip("Average swimming speed, in world units per second.")]
        [Min(0.1f)] public float swimSpeed = 1.5f;
    }
}
