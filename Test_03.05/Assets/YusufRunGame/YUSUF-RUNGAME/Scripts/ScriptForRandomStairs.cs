using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptForRandomStairs : MonoBehaviour
{
    public GameObject[] Option;
    private PhotonView photonView;

    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();
        if (PhotonNetwork.IsMasterClient)
        {
            int randomnumber = Random.Range(0, 3);
            photonView.RPC("RandomStairs", RpcTarget.All, randomnumber);
        }

    }
    [PunRPC]
    void RandomStairs (int number)
    {
        Option[0].SetActive(false);
        Option[1].SetActive(false);
        Option[2].SetActive(false);
        Option[number].SetActive(true);
    }

}
