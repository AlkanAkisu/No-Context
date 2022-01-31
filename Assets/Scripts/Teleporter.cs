using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{

    [SerializeField] Transform spawnPos;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<TeleportableObject>() != null)
            other.transform.position = spawnPos.position;
    }


}
