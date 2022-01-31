using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pillow : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        bool isPlayer = other.transform.GetComponent<CharacterController>() != null;
        if (!isPlayer)
            return;
        AudioManager.i.PlayOneShot("PillowHit");
    }
}
