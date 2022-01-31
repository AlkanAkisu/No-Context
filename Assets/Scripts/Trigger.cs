using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class Trigger : MonoBehaviour
{

    [FormerlySerializedAs("U_OnTriggerEnter")] [SerializeField] UnityEvent uOnTriggerEnter;
    [SerializeField] UnityEvent U_OnTriggerExit;
    [SerializeField] UnityEvent U_OnTriggerStay;
    public Action<Collider2D> OnTriggerEnter;
    public Action<Collider2D> OnTriggerExit;
    public Action<Collider2D> OnTriggerStay;

    [SerializeField] bool isOneTime;
    private bool isTriggered;

    private void Awake()
    {
        isTriggered = false;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isTriggered && isOneTime)
            return;
        OnTriggerEnter?.Invoke(other);
        if (other.GetComponent<CharacterController>() != null)
        {
            uOnTriggerEnter?.Invoke();
            Debug.Log($"On trigger enter");
        }
        isTriggered = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        OnTriggerExit?.Invoke(other);
        if (other.GetComponent<CharacterController>() != null)
            U_OnTriggerExit?.Invoke();

    }

    private void OnTriggerStay2D(Collider2D other)
    {
        OnTriggerStay?.Invoke(other);
        if (other.GetComponent<CharacterController>() != null)
            U_OnTriggerStay?.Invoke();

    }
}
