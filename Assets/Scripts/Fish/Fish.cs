using UnityEngine;

namespace ReefRush
{
    /// <summary>
    /// One fish: it swims left or right across the reef until it swims off the other side.
    /// It has no Update of its own: the FishSpawner moves every fish in one loop by calling Swim.
    /// </summary>
    public sealed class Fish : MonoBehaviour
    {
        [Tooltip("World units per second.")]
        [SerializeField, Min(0f)] private float speed = 1.5f;

        private SpriteRenderer sprite;
        private float direction = 1f;   // 1 = swimming right, -1 = swimming left

        /// <summary>What kind of fish this is (set by the spawner).</summary>
        public FishInfo Info { get; private set; }
        public FishType Type => Info.type;
        public int Points => Info.points;

        /// <summary>True once the fish is a whole body-length past the edge it was swimming towards.</summary>
        public bool IsOffReef
        {
            get
            {
                float x = transform.position.x;
                return direction > 0f ? x > FishSpawner.Reef.xMax + 1f : x < FishSpawner.Reef.xMin - 1f;
            }
        }

        private void Awake()
        {
            sprite = GetComponent<SpriteRenderer>();
        }

        /// <summary>Called by the spawner straight after it creates the fish.</summary>
        public void Setup(FishInfo info, float swimSpeed)
        {
            Info = info;
            sprite.sprite = info.sprite;
            speed = swimSpeed;
            direction = Random.value < 0.5f ? -1f : 1f;
            sprite.flipX = direction < 0f;   // the sprites face right, so flip the ones swimming left
        }

        /// <summary>Move for one frame. deltaTime is passed in by whoever is moving the fish.</summary>
        public void Swim(float deltaTime)
        {
            transform.position += Vector3.right * (direction * speed * deltaTime);
        }
    }
}
