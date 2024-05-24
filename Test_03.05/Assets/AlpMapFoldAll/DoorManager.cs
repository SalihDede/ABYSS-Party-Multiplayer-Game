using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{

    public GameObject Kap�0;
    public GameObject Kap�1;
    public GameObject Kap�2;
    public GameObject Kap�3;
    public GameObject Kap�4;
    public GameObject Kap�5;
    public GameObject Kap�6;
    public GameObject Kap�7;
    public GameObject Kap�8;

    private float reappearTime = 0.9f;
    void Start()
    {

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
                if (hit.collider.gameObject == Kap�0 || hit.collider.gameObject == Kap�1 || hit.collider.gameObject == Kap�2 || // ... Di�er nesneleri ekleyin
                    hit.collider.gameObject == Kap�3 || hit.collider.gameObject == Kap�4 || hit.collider.gameObject == Kap�5 || // ... Di�er nesneleri ekleyin
                    hit.collider.gameObject == Kap�6 || hit.collider.gameObject == Kap�7 || hit.collider.gameObject == Kap�8)
                {
                    hit.collider.gameObject.SetActive(false);
                    StartCoroutine(ReappearObject(hit.collider.gameObject));
                }
            }
        }
    }
    IEnumerator ReappearObject(GameObject obj)
    {
        yield return new WaitForSeconds(reappearTime);
        obj.SetActive(true);
    }
}

