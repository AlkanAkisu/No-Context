using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyItself : MonoBehaviour
{
    [SerializeField] float destroyDelay;
    private void Awake()
    {
        Destroy(gameObject, destroyDelay);
    }
}
