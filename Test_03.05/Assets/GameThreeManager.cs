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

    public List<GameObject> StartingMans = new List<GameObject>();
    public TMP_Text countdownText;
    public bool StartTime;
    public TMP_Text Win;
    public bool IsWin;
    public List<GameObject> Ranking = new List<GameObject>();
    public GameObject GameManagerr;
    public GameObject CharacterSpawn;
    public GameObject GameThreeGUI;


    private PhotonView photonView;

    void Start()
    {
        
        photonView = GetComponent<PhotonView>();

        GameManagerr = GameObject.Find("GameManager");
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
        GameThreeGUI.SetActive(false);
        StartTime = result;
    }


    public void RandomMapGenerator()
    {
        GameObject player = PhotonNetwork.Instantiate("YusufGamePlayer", CharacterSpawn.transform.position, Quaternion.identity);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
