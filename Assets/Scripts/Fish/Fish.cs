using UnityEngine;

namespace ReefRush
{
    /// <summary>
    /// One fish: it swims left or right across the reef and turns round at the edges.
    /// </summary>
    public sealed class Fish : MonoBehaviour
    {
        [Tooltip("World units per second.")]
        [SerializeField, Min(0f)] private float speed = 1.5f;

        private SpriteRenderer sprite;
        private float direction = 1f;   // 1 = swimming right, -1 = swimming left

        private void Awake()
        {
            sprite = GetComponent<SpriteRenderer>();
        }

        /// <summary>Called by the spawner straight after it creates the fish.</summary>
        public void Setup(Sprite look, float swimSpeed)
        {
            sprite.sprite = look;
            speed = swimSpeed;
            direction = Random.value < 0.5f ? -1f : 1f;
            sprite.flipX = direction < 0f;   // the sprites face right, so flip the ones swimming left
        }

        private void Update()
        {
            transform.position += Vector3.right * (direction * speed * Time.deltaTime);

            // Turn round at the edges of the reef.
            float x = transform.position.x;
            bool pastRightEdge = x > FishSpawner.Reef.xMax && direction > 0f;
            bool pastLeftEdge = x < FishSpawner.Reef.xMin && direction < 0f;
            if (pastRightEdge || pastLeftEdge)
            {
                direction = -direction;
                sprite.flipX = direction < 0f;
            }
        }
    }
}
