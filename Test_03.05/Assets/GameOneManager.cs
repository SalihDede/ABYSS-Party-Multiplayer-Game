using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class GameOneManager : MonoBehaviourPunCallbacks
{
    public GameObject GameManagerrr;
    public bool Goal;
    public GameObject BallPrefab;

    public List<GameObject> Ranking = new List<GameObject>();
    public List<GameObject> Starters = new List<GameObject>();


    public int Player1;
    public int Player2;
    public int Player3;
    public int Player4;



    public TMP_Text countdownText;


    public TMP_Text Player1Text;
    public TMP_Text Player2Text;
    public TMP_Text Player3Text;
    public TMP_Text Player4Text;

    public TMP_Text LastTouch;

    public GameObject Spawn0;
    public GameObject BallSpawn;
    public GameObject SpawnedBall;

    private PhotonView photonView;
    public bool GameFinished;
    void Start()
    {
        GameManagerrr = GameObject.Find("GameManager");


        Goal = true;
        RandomMapGenerator();
    }





    IEnumerator GameFinishedCoroutine()
    {
        Starters.Sort((player1, player2) => player2.GetComponent<PlayerABYSS>().score.CompareTo(player1.GetComponent<PlayerABYSS>().score));

        if (Starters.Count == 2)
        {

                GameManagerrr.GetComponent<GameManager>().PlayersTemp.Clear();
                for (int i = 0; i < 2; i++)
                {
                    foreach (GameObject Player in GameManagerrr.GetComponent<GameManager>().PlayersSorted)
                    {
                        if (Starters[i].GetComponent<PhotonView>().ViewID / 1000 == Player.GetComponent<PhotonView>().ViewID / 1000)
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

     

            yield return new WaitForSeconds(10);
            foreach (GameObject player in Starters)
            {
                Destroy(player);
            }
            Starters.Clear();
            gameObject.SetActive(false);
        }
    }

        IEnumerator StartCountdownCoroutine()
        {
            int countdown = 60; // Initial countdown value
            while (countdown > 0)
            {
                countdownText.text = countdown.ToString();
                yield return new WaitForSeconds(1);
                countdown--;
            }

            countdownText.text = "Finish";
            yield return new WaitForSeconds(1);
            countdownText.gameObject.SetActive(false); // Hide the countdown text



        }




    void Update()
    {
        if(countdownText.text == "Finish" || GameFinished == true)
        {
            GameFinished = true;

            Destroy(SpawnedBall);
            StartCoroutine(GameFinishedCoroutine());




        }


        if(!GameFinished)
        {
      
            if (Goal && PhotonNetwork.IsMasterClient)
            {
                Goal = false;
                
                SpawnedBall = PhotonNetwork.Instantiate("Soccer Ball", BallSpawn.transform.position, Quaternion.identity);
                StartCoroutine(StartCountdownCoroutine());
            }


            if (SpawnedBall.GetComponent<ball>().photonView.OwnerActorNr == Starters[0].GetComponent<PhotonView>().OwnerActorNr)
            {
                Debug.Log("1. OYUNCU TOPUN SAHÝBÝ");
                StartCoroutine(ScoreUp(Starters[0].GetComponent<PhotonView>().OwnerActorNr));
            }
            if (SpawnedBall.GetComponent<ball>().photonView.OwnerActorNr == Starters[1].GetComponent<PhotonView>().OwnerActorNr)
            {
                Debug.Log("2. OYUNCU TOPUN SAHÝBÝ");
                StartCoroutine(ScoreUp(Starters[1].GetComponent<PhotonView>().OwnerActorNr));
            }
            /*
            if (SpawnedBall.GetComponent<ball>().photonView.OwnerActorNr == Starters[2].GetComponent<PhotonView>().OwnerActorNr)
            {
                StartCoroutine(ScoreUp(Starters[2].GetComponent<PlayerABYSS>().score));
            }
            if (SpawnedBall.GetComponent<ball>().photonView.OwnerActorNr == Starters[3].GetComponent<PhotonView>().OwnerActorNr)
            {
                StartCoroutine(ScoreUp(Starters[3].GetComponent<PlayerABYSS>().score));
            }
            */


        }



      
            Player1Text.text = "Player 1: " + Starters[0].GetComponent<PlayerABYSS>().score;
            Player2Text.text = "Player 2: " + Starters[1].GetComponent<PlayerABYSS>().score;
           /* Player3Text.text = "Player 3: " + Starters[2].GetComponent<PlayerABYSS>().score;
            Player4Text.text = "Player 4: " + Starters[3].GetComponent<PlayerABYSS>().score;
           */
    }

        public void RandomMapGenerator()
        {
             GameObject player1 = PhotonNetwork.Instantiate("TemplatePlayer", Spawn0.transform.position, Quaternion.identity);
        }

    IEnumerator ScoreUp(int thisguy)
    {
        while (thisguy < 100)
        {
            yield return new WaitForSeconds(1);
            if (Starters[0].GetComponent<PhotonView>().OwnerActorNr == thisguy)
            {
                Starters[0].GetComponent<PlayerABYSS>().score++;
            }
            if (Starters[1].GetComponent<PhotonView>().OwnerActorNr == thisguy)
            {
                Starters[1].GetComponent<PlayerABYSS>().score++;
            }
        }
    }
}
