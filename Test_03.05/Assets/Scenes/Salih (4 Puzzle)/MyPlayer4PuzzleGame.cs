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

        public float pushPullForce = 0.1f; //Force

        private const string MouseXInput = "Mouse X";
        private const string MouseYInput = "Mouse Y";
        private const string MouseScrollInput = "Mouse ScrollWheel";
        private const string HorizontalInput = "Horizontal";
        private const string VerticalInput = "Vertical";

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;

            
            OrbitCamera.SetFollowTransform(CameraFollowPoint);

           
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
            HandlePuzzleInteraction();
        }

        private void LateUpdate()
        {
            HandleCameraInput();
        }

        private void HandleCameraInput()
        {
       
            float mouseLookAxisUp = Input.GetAxisRaw(MouseYInput);
            float mouseLookAxisRight = Input.GetAxisRaw(MouseXInput);
            Vector3 lookInputVector = new Vector3(mouseLookAxisRight, mouseLookAxisUp, 0f);

          
            if (Cursor.lockState != CursorLockMode.Locked)
            {
                lookInputVector = Vector3.zero;
            }

         
            float scrollInput = -Input.GetAxis(MouseScrollInput);

        
            OrbitCamera.UpdateWithInput(Time.deltaTime, scrollInput, lookInputVector);

           
            if (Input.GetMouseButtonDown(1))
            {
                OrbitCamera.TargetDistance = (OrbitCamera.TargetDistance == 0f) ? OrbitCamera.DefaultDistance : 0f;
            }
        }

        private void HandleCharacterInput()
        {
            PlayerCharacterInputs characterInputs = new PlayerCharacterInputs();

            
            characterInputs.MoveAxisForward = Input.GetAxisRaw(VerticalInput);
            characterInputs.MoveAxisRight = Input.GetAxisRaw(HorizontalInput);
            characterInputs.CameraRotation = OrbitCamera.Transform.rotation;
            characterInputs.JumpDown = Input.GetKeyDown(KeyCode.Space);
            characterInputs.CrouchDown = Input.GetKeyDown(KeyCode.C);
            characterInputs.CrouchUp = Input.GetKeyUp(KeyCode.C);

            
            Character.SetInputs(ref characterInputs);
        }

        private void HandlePuzzleInteraction()
        {
            
            if (Input.GetKey(KeyCode.P))
            {
                
                PullPuzzleObjects();
            }
            else if (Input.GetKeyUp(KeyCode.P))
            {
                
                PushPuzzleObjects();
            }
        }

        private void PullPuzzleObjects()
        {
             // Ittreate object
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, 5f); //Searc 5 ft
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
            // Ittreate object
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, 5f); //Searc 5 ft
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
