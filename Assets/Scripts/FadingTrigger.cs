using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadingTrigger : MonoBehaviour
{
    [SerializeField] FadeInAnimaton fadeInAnimaton;
    private bool isTriggered;
    [SerializeField] float largerThanX;
    [SerializeField] private Transform player;

    private void Awake()
    {
        isTriggered = false;
    }

    private void Update()
    {
        if (player.position.x > largerThanX)
            StartFading();
    }
    [NaughtyAttributes.Button]
    public void StartFading()
    {
        if (isTriggered)
            return;
        fadeInAnimaton.StartFading();
        isTriggered = true;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {

        StartFading();

    }
}
