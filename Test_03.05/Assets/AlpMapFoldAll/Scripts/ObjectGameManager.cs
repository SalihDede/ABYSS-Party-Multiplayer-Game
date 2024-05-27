using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UI ��elerini kullanmak i�in gerekli

public class ObjectGameManager : MonoBehaviour
{
    public GameObject chair; public GameObject bluePlate; public GameObject happyPillow; public GameObject Fork;
    public GameObject Spoon; public GameObject Knife; public GameObject Hat;
    public GameObject Sword; public GameObject Glassess; public GameObject Computer;
    public GameObject Calculator; public GameObject Brown; public GameObject Red; public GameObject Blue;
    public GameObject Green; public GameObject Orange; public GameObject Yellow;
    public GameObject LargeBox; public GameObject MediumBox; public GameObject SmallBox;
    public GameObject Radio; public GameObject Bag; public GameObject Ball; public GameObject Slipper;
    public GameObject Pencil; public GameObject WateringCan; public GameObject sadPillow;

    public int score = 0;
    public Text scoreText; // Puan� g�sterecek Text ��esi

    // Start is called before the first frame update
    void Start()
    {
        // Puan� ba�lang��ta s�f�rla
        scoreText.text = "Point " + score;
    }

    // Update is called once per frame
    void Update()
    {
        // Sol t�klama kontrol�
        if (Input.GetMouseButtonDown(0))
        {
            // Mouse ���n�n�n hangi nesneye �arpt���n� bul
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                // �arp�lan nesne yukar�daki listede mi?
                if (hit.collider.gameObject == chair ||
                    hit.collider.gameObject == bluePlate ||
                    hit.collider.gameObject == happyPillow ||
                    hit.collider.gameObject == Fork ||
                    hit.collider.gameObject == Spoon ||
                    hit.collider.gameObject == Knife ||
                    hit.collider.gameObject == Hat ||
                    hit.collider.gameObject == Sword ||
                    hit.collider.gameObject == Glassess ||
                    hit.collider.gameObject == Computer ||
                    hit.collider.gameObject == Calculator ||
                    hit.collider.gameObject == Brown ||
                    hit.collider.gameObject == Red ||
                    hit.collider.gameObject == Blue ||
                    hit.collider.gameObject == Green ||
                    hit.collider.gameObject == Orange ||
                    hit.collider.gameObject == Yellow ||
                    hit.collider.gameObject == LargeBox ||
                    hit.collider.gameObject == MediumBox ||
                    hit.collider.gameObject == SmallBox ||
                    hit.collider.gameObject == Radio ||
                    hit.collider.gameObject == Bag ||
                    hit.collider.gameObject == Ball ||
                    hit.collider.gameObject == Slipper ||
                    hit.collider.gameObject == Pencil ||
                    hit.collider.gameObject == WateringCan ||
                    hit.collider.gameObject == sadPillow)
                {
                    // Puan art�r
                    score++;
                    // Puan� Text ��esinde g�ncelle
                    scoreText.text = "Puan: " + score;
                }
            }
        }
    }
}