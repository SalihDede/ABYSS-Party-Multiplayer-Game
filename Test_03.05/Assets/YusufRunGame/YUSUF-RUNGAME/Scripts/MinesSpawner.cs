using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;

public class MinesSpawner : MonoBehaviour
{
    public GameObject[] Mines;
    private PhotonView photonView;

    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();
        if (PhotonNetwork.IsMasterClient)
        {
            int randomnumber = Random.Range(0, 3);
            photonView.RPC("RandomMines", RpcTarget.All, randomnumber);
        }
        
    }

    void RandomMines(int number)
    {
        Mines[0].SetActive(true);
        Mines[1].SetActive(true);
        Mines[2].SetActive(true);
        Mines[number].SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
