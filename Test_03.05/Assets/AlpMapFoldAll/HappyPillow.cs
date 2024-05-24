using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HappyPillow : MonoBehaviour
{
    public GameObject secondHappyPillow; // Ýkinci HappyPillow objesini bu alana atayacaðýz

    private Vector3[] happyPillow = new Vector3[]
    {
        new Vector3(26.5585f, 1.446077f, -41.06846f),
        new Vector3(15.11f, 1.464f, -39.648f),
        new Vector3(35.846f, 2.698f, -35.863f)
    };

    private Vector3[] happyPillowFace = new Vector3[]
    {
        new Vector3(26.707f, 1.483f, -41.175f),
        new Vector3(15.207f, 1.506f, -39.768f),
        new Vector3(36.011f, 2.737f, -35.988f)
    };

    // Start is called before the first frame update
    void Start()
    {
        // Ýlk objenin pozisyonunu rastgele belirle
        int randomIndex = Random.Range(0, happyPillow.Length);
        transform.position = happyPillow[randomIndex];

        // Ýkinci objenin pozisyonunu belirle
        if (secondHappyPillow != null)
        {
            secondHappyPillow.transform.position = happyPillowFace[randomIndex];
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
