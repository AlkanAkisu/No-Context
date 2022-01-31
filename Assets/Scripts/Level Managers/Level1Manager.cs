using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;

public class Level1Manager : MonoBehaviour
{
    // Encounter 1
    [Header("Encounter 1")]
    [SerializeField] Collider2D colliderToRemove;
    [SerializeField] int firstEventhappenAfterFail;
    [SerializeField] Trigger firstEncountertrigger;

    [Header("Encounter 2")]
    [SerializeField] ActorMoveController actorMoveController;
    [SerializeField] int secondEventHappenAfterFail;
    [SerializeField] Trigger secondEncountertrigger;
    [SerializeField] Trigger secondEncounterPassed;

    [Header("Encounter 3")]
    [SerializeField] GameObject blackPanel;
    [SerializeField] GameObject[] objectsToDestroy;

    [SerializeField] RectTransform mirror;

    int playerFailCounterFirst = 0;
    int playerFailCounterSecond = 0;
    private bool isMirrorInteracted;

    private void Awake()
    {
        playerFailCounterFirst = 0;
        firstEncountertrigger.OnTriggerEnter += (other) => PlayerFailedFirst(other.transform);
        secondEncountertrigger.OnTriggerEnter += (other) => PlayerFailedSecond(other.transform);
        secondEncounterPassed.OnTriggerEnter += (other) => ResetJumpLimit();
        isMirrorInteracted = false;
    }

    public void PlayerFailedFirst(Transform tr)
    {
        if (tr.GetComponent<CharacterController>() == null)
            return;
        Debug.Log($"PlayerFailedFirst");
        playerFailCounterFirst++;
        if (playerFailCounterFirst >= firstEventhappenAfterFail)
        {
            RemoveCollider();
        }

    }
    public void RemoveCollider()
    {
        Debug.Log($"Collider Removed");
        colliderToRemove.enabled = false;
    }

    public void PlayerFailedSecond(Transform tr)
    {
        if (tr.GetComponent<CharacterController>() == null)
            return;
        Debug.Log($"PlayerFailedSecond");
        playerFailCounterSecond++;
        if (playerFailCounterFirst >= secondEventHappenAfterFail)
        {
            RemoveJumpLimit();
        }

    }

    public void RemoveJumpLimit()
    {
        actorMoveController.DoNotCheck = true;
    }


    public void ResetJumpLimit()
    {
        actorMoveController.DoNotCheck = false;
    }

    [NaughtyAttributes.Button]
    public void MirrorInteracted()
    {
        ToggleMirror();
        if (!isMirrorInteracted)
            Invoke(nameof(DestroyObjects), 0.4f);
        isMirrorInteracted = true;
    }


    public void DestroyObjects()
    {
        Array.ForEach(objectsToDestroy, go => Destroy(go));
        objectsToDestroy = null;
    }

    private void Update()
    {
        if (blackPanel.activeInHierarchy)
            if (KeyCode.E.Down())
                ToggleMirror();
    }

    public void ToggleMirror()
    {
        if (mirror.anchoredPosition.y < -10)
        {
            DOTween.To(() => mirror.anchoredPosition, (value) => mirror.anchoredPosition = value, Vector2.zero, 0.6f).SetEase(Ease.InOutQuad);
            blackPanel.SetActive(true);
        }
        else
        {
            DOTween.To(() => mirror.anchoredPosition, (value) => mirror.anchoredPosition = value, Vector2.zero.ChangeVector(y: -2135), 0.6f).SetEase(Ease.InOutQuad); ;
            blackPanel.SetActive(false);

        }
    }

}
