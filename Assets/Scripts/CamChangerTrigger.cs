using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamChangerTrigger : MonoBehaviour
{
    [SerializeField] CameraTypes changeTo = CameraTypes.Bedroom;



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.GetComponent<CharacterController>() == null)
            return;

        var camChanger = FindObjectOfType<CameraChanger>();
        if (camChanger == null)
        {

            Debug.LogWarning($"Can't Find {nameof(CameraChanger)}");
            return;
        }

        camChanger.ChangeToSelected(changeTo);
    }
}
