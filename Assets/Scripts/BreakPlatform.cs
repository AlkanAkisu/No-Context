using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakPlatform : MonoBehaviour
{
    [SerializeField] GameObject platform, debris;

    [NaughtyAttributes.Button]
    public void Break()
    {
        platform.SetActive(false);
        debris.SetActive(true);
    }
}
