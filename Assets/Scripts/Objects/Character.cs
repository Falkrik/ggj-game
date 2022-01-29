using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Character : MonoBehaviour
{
    private float rbVelocityLimit;
    private int playerNumber;
    private Player player;
    private Rigidbody2D rb;
    private Collider2D coll;

    public Rigidbody2D CharacterRigidBody { get => rb; }
    public Collider2D CharacterCollider { get => coll; }

    public Player CharacterPlayer { get => player; }

    public Character(Player characterPlayer)
    {
        player = characterPlayer;
        playerNumber = player.PlayerNumber;
    }

    public void MoveCharacter(Vector2 moveSpeed)
    {
        //Add animation controls here.
        rb.AddForce(moveSpeed, ForceMode2D.Force);
    }

    public void Jump(Vector2 jumpForce)
    {
        rb.AddForce(jumpForce, ForceMode2D.Impulse);
        //Add animation and particle effects here.
    }

    public void ResetJumpCount() => player.ResetJumpCount();

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

    #region Unity Methods
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    #endregion
}
