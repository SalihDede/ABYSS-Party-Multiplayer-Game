using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Add this to use the UI components
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
        public Image puzzleImage; // Reference to the Image component

        public float pushPullForce = 3f; // Push and Pull Force

        private const string MouseXInput = "Mouse X";
        private const string MouseYInput = "Mouse Y";
        private const string MouseScrollInput = "Mouse ScrollWheel";
        private const string HorizontalInput = "Horizontal";
        private const string VerticalInput = "Vertical";

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;

            // Tell camera to follow transform
            OrbitCamera.SetFollowTransform(CameraFollowPoint);

            // Ignore the character's collider(s) for camera obstruction checks
            OrbitCamera.IgnoredColliders.Clear();
            OrbitCamera.IgnoredColliders.AddRange(Character.GetComponentsInChildren<Collider>());

            // Ensure the image is initially hidden
            puzzleImage.gameObject.SetActive(false);
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Cursor.lockState = CursorLockMode.Locked;
                HandleTileSelection(); // Handle tile selection on mouse click
            }

            HandleCharacterInput();
            HandlePuzzleInteraction(); // Handle puzzle interactions
            HandleImageToggle(); // Handle toggling the image visibility
        }

        private void LateUpdate()
        {
            HandleCameraInput();
        }

        private void HandleCameraInput()
        {
            // Create the look input vector for the camera
            float mouseLookAxisUp = Input.GetAxisRaw(MouseYInput);
            float mouseLookAxisRight = Input.GetAxisRaw(MouseXInput);
            Vector3 lookInputVector = new Vector3(mouseLookAxisRight, mouseLookAxisUp, 0f);

            // Prevent moving the camera while the cursor isn't locked
            if (Cursor.lockState != CursorLockMode.Locked)
            {
                lookInputVector = Vector3.zero;
            }

            // Input for zooming the camera (disabled in WebGL because it can cause problems)
            float scrollInput = -Input.GetAxis(MouseScrollInput);
#if UNITY_WEBGL
            scrollInput = 0f;
#endif

            // Apply inputs to the camera
            OrbitCamera.UpdateWithInput(Time.deltaTime, scrollInput, lookInputVector);

            // Handle toggling zoom level
            if (Input.GetMouseButtonDown(1))
            {
                OrbitCamera.TargetDistance = (OrbitCamera.TargetDistance == 0f) ? OrbitCamera.DefaultDistance : 0f;
            }
        }

        private void HandleCharacterInput()
        {
            PlayerCharacterInputs characterInputs = new PlayerCharacterInputs();

            // Build the CharacterInputs struct
            characterInputs.MoveAxisForward = Input.GetAxisRaw(VerticalInput);
            characterInputs.MoveAxisRight = Input.GetAxisRaw(HorizontalInput);
            characterInputs.CameraRotation = OrbitCamera.Transform.rotation;
            characterInputs.JumpDown = Input.GetKeyDown(KeyCode.Space);
            characterInputs.CrouchDown = Input.GetKeyDown(KeyCode.C);
            characterInputs.CrouchUp = Input.GetKeyUp(KeyCode.C);

            // Apply inputs to character
            Character.SetInputs(ref characterInputs);
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

        // Handle toggling the image visibility
        private void HandleImageToggle()
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                puzzleImage.gameObject.SetActive(!puzzleImage.gameObject.activeSelf);
            }
        }
    }
}
