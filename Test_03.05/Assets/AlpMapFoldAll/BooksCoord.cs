using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BooksCoord : MonoBehaviour
{
    public GameObject Red;
    public GameObject Blue;
    public GameObject Green;
    public GameObject Orange;
    public GameObject Yellow;



    private Vector3[] brownBook = new Vector3[]
    {
        new Vector3(85.86394f, 9.270824f, 4.318f),
        new Vector3(60.598f, 8.758f, 7.851f),
    };

    private Vector3[] redBook = new Vector3[]
    {
        new Vector3(83.06701f, 9.278195f, 35.90474f),
        new Vector3(85.86394f, 9.270824f, 4.318f),
    };

    private Vector3[] blueBook = new Vector3[]
    {
        new Vector3(82.1f, 9.13f, 2.293f),
        new Vector3(29.066f, 3.859f, 66.833f),
    };

    private Vector3[] greenBook = new Vector3[]
    {
        new Vector3(72.89f, 10.02f, 6.63f),
        new Vector3(34.199f, 1.53f, 66.84f),
    };

    private Vector3[] orangeBook = new Vector3[]
    {
        new Vector3(85.1582f, 9.734f, 56.15f),
        new Vector3(37.151f, 3.548f, -35.85f),
    };
    private Vector3[] yellowBook = new Vector3[]
    {
        new Vector3(73.34819f, 8.018076f, 2.08f),
        new Vector3(71.75f, 7.99f, 2.394f),
    };

    // Start is called before the first frame update
    void Start()
    {
        // Ýlk objenin pozisyonunu rastgele belirle
        int randomIndex = Random.Range(0, brownBook.Length);
        transform.position = brownBook[randomIndex];

        // Ýkinci objenin pozisyonunu belirle
        if (Red != null && Blue != null && Green != null && Orange != null && Yellow != null)
        {
            Red.transform.position = redBook[randomIndex];
            Blue.transform.position = blueBook[randomIndex];
            Green.transform.position = greenBook[randomIndex];
            Orange.transform.position = orangeBook[randomIndex];
            Yellow.transform.position = yellowBook[randomIndex];
            
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

}