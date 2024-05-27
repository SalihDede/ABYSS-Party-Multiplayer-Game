using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{

    public GameObject Kap�0; //Sa�a Z
    public GameObject Kap�1; //Sa�a Z
    public GameObject Kap�2;
    public GameObject Kap�3; //Sa�a X
    public GameObject Kap�4; //Sa�a X
    public GameObject Kap�5; //D�necek
    public GameObject Kap�6; //D�necek
    public GameObject Kap�7; //D�necek
    public GameObject Kap�8; //Sa�aX

    private float reappearTime = 0.5f; // Geri d�n�� s�resi (2 saniye)
    private float clickCooldown = 0.5f; // T�klama so�uma s�resi (2 saniye)

    // Her kap�n�n a��k/kapal� durumunu ve son t�klama zaman�n� takip etmek i�in bir dictionary kullanaca��z
    private Dictionary<GameObject, bool> doorStates = new Dictionary<GameObject, bool>();
    private Dictionary<GameObject, float> lastClickTime = new Dictionary<GameObject, float>();

    void Start()
    {
        // Ba�lang��ta t�m kap�lar kapal� olarak i�aretlenir ve son t�klama zaman� 0 olarak ayarlan�r
        doorStates.Add(Kap�0, false);
        lastClickTime.Add(Kap�0, 0f);
        doorStates.Add(Kap�1, false);
        lastClickTime.Add(Kap�1, 0f);
        doorStates.Add(Kap�2, false);
        lastClickTime.Add(Kap�2, 0f);
        doorStates.Add(Kap�3, false);
        lastClickTime.Add(Kap�3, 0f);
        doorStates.Add(Kap�4, false);
        lastClickTime.Add(Kap�4, 0f);
        doorStates.Add(Kap�5, false);
        lastClickTime.Add(Kap�5, 0f);
        doorStates.Add(Kap�6, false);
        lastClickTime.Add(Kap�6, 0f);
        doorStates.Add(Kap�7, false);
        lastClickTime.Add(Kap�7, 0f);
        doorStates.Add(Kap�8, false);
        lastClickTime.Add(Kap�8, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        // Sol t�klama kontrol�
        if (Input.GetMouseButtonDown(0))
        {
            // Fare imlecini t�klanan nesneye y�nlendirin
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                // T�klanan nesneye g�re i�lem yap�n
                if (hit.collider.gameObject == Kap�0 && Time.time - lastClickTime[Kap�0] > clickCooldown)
                {
                    // Kap�n�n durumunu de�i�tir
                    doorStates[Kap�0] = !doorStates[Kap�0];
                    if (doorStates[Kap�0])
                    {
                        // Z y�n�nde -1.25 metre hareket ettir (a��k)
                        StartCoroutine(MoveObject(Kap�0, new Vector3(0, 0, -1.25f)));
                    }
                    else
                    {
                        // Z y�n�nde 1.25 metre hareket ettir (kapal�)
                        StartCoroutine(MoveObject(Kap�0, new Vector3(0, 0, 1.25f)));
                    }
                    // Son t�klama zaman�n� g�ncelle
                    lastClickTime[Kap�0] = Time.time;
                }
                else if (hit.collider.gameObject == Kap�1 && Time.time - lastClickTime[Kap�1] > clickCooldown)
                {
                    doorStates[Kap�1] = !doorStates[Kap�1];
                    if (doorStates[Kap�1])
                    {
                        // Z y�n�nde -1.26 metre hareket ettir (a��k)
                        StartCoroutine(MoveObject(Kap�1, new Vector3(0, 0, -1.26f)));
                    }
                    else
                    {
                        // Z y�n�nde 1.26 metre hareket ettir (kapal�)
                        StartCoroutine(MoveObject(Kap�1, new Vector3(0, 0, 1.26f)));
                    }
                    lastClickTime[Kap�1] = Time.time;
                }
                else if (hit.collider.gameObject == Kap�2 && Time.time - lastClickTime[Kap�2] > clickCooldown)
                {
                    doorStates[Kap�2] = !doorStates[Kap�2];
                    if (doorStates[Kap�2])
                    {
                        // Z y�n�nde 1.166 metre hareket ettir (a��k)
                        StartCoroutine(MoveObject(Kap�2, new Vector3(0, 0, 1.166f)));
                    }
                    else
                    {
                        // Z y�n�nde -1.166 metre hareket ettir (kapal�)
                        StartCoroutine(MoveObject(Kap�2, new Vector3(0, 0, -1.166f)));
                    }
                    lastClickTime[Kap�2] = Time.time;
                }
                else if (hit.collider.gameObject == Kap�3 && Time.time - lastClickTime[Kap�3] > clickCooldown)
                {
                    doorStates[Kap�3] = !doorStates[Kap�3];
                    if (doorStates[Kap�3])
                    {
                        // X y�n�nde 1 metre hareket ettir (a��k)
                        StartCoroutine(MoveObject(Kap�3, new Vector3(1.25f, 0, 0)));
                    }
                    else
                    {
                        // X y�n�nde -1 metre hareket ettir (kapal�)
                        StartCoroutine(MoveObject(Kap�3, new Vector3(-1.25f, 0, 0)));
                    }
                    lastClickTime[Kap�3] = Time.time;
                }
                else if (hit.collider.gameObject == Kap�4 && Time.time - lastClickTime[Kap�4] > clickCooldown)
                {
                    doorStates[Kap�4] = !doorStates[Kap�4];
                    if (doorStates[Kap�4])
                    {
                        // X y�n�nde 1 metre hareket ettir (a��k)
                        StartCoroutine(MoveObject(Kap�4, new Vector3(1.25f, 0, 0)));
                    }
                    else
                    {
                        // X y�n�nde -1 metre hareket ettir (kapal�)
                        StartCoroutine(MoveObject(Kap�4, new Vector3(-1.25f, 0, 0)));
                    }
                    lastClickTime[Kap�4] = Time.time;
                }
                else if (hit.collider.gameObject == Kap�5 && Time.time - lastClickTime[Kap�5] > clickCooldown)
                {
                    doorStates[Kap�5] = !doorStates[Kap�5];
                    if (doorStates[Kap�5])
                    {
                        // Y ekseni etraf�nda 90 derece d�nd�r (a��k)
                        StartCoroutine(RotateObject(Kap�5, new Vector3(0, 0, 90f)));

                    }
                    else
                    {
                        // Y ekseni etraf�nda -90 derece d�nd�r (kapal�)
                        StartCoroutine(RotateObject(Kap�5, new Vector3(0, 0, -90f)));
                    }
                    lastClickTime[Kap�5] = Time.time;
                }
                else if (hit.collider.gameObject == Kap�6 && Time.time - lastClickTime[Kap�6] > clickCooldown)
                {
                    doorStates[Kap�6] = !doorStates[Kap�6];
                    if (doorStates[Kap�6])
                    {
                        // Y ekseni etraf�nda 90 derece d�nd�r (a��k)
                        StartCoroutine(RotateObject(Kap�6, new Vector3(0, 0, 90f)));
                        
                    }
                    else
                    {
                        // Y ekseni etraf�nda -90 derece d�nd�r (kapal�)              
                        StartCoroutine(RotateObject(Kap�6, new Vector3(0, 0, -90f)));
                        
                    }
                    lastClickTime[Kap�6] = Time.time;
                }
                else if (hit.collider.gameObject == Kap�7 && Time.time - lastClickTime[Kap�7] > clickCooldown)
                {
                    doorStates[Kap�7] = !doorStates[Kap�7];
                    if (doorStates[Kap�7])
                    {
                        // Y ekseni etraf�nda 90 derece d�nd�r (a��k)
                        StartCoroutine(RotateObject(Kap�7, new Vector3(0, 0, 90f)));
                    }
                    else
                    {
                        // Y ekseni etraf�nda -90 derece d�nd�r (kapal�)
                        StartCoroutine(RotateObject(Kap�7, new Vector3(0, 0, -90f)));
                    }
                    lastClickTime[Kap�7] = Time.time;
                }
                else if (hit.collider.gameObject == Kap�8 && Time.time - lastClickTime[Kap�8] > clickCooldown)
                {
                    doorStates[Kap�8] = !doorStates[Kap�8];
                    if (doorStates[Kap�8])
                    {
                        // X y�n�nde 1 metre hareket ettir (a��k)
                        StartCoroutine(MoveObject(Kap�8, new Vector3(1.25f, 0, 0)));
                    }
                    else
                    {
                        // X y�n�nde -1 metre hareket ettir (kapal�)
                        StartCoroutine(MoveObject(Kap�8, new Vector3(-1.25f, 0, 0)));
                    }
                    lastClickTime[Kap�8] = Time.time;
                }
            }
        }
    }

    IEnumerator MoveObject(GameObject obj, Vector3 direction)
    {
        // Hareket ettirmeden �nce ba�lang�� pozisyonunu kaydet
        Vector3 targetPosition = obj.transform.position + direction;
        Vector3 startPosition = obj.transform.position;

        float elapsedTime = 0f;

        while (elapsedTime < reappearTime)
        {
            // Do�rusal interpolasyon kullanarak nesneyi hedef konuma ta��
            obj.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / reappearTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
    IEnumerator RotateObject(GameObject obj, Vector3 eulerAngles)
    {
        // D�nd�rmeden �nce ba�lang�� rotasyonunu kaydet
        Quaternion targetRotation = obj.transform.rotation * Quaternion.Euler(eulerAngles);
        Quaternion startRotation = obj.transform.rotation;

        float elapsedTime = 0f;

        while (elapsedTime < reappearTime)
        {
            // Slerp kullanarak nesneyi hedef rotasyona d�nd�r
            obj.transform.rotation = Quaternion.Slerp(startRotation, targetRotation, elapsedTime / reappearTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}