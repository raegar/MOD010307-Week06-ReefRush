using UnityEngine;

namespace ReefRush
{
    /// <summary>
    /// For the stress test only: makes one fish move itself, with its own Update,
    /// instead of being moved by the FishSpawner's loop. The spawner adds it when Use Manager Loop is off.
    /// </summary>
    public sealed class SelfSwimmer : MonoBehaviour
    {
        private Fish fish;

        private void Awake()
        {
            fish = GetComponent<Fish>();
        }

        private void Update()
        {
            fish.Swim(Time.deltaTime);
        }
    }
}
