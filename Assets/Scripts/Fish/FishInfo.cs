using System;
using UnityEngine;

namespace ReefRush
{
    /// <summary>
    /// Everything that is different about one kind of fish. [Serializable] lets the Inspector show it,
    /// so a whole array of them can be filled in on the FishSpawner without writing any code.
    /// </summary>
    [Serializable]
    public class FishInfo
    {
        public FishType type;
        public Sprite sprite;
        [Min(0)] public int points = 10;
    }
}
