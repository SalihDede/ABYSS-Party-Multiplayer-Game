using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

/*public class GameFourManager : MonoBehaviourPunCallbacks
{
    // Oyun yöneticisi nesnesini saklar
    public GameObject GameManagerrr;

    // Her oyuncunun puanýný göstereceði TMP_Text nesnesi
    public TMP_Text PlayerScoreText;                                                                 //ADDED

    // Oyun baþlangýç zamaný için bir bayrak
    public bool StartTime;

    // Koordinat yöneticisi nesnesini saklar
    public GameObject OBJCordinate;

    // Oyunun GUI'si için nesneler (kullanýlmýyor)
    //public GameObject GameFourGUI;
    //public GameObject GameFourGUI2;

    // Oyuncularýn sýralamasý için bir liste
    public List<GameObject> Ranking = new List<GameObject>();

    // Oyunun baþýnda ortaya çýkan oyuncular için bir liste
    public List<GameObject> Starters = new List<GameObject>();

    // Kazanma durumu için bir bayrak (kullanýlmýyor)
    //public TMP_Text Win;
    public bool IsWin;

    // Ýkinci oyuncunun ismini göstermek için bir metin nesnesi
    public TMP_Text Player2Text;

    // Oyun alanýndaki tüm nesneleri içeren bir liste
    public List<GameObject> allObjectsMain = new List<GameObject>();

    // Son dokunan oyuncunun adýný göstermek için bir metin nesnesi
    public TMP_Text LastTouch;

    // Oyuncularýn spawnlanacaðý noktalarý saklayan bir dizi
    public GameObject[] Spawns;

    // Oyunun baþlatýlýp baþlatýlmadýðýný kontrol eden bir bayrak
    public bool GameStarted;

    // Geri sayým metnini göstermek için bir metin nesnesi
    public TMP_Text countdownText;

    // Oyun nesnesi oluþturulduðunda çaðrýlýr
    void Start()
    {
        // Oyun yöneticisi ve koordinat yöneticisini bulur
        GameManagerrr = GameObject.Find("GameManager");
        OBJCordinate = GameObject.Find("CoordinateManager");

        PlayerScoreText = GameObject.Find("PlayerScoreText").GetComponent<TMP_Text>();               //ADDED
    }

    // Her karede çaðrýlýr
    void Update()
    {
                      //AAAAAAAAAAAAAAADDDDDDDDDDDDEEEEEEEEEEEEEEEDDDDDDDDDDD
        // Oyuncunun puanýný metin nesnesine yazar
        if (PhotonNetwork.IsConnected && PlayerScoreText != null)
        {
            // Oyuncunun sýrasýna göre metin nesnesine yazý yazdýrýlýr
            if (PhotonNetwork.LocalPlayer.ActorNumber == 1)
            {
                PlayerScoreText.text = "Player 1: " + GameManagerrr.GetComponent<GameManager>().PlayersSorted[0].GetComponent<GameFourPlayer>().score;
            }
            else if (PhotonNetwork.LocalPlayer.ActorNumber == 2)
            {
                PlayerScoreText.text = "Player 2: " + GameManagerrr.GetComponent<GameManager>().PlayersSorted[1].GetComponent<GameFourPlayer>().score;
            }
        }
                      //AAAAAAAAAAAAAAADDDDDDDDDDDDEEEEEEEEEEEEEEEDDDDDDDDDDD

        // Oyun baþlatýldýysa
        if (GameStarted)
        {
            // Oyun baþlatýldý bayraðýný false olarak ayarlar
            GameStarted = false;

            // Rastgele bir harita oluþturur
            RandomMapGenerator();

            // Oyun alanýndaki tüm nesnelerin sayýsý 28 ise
            if (allObjectsMain.Count == 28)
            {
                // Tüm nesneleri aktif hale getirir
                foreach (GameObject player in allObjectsMain)
                {
                    player.SetActive(true);
                }
            }
        }

        // Sýralamanýn sayýsý 2 ise
        if (Ranking.Count == 2)
        {
            // Oyuncularý puanlarýna göre sýralar
            Ranking.Sort((player1, player2) => player2.GetComponent<GameFourPlayer>().score.CompareTo(player1.GetComponent<GameFourPlayer>().score));

            // Oyun yöneticisinin sýralanmýþ oyuncular listesini temizler
            GameManagerrr.GetComponent<GameManager>().PlayersTemp.Clear();

            // Sýralamadaki her oyuncu için
            for (int i = 0; i < 2; i++)
            {
                // Oyun yöneticisinin sýralanmýþ oyuncular listesindeki oyuncularý, sýralamanýn içindeki oyunculara göre eþleþtirir
                foreach (GameObject Player in GameManagerrr.GetComponent<GameManager>().PlayersSorted)
                {
                    if (Ranking[i].GetComponent<PhotonView>().ViewID / 1000 == Player.GetComponent<PhotonView>().ViewID / 1000)
                    {
                        GameManagerrr.GetComponent<GameManager>().PlayersTemp.Add(Player);
                    }
                }
            }

            // Oyun yöneticisinin sýralanmýþ oyuncular listesini temizler
            GameManagerrr.GetComponent<GameManager>().PlayersSorted.Clear();

            // Sýralanmýþ oyuncular listesinin sayýsý 2 deðilse
            if (GameManagerrr.GetComponent<GameManager>().PlayersSorted.Count != 2)
            {
                // Sýralanmýþ oyuncular listesine, geçici listeyi ekler
                GameManagerrr.GetComponent<GameManager>().PlayersSorted.AddRange(GameManagerrr.GetComponent<GameManager>().PlayersTemp);
            }

            // Sýralamanýn içindeki her oyuncu için
            foreach (GameObject Player in Ranking)
            {
                // Oyuncuyu yok eder
                Destroy(Player);
            }

            // Sýralamanýn ve oyun baþlangýç listesinin içeriðini temizler
            Ranking.Clear();
            Starters.Clear();

            // Oyun yöneticisinin kamerasýný aktif hale getirir
            GameManagerrr.GetComponent<GameManager>().Kamera.SetActive(true);

            // Mini oyunun bittiðini belirtir
            GameManagerrr.GetComponent<GameManager>().MiniGameStarted = false;

            // Fareyi görünür hale getirir
            Cursor.visible = true;

            // Fareyi kilitli durumdan çýkarýr
            Cursor.lockState = CursorLockMode.None;

            // Oyun yöneticisi nesnesini devre dýþý býrakýr
            gameObject.SetActive(false);
        }
    }

    // Rastgele bir harita oluþturur
    public void RandomMapGenerator()
    {
        // Bir oyuncunun spawnlandýðý noktayý belirler
        GameObject player = PhotonNetwork.Instantiate("AlpGamePlayer", Spawns[0].transform.position, Quaternion.identity);
    }
} */