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

    public void MoveCharacter(Vector2 moveDir)
    {
        //Add animation controls here.
        rb.AddForce(moveDir * moveSpeed, ForceMode2D.Force);
    }

    public void Jump(Vector2 jumpForce)
    {
        rb.AddForce(jumpForce, ForceMode2D.Impulse);
        //Add animation and particle effects here.
    }

    public void ResetJumpCount() => currentJumpCount = 0;

    public void UsePush()
    {
        //Complete after.
    }

    public void UseDuality()
    {
        //Complete after.
    }

    public void 
}
