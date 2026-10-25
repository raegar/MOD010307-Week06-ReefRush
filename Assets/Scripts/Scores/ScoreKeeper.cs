using UnityEngine;
using UnityEngine.UI;

namespace ReefRush
{
    /// <summary>
    /// Adds up the score: each caught fish's points, plus any bonus from the target card.
    /// </summary>
    public sealed class ScoreKeeper : MonoBehaviour
    {
        [SerializeField] private FishSpawner spawner;
        [SerializeField] private TargetCard targetCard;
        [SerializeField] private Text scoreText;

        public int Score { get; private set; }

        private void OnEnable()
        {
            spawner.FishCaught += OnFishCaught;
        }

        private void OnDisable()
        {
            spawner.FishCaught -= OnFishCaught;
        }

        private void Start()
        {
            ShowScore();
        }

        /// <summary>Back to 0 for a new game.</summary>
        public void ResetScore()
        {
            Score = 0;
            ShowScore();
        }

        private void OnFishCaught(Fish fish)
        {
            Score += fish.Points + targetCard.Tick(fish.Type);
            ShowScore();
        }

        private void ShowScore()
        {
            scoreText.text = $"Score: {Score}";
        }
    }
}
