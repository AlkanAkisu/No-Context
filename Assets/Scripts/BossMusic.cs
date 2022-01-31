using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMusic : MonoBehaviour
{
    private bool isPlaying;

    private void Awake()
    {
        isPlaying = false;
    }
    public void PlayBossMusic()
    {
        if (isPlaying)
            return;
        isPlaying = true;
        AudioManager.i.Stop("Theme");

        AudioManager.i.Play("Boss");
    }
    
    public void StopBossMusic()
    {
        AudioManager.i.Stop("Boss");
        AudioManager.i.Play("Theme");
    }

}
