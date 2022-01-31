using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ActorMoveController : MonoBehaviour
{

    [SerializeField] float maxSpeed;
    [SerializeField] float secondsToReachMaxSpeed;
    [SerializeField] float secondsToStopFully;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float jumpForce;
    [SerializeField] float gravityMultiplier;

    [SerializeField] GroundChecker groundChecker;
    private bool doNotCheck;

    public bool IsFalling => rb.velocity.y < -1;

    public bool IsGround => groundChecker.IsGround || DoNotCheck;

    public bool DoNotCheck { get => doNotCheck; set => doNotCheck = value; }

    // Start is called before the first frame update
    private void Awake()
    {
        DoNotCheck = false;
    }

    public void Move(int dir)
    {
        var t = secondsToReachMaxSpeed / Time.deltaTime;
        var increaseSpeed = maxSpeed / t;
        rb.velocity += Vector2.right * increaseSpeed * dir;
        var clampedX = Mathf.Clamp(rb.velocity.x, -maxSpeed, maxSpeed);
        rb.velocity = rb.velocity.ChangeVector(x: clampedX);
    }

    public void Brake()
    {
        if (rb.velocity.x.Abs() < 0.5f)
            ZeroXSpeed();
        var t = secondsToStopFully / Time.deltaTime;
        var decreaseSpeed = maxSpeed / t;
        int dir = rb.velocity.x.Sign();
        rb.velocity += Vector2.left * decreaseSpeed * dir;
        if (rb.velocity.x.Sign() * dir < 0) // changed direction
            ZeroXSpeed();
    }

    private void ZeroXSpeed()
    {
        rb.velocity = rb.velocity.ChangeVector(x: 0);
    }
    [NaughtyAttributes.Button]
    public void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce);
    }

    public void IncreaseGravity()
    {
        rb.gravityScale = gravityMultiplier;
    }

    public void ResetGravity()
    {
        // rb.gravityScale = 1;
    }




}
