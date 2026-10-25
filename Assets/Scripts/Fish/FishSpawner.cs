using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ReefRush
{
    /// <summary>
    /// Spawns fish, keeps a List of every fish on the reef, moves them all in one loop,
    /// and removes them again when they are caught or swim away.
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

        [Tooltip("How close (in world units) a tap must be to a fish to catch it.")]
        [SerializeField, Min(0.1f)] private float catchRadius = 0.6f;

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
            // It runs BACKWARDS, from the last fish to the first, so that removing a fish
            // can't make the loop skip the one after it.
            float deltaTime = Time.deltaTime;
            int countBefore = allFish.Count;
            for (int i = allFish.Count - 1; i >= 0; i--)
            {
                Fish fish = allFish[i];
                fish.Swim(deltaTime);
                if (fish.IsOffReef)
                {
                    allFish.RemoveAt(i);      // forget it first...
                    Destroy(fish.gameObject); // ...then remove it from the scene
                }
            }
            if (allFish.Count != countBefore) ShowCount();
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

        /// <summary>The fish nearest to a point, or null if none is within catchRadius.</summary>
        public Fish FishAt(Vector2 point)
        {
            Fish nearest = null;
            float nearestDistance = catchRadius;
            foreach (Fish fish in allFish)
            {
                float distance = Vector2.Distance(fish.transform.position, point);
                if (distance < nearestDistance)
                {
                    nearest = fish;
                    nearestDistance = distance;
                }
            }
            return nearest;
        }

        /// <summary>Take a fish off the reef because the player caught it.</summary>
        public void Catch(Fish fish)
        {
            // Remove returns false if the fish wasn't in the list (already caught), so it can't be caught twice.
            if (!allFish.Remove(fish)) return;
            Destroy(fish.gameObject);
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
