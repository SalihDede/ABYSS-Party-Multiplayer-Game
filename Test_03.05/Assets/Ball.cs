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



}
