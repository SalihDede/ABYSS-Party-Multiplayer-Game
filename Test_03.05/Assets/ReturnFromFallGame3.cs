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
            other.gameObject.GetComponent<Rigidbody>().useGravity = false;
            other.gameObject.transform.position = SpawnAgain.transform.position;
            other.gameObject.GetComponent<Rigidbody>().useGravity = true;
        }
    }

}
