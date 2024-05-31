using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class GameFourManager : MonoBehaviourPunCallbacks
{
    public GameObject GameManagerr;
    public bool StartTime;
    //public GameObject GameFourGUI;
    //public GameObject GameFourGUI2;
    public List<GameObject> Ranking = new List<GameObject>();
    public List<GameObject> Starters = new List<GameObject>();
    //public TMP_Text Win;
    public bool IsWin;
    public TMP_Text Player2Text;

    public TMP_Text LastTouch;

    public GameObject[] Spawns;




    public TMP_Text countdownText;


    void Start()
    {
        GameManagerr = GameObject.Find("GameManager");
        RandomMapGenerator();
       // StartCoroutine(StartCountdownCoroutine());
    }

    IEnumerator StartCountdownCoroutine()
    {
        int countdown = 15;
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
        //GameFourGUI.SetActive(false);
        //GameFourGUI2.SetActive(false);
    }


    void Update()
    {

        




        if (Ranking.Count == 2)
        {
            Ranking.Sort((player1, player2) => player2.GetComponent<GameFourPlayer>().score.CompareTo(player1.GetComponent<GameFourPlayer>().score));

           

            GameManagerr.GetComponent<GameManager>().Kamera.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    public void RandomMapGenerator()
    {


        GameObject player = PhotonNetwork.Instantiate("AlpGamePlayer", Spawns[0].transform.position, Quaternion.identity);





    }
}
