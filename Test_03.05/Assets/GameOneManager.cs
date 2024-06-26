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
    public bool Goal1;
    public bool Goal2;
    public GameObject BallPrefab;
    public GameObject GUIDE;
    public List<GameObject> Ranking = new List<GameObject>();
    public List<GameObject> Starters = new List<GameObject>();

    Player thisguy;

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

    public bool GameFinished;
    void Start()
    {
        GameManagerrr = GameObject.Find("GameManager");
        GameFinished = false;
        StartCoroutine(GUIDECoroutine());

    }



    [PunRPC]
    void HostListCopying(List<GameObject> Starterss)
    {

        for(int i = 0; i< 4; i++)
        {

            Starters[i] = Starterss[i];
        }


    }

    IEnumerator GameFinishedCoroutine()
    {

  /*      if(PhotonNetwork.IsMasterClient)
        {
           
               
                photonView.RPC("HostListCopying", RpcTarget.All, Starters);

        }
  */
        Starters.Sort((player1, player2) => player2.GetComponent<PlayerABYSS>().score.CompareTo(player1.GetComponent<PlayerABYSS>().score));


        if (Starters.Count == 4)
        {

                GameManagerrr.GetComponent<GameManager>().PlayersTemp.Clear();
                for (int i = 0; i < 4; i++)
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

                if (GameManagerrr.GetComponent<GameManager>().PlayersSorted.Count != 4)
                {
                    GameManagerrr.GetComponent<GameManager>().PlayersSorted.AddRange(GameManagerrr.GetComponent<GameManager>().PlayersTemp);
                }

     

            yield return new WaitForSeconds(3);
            foreach (GameObject player in Starters)
            {
                Destroy(player);
            }
            Starters.Clear();

            foreach (GameObject player in GameManagerrr.GetComponent<GameManager>().PlayersSorted)
            {
                if (player.GetComponent<PhotonView>().IsMine)
                {
                    player.GetComponent<DiceController>().GUI.SetActive(true);
                }
            }


            GameManagerrr.GetComponent<GameManager>().Kamera.SetActive(true);
            GameFinished = false;
            GameManagerrr.GetComponent<GameManager>().MiniGameStarted = false;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            gameObject.SetActive(false);
        }
    }

        IEnumerator StartCountdownCoroutine()
        {

            int countdown = 45; // Initial countdown value
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



    public void BallSpawnMethod()
    {
        if (Goal && PhotonNetwork.IsMasterClient)
        {
            Goal = false;

            PhotonNetwork.Instantiate("Soccer Ball", BallSpawn.transform.position, Quaternion.identity);
        }
    }

    void FixedUpdate()
    {

      



        if (GameFinished)
        {
            countdownText.text = "";
            GameFinished = true;

            Destroy(SpawnedBall);
            StartCoroutine(GameFinishedCoroutine());




        }
        else
        {
            StopCoroutine(GameFinishedCoroutine());
        }





        if(!GameFinished)
        {
                if (Goal)
                {

                        BallSpawnMethod();
                        RandomMapGenerator();
                        StartCoroutine(StartCountdownCoroutine());
                        Goal = false;
                }
     

            if(Starters.Count == 4)
            {

                if (SpawnedBall.GetComponent<PhotonView>().Owner == Starters[0].GetComponent<PhotonView>().Owner && SpawnedBall.GetComponent<ball>().SomeoneTouch)
                {
                    Debug.Log("1. OYUNCU TOPUN SAH�B�");
                    thisguy = Starters[0].GetComponent<PhotonView>().Owner;
                    StartCoroutine(ScoreUp());
                }

                if (SpawnedBall.GetComponent<PhotonView>().Owner == Starters[1].GetComponent<PhotonView>().Owner && SpawnedBall.GetComponent<ball>().SomeoneTouch)
                {
                    thisguy = Starters[1].GetComponent<PhotonView>().Owner;
                    StartCoroutine(ScoreUp());
                }

                
                if (SpawnedBall.GetComponent<ball>().photonView.OwnerActorNr == Starters[2].GetComponent<PhotonView>().OwnerActorNr)
                {
                    thisguy = Starters[2].GetComponent<PhotonView>().Owner;
                    StartCoroutine(ScoreUp());
                }
                if (SpawnedBall.GetComponent<ball>().photonView.OwnerActorNr == Starters[3].GetComponent<PhotonView>().OwnerActorNr)
                {
                    thisguy = Starters[3].GetComponent<PhotonView>().Owner;
                    StartCoroutine(ScoreUp());
                }
                


            }




            Player1Text.text = Starters[0].GetComponent<PhotonView>().Owner.NickName+": " + Starters[0].GetComponent<PlayerABYSS>().score;
            Player2Text.text = Starters[1].GetComponent<PhotonView>().Owner.NickName + ": " + Starters[1].GetComponent<PlayerABYSS>().score;
            Player3Text.text = Starters[2].GetComponent<PhotonView>().Owner.NickName + ": " + Starters[2].GetComponent<PlayerABYSS>().score;
            Player4Text.text = Starters[3].GetComponent<PhotonView>().Owner.NickName + ": " + Starters[3].GetComponent<PlayerABYSS>().score;
            
        }

    }

    public IEnumerator GUIDECoroutine()
    {
        int countdown = 15; // Initial countdown value
        while (countdown > 0)
        {
            countdownText.text = countdown.ToString();
            yield return new WaitForSeconds(1);
            if (countdown < 5)
            {
                GUIDE.SetActive(false);
            }
            countdown--;
        }
        Goal = true;
        countdownText.text = "";
        yield return new WaitForSeconds(1);
        countdownText.gameObject.SetActive(false); // Hide the countdown text
    }


    public void RandomMapGenerator()
        {
        
             GameObject player1 = PhotonNetwork.Instantiate("TemplatePlayer", Spawn0.transform.position, Quaternion.identity);
        }

    IEnumerator ScoreUp()
    {
        while (!GameFinished)
        {
            yield return new WaitForSeconds(1);
            if (Starters[0].GetComponent<PhotonView>().Owner == thisguy)
            {
                if (Starters[0].GetComponent<PhotonView>().Owner != thisguy)
                {
                    break;
                }
                Starters[0].GetComponent<PlayerABYSS>().score++;
            }
            if (Starters[1].GetComponent<PhotonView>().Owner == thisguy)
            {   if(Starters[1].GetComponent<PhotonView>().Owner != thisguy)
                {
                    break;
                }
                Starters[1].GetComponent<PlayerABYSS>().score++;
            }
            
            if (Starters[2].GetComponent<PhotonView>().Owner == thisguy)
            {   if(Starters[2].GetComponent<PhotonView>().Owner != thisguy)
                {
                    break;
                }
                Starters[2].GetComponent<PlayerABYSS>().score++;
            }
            if (Starters[3].GetComponent<PhotonView>().Owner == thisguy)
            {   if(Starters[3].GetComponent<PhotonView>().Owner != thisguy)
                {
                    break;
                }
                Starters[3].GetComponent<PlayerABYSS>().score++;
            }
            
        }
    }
}
