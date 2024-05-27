using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class ScriptForRandomWalls : MonoBehaviour
{
    public GameObject[] Option;
    private PhotonView photonView;
    void Start()
    {
        photonView = GetComponent<PhotonView>();
        if(PhotonNetwork.IsMasterClient)
        {
            int randomnumber = Random.Range(0, 3);
            photonView.RPC("RandomDoorClose", RpcTarget.All, randomnumber);
        }

    }


    [PunRPC]
    void RandomDoorClose(int number)
    {
        Option[0].SetActive(true);
        Option[1].SetActive(true);
        Option[2].SetActive(true);
        Option[number].SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
