using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static TaskManager;

public class Popup : MonoBehaviour
{
    [SerializeField] UnityEvent onKeyPressed;
    [SerializeField] GameObject[] toBeDestroyed;
    [SerializeField] Tasks task;
    [SerializeField] private bool doNotDestroy;

    private void Update()
    {
        if (KeyCode.E.Down())
            PressedKey();
    }
    public void PressedKey()
    {
        onKeyPressed?.Invoke();
        if (!task.Equals(Tasks.Ignore))
            FindObjectOfType<TaskManager>().TaskDone(task);
        if (!doNotDestroy)
            Destroy(gameObject);
        Array.ForEach(toBeDestroyed, go => Destroy(go));
    }
}
