using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grower : MonoBehaviour
{
    [SerializeField] GameObject proximity;
    float distance;
    Vector3 startPos;
    float startDis;

    private void Start() {
        startDis = distance = Vector3.Distance(transform.position,proximity.transform.position);
        startPos = proximity.transform.position;
    }

    private void Update() {
       
       distance = Math.Abs(Vector3.Distance(transform.position,proximity.transform.position) - startDis);
        Debug.Log(distance);
        growInSize(distance);
    }

    private void growInSize(float distance)
    {
        transform.localScale = new Vector3(1,((distance*0.15f)+1),1);

       
    }
}
