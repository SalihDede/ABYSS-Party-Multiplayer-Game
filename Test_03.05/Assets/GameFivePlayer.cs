using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFivePlayer : MonoBehaviour
{

    public bool IsHeSolve;
    public GameObject GameFiveManager;
    public GameObject Line1;
    public GameObject Line2;
    public GameObject Line3;
    public GameObject Line4;
    public List<GameObject> Spawns = new List<GameObject>();



    void Start()
    {

        GameFiveManager = GameObject.Find("SalihGame");


        Line1 = GameObject.Find("SalihGameSpawn1");
        Line2 = GameObject.Find("SalihGameSpawn2");
        Line3 = GameObject.Find("SalihGameSpawn3");
        Line4 = GameObject.Find("SalihGameSpawn4");

        Spawns.Add(Line1);
        Spawns.Add(Line2);
        Spawns.Add(Line3);
        Spawns.Add(Line4);


        GameFiveManager.GetComponent<GameFiveManager>().Starters.Add(gameObject);

        if (Spawns.Count == 4)
        {
            transform.position = Spawns[GameFiveManager.GetComponent<GameFiveManager>().Starters.IndexOf(gameObject)].transform.position;

        }


    }


    void Update()
    {
      
        if (IsHeSolve)
        {
            if(!GameFiveManager.GetComponent<GameFiveManager>().Ranking.Contains(gameObject))
            {
                GameFiveManager.GetComponent<GameFiveManager>().Ranking.Add(gameObject);
            }
        }


    }
}
