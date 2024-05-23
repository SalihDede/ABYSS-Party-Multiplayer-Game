using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerABYSS : MonoBehaviour
{

    public Animator Anim;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {




        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
        {
            Anim.SetBool("IsRun", true);
        }
        else
        {
            Anim.SetBool("IsRun", false);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Anim.SetBool("IsJump", true);
        }
        else
        {
            Anim.SetBool("IsJump", false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Ball")
        {
            if (Input.GetMouseButtonDown(0))
            {
                Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
                rb.AddForce(transform.forward * 10f, ForceMode.Impulse);
            }
        }
    }

}
