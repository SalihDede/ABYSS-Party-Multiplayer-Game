using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Ball : MonoBehaviour
{
    public GameObject SemihGame;
    public GameObject DeathEffect;

    void Start()
    {
        SemihGame = GameObject.Find("SemihGame");
        DeathEffect.SetActive(false);
    }




    void Update()
    {
        
    }

    private IEnumerator Effect()
    {
        DeathEffect.SetActive(true);
        yield return new WaitForSeconds(3);
    }

    private void OnTriggerEnter(Collider other)
    {

        if(other.tag == "0" || other.tag == "1" || other.tag == "2" || other.tag == "3")
        {
            if(other.tag == "0")
            {
                SemihGame.GetComponent<GameOneManager>().Player1 += 1;
            }
            if (other.tag == "1")
            {
                SemihGame.GetComponent<GameOneManager>().Player2 += 1;
            }
            if (other.tag == "2")
            {
                SemihGame.GetComponent<GameOneManager>().Player3 += 1;
            }
            if (other.tag == "3")
            {
                SemihGame.GetComponent<GameOneManager>().Player4 += 1;
            }
            SemihGame.GetComponent<GameOneManager>().LastTouch.text = other.tag;
        }

        if(other.tag == "NET1")
        {
            StartCoroutine(Effect());
            Destroy(gameObject);
            SemihGame.GetComponent<GameOneManager>().Goal = true;
        }
    }

}
