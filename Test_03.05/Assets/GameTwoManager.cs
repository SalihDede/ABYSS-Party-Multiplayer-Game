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

    private PhotonView photonView;


    public TMP_Text countdownText;


    void Start()
    {

        photonView = GetComponent<PhotonView>();
        GameManagerrr = GameObject.Find("GameManager");
        RandomMapGenerator();
        StartCoroutine(StartCountdownCoroutine()); 
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
        photonView.RPC("StartRace", RpcTarget.All, true);
    }

    

    [PunRPC]
    void StartRace(bool result)
    {
        GameTwoGUI.SetActive(false);
        StartTime = result;
    }



    void Update()
    {

        if (Ranking.Count == 2)
        {
            GameManagerrr.GetComponent<GameManager>().Kamera.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    public void RandomMapGenerator()
    {


                GameObject player = PhotonNetwork.Instantiate("Prometheus", Spawns[0].transform.position, Quaternion.identity);
           

       
     
        
    }
}
