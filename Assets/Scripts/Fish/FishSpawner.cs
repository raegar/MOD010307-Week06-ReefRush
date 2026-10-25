using UnityEngine;

namespace ReefRush
{
    /// <summary>
    /// Fills the reef with fish when the game starts.
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

        private void Start()
        {
            for (int i = 0; i < startCount; i++)
            {
                SpawnFish();
            }
        }

        private void SpawnFish()
        {
            Vector2 position = new Vector2(Random.Range(Reef.xMin, Reef.xMax), Random.Range(Reef.yMin, Reef.yMax));
            Fish fish = Instantiate(fishPrefab, position, Quaternion.identity, transform);

            // Random.Range with two ints never returns the second number, so this is always a valid index.
            int index = Random.Range(0, fishSprites.Length);
            fish.Setup(fishSprites[index], Random.Range(1f, 2.5f));
        }
    }
}
