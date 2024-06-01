using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class GameFiveManager : MonoBehaviourPunCallbacks
{
    public GameObject GameManagerrrr;
    public bool StartTime;
    public GameObject GameTwoGUI;
    public List<GameObject> Ranking = new List<GameObject>();
    public TMP_Text Win;
    public bool IsWin;
    public GameObject[] Spawns;
    public bool GameFinished;
    public TMP_Text countdownText;



    void Start()
    {
    GameManagerrrr = GameObject.Find("GameManager");
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

    public void RandomMapGenerator(){
        GameObject player = PhotonNetwork.Instantiate("Prometheus", Spawns[0].transform.position, Quaternion.identity);

    }





    void Update()
    {


    }

    
}
