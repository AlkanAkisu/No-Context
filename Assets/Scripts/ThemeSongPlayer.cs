using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThemeSongPlayer : MonoBehaviour
{
    private void Start()
    {
        AudioManager.i.Play("Theme");
    }
}
