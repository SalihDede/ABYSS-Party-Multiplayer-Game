using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{

    public GameObject Kapý0; //Saða Z
    public GameObject Kapý1; //Saða Z
    public GameObject Kapý2;
    public GameObject Kapý3; //Saða X
    public GameObject Kapý4; //Saða X
    public GameObject Kapý5; //Dönecek
    public GameObject Kapý6; //Dönecek
    public GameObject Kapý7; //Dönecek
    public GameObject Kapý8; //SaðaX

    private float reappearTime = 0.5f; // Geri dönüþ süresi (2 saniye)
    private float clickCooldown = 0.5f; // Týklama soðuma süresi (2 saniye)

    // Her kapýnýn açýk/kapalý durumunu ve son týklama zamanýný takip etmek için bir dictionary kullanacaðýz
    private Dictionary<GameObject, bool> doorStates = new Dictionary<GameObject, bool>();
    private Dictionary<GameObject, float> lastClickTime = new Dictionary<GameObject, float>();

    void Start()
    {
        // Baþlangýçta tüm kapýlar kapalý olarak iþaretlenir ve son týklama zamaný 0 olarak ayarlanýr
        doorStates.Add(Kapý0, false);
        lastClickTime.Add(Kapý0, 0f);
        doorStates.Add(Kapý1, false);
        lastClickTime.Add(Kapý1, 0f);
        doorStates.Add(Kapý2, false);
        lastClickTime.Add(Kapý2, 0f);
        doorStates.Add(Kapý3, false);
        lastClickTime.Add(Kapý3, 0f);
        doorStates.Add(Kapý4, false);
        lastClickTime.Add(Kapý4, 0f);
        doorStates.Add(Kapý5, false);
        lastClickTime.Add(Kapý5, 0f);
        doorStates.Add(Kapý6, false);
        lastClickTime.Add(Kapý6, 0f);
        doorStates.Add(Kapý7, false);
        lastClickTime.Add(Kapý7, 0f);
        doorStates.Add(Kapý8, false);
        lastClickTime.Add(Kapý8, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        // Sol týklama kontrolü
        if (Input.GetMouseButtonDown(0))
        {
            // Fare imlecini týklanan nesneye yönlendirin
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                // Týklanan nesneye göre iþlem yapýn
                if (hit.collider.gameObject == Kapý0 && Time.time - lastClickTime[Kapý0] > clickCooldown)
                {
                    // Kapýnýn durumunu deðiþtir
                    doorStates[Kapý0] = !doorStates[Kapý0];
                    if (doorStates[Kapý0])
                    {
                        // Z yönünde -1.25 metre hareket ettir (açýk)
                        StartCoroutine(MoveObject(Kapý0, new Vector3(0, 0, -1.25f)));
                    }
                    else
                    {
                        // Z yönünde 1.25 metre hareket ettir (kapalý)
                        StartCoroutine(MoveObject(Kapý0, new Vector3(0, 0, 1.25f)));
                    }
                    // Son týklama zamanýný güncelle
                    lastClickTime[Kapý0] = Time.time;
                }
                else if (hit.collider.gameObject == Kapý1 && Time.time - lastClickTime[Kapý1] > clickCooldown)
                {
                    doorStates[Kapý1] = !doorStates[Kapý1];
                    if (doorStates[Kapý1])
                    {
                        // Z yönünde -1.26 metre hareket ettir (açýk)
                        StartCoroutine(MoveObject(Kapý1, new Vector3(0, 0, -1.26f)));
                    }
                    else
                    {
                        // Z yönünde 1.26 metre hareket ettir (kapalý)
                        StartCoroutine(MoveObject(Kapý1, new Vector3(0, 0, 1.26f)));
                    }
                    lastClickTime[Kapý1] = Time.time;
                }
                else if (hit.collider.gameObject == Kapý2 && Time.time - lastClickTime[Kapý2] > clickCooldown)
                {
                    doorStates[Kapý2] = !doorStates[Kapý2];
                    if (doorStates[Kapý2])
                    {
                        // Z yönünde 1.166 metre hareket ettir (açýk)
                        StartCoroutine(MoveObject(Kapý2, new Vector3(0, 0, 1.166f)));
                    }
                    else
                    {
                        // Z yönünde -1.166 metre hareket ettir (kapalý)
                        StartCoroutine(MoveObject(Kapý2, new Vector3(0, 0, -1.166f)));
                    }
                    lastClickTime[Kapý2] = Time.time;
                }
                else if (hit.collider.gameObject == Kapý3 && Time.time - lastClickTime[Kapý3] > clickCooldown)
                {
                    doorStates[Kapý3] = !doorStates[Kapý3];
                    if (doorStates[Kapý3])
                    {
                        // X yönünde 1 metre hareket ettir (açýk)
                        StartCoroutine(MoveObject(Kapý3, new Vector3(1.25f, 0, 0)));
                    }
                    else
                    {
                        // X yönünde -1 metre hareket ettir (kapalý)
                        StartCoroutine(MoveObject(Kapý3, new Vector3(-1.25f, 0, 0)));
                    }
                    lastClickTime[Kapý3] = Time.time;
                }
                else if (hit.collider.gameObject == Kapý4 && Time.time - lastClickTime[Kapý4] > clickCooldown)
                {
                    doorStates[Kapý4] = !doorStates[Kapý4];
                    if (doorStates[Kapý4])
                    {
                        // X yönünde 1 metre hareket ettir (açýk)
                        StartCoroutine(MoveObject(Kapý4, new Vector3(1.25f, 0, 0)));
                    }
                    else
                    {
                        // X yönünde -1 metre hareket ettir (kapalý)
                        StartCoroutine(MoveObject(Kapý4, new Vector3(-1.25f, 0, 0)));
                    }
                    lastClickTime[Kapý4] = Time.time;
                }
                else if (hit.collider.gameObject == Kapý5 && Time.time - lastClickTime[Kapý5] > clickCooldown)
                {
                    doorStates[Kapý5] = !doorStates[Kapý5];
                    if (doorStates[Kapý5])
                    {
                        // Y ekseni etrafýnda 90 derece döndür (açýk)
                        StartCoroutine(RotateObject(Kapý5, new Vector3(0, 0, 90f)));

                    }
                    else
                    {
                        // Y ekseni etrafýnda -90 derece döndür (kapalý)
                        StartCoroutine(RotateObject(Kapý5, new Vector3(0, 0, -90f)));
                    }
                    lastClickTime[Kapý5] = Time.time;
                }
                else if (hit.collider.gameObject == Kapý6 && Time.time - lastClickTime[Kapý6] > clickCooldown)
                {
                    doorStates[Kapý6] = !doorStates[Kapý6];
                    if (doorStates[Kapý6])
                    {
                        // Y ekseni etrafýnda 90 derece döndür (açýk)
                        StartCoroutine(RotateObject(Kapý6, new Vector3(0, 0, 90f)));
                        
                    }
                    else
                    {
                        // Y ekseni etrafýnda -90 derece döndür (kapalý)              
                        StartCoroutine(RotateObject(Kapý6, new Vector3(0, 0, -90f)));
                        
                    }
                    lastClickTime[Kapý6] = Time.time;
                }
                else if (hit.collider.gameObject == Kapý7 && Time.time - lastClickTime[Kapý7] > clickCooldown)
                {
                    doorStates[Kapý7] = !doorStates[Kapý7];
                    if (doorStates[Kapý7])
                    {
                        // Y ekseni etrafýnda 90 derece döndür (açýk)
                        StartCoroutine(RotateObject(Kapý7, new Vector3(0, 0, 90f)));
                    }
                    else
                    {
                        // Y ekseni etrafýnda -90 derece döndür (kapalý)
                        StartCoroutine(RotateObject(Kapý7, new Vector3(0, 0, -90f)));
                    }
                    lastClickTime[Kapý7] = Time.time;
                }
                else if (hit.collider.gameObject == Kapý8 && Time.time - lastClickTime[Kapý8] > clickCooldown)
                {
                    doorStates[Kapý8] = !doorStates[Kapý8];
                    if (doorStates[Kapý8])
                    {
                        // X yönünde 1 metre hareket ettir (açýk)
                        StartCoroutine(MoveObject(Kapý8, new Vector3(1.25f, 0, 0)));
                    }
                    else
                    {
                        // X yönünde -1 metre hareket ettir (kapalý)
                        StartCoroutine(MoveObject(Kapý8, new Vector3(-1.25f, 0, 0)));
                    }
                    lastClickTime[Kapý8] = Time.time;
                }
            }
        }
    }

    IEnumerator MoveObject(GameObject obj, Vector3 direction)
    {
        // Hareket ettirmeden önce baþlangýç pozisyonunu kaydet
        Vector3 targetPosition = obj.transform.position + direction;
        Vector3 startPosition = obj.transform.position;

        float elapsedTime = 0f;

        while (elapsedTime < reappearTime)
        {
            // Doðrusal interpolasyon kullanarak nesneyi hedef konuma taþý
            obj.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / reappearTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
    IEnumerator RotateObject(GameObject obj, Vector3 eulerAngles)
    {
        // Döndürmeden önce baþlangýç rotasyonunu kaydet
        Quaternion targetRotation = obj.transform.rotation * Quaternion.Euler(eulerAngles);
        Quaternion startRotation = obj.transform.rotation;

        float elapsedTime = 0f;

        while (elapsedTime < reappearTime)
        {
            // Slerp kullanarak nesneyi hedef rotasyona döndür
            obj.transform.rotation = Quaternion.Slerp(startRotation, targetRotation, elapsedTime / reappearTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}