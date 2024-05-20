using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class OnlineSystem : MonoBehaviour
{
    public GameObject camera;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!gameObject.GetComponent<PhotonView>().IsMine)
        {
            gameObject.GetComponent<KinematicCharacterController.KinematicCharacterMotor>().enabled = false;
            gameObject.GetComponent<KinematicCharacterController.Walkthrough.MultipleMovementStates.MyCharacterController>().enabled = false;
            camera.GetComponent<Camera>().enabled = false;
        }
    }
}
