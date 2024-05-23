using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerGameTwo : MonoBehaviour
{

    public GameObject GameManagerr;
    public int CheckPoint = 0;
    public int Finish = -1;
    public int TempFinish;
    public int TempCheckPoint;
    public bool passedFinish;
    public bool passedCheckPoint;

    private PhotonView photonView;
    void Start()
    {
        GameManagerr = GameObject.Find("Baha Game");
        photonView = GetComponent<PhotonView>();
    }


    void Update()
    {
        
        if(Finish >=3 && CheckPoint >=3)
        {
            photonView.RPC("Winner", RpcTarget.All, photonView.OwnerActorNr.ToString());
        }


   

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
            GameManagerr.GetComponent<GameTwoManager>().Win.text = "Winner is " + name;
        }
        GameManagerr.GetComponent<GameTwoManager>().Ranking.Add(gameObject);
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
