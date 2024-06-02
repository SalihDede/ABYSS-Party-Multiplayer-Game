using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class PlayerThirdGame : MonoBehaviour
{
    
    public GameObject GameManagerr;
    public GameObject CP2;
    public bool isDeath;

    void Start()
    {
        CP2 = GameObject.Find("SpawnerBefore2");
        GameManagerr = GameObject.Find("YusufGame");
        GameManagerr.GetComponent<GameThreeManager>().StartingMans.Add(gameObject);
    }

    [PunRPC]
    void Death2()
    {
            Debug.Log("Harbi Oldu");
            if (gameObject.GetComponent<PhotonView>().IsMine)
            {
                gameObject.transform.position = CP2.transform.position;
            }
       
    }

    void Update()
    {
  
        if(isDeath)
        {
            isDeath = false;
            gameObject.GetComponent<PhotonView>().RPC("Death2", RpcTarget.All);
        }


    }
}
