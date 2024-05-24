using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kapı3 : MonoBehaviour
{
    Vector3 baslangicPozisyonu;
    Quaternion baslangicRotasyonu;
    Quaternion hedefRotasyon;
    float kapıAcilacakMesafe = 0.4f;
    float kapıHareketHizi = 1.5f;
    bool kapıAcik = false;

    void Start()
    {
        baslangicPozisyonu = transform.position;
        baslangicRotasyonu = transform.rotation;
        hedefRotasyon = Quaternion.Euler(-90,0, 90); // Kapının z ekseninde 90 derece dönmesi
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
            transform.position = Vector3.MoveTowards(transform.position, baslangicPozisyonu - transform.right * kapıAcilacakMesafe, kapıHareketHizi * Time.fixedDeltaTime);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, hedefRotasyon, 90 * Time.fixedDeltaTime);
        }
        else
        {
            // Kapıyı kapatma animasyonu oynat
            transform.position = Vector3.MoveTowards(transform.position, baslangicPozisyonu, kapıHareketHizi * Time.fixedDeltaTime);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, baslangicRotasyonu, 90 * Time.fixedDeltaTime);
        }
    }
}