using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KinematicCharacterController;

public class PlayerThirdGame : MonoBehaviour
{
    
    public GameObject GameManagerr;

    void Start()
    {
        GameManagerr = GameObject.Find("YusufGame");
        GameManagerr.GetComponent<GameThreeManager>().StartingMans.Add(gameObject);
    }



    void Update()
    {
  
    }
}
