using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class GameTwoManager : MonoBehaviourPunCallbacks
{
    public GameObject GameManagerrr;
    public GameObject GUIDE;
    public bool StartTime;
    public GameObject GameTwoGUI;
    public List<GameObject> Ranking = new List<GameObject>();
    public List<GameObject> StartCar = new List<GameObject>();
    public TMP_Text Win;
    public bool IsWin;
    public TMP_Text Player2Text;

    public TMP_Text LastTouch;

    public GameObject[] Spawns;
    private int currentSpawnIndex = 0; // Current spawn point index

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
            GUIDE.SetActive(true);
            countdownText.text = countdown.ToString(); 
            yield return new WaitForSeconds(1); 
            if(countdown<5)
            {
                GUIDE.SetActive(false);
            }
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

        if (Ranking.Count == 0)
        {
            Win.text = "";
        }
        if (Ranking.Count == 1)
        {
            Win.text = Ranking.IndexOf(Ranking[0]) + " " + Ranking[0].GetComponent<PhotonView>().Controller.NickName + "\n";
        }
        if (Ranking.Count == 2)
        {
            Win.text = Ranking.IndexOf(Ranking[0])+" " + Ranking[0].GetComponent<PhotonView>().Controller.NickName + "\n" + Ranking.IndexOf(Ranking[1]) + " " + Ranking[1].GetComponent<PhotonView>().Controller.NickName;
        }
        if (Ranking.Count == 3)
        {
            Win.text = Ranking.IndexOf(Ranking[0]) + " " + Ranking[0].GetComponent<PhotonView>().Controller.NickName + "\n" + Ranking.IndexOf(Ranking[1]) + " " + Ranking[1].GetComponent<PhotonView>().Controller.NickName + "\n" + Ranking.IndexOf(Ranking[2]) + " " + Ranking[2].GetComponent<PhotonView>().Controller.NickName;
        }
        if (Ranking.Count == 4)
        {
            Win.text = Ranking.IndexOf(Ranking[0]) + " " + Ranking[0].GetComponent<PhotonView>().Controller.NickName + "\n" + Ranking.IndexOf(Ranking[1]) + " " + Ranking[1].GetComponent<PhotonView>().Controller.NickName + "\n" + Ranking.IndexOf(Ranking[2]) + " " + Ranking[2].GetComponent<PhotonView>().Controller.NickName + "\n" + Ranking.IndexOf(Ranking[3]) + " " + Ranking[3].GetComponent<PhotonView>().Controller.NickName;
        }


        if (Ranking.Count == 4)
        {
            GameManagerrr.GetComponent<GameManager>().PlayersSorted[0].GetComponent<DiceController>().Kameraa.gameObject.SetActive(true);
            GameManagerrr.GetComponent<GameManager>().PlayersSorted[1].GetComponent<DiceController>().Kameraa.gameObject.SetActive(false);
            GameManagerrr.GetComponent<GameManager>().PlayersSorted[2].GetComponent<DiceController>().Kameraa.gameObject.SetActive(false);
            GameManagerrr.GetComponent<GameManager>().PlayersSorted[3].GetComponent<DiceController>().Kameraa.gameObject.SetActive(false);
            gameObject.SetActive(false);

            GameManagerrr.GetComponent<GameManager>().PlayersTemp.Clear();
            for (int i=0;i<4;i++)
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

            if (GameManagerrr.GetComponent<GameManager>().PlayersSorted.Count !=4)
            {
                GameManagerrr.GetComponent<GameManager>().PlayersSorted.AddRange(GameManagerrr.GetComponent<GameManager>().PlayersTemp);
            }

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
        if (Spawns.Length == 0)
        {
            Debug.LogError("No spawn points available.");
            return;
        }

        // Get the player's index in the Photon player list
        int playerIndex = PhotonNetwork.LocalPlayer.ActorNumber - 1;

        // Ensure the spawn index is within bounds
        int spawnIndex = playerIndex % Spawns.Length;

        // Instantiate the player at the corresponding spawn point
        PhotonNetwork.Instantiate("Prometheus", Spawns[spawnIndex].transform.position, Quaternion.identity);
    }
}
