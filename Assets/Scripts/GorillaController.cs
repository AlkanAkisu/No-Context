using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GorillaController : MonoBehaviour
{
    [SerializeField] Transform gorilla, human;
    [SerializeField] CharacterController chController;
    [SerializeField] GroundChecker groundChecker;
    private bool wasGrounded;

    private void Awake()
    {
        wasGrounded = false;
    }
    public void SpawnGorilla()
    {
        gorilla.gameObject.SetActive(true);
        human.gameObject.SetActive(false);
    }

    public void DeSpawnGorilla()
    {
        gorilla.gameObject.SetActive(false);
        human.gameObject.SetActive(true);
    }

    private void Update()
    {
        if (!gorilla.gameObject.activeInHierarchy)
            return;

        if (!wasGrounded && chController.IsGround)
        {
            // Gorilla Form Fall Down
            Debug.Log($"Gorilla Fall");
            if (groundChecker.IsBreakable != null)
                groundChecker.IsBreakable.Break();
        }

        wasGrounded = chController.IsGround;
    }
}
