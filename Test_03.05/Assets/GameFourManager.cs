using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class GameFourManager : MonoBehaviourPunCallbacks
{
    public GameObject GameManagerrr;
    public bool StartTime;
    public GameObject OBJCordinate;
    public GameObject DoorManager;
    //public GameObject GameFourGUI;
    //public GameObject GameFourGUI2;
    public List<GameObject> Ranking = new List<GameObject>();
    public List<GameObject> Starters = new List<GameObject>();
    //public TMP_Text Win;
    public bool IsWin;
    public TMP_Text Player2Text;
    public List<GameObject> allObjectsMain = new List<GameObject>();
    public TMP_Text LastTouch;

    public GameObject[] Spawns;

    public bool GameStarted;

    

    public TMP_Text countdownText;


    void Start()
    {
        GameManagerrr = GameObject.Find("GameManager");
        OBJCordinate = GameObject.Find("CoordinateManager");
    }




    void Update()
    {

        if(GameStarted)
        {
            GameStarted = false;
            RandomMapGenerator();
            if(allObjectsMain.Count == 28)
            {
                foreach (GameObject player in allObjectsMain)
                {
                    player.SetActive(true);
                }
            }

        }




        if (Ranking.Count == 4)
        {
            Ranking.Sort((player1, player2) => player2.GetComponent<GameFourPlayer>().score.CompareTo(player1.GetComponent<GameFourPlayer>().score));


            GameManagerrr.GetComponent<GameManager>().PlayersTemp.Clear();
            for (int i = 0; i < 4; i++)
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

            if (GameManagerrr.GetComponent<GameManager>().PlayersSorted.Count != 4)
            {
                GameManagerrr.GetComponent<GameManager>().PlayersSorted.AddRange(GameManagerrr.GetComponent<GameManager>().PlayersTemp);
            }


            foreach(GameObject Player in Ranking)
            {
                Destroy(Player);
            }
            Ranking.Clear();
            Starters.Clear();
            
            GameManagerrr.GetComponent<GameManager>().Kamera.SetActive(true);
            GameManagerrr.GetComponent<GameManager>().MiniGameStarted = false;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            gameObject.SetActive(false);
        }
    }

    public void RandomMapGenerator()
    {


        GameObject player = PhotonNetwork.Instantiate("AlpGamePlayer", Spawns[0].transform.position, Quaternion.identity);





    }
}
