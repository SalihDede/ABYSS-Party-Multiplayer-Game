using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{

    public GameObject Kapý0;
    public GameObject Kapý1;
    public GameObject Kapý2;
    public GameObject Kapý3;
    public GameObject Kapý4;
    public GameObject Kapý5;
    public GameObject Kapý6;
    public GameObject Kapý7;
    public GameObject Kapý8;

    private float reappearTime = 0.9f;
    void Start()
    {

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
                if (hit.collider.gameObject == Kapý0 || hit.collider.gameObject == Kapý1 || hit.collider.gameObject == Kapý2 || // ... Diðer nesneleri ekleyin
                    hit.collider.gameObject == Kapý3 || hit.collider.gameObject == Kapý4 || hit.collider.gameObject == Kapý5 || // ... Diðer nesneleri ekleyin
                    hit.collider.gameObject == Kapý6 || hit.collider.gameObject == Kapý7 || hit.collider.gameObject == Kapý8)
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

