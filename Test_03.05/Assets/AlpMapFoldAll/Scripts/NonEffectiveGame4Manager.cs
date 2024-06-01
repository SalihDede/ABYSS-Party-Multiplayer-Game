using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

/*public class GameFourManager : MonoBehaviourPunCallbacks
{
    // Oyun y�neticisi nesnesini saklar
    public GameObject GameManagerrr;

    // Her oyuncunun puan�n� g�sterece�i TMP_Text nesnesi
    public TMP_Text PlayerScoreText;                                                                 //ADDED

    // Oyun ba�lang�� zaman� i�in bir bayrak
    public bool StartTime;

    // Koordinat y�neticisi nesnesini saklar
    public GameObject OBJCordinate;

    // Oyunun GUI'si i�in nesneler (kullan�lm�yor)
    //public GameObject GameFourGUI;
    //public GameObject GameFourGUI2;

    // Oyuncular�n s�ralamas� i�in bir liste
    public List<GameObject> Ranking = new List<GameObject>();

    // Oyunun ba��nda ortaya ��kan oyuncular i�in bir liste
    public List<GameObject> Starters = new List<GameObject>();

    // Kazanma durumu i�in bir bayrak (kullan�lm�yor)
    //public TMP_Text Win;
    public bool IsWin;

    // �kinci oyuncunun ismini g�stermek i�in bir metin nesnesi
    public TMP_Text Player2Text;

    // Oyun alan�ndaki t�m nesneleri i�eren bir liste
    public List<GameObject> allObjectsMain = new List<GameObject>();

    // Son dokunan oyuncunun ad�n� g�stermek i�in bir metin nesnesi
    public TMP_Text LastTouch;

    // Oyuncular�n spawnlanaca�� noktalar� saklayan bir dizi
    public GameObject[] Spawns;

    // Oyunun ba�lat�l�p ba�lat�lmad���n� kontrol eden bir bayrak
    public bool GameStarted;

    // Geri say�m metnini g�stermek i�in bir metin nesnesi
    public TMP_Text countdownText;

    // Oyun nesnesi olu�turuldu�unda �a�r�l�r
    void Start()
    {
        // Oyun y�neticisi ve koordinat y�neticisini bulur
        GameManagerrr = GameObject.Find("GameManager");
        OBJCordinate = GameObject.Find("CoordinateManager");

        PlayerScoreText = GameObject.Find("PlayerScoreText").GetComponent<TMP_Text>();               //ADDED
    }

    // Her karede �a�r�l�r
    void Update()
    {
                      //AAAAAAAAAAAAAAADDDDDDDDDDDDEEEEEEEEEEEEEEEDDDDDDDDDDD
        // Oyuncunun puan�n� metin nesnesine yazar
        if (PhotonNetwork.IsConnected && PlayerScoreText != null)
        {
            // Oyuncunun s�ras�na g�re metin nesnesine yaz� yazd�r�l�r
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

        // Oyun ba�lat�ld�ysa
        if (GameStarted)
        {
            // Oyun ba�lat�ld� bayra��n� false olarak ayarlar
            GameStarted = false;

            // Rastgele bir harita olu�turur
            RandomMapGenerator();

            // Oyun alan�ndaki t�m nesnelerin say�s� 28 ise
            if (allObjectsMain.Count == 28)
            {
                // T�m nesneleri aktif hale getirir
                foreach (GameObject player in allObjectsMain)
                {
                    player.SetActive(true);
                }
            }
        }

        // S�ralaman�n say�s� 2 ise
        if (Ranking.Count == 2)
        {
            // Oyuncular� puanlar�na g�re s�ralar
            Ranking.Sort((player1, player2) => player2.GetComponent<GameFourPlayer>().score.CompareTo(player1.GetComponent<GameFourPlayer>().score));

            // Oyun y�neticisinin s�ralanm�� oyuncular listesini temizler
            GameManagerrr.GetComponent<GameManager>().PlayersTemp.Clear();

            // S�ralamadaki her oyuncu i�in
            for (int i = 0; i < 2; i++)
            {
                // Oyun y�neticisinin s�ralanm�� oyuncular listesindeki oyuncular�, s�ralaman�n i�indeki oyunculara g�re e�le�tirir
                foreach (GameObject Player in GameManagerrr.GetComponent<GameManager>().PlayersSorted)
                {
                    if (Ranking[i].GetComponent<PhotonView>().ViewID / 1000 == Player.GetComponent<PhotonView>().ViewID / 1000)
                    {
                        GameManagerrr.GetComponent<GameManager>().PlayersTemp.Add(Player);
                    }
                }
            }

            // Oyun y�neticisinin s�ralanm�� oyuncular listesini temizler
            GameManagerrr.GetComponent<GameManager>().PlayersSorted.Clear();

            // S�ralanm�� oyuncular listesinin say�s� 2 de�ilse
            if (GameManagerrr.GetComponent<GameManager>().PlayersSorted.Count != 2)
            {
                // S�ralanm�� oyuncular listesine, ge�ici listeyi ekler
                GameManagerrr.GetComponent<GameManager>().PlayersSorted.AddRange(GameManagerrr.GetComponent<GameManager>().PlayersTemp);
            }

            // S�ralaman�n i�indeki her oyuncu i�in
            foreach (GameObject Player in Ranking)
            {
                // Oyuncuyu yok eder
                Destroy(Player);
            }

            // S�ralaman�n ve oyun ba�lang�� listesinin i�eri�ini temizler
            Ranking.Clear();
            Starters.Clear();

            // Oyun y�neticisinin kameras�n� aktif hale getirir
            GameManagerrr.GetComponent<GameManager>().Kamera.SetActive(true);

            // Mini oyunun bitti�ini belirtir
            GameManagerrr.GetComponent<GameManager>().MiniGameStarted = false;

            // Fareyi g�r�n�r hale getirir
            Cursor.visible = true;

            // Fareyi kilitli durumdan ��kar�r
            Cursor.lockState = CursorLockMode.None;

            // Oyun y�neticisi nesnesini devre d��� b�rak�r
            gameObject.SetActive(false);
        }
    }

    // Rastgele bir harita olu�turur
    public void RandomMapGenerator()
    {
        // Bir oyuncunun spawnland��� noktay� belirler
        GameObject player = PhotonNetwork.Instantiate("AlpGamePlayer", Spawns[0].transform.position, Quaternion.identity);
    }
} */