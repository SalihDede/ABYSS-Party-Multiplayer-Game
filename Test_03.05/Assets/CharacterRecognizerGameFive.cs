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


    private void OnTriggerEnter(Collider other)
    {
        
        if(other.tag == "Bullet")
        {


            if (PuzzleManager.GetComponent<PuzzleManager>().MainPlayerOfMap == null)
            {
                PuzzleManager.GetComponent<PuzzleManager>().MainPlayerOfMap = other.gameObject;
            }

            if (!GeneralManager.GetComponent<GameFiveManager>().Starters.Contains(other.gameObject))
            {
                GeneralManager.GetComponent<GameFiveManager>().Starters.Add(other.gameObject);
            }

        }

    }

}
