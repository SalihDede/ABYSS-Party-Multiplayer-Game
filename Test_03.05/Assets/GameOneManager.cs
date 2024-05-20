using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOneManager : MonoBehaviour
{

    public GameObject Engel;

    void Start()
    {
        RandomMapGenerator();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RandomMapGenerator()
    {
        for(int i = -21;i< 264; i++)
        {
           GameObject CurrentEngel = Instantiate(Engel);
            int x = Random.Range(-2,-11);
            int y = Random.Range(1,5);
            int z = i;

            float Scale = Random.Range(0.5f,1f);

            CurrentEngel.transform.position = new Vector3(x, y, z);
            CurrentEngel.transform.localScale = new Vector3(Scale, Scale, Scale);
        }
    }


}
