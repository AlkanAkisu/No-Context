using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using DG.Tweening;
using UnityEngine.Events;

public class FadeInAnimaton : MonoBehaviour
{
    [SerializeField] Light2D light2D;
    [SerializeField] float endIntensity;
    [SerializeField] UnityEvent onTweenFinished;


    [NaughtyAttributes.Button]
    public void StartFading()
    {
        onTweenFinished?.Invoke();
        DOTween.To(
            () => light2D.intensity,
            (intensity) => light2D.intensity = intensity,
            endIntensity,
            1f
        );
    }
}
