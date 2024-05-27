using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptForRandomPlane : MonoBehaviour
{
    public GameObject objectToSpawn;

    public float xAxisOfPlane;
    public float xBorderLeft = -137.115f;
    public float xBorderRight = -132.52f;
    public float yAxisOfPlane = 0.72f;
    public float zAxisOfPlane = 233.571f;

    void Start()
    {
        Debug.Log($"Initial Z Axis: {zAxisOfPlane}");
        int i = 0;
        while (i < 27)
        {
            xAxisOfPlane = Random.Range(xBorderLeft, xBorderRight);
            Vector3 position = new Vector3(xAxisOfPlane, yAxisOfPlane, zAxisOfPlane);

            Debug.Log($"Spawning object {i + 1} at position: {position}");

            GameObject currentPlane = Instantiate(objectToSpawn);
            currentPlane.SetActive(true);
            currentPlane.transform.position = position;

            yAxisOfPlane += 1.704f;
            zAxisOfPlane += 4.710f;

            Debug.Log($"After Iteration {i + 1}: New Y Axis: {yAxisOfPlane}, New Z Axis: {zAxisOfPlane}");

            i++;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}