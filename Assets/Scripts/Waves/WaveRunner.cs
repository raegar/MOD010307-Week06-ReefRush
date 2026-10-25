using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace ReefRush
{
    /// <summary>
    /// Sends the waves of fish in order. A wave is over when all its fish have been spawned
    /// and every one of them has been caught or has swum away.
    /// </summary>
    public sealed class WaveRunner : MonoBehaviour
    {
        [SerializeField] private FishSpawner spawner;
        [SerializeField] private Text bannerText;
        [SerializeField, Min(0f)] private float bannerSeconds = 1.5f;

        [Tooltip("The waves, in the order they are played.")]
        [SerializeField] private Wave[] waves;

        /// <summary>The wave being played: 1 for the first wave, 0 before the first one starts.</summary>
        public int WaveNumber { get; private set; }
        public int WaveTotal => waves.Length;

        /// <summary>Raised after the last fish of the last wave has gone.</summary>
        public event Action AllWavesCleared;

        private void Start()
        {
            bannerText.enabled = false;
            StartCoroutine(RunWaves());
        }

        // A coroutine: a method that can pause (yield) and carry on from the same place in a later frame.
        private IEnumerator RunWaves()
        {
            for (int w = 0; w < waves.Length; w++)
            {
                Wave wave = waves[w];
                WaveNumber = w + 1;

                bannerText.text = $"Wave {WaveNumber} of {waves.Length}";
                bannerText.enabled = true;
                yield return new WaitForSeconds(bannerSeconds);
                bannerText.enabled = false;

                for (int i = 0; i < wave.fishCount; i++)
                {
                    spawner.SpawnAtEdge(wave.swimSpeed);
                    yield return new WaitForSeconds(wave.spawnInterval);
                }

                // Wait, one frame at a time, until the reef is empty.
                while (spawner.Count > 0)
                {
                    yield return null;
                }
            }

            bannerText.text = "Reef cleared!";
            bannerText.enabled = true;
            AllWavesCleared?.Invoke();
        }
    }
}
