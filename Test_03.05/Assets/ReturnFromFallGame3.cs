using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnFromFallGame3 : MonoBehaviour
{
    public GameObject SpawnAgain;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Bullet")
        {
            Debug.Log("Deðdi");
            other.gameObject.GetComponent<PlayerThirdGame>().isDeath = true;
        }
    }

}
