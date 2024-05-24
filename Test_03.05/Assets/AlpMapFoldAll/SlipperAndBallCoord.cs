using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlipperAndBallCoord : MonoBehaviour
{
    public GameObject Slipper; // �kinci HappyPillow objesini bu alana atayaca��z

    private Vector3[] ball = new Vector3[]
    {
        new Vector3(86.6f, 8.316042f, -11.83404f),
        new Vector3(15.11f, 1.464f, 2.45f),
        new Vector3(80.836f, 8.316042f, 54.05f)
    };

    private Vector3[] slipper = new Vector3[]
    {
        new Vector3(81.41604f, 7.841867f, -1.08615f),
        new Vector3(30.642f, 1.45f, 66.69f),
        new Vector3(86.04f, 7.94f, -12.839f)
    };

    // Start is called before the first frame update
    void Start()
    {
        // �lk objenin pozisyonunu rastgele belirle
        int randomIndex = Random.Range(0, ball.Length);
        transform.position = ball[randomIndex];

        // �kinci objenin pozisyonunu belirle
        if (Slipper != null)
        {
            Slipper.transform.position = slipper[randomIndex];
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
