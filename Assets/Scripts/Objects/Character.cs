using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Character : MonoBehaviour
{
    protected int playerNumber;
    protected Player player;
    protected Rigidbody2D rb;

    public Player CharacterPlayer { get => player; }

    public Character(Player characterPlayer)
    {
        player = characterPlayer;
        playerNumber = player.PlayerNumber;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Move(Vector2 moveSpeed)
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
}
