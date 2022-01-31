using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpTrigger : Trigger
{
    [SerializeField] Transform popUp;

    private void Awake()
    {
        popUp.gameObject.SetActive(false);
        OnTriggerEnter += (col) => SetPopup(col, true);
        // OnTriggerStay += (col) => SetPopup(col, true);
        OnTriggerExit += (col) => SetPopup(col, false);
    }

    private void SetPopup(Collider2D tr, bool active)
    {
        var isCharacter = tr.GetComponent<CharacterController>() != null;
        if (!isCharacter)
            return;

        popUp?.gameObject.SetActive(active);
    }




}
