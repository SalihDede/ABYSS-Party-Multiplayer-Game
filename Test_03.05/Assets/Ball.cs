using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BallController : MonoBehaviourPun, IPunObservable
{
    Rigidbody rb;
    public float speed = 10f; // Adjust speed as needed

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (photonView.IsMine)
        {
            // Process player input to control the ball
            // For example, using Input.GetAxis("Horizontal") and Input.GetAxis("Vertical")
            // to control the ball's movement.
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

            rb.AddForce(movement * speed);
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(rb.position);
            stream.SendNext(rb.velocity);
        }
        else
        {
            rb.position = (Vector3)stream.ReceiveNext();
            rb.velocity = (Vector3)stream.ReceiveNext();
        }
    }
}
