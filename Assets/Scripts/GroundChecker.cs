using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [SerializeField] private bool isGround;
    [SerializeField, NaughtyAttributes.Tag] string groundTag;


    public bool IsGround => isGround;
    public BreakPlatform IsBreakable { get; private set; }
    private void Awake()
    {
        IsBreakable = null;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<BreakPlatform>() != null)
            IsBreakable = other.GetComponent<BreakPlatform>();
        if (other.CompareTag(groundTag))
            isGround = true;
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.GetComponent<BreakPlatform>() != null)
            IsBreakable = other.GetComponent<BreakPlatform>();
        if (other.CompareTag(groundTag))
            isGround = true;
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<BreakPlatform>() != null)
            IsBreakable = null;
        if (other.CompareTag(groundTag))
            isGround = false;

    }
}
