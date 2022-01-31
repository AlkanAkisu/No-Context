using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FadeInPanel : MonoBehaviour
{
    [SerializeField] UnityEvent onFadingCompleted;

    public void FadingInCompleted()
    {
        onFadingCompleted?.Invoke();
        Destroy(gameObject);
    }
}
