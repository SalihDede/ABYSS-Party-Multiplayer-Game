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

        public float pushPullForce = 10f; // Çekme ve itme kuvveti

        private const string MouseXInput = "Mouse X";
        private const string MouseYInput = "Mouse Y";
        private const string MouseScrollInput = "Mouse ScrollWheel";
        private const string HorizontalInput = "Horizontal";
        private const string VerticalInput = "Vertical";

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;

            // Kameranın takip etmesini sağla
            OrbitCamera.SetFollowTransform(CameraFollowPoint);

            // Kamera engellerini kontrol ederken karakterin collider'larını yok say
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
            HandlePuzzleInteraction(); // Puzzle objeleriyle etkileşimi kontrol et
        }

        private void LateUpdate()
        {
            HandleCameraInput();
        }

        private void HandleCameraInput()
        {
            // Kamera için giriş vektörünü oluştur
            float mouseLookAxisUp = Input.GetAxisRaw(MouseYInput);
            float mouseLookAxisRight = Input.GetAxisRaw(MouseXInput);
            Vector3 lookInputVector = new Vector3(mouseLookAxisRight, mouseLookAxisUp, 0f);

            // Farenin kilitli olmadığı durumda kamerayı hareket ettirmeyi engelle
            if (Cursor.lockState != CursorLockMode.Locked)
            {
                lookInputVector = Vector3.zero;
            }

            // Kamerayı hareket ettirme girişi
            float scrollInput = -Input.GetAxis(MouseScrollInput);

            // Kameraya girişleri uygula
            OrbitCamera.UpdateWithInput(Time.deltaTime, scrollInput, lookInputVector);

            // Yakınlaşma seviyesini değiştirme
            if (Input.GetMouseButtonDown(1))
            {
                OrbitCamera.TargetDistance = (OrbitCamera.TargetDistance == 0f) ? OrbitCamera.DefaultDistance : 0f;
            }
        }

        private void HandleCharacterInput()
        {
            PlayerCharacterInputs characterInputs = new PlayerCharacterInputs();

            // Karakter girişlerini oluştur
            characterInputs.MoveAxisForward = Input.GetAxisRaw(VerticalInput);
            characterInputs.MoveAxisRight = Input.GetAxisRaw(HorizontalInput);
            characterInputs.CameraRotation = OrbitCamera.Transform.rotation;
            characterInputs.JumpDown = Input.GetKeyDown(KeyCode.Space);
            characterInputs.CrouchDown = Input.GetKeyDown(KeyCode.C);
            characterInputs.CrouchUp = Input.GetKeyUp(KeyCode.C);

            // Girişleri karaktere uygula
            Character.SetInputs(ref characterInputs);
        }

        private void HandlePuzzleInteraction()
        {
            // P tuşuna basılı tutulduğunda çekme ve itme işlevlerini kontrol et
            if (Input.GetKey(KeyCode.P))
            {
                // Çekme işlevini kontrol et
                PullPuzzleObjects();
            }
            else if (Input.GetKeyUp(KeyCode.P))
            {
                // İtme işlevini kontrol et
                PushPuzzleObjects();
            }
        }

        private void PullPuzzleObjects()
        {
            // Çekme işlevi için etkileşime giren nesneleri bul
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, 5f); // Oyuncunun etrafındaki 5 birimlik bir alanı kontrol et
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
            // İtme işlevi için etkileşime giren nesneleri bul
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, 5f); // Oyuncunun etrafındaki 5 birimlik bir alanı kontrol et
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
