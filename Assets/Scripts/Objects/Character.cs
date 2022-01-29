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
    private bool isJumping = false;

    public Rigidbody2D CharacterRigidBody { get => rb; }
    public Collider2D CharacterCollider { get => coll; }
    public Player CharacterPlayer { get => player; set => player = value; }
    public bool IsJumping { get => isJumping; set => isJumping = value; }


    private void FixedUpdate()
    {
        MoveCharacter();

        if(isJumping)
            Jump(player.JumpForce);
    }

    private void MoveCharacter()
    {
        //Add animation controls here.
        if(player.MoveDirection == Vector2.zero)
            rb.AddForce(-player.MoveDirection * player.DecelerationSpeed);
        else
            rb.AddForce(player.MoveDirection * player.AccelerationSpeed, ForceMode2D.Force);
    }

    private void Jump(float jumpForce)
    {
        Vector2 jump = new Vector2(0, jumpForce);
        rb.AddForce(jump, ForceMode2D.Impulse);
        isJumping = false;

        //Add animation and particle effects here.
    }

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
