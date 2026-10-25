using UnityEngine;
using UnityEngine.InputSystem;

namespace ReefRush
{
    /// <summary>
    /// Turns a tap (or a mouse click) into a caught fish.
    /// </summary>
    public sealed class TapCatcher : MonoBehaviour
    {
        [SerializeField] private FishSpawner spawner;

        private Camera mainCamera;

        private void Start()
        {
            mainCamera = Camera.main;
        }

        private void Update()
        {
            // Pointer.current is the mouse in the editor, or a finger on a phone.
            Pointer pointer = Pointer.current;
            if (pointer == null || !pointer.press.wasPressedThisFrame) return;

            Vector2 tap = mainCamera.ScreenToWorldPoint(pointer.position.ReadValue());
            Fish fish = spawner.FishAt(tap);
            if (fish != null) spawner.Catch(fish);
        }
    }
}
