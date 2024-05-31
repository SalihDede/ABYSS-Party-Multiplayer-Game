using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerGameTwo : MonoBehaviourPunCallbacks
{

    public GameObject GameManagerr;
    public int CheckPoint = 0;
    public int Finish = -1;
    public int TempFinish;
    public int TempCheckPoint;
    public bool passedFinish;
    public bool passedCheckPoint;
    public GameObject Line1;
    public GameObject Line2;
    public GameObject Line3;
    public GameObject Line4;

    public List<GameObject> Spawns = new List<GameObject>();


    private PhotonView photonView;
    void Start()
    {
        gameObject.transform.eulerAngles = new Vector3(359.936462f, 281.749481f, 359.754913f);
        

        GameManagerr = GameObject.Find("Baha Game");

        Line1 = GameObject.Find("Line1");
        Line2 = GameObject.Find("Line2");
        Line3 = GameObject.Find("Line3");
        Line4 = GameObject.Find("Line4");

        Spawns.Add(Line1);
        Spawns.Add(Line2);
        Spawns.Add(Line3);
        Spawns.Add(Line4);


        photonView = GetComponent<PhotonView>();

        GameManagerr.GetComponent<GameTwoManager>().StartCar.Add(gameObject);


        transform.position = Spawns[GameManagerr.GetComponent<GameTwoManager>().StartCar.IndexOf(gameObject)].transform.position;


    }


    void Update()
    {
        



        if(Finish >=3 && CheckPoint >=2)
        {
            photonView.RPC("Winner", RpcTarget.All, photonView.OwnerActorNr.ToString());
        }


        if(GameManagerr.GetComponent<GameTwoManager>().StartTime)
        {
            gameObject.GetComponent<PrometeoCarController>().enabled = true;
        }
        else
        {
            gameObject.GetComponent<PrometeoCarController>().enabled = false;
        }

   

        if(PhotonNetwork.IsMasterClient && GameManagerr.GetComponent<GameTwoManager>().countdownText.text == "GO!")
        {
            photonView.RPC("StartRace", RpcTarget.All, true);
        }


    }


    [PunRPC]
    void StartRace(bool result)
    {
        GameManagerr.GetComponent<GameTwoManager>().GameTwoGUI.SetActive(false);
        GameManagerr.GetComponent<GameTwoManager>().StartTime = result;
    }



    [PunRPC]
    void FinishUpdate()
    {

        Finish += 1;
        
    }
    [PunRPC]
    void CheckPointUpdate()
    {
        CheckPoint += 1;
    }

    [PunRPC]
    void Winner(string name)
    {
        if(!GameManagerr.GetComponent<GameTwoManager>().IsWin)
        {
            GameManagerr.GetComponent<GameTwoManager>().Win.gameObject.SetActive(true);
            GameManagerr.GetComponent<GameTwoManager>().Win.text = "Winner is " + name;
        }
        if(!GameManagerr.GetComponent<GameTwoManager>().Ranking.Contains(gameObject))
        {
            GameManagerr.GetComponent<GameTwoManager>().Ranking.Add(gameObject);
        }

    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Finish")
        {
            if(!passedFinish)
            {
                passedFinish = true;
                passedCheckPoint = false;
                photonView.RPC("FinishUpdate", RpcTarget.All);
            }

        }
        if (other.tag == "CheckPoint")
        {
            if (!passedCheckPoint)
            {
                passedFinish = false;
                passedCheckPoint = true;
                photonView.RPC("CheckPointUpdate", RpcTarget.All);
            }
        }
    }

}
