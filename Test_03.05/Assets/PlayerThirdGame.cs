using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KinematicCharacterController;

public class PlayerThirdGame : MonoBehaviour
{
    
    public GameObject GameManagerr;

    void Start()
    {
        GameManagerr = GameObject.Find("Yusuf Game");
        GameManagerr.GetComponent<GameThreeManager>().StartingMans.Add(gameObject);
    }



    void Update()
    {
        if(GameManagerr.GetComponent<GameThreeManager>().StartTime)
        {
            gameObject.GetComponent<KinematicCharacterController.Walkthrough.MultipleMovementStates.MyCharacterController>().enabled = true;
        }
        else
        {
            gameObject.GetComponent<KinematicCharacterController.Walkthrough.MultipleMovementStates.MyCharacterController>().enabled = false;
        }
    }
}
