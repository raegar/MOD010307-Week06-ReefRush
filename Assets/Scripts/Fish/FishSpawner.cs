using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ReefRush
{
    /// <summary>
    /// Spawns fish, keeps a List of every fish on the reef, and moves them all in one loop.
    /// </summary>
    public sealed class FishSpawner : MonoBehaviour
    {
        // The area fish swim in: x from -4 to 4, y from -5 to 5.5. static: one reef, shared by every script.
        public static readonly Rect Reef = new Rect(-4f, -5f, 8f, 10.5f);

        [SerializeField] private Fish fishPrefab;

        [Tooltip("The looks a fish can have. Each new fish picks one at random.")]
        [SerializeField] private Sprite[] fishSprites;

        [Tooltip("How many fish to spawn when the game starts.")]
        [SerializeField, Min(0)] private int startCount = 10;

        [Tooltip("How many fish the Swarm button adds.")]
        [SerializeField, Min(1)] private int swarmSize = 500;

        [SerializeField] private Text countText;

        // Every fish on the reef right now. It starts empty and grows as fish are added.
        private readonly List<Fish> allFish = new List<Fish>();

        public int Count => allFish.Count;

        private void Start()
        {
            for (int i = 0; i < startCount; i++)
            {
                SpawnFish();
            }
            ShowCount();
        }

        private void Update()
        {
            // One loop moves every fish, however many there are.
            float deltaTime = Time.deltaTime;
            for (int i = 0; i < allFish.Count; i++)
            {
                allFish[i].Swim(deltaTime);
            }
        }

        /// <summary>Called by the Swarm button.</summary>
        public void Swarm()
        {
            for (int i = 0; i < swarmSize; i++)
            {
                SpawnFish();
            }
            ShowCount();
        }

        private void SpawnFish()
        {
            Vector2 position = new Vector2(Random.Range(Reef.xMin, Reef.xMax), Random.Range(Reef.yMin, Reef.yMax));
            Fish fish = Instantiate(fishPrefab, position, Quaternion.identity, transform);

            // Random.Range with two ints never returns the second number, so this is always a valid index.
            int index = Random.Range(0, fishSprites.Length);
            fish.Setup(fishSprites[index], Random.Range(1f, 2.5f));

            allFish.Add(fish);
        }

        private void ShowCount()
        {
            countText.text = $"Fish: {allFish.Count}";
        }
    }
}
