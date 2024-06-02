using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRecognizerGameFive : MonoBehaviour
{

    public GameObject PuzzleManager;
    public GameObject GeneralManager;


    void Start()
    {
        
    }

    void Update()
    {
        
    }


    private void OnTriggerStay(Collider other)
    {
        
        if(other.tag == "Bullet")
        {
          
                PuzzleManager.GetComponent<PuzzleManager>().MainPlayerOfMap = other.gameObject;
           
        }

    }

}
