using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ReefRush
{
    /// <summary>
    /// The target card: a few fish to catch, shown as icons. Catch one that is on the card for a bonus;
    /// catch them all for a bigger bonus and a new card.
    /// </summary>
    public sealed class TargetCard : MonoBehaviour
    {
        [SerializeField] private FishSpawner spawner;

        [Tooltip("One Image per place on the card. The card holds as many targets as there are icons.")]
        [SerializeField] private Image[] icons;

        [SerializeField, Min(0)] private int targetBonus = 25;
        [SerializeField, Min(0)] private int fullCardBonus = 100;

        // The fish still to catch. A List, because it shrinks as targets are caught.
        private readonly List<FishType> targets = new List<FishType>();

        private void Start()
        {
            Deal();
        }

        /// <summary>Fill the card with new targets, picked at random.</summary>
        public void Deal()
        {
            targets.Clear();
            for (int i = 0; i < icons.Length; i++)
            {
                targets.Add(spawner.RandomFishInfo().type);
            }
            ShowTargets();
        }

        /// <summary>Cross a caught fish off the card. Returns the bonus points it earned (0 if it wasn't a target).</summary>
        public int Tick(FishType caught)
        {
            // Remove takes out the FIRST matching item, and tells us whether it found one.
            if (!targets.Remove(caught)) return 0;

            if (targets.Count > 0)
            {
                ShowTargets();
                return targetBonus;
            }

            // That was the last one: a full card.
            Deal();
            return targetBonus + fullCardBonus;
        }

        private void ShowTargets()
        {
            // icons and targets are two collections kept in step by using the same index, i.
            for (int i = 0; i < icons.Length; i++)
            {
                bool hasTarget = i < targets.Count;
                icons[i].enabled = hasTarget;
                if (hasTarget) icons[i].sprite = spawner.InfoFor(targets[i]).sprite;
            }
        }
    }
}
