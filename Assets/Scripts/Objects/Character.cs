using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Character : MonoBehaviour
{
    private int playerNumber;
    private Player player;
    private Rigidbody2D rb;
    private Collider2D coll;
    private bool groundedJump = false;
    private bool aerialJump = false;

    public Rigidbody2D CharacterRigidBody { get => rb; }
    public Collider2D CharacterCollider { get => coll; }
    public Player CharacterPlayer { get => player; set => player = value; }
    public bool GroundedJump { get => groundedJump; set => groundedJump = value; }
    public bool AerialJump { get => AerialJump; set => aerialJump = value; }

    public void UsePush()
    {
        //Complete after.
    }

    public void UseDuality()
    {
        //Complete after.
    }

    public void Die()
    {
        //Complete after.
    }

    public void ChangePlayerGrounding(bool isGrounded)
    {
        player.ChangePlayerGrounding(isGrounded);
    }

    private void FixedUpdate()
    {
        MoveCharacter();
        FallSpeed();

        if(aerialJump)
            Jump(player.AerialJumpForce);

        if (groundedJump)
            Jump(player.GroundedJumpForce);
    }

    private void MoveCharacter()
    {
        if (player.SpeedLimit != 0)
        {
            if(rb.velocity.magnitude > player.SpeedLimit || rb.velocity.magnitude < -player.SpeedLimit)
                rb.velocity = Vector2.ClampMagnitude(rb.velocity, player.SpeedLimit);
        }

        //Add animation controls here.
        if ((player.MoveDirection == Vector2.zero && rb.velocity.x > 0) || (player.MoveDirection == Vector2.zero && rb.velocity.x < 0))
            rb.AddForce(-rb.velocity * player.DecelerationSpeed);
        if (player.MoveDirection != Vector2.zero && Mathf.Abs(rb.velocity.x) < player.SpeedLimit && rb.velocity.y < player.SpeedLimit)
            rb.AddForce(player.MoveDirection * player.AccelerationSpeed, ForceMode2D.Force);
    }

    private void FallSpeed()
    {
        if (rb.velocity.y < 0)
            rb.velocity += Vector2.up * Physics2D.gravity.y * player.FallMultiplier * Time.deltaTime;
    }

    private void Jump(float jumpForce)
    {
        Vector2 jump = new Vector2(rb.velocity.x, jumpForce);
        rb.velocity = jump;

        groundedJump = false;
        aerialJump = false;

        //Add animation and particle effects here.
    }

    #region Unity Methods
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //This collider may need to be replaced with the specific collider type.
        coll = GetComponent<Collider2D>();
        player = GetComponentInParent<Player>();
    }
    #endregion
}
