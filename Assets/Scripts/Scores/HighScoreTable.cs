using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace ReefRush
{
    /// <summary>
    /// The top scores, highest first. Saved on the device, so they are still there next time the game opens.
    /// </summary>
    public sealed class HighScoreTable : MonoBehaviour
    {
        private const string SaveKey = "ReefRush.HighScores";
        private const string HighlightColour = "#FFD54F";

        [SerializeField, Min(1)] private int maxEntries = 10;

        [Header("UI: one Text per column")]
        [SerializeField] private Text ranksText;
        [SerializeField] private Text initialsText;
        [SerializeField] private Text scoresText;

        private List<ScoreEntry> entries = new List<ScoreEntry>();

        // JsonUtility can't save a List on its own, only an object, so the list is wrapped in one.
        [Serializable]
        private class SaveData
        {
            public List<ScoreEntry> entries;
        }

        private void Awake()
        {
            Load();
            Show(-1);
        }

        /// <summary>Add a score. Returns its place in the table (0 = top), or -1 if it didn't make the table.</summary>
        public int Add(ScoreEntry entry)
        {
            entries.Add(entry);
            entries.Sort(HighestFirst);

            // Keep only the top few: remove everything from position maxEntries to the end.
            if (entries.Count > maxEntries)
            {
                entries.RemoveRange(maxEntries, entries.Count - maxEntries);
            }

            Save();
            int place = entries.IndexOf(entry);   // -1 if it was just removed
            Show(place);
            return place;
        }

        /// <summary>
        /// Tells Sort which of two entries goes first. Return a negative number if a goes first,
        /// a positive number if b goes first, or 0 if they are equal.
        /// </summary>
        private static int HighestFirst(ScoreEntry a, ScoreEntry b)
        {
            // b compared with a (not a with b) puts the HIGHER score first.
            int byScore = b.score.CompareTo(a.score);
            if (byScore != 0) return byScore;

            // A tie-breaker for equal scores: whoever reached the later wave goes first.
            return b.wave.CompareTo(a.wave);
        }

        private void Show(int highlight)
        {
            string ranks = "";
            string initials = "";
            string scores = "";
            for (int i = 0; i < entries.Count; i++)
            {
                string colour = i == highlight ? HighlightColour : "#FFFFFF";
                ranks += $"<color={colour}>{i + 1}.</color>\n";
                initials += $"<color={colour}>{entries[i].initials}</color>\n";
                scores += $"<color={colour}>{entries[i].score}</color>\n";
            }
            if (entries.Count == 0) initials = "No scores yet";

            ranksText.text = ranks;
            initialsText.text = initials;
            scoresText.text = scores;
        }

        private void Save()
        {
            string json = JsonUtility.ToJson(new SaveData { entries = entries });
            PlayerPrefs.SetString(SaveKey, json);
            PlayerPrefs.Save();
        }

        private void Load()
        {
            string json = PlayerPrefs.GetString(SaveKey, "");
            if (json == "") return;

            SaveData data = JsonUtility.FromJson<SaveData>(json);
            if (data != null && data.entries != null) entries = data.entries;
        }

        // ---------------------------------------------------------------- testing

        [ContextMenu("Add Test Score")]
        private void AddTestScore()
        {
            string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string initials = "";
            for (int i = 0; i < 3; i++)
            {
                initials += letters[Random.Range(0, letters.Length)];
            }
            Add(new ScoreEntry(initials, Random.Range(1, 60) * 10, Random.Range(1, 5)));
        }

        [ContextMenu("Clear Scores")]
        private void ClearScores()
        {
            entries.Clear();
            Save();
            Show(-1);
        }
    }
}
