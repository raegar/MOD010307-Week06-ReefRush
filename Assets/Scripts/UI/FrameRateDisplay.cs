using UnityEngine;
using UnityEngine.UI;

namespace ReefRush
{
    /// <summary>
    /// Shows the frame rate and frame time, smoothed so the numbers are readable.
    /// </summary>
    public sealed class FrameRateDisplay : MonoBehaviour
    {
        [SerializeField] private Text label;

        [Tooltip("Seconds between updates of the text.")]
        [SerializeField, Min(0.05f)] private float refreshSeconds = 0.25f;

        private float frameTime;
        private float untilRefresh;

        private void Update()
        {
            // unscaledDeltaTime: the real time this frame took, even if the game is paused or slowed down.
            // Lerp moves frameTime 10% of the way towards this frame's time, so one slow frame doesn't make it jump.
            float thisFrame = Time.unscaledDeltaTime;
            frameTime = frameTime == 0f ? thisFrame : Mathf.Lerp(frameTime, thisFrame, 0.1f);

            untilRefresh -= Time.unscaledDeltaTime;
            if (untilRefresh > 0f) return;
            untilRefresh = refreshSeconds;

            label.text = $"{1f / frameTime:0} FPS  ({frameTime * 1000f:0.0} ms)";
        }
    }
}
