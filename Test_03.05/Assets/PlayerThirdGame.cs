using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class PlayerThirdGame : MonoBehaviour
{
    private PhotonView photonView;
    public GameObject GameManagerr;
    public GameObject CP2;
    public bool isDeath;

    void Start()
    {
        photonView = gameObject.GetComponent<PhotonView>();
        CP2 = GameObject.Find("seksenseksen");
        GameManagerr = GameObject.Find("YusufGame");
        GameManagerr.GetComponent<GameThreeManager>().StartingMans.Add(gameObject);
    }

    [PunRPC]
    void Death2()
    {
        Debug.Log("Death2 method called"); // Check if the method is called
        
        if (photonView != null)
        {
            Debug.Log("PhotonView found"); // Check if PhotonView is found
            if (photonView.IsMine)
            {
                Debug.Log("IsMine is true"); // Check if IsMine is true
                if (CP2 != null)
                {
                    Debug.Log("CP2 found"); // Check if CP2 is found
                    gameObject.transform.position = CP2.transform.position;
                }
                else
                {
                    Debug.LogError("CP2 is null"); // Log an error if CP2 is null
                }
            }
            else
            {
                Debug.LogWarning("IsMine is false"); // Log a warning if IsMine is false
            }
        }
        else
        {
            Debug.LogError("PhotonView is null"); // Log an error if PhotonView is null
        }
    }

    void Update()
    {
  
        if(isDeath)
        {
            //isDeath = false;
            photonView.RPC("Death2", RpcTarget.All);
        }


    }
}
