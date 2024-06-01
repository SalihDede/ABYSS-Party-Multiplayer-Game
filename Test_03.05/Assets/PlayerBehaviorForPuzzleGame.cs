using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KinematicCharacterController;
using KinematicCharacterController.Examples;
using System.Linq;

namespace KinematicCharacterController.Walkthrough.MultipleMovementStates
{
    public class PlayerBehaviorForPuzzleGame : MonoBehaviour
    {
        public ExampleCharacterCamera OrbitCamera;
        public Transform CameraFollowPoint;
        public MyCharacterController Character;
        public PuzzleManager puzzleManager; // Reference to the PuzzleManager

        public float pushPullForce = 3f; // Push and Pull Force

        private const string MouseXInput = "Mouse X";
        private const string MouseYInput = "Mouse Y";
        private const string MouseScrollInput = "Mouse ScrollWheel";
        private const string HorizontalInput = "Horizontal";
        private const string VerticalInput = "Vertical";

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;

          
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Cursor.lockState = CursorLockMode.Locked;
                HandleTileSelection(); // Handle tile selection on mouse click
            }

            HandlePuzzleInteraction(); // Handle puzzle interactions
        }


        // Handle tile selection and movement
        private void HandleTileSelection()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Cast a ray from the camera to the mouse position
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) // Check if the ray hit any object
            {
                Transform selectedTile = hit.transform; // Get the transform of the hit object
                // Add logic to handle the selected tile if needed
            }
        }

        // Handle puzzle interactions
        private void HandlePuzzleInteraction()
        {
            if (Input.GetKey(KeyCode.P))
            {
                PullPuzzleObjects();
            }
            else if (Input.GetKey(KeyCode.U))
            {
                PushPuzzleObjects();
            }
        }

        private void PullPuzzleObjects()
        {
            // Find the objects nearby within a 3-unit radius
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, 3f);
            foreach (Collider col in hitColliders)
            {
                if (col.CompareTag("PUZZLE"))
                {
                    Rigidbody rb = col.GetComponent<Rigidbody>();
                    if (rb != null)
                    {
                        Vector3 directionToPlayer = (transform.position - col.transform.position).normalized;
                        rb.AddForce(directionToPlayer * pushPullForce);
                    }
                }
            }
        }

        private void PushPuzzleObjects()
        {
            // Find the objects nearby within a 3-unit radius
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, 3f);
            foreach (Collider col in hitColliders)
            {
                if (col.CompareTag("PUZZLE"))
                {
                    Rigidbody rb = col.GetComponent<Rigidbody>();
                    if (rb != null)
                    {
                        Vector3 directionFromPlayer = (col.transform.position - transform.position).normalized;
                        rb.AddForce(directionFromPlayer * pushPullForce);
                    }
                }
            }
        }
    }
}
