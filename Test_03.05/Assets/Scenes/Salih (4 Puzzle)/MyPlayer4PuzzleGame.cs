using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KinematicCharacterController;
using KinematicCharacterController.Examples;
using System.Linq;

namespace KinematicCharacterController.Walkthrough.MultipleMovementStates
{
    public class MyPlayer4PuzzleGame : MonoBehaviour
    {
        public ExampleCharacterCamera OrbitCamera;
        public Transform CameraFollowPoint;
        public MyCharacterController Character;

        public float pushPullForce = 1f; // Push and Pull Force

        private const string HorizontalInput = "Horizontal";
        private const string VerticalInput = "Vertical";

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;

            // Camera follow us
            OrbitCamera.SetFollowTransform(CameraFollowPoint);

            // not control collider
            OrbitCamera.IgnoredColliders.Clear();
            OrbitCamera.IgnoredColliders.AddRange(Character.GetComponentsInChildren<Collider>());
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Cursor.lockState = CursorLockMode.Locked;
            }

            HandleCharacterInput();
            HandlePuzzleInteraction(); // Itteration of puzzles tag
        }

        private void LateUpdate()
        {
            HandleCameraInput();
        }

        private void HandleCameraInput()
        {
            // Camera vector (input)
            float mouseLookAxisUp = Input.GetAxisRaw("Mouse Y");
            float mouseLookAxisRight = Input.GetAxisRaw("Mouse X");
            Vector3 lookInputVector = new Vector3(mouseLookAxisRight, mouseLookAxisUp, 0f);

            
            if (Cursor.lockState != CursorLockMode.Locked)
            {
                lookInputVector = Vector3.zero;
            }

            // Move Camera
            float scrollInput = -Input.GetAxis("Mouse ScrollWheel");

            // Apply camera inpts
            OrbitCamera.UpdateWithInput(Time.deltaTime, scrollInput, lookInputVector);

            // Zoom level
            if (Input.GetMouseButtonDown(1))
            {
                OrbitCamera.TargetDistance = (OrbitCamera.TargetDistance == 0f) ? OrbitCamera.DefaultDistance : 0f;
            }
        }

        private void HandleCharacterInput()
        {
            PlayerCharacterInputs characterInputs = new PlayerCharacterInputs();

            // Character inputs
            characterInputs.MoveAxisForward = Input.GetAxisRaw(VerticalInput);
            characterInputs.MoveAxisRight = Input.GetAxisRaw(HorizontalInput);
            characterInputs.CameraRotation = OrbitCamera.Transform.rotation;
            characterInputs.JumpDown = Input.GetKeyDown(KeyCode.Space);
            characterInputs.CrouchDown = Input.GetKeyDown(KeyCode.C);
            characterInputs.CrouchUp = Input.GetKeyUp(KeyCode.C);

            // Aplly inputs chrcter
            Character.SetInputs(ref characterInputs);
        }

        private void HandlePuzzleInteraction()
        {
            // Control when user push P
            if (Input.GetKey(KeyCode.P))
            {
                // Kontrol pull
                PullPuzzleObjects();
            }
            else if (Input.GetKey(KeyCode.U))
            {
                // kontrol push
                PushPuzzleObjects();
            }
        }

        private void PullPuzzleObjects()
        {
            // Find the object whic is near by (itterarion)
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, 3f); // search for 3 unit area
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
            // Find objects for push
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, 3f); // search for 3 unit area
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
