using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
public class Movement1 : MonoBehaviour
{
    [SerializeField] float distanceToCover;
    private float speed;
    public Vector3 startingPosition;
    private Vector3 v;
    private PhotonView photonView;
    void Start()
    {
        photonView = GetComponent<PhotonView>();
        if (PhotonNetwork.IsMasterClient)
        {
            float speedrandom = Random.Range(1.5f, 3f);
            photonView.RPC("RandomWallMove", RpcTarget.All, speedrandom);
        }


        startingPosition = transform.position;
        speed = Random.Range(1.5f, 3f);
    }
    [PunRPC]
    void RandomWallMove(float number)
    {
        speed = number;
    }
    // Update is called once per frame
    void Update()
    {

        v = startingPosition;
        v.z += distanceToCover*Mathf.Sin(Time.time*speed);
        transform.position = v;
    }
}
