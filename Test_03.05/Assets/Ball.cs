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
            SemihGame.GetComponent<GameOneManager>().LastTouch.text = other.tag;
        }

        if(other.tag == "NET1")
        {
            StartCoroutine(Effect());
            Destroy(gameObject);
        }
    }

}
