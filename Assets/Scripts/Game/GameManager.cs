using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace ReefRush
{
    /// <summary>The states a game of Reef Rush moves through.</summary>
    public enum GameState
    {
        Ready,
        Playing,
        Over
    }

    /// <summary>
    /// Runs a game: waits for Start, sends the waves, ends the game when every wave is cleared
    /// or too many fish get away, then lets the player put their score on the high score table.
    /// </summary>
    public sealed class GameManager : MonoBehaviour
    {
        [Header("Game systems")]
        [SerializeField] private FishSpawner spawner;
        [SerializeField] private WaveRunner waves;
        [SerializeField] private ScoreKeeper scoreKeeper;
        [SerializeField] private TargetCard targetCard;
        [SerializeField] private TapCatcher tapCatcher;
        [SerializeField] private HighScoreTable highScores;

        [Header("Rules")]
        [Tooltip("The game is lost when this many fish have swum away.")]
        [SerializeField, Min(1)] private int maxEscapes = 10;

        [Header("UI")]
        [SerializeField] private GameObject readyPanel;
        [SerializeField] private GameObject swarmButton;
        [SerializeField] private GameObject resultPanel;
        [SerializeField] private Text resultText;
        [SerializeField] private Text escapedText;
        [SerializeField] private GameObject saveRow;
        [SerializeField] private InputField initialsInput;

        public GameState State { get; private set; }

        private int escaped;

        private void OnEnable()
        {
            spawner.FishEscaped += OnFishEscaped;
            waves.AllWavesCleared += OnAllWavesCleared;
        }

        private void OnDisable()
        {
            spawner.FishEscaped -= OnFishEscaped;
            waves.AllWavesCleared -= OnAllWavesCleared;
        }

        private void Start()
        {
            ShowEscaped();
            SetState(GameState.Ready);
        }

        /// <summary>Called by the Start button.</summary>
        public void StartGame()
        {
            if (State != GameState.Ready) return;

            spawner.Clear();   // the swarm you played with on the Ready screen goes
            scoreKeeper.ResetScore();
            targetCard.Deal();
            escaped = 0;
            ShowEscaped();

            SetState(GameState.Playing);
            waves.Begin();
        }

        /// <summary>Called by the Save button: put the score on the high score table.</summary>
        public void SaveScore()
        {
            string initials = initialsInput.text.Trim().ToUpper();
            if (initials == "") initials = "???";

            highScores.Add(new ScoreEntry(initials, scoreKeeper.Score, waves.WaveNumber));
            saveRow.SetActive(false);   // one entry per game
        }

        /// <summary>Called by the Replay button: reload the scene for a fresh game.</summary>
        public void Replay()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        private void OnFishEscaped(Fish fish)
        {
            if (State != GameState.Playing) return;

            escaped++;
            ShowEscaped();
            if (escaped >= maxEscapes) EndGame("Too many got away!");
        }

        private void OnAllWavesCleared()
        {
            if (State == GameState.Playing) EndGame("Reef cleared!");
        }

        private void EndGame(string message)
        {
            waves.Stop();
            resultText.text = $"{message}\nScore: {scoreKeeper.Score}";
            SetState(GameState.Over);
        }

        /// <summary>The only place the state changes, so every change happens the same way.</summary>
        private void SetState(GameState newState)
        {
            State = newState;
            readyPanel.SetActive(newState == GameState.Ready);
            swarmButton.SetActive(newState == GameState.Ready);
            resultPanel.SetActive(newState == GameState.Over);
            tapCatcher.enabled = newState == GameState.Playing;
        }

        private void ShowEscaped()
        {
            escapedText.text = $"Escaped: {escaped}/{maxEscapes}";
        }
    }
}
