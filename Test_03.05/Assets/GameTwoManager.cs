using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class GameTwoManager : MonoBehaviourPunCallbacks
{
    public GameObject GameManagerrr;
    public bool StartTime;
    public GameObject GameTwoGUI;
    public List<GameObject> Ranking = new List<GameObject>();
    public List<GameObject> StartCar = new List<GameObject>();
    public TMP_Text Win;
    public bool IsWin;
    public TMP_Text Player2Text;

    public TMP_Text LastTouch;

    public GameObject[] Spawns;

    public bool GameFinished;



    public TMP_Text countdownText;


    void Start()
    {

        GameManagerrr = GameObject.Find("GameManager");

    }

    IEnumerator StartCountdownCoroutine()
    {
        int countdown = 15; // Initial countdown value
        while (countdown > 0)
        {
            countdownText.text = countdown.ToString(); 
            yield return new WaitForSeconds(1); 
            countdown--; 
        }

        countdownText.text = "GO!"; 
        yield return new WaitForSeconds(1); 
        countdownText.gameObject.SetActive(false); // Hide the countdown text
    }

    





    void Update()
    {


        if(!GameFinished)
        {
            countdownText.gameObject.SetActive(true);
            GameFinished = true;
            RandomMapGenerator();
            StartCoroutine(StartCountdownCoroutine());
        }


        if (Ranking.Count == 2)
        {
            GameManagerrr.GetComponent<GameManager>().Kamera.SetActive(true);
            gameObject.SetActive(false);

            GameManagerrr.GetComponent<GameManager>().PlayersTemp.Clear();
            for (int i=0;i<2;i++)
            {
                foreach (GameObject Player in GameManagerrr.GetComponent<GameManager>().PlayersSorted)
                {
                    if (Ranking[i].GetComponent<PhotonView>().ViewID/1000 == Player.GetComponent<PhotonView>().ViewID/1000)
                    {
                        GameManagerrr.GetComponent<GameManager>().PlayersTemp.Add(Player);
                    }
                }
            }

            GameManagerrr.GetComponent<GameManager>().PlayersSorted.Clear();

            if (GameManagerrr.GetComponent<GameManager>().PlayersSorted.Count !=2)
            {
                GameManagerrr.GetComponent<GameManager>().PlayersSorted.AddRange(GameManagerrr.GetComponent<GameManager>().PlayersTemp);
            }



            //GameManagerrr.GetComponent<GameManager>().PlayersSorted.Sort((player1, player2) => player2.GetComponent<>().score.CompareTo(player1.GetComponent<GameFourPlayer>().score));


            GameFinished = false;
            foreach (GameObject player in Ranking)
            {
                Destroy(player);
            }
            Ranking.Clear();

        }
    }

    public void RandomMapGenerator()
    {


                GameObject player = PhotonNetwork.Instantiate("SalihGamePlayer", Spawns[0].transform.position, Quaternion.identity);
           

       
     
        
    }
}
