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

        if(other.name == "0" || other.name == "1" || other.name == "2" || other.name == "3")
        {

            SemihGame.GetComponent<GameOneManager>().LastTouch.text = other.name;
        }

        if(other.tag == "NET1")
        {

            if (SemihGame.GetComponent<GameOneManager>().LastTouch.text == "0")
            {
                SemihGame.GetComponent<GameOneManager>().Player1 += 1;
            }
            if (SemihGame.GetComponent<GameOneManager>().LastTouch.text == "1")
            {
                SemihGame.GetComponent<GameOneManager>().Player2 += 1;
            }
            if (SemihGame.GetComponent<GameOneManager>().LastTouch.text == "2")
            {
                SemihGame.GetComponent<GameOneManager>().Player3 += 1;
            }
            if (SemihGame.GetComponent<GameOneManager>().LastTouch.text == "3")
            {
                SemihGame.GetComponent<GameOneManager>().Player4 += 1;
            }
            StartCoroutine(Effect());
            Destroy(gameObject);
            SemihGame.GetComponent<GameOneManager>().Goal = true;
        }
    }

}
