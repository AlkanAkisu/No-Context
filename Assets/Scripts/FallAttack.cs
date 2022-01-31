using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallAttack : MonoBehaviour
{
    private bool playerInArea;

    public Func<Collider2D, bool> IsPlayer => (col) => col.transform.GetComponent<CharacterController>() != null;
    public bool IsplayerInArea => playerInArea;

    private void Awake()
    {
        playerInArea = false;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!IsPlayer(other))
            return;

        playerInArea = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!IsPlayer(other))
            return;

        playerInArea = false;
    }
}
