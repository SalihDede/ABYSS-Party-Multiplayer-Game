using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LargeBox : MonoBehaviour
{
    public GameObject MediumBox; 
    public GameObject SmallBox;


    private Vector3[] possiblePositions = new Vector3[]
    {
        new Vector3(3.212101f, 1.381771f, -35.789f),
        new Vector3(-1.8f, 1.381771f, -37.98f),
    };

    private Vector3[] secondObjectPositions = new Vector3[]
    {
        new Vector3(2.28f, 1.381771f, -39.79728f),
        new Vector3(-2.33f, 1.381771f, -39.79728f),
    };
    
    private Vector3[] thirdObjectPositions = new Vector3[]
    {
        new Vector3(-2f, 1.381771f, -35.83f),
        new Vector3(81.53f, 7.98f, -17.23f),
    };

    // Start is called before the first frame update
    void Start()
    {
        // Ýlk objenin pozisyonunu rastgele belirle
        int randomIndex = Random.Range(0, possiblePositions.Length);
        transform.position = possiblePositions[randomIndex];

        // Ýkinci objenin pozisyonunu belirle
        if (MediumBox != null && SmallBox !=null)
        {
            MediumBox.transform.position = secondObjectPositions[randomIndex];
            SmallBox.transform.position = thirdObjectPositions[randomIndex];
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
