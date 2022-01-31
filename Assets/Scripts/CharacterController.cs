using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] ActorMoveController actorMoveController;
    [SerializeField] Animator humanAnimator, gorillaAnimator;
    [SerializeField] GameObject weaponHand;
    [SerializeField] GameObject freeHand;
    [SerializeField] GorillaController gorillaController;
    [SerializeField] GroundChecker groundChecker;

    int walkingId = Animator.StringToHash("isWalking");

    bool waiting;
    private Rigidbody2D rigidBody;

    public bool IsGround => actorMoveController.IsGround;
    public bool IsFalling => actorMoveController.IsFalling;
    public bool CanJump => Input.GetKeyDown(KeyCode.Space) && IsGround;
    public bool CanMove => !waiting;
    public bool IsGorilla { get; set; }
    public Animator currentAnimator => IsGorilla ? gorillaAnimator : humanAnimator;

    private void Awake()
    {
        waiting = false;
        rigidBody = GetComponent<Rigidbody2D>();
        IsGorilla = false;
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();

        HandleJump();

    }

    private void HandleMovement()
    {
        if (!CanMove)
            return;
        var inputAxis = Input.GetAxisRaw("Horizontal");
        var dir = 0;
        if (inputAxis.Abs() > 0.5f)
            dir = inputAxis.Sign();
        if (dir != 0)
        {
            actorMoveController.Move(dir);
            currentAnimator.SetBool(walkingId, true);
            transform.localScale = transform.localScale.ChangeVector(x: -transform.localScale.x.Abs() * dir);
        }
        else
        {
            actorMoveController.Brake();


            currentAnimator.SetBool(walkingId, false);
        }



    }

    private void HandleJump()
    {
        if (CanJump)
        {
            actorMoveController.Jump();
            AudioManager.i.PlayOneShot("Jump");
        }

        if (IsFalling)
            actorMoveController.IncreaseGravity();
        else
            actorMoveController.ResetGravity();
    }

    public void Wait(int seconds)
    {
        DisableMovement();
        Invoke(nameof(EnableMovement), seconds);

        Debug.Log($"CanMove: {CanMove}");
    }

    private void EnableMovement()
    {
        Debug.Log($"Movement enabled");
        waiting = false;
    }
    private void DisableMovement()
    {
        Debug.Log($"Movement disabled");
        waiting = true;

    }
    public void Weaponize()
    {
        weaponHand.SetActive(true);
        freeHand.SetActive(false);
    }
    public void UnWeaponize()
    {
        weaponHand.SetActive(false);
        freeHand.SetActive(true);

    }

    public void ToggleForm()
    {
        if (IsGorilla)
        {
            ChangeToHuman();
        }
        else
        {
            ChangeToGorilla();

        }
    }

    [NaughtyAttributes.Button]
    public void ChangeToGorilla()
    {
        gorillaController.SpawnGorilla();
        IsGorilla = true;
        rigidBody.mass = 2;

    }
    [NaughtyAttributes.Button]
    public void ChangeToHuman()
    {
        gorillaController.DeSpawnGorilla();
        IsGorilla = false;
        rigidBody.mass = 1;
    }
}
