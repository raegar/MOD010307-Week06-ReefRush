using System.Collections.Generic;
using UnityEngine;

namespace ReefRush
{
    /// <summary>
    /// Friday's collection exercises. Add this component to any GameObject, press Play, then run each drill
    /// from the component's ⋮ menu. For each one: PREDICT what the Console will show, RUN it, EXPLAIN what
    /// happened, then FIX it (most of them have a bug on purpose).
    /// </summary>
    public sealed class CollectionDrills : MonoBehaviour
    {
        [ContextMenu("1 Print the scores")]
        private void PrintScores()
        {
            int[] scores = { 120, 340, 90 };
            for (int i = 0; i <= scores.Length; i++)
            {
                Debug.Log($"Score {i}: {scores[i]}");
            }
        }

        [ContextMenu("2 First player")]
        private void FirstPlayer()
        {
            List<string> players = new List<string>();
            Debug.Log("First player: " + players[0]);
        }

        [ContextMenu("3 Remove the small fish (foreach)")]
        private void RemoveSmallForeach()
        {
            List<int> fishSizes = new List<int> { 5, 1, 7, 2, 2, 9 };
            foreach (int size in fishSizes)
            {
                if (size < 3) fishSizes.Remove(size);
            }
            Debug.Log("Big fish: " + string.Join(", ", fishSizes));
        }

        [ContextMenu("4 Remove the small fish (for)")]
        private void RemoveSmallFor()
        {
            List<int> fishSizes = new List<int> { 5, 1, 7, 2, 2, 9 };
            for (int i = 0; i < fishSizes.Count; i++)
            {
                if (fishSizes[i] < 3) fishSizes.RemoveAt(i);
            }
            Debug.Log("Big fish: " + string.Join(", ", fishSizes));
        }

        [ContextMenu("5 Back up the inventory")]
        private void BackUpInventory()
        {
            List<string> inventory = new List<string> { "Net", "Bait" };
            List<string> backup = inventory;
            inventory.Add("Rod");
            Debug.Log($"Inventory has {inventory.Count} items; the backup has {backup.Count}.");
        }

        [ContextMenu("6 Best score")]
        private void BestScore()
        {
            List<int> scores = new List<int> { 300, 120, 450, 80 };
            scores.Sort();
            Debug.Log("Best score: " + scores[0]);
        }

        [ContextMenu("7 Find the highest (write this one)")]
        private void FindHighest()
        {
            int[] depths = { 12, 45, 7, 45, 30 };
            int highest = 0;
            // TODO: use a loop to find the highest depth, without using Sort or Max.
            Debug.Log("Deepest dive: " + highest);
        }

        [ContextMenu("8 Count the gone fish")]
        private void CountGoneFish()
        {
            List<GameObject> fish = new List<GameObject>();
            for (int i = 0; i < 3; i++) fish.Add(new GameObject("Drill fish " + i));

            DestroyImmediate(fish[1]);   // caught and destroyed, but nobody told the list
            Debug.Log("Fish in the list: " + fish.Count);
            foreach (GameObject f in fish)
            {
                Debug.Log("Fish: " + f.name);
            }
            foreach (GameObject f in fish) if (f != null) Destroy(f);   // tidy up
        }

        [ContextMenu("9 Total the catch (use a breakpoint)")]
        private void TotalCatch()
        {
            int[] catches = { 3, 5, 2, 6, 4 };
            int total = 0;
            for (int i = 1; i < catches.Length; i += 2)
            {
                total += catches[i];
            }
            Debug.Log($"Total caught: {total} (should be 20)");
        }

        [ContextMenu("10 Tally by type")]
        private void TallyByType()
        {
            FishType[] caught = { FishType.Red, FishType.Blue, FishType.Red, FishType.Puffer, FishType.Red };
            Dictionary<FishType, int> tally = new Dictionary<FishType, int>();
            foreach (FishType type in caught)
            {
                tally[type]++;
            }
            foreach (KeyValuePair<FishType, int> pair in tally)
            {
                Debug.Log($"{pair.Key}: {pair.Value}");
            }
        }
    }
}
