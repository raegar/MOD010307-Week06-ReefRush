using System;

namespace ReefRush
{
    /// <summary>One line of the high score table.</summary>
    [Serializable]
    public class ScoreEntry
    {
        public string initials;
        public int score;
        public int wave;

        public ScoreEntry(string initials, int score, int wave)
        {
            this.initials = initials;
            this.score = score;
            this.wave = wave;
        }
    }
}
