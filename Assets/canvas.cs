using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class canvas : MonoBehaviour
{
    // Start is called before the first frame update

    Animator anime;

    void Start()
    {
        anime = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            fadeAway();
        }
    }

    void fadeAway()
    {
        anime.SetTrigger("fade");
    }
}
