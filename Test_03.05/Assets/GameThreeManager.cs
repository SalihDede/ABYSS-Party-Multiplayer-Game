using Photon.Pun;
using Photon.Pun.Demo.PunBasics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;


public class GameThreeManager : MonoBehaviourPunCallbacks
{

    public GameObject Door;

    public bool gameActive = false; // Indicates if the game has started
    public List<GameObject> StartingMans = new List<GameObject>();

    public bool StartTime;
    public TMP_Text Win;
    public bool IsWin;
    public List<GameObject> Ranking = new List<GameObject>();
    public GameObject GameManagerrr;
    public GameObject CharacterSpawn;
    public GameObject GameThreeGUI;

    public TMP_Text countdownText;
    public TMP_Text ElapsedTime;
    public float gameDuration = 120f; // Duration of the game in seconds
    private float elapsedTime = 0f; // Time elapsed since the start of the game
    private bool gameStarted = false; // Indicates if the game has started

    private PhotonView photonView;

    void Start()
    {
        
        photonView = GetComponent<PhotonView>();

        GameManagerrr = GameObject.Find("GameManager");

      
    }

    IEnumerator StartCountdownCoroutine()
    {
        int countdown = 10; // Initial countdown value
        while (countdown > 0)
        {
            countdownText.text = countdown.ToString(); 
            yield return new WaitForSeconds(1); 
            countdown--; 
        }

        countdownText.text = "GO!"; 
        yield return new WaitForSeconds(1); 
        countdownText.gameObject.SetActive(false); // Hide the countdown text
        photonView.RPC("StartRace", RpcTarget.All, true);
    }

    [PunRPC]
    void StartRace(bool result)
    {
        StartTime = result;
        gameStarted = result;
    }


    public void RandomMapGenerator()
    {
        GameObject player = PhotonNetwork.Instantiate("YusufGamePlayer", CharacterSpawn.transform.position, Quaternion.identity);
    }


    // Update is called once per frame
    void Update()
    {
        if (gameActive)
        {
            RandomMapGenerator();
            gameActive = false;
            StartCoroutine(StartCountdownCoroutine());
        }


        ElapsedTime.text = ((int)(elapsedTime)).ToString();


        if (StartTime)
        {
            if (gameStarted)
            {
                elapsedTime += Time.deltaTime;
            }
            Door.SetActive(false);
        }
        else
        {

            Door.SetActive(true);
        }

        if (Ranking.Count == 2)
        {
            GameManagerrr.GetComponent<GameManager>().Kamera.SetActive(true);

            GameManagerrr.GetComponent<GameManager>().PlayersTemp.Clear();
            for (int i = 0; i < 2; i++)
            {
                foreach (GameObject Player in GameManagerrr.GetComponent<GameManager>().PlayersSorted)
                {
                    if (Ranking[i].GetComponent<PhotonView>().ViewID / 1000 == Player.GetComponent<PhotonView>().ViewID / 1000)
                    {
                        GameManagerrr.GetComponent<GameManager>().PlayersTemp.Add(Player);
                    }
                }
            }

            GameManagerrr.GetComponent<GameManager>().PlayersSorted.Clear();

            if (GameManagerrr.GetComponent<GameManager>().PlayersSorted.Count != 2)
            {
                GameManagerrr.GetComponent<GameManager>().PlayersSorted.AddRange(GameManagerrr.GetComponent<GameManager>().PlayersTemp);
            }

            foreach (GameObject player in Ranking)
            {
                Destroy(player);
            }
            Ranking.Clear();
            StartingMans.Clear();
            GameManagerrr.GetComponent<GameManager>().MiniGameStarted = false;
            Cursor.lockState = CursorLockMode.None;
            gameObject.SetActive(false);

        }
    }
}
