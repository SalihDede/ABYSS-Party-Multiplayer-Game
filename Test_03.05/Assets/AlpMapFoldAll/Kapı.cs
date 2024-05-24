using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kapı : MonoBehaviour
{
    Vector3 baslangicPozisyonu;
    Vector3 hedefPozisyonu;
    float kapıAcilacakMesafe = 1.3f;
    float kapıHareketHizi = 1.5f;
    bool kapıAcik = false;

    void Start()
    {
        baslangicPozisyonu = transform.position;
        hedefPozisyonu = new Vector3(baslangicPozisyonu.x, baslangicPozisyonu.y, baslangicPozisyonu.z - kapıAcilacakMesafe);
    }

    void FixedUpdate()
    {
        float oyuncuMesafe = Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Bullet").transform.position);
        if (Input.GetKeyDown(KeyCode.F))
        {
            kapıAcik = !kapıAcik; // Kapı durumunu tersine çevir
        }

        if (kapıAcik)
        {
            // Kapıyı açma animasyonu oynat
            transform.position = Vector3.MoveTowards(transform.position, hedefPozisyonu, kapıHareketHizi * Time.fixedDeltaTime);
        }
        else
        {
            // Kapıyı kapatma animasyonu oynat
            transform.position = Vector3.MoveTowards(transform.position, baslangicPozisyonu, kapıHareketHizi * Time.fixedDeltaTime);
        }
    }
}