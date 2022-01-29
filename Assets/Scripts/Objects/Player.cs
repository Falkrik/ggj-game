using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Player : MonoBehaviour
{
    [SerializeField] protected GameObject characterPrefab;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float hitStunTime;
    [SerializeField] private float hitStunMovementFalloff;
    [SerializeField] private float pushForce;
    [SerializeField] private float pushDuration;
    [SerializeField] private float pushCooldown;

    private int playerNumber;
    private int maxJumpCount = 2;
    private int currentJumpCount = 0;
    private bool isHitStun = false;
    protected ControlScheme controls;
    protected Character playerCharacter;
    protected Vector2 moveDir;

    public Vector2 MoveDirection { get => moveDir; }
    public int PlayerNumber { get => playerNumber; }

    [ContextMenu("Spawn Character")]
    public void SpawnCharacter()
    {
        playerCharacter = Instantiate(characterPrefab, this.transform).GetComponent<Character>();
        ResetJumpCount();
        moveDir = Vector2.zero;
    }

    public void ResetJumpCount() => currentJumpCount = 0;


    private void Update()
    {
        if(playerCharacter != null)
        {
            GetInput();
            CooldownTimer();
        }
    }

    private void CooldownTimer()
    {
        return;
    }

    private void GetInput()
    {
        if(controls == ControlScheme.WASD)
            ListenWASDInput();

        if (controls == ControlScheme.Arrows)
            ListenARROWInput();
    }

    private void ListenWASDInput()
    {
        if (Input.GetKeyDown(KeyCode.W))
            Jump();
        if (Input.GetKeyDown(KeyCode.A))
            moveDir += Vector2.left;
        if (Input.GetKeyDown(KeyCode.D))
            moveDir += Vector2.right;
        //if (Input.GetKeyDown(KeyCode.S))
        //    return;

        if (Input.GetKeyUp(KeyCode.W))
            Jump();
        if (Input.GetKeyUp(KeyCode.A))
            moveDir -= Vector2.left;
        if (Input.GetKeyUp(KeyCode.D))
            moveDir -= Vector2.right;
        //if (Input.GetKeyUp(KeyCode.S))
        //    return;

        Move();
    }

    private void ListenARROWInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
            Jump();
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            moveDir += Vector2.left;
        if (Input.GetKeyDown(KeyCode.RightArrow))
            moveDir += Vector2.right;
        //if (Input.GetKeyDown(KeyCode.DownArrow))
        //    moveDir += Vector2.down;


        if (Input.GetKeyUp(KeyCode.UpArrow))
            Jump();
        if (Input.GetKeyUp(KeyCode.LeftArrow))
            moveDir -= Vector2.left;
        if (Input.GetKeyUp(KeyCode.RightArrow))
            moveDir -= Vector2.right;
        //if (Input.GetKeyUp(KeyCode.DownArrow))
        //    moveDir -= Vector2.down;

        Move();
    }

    private void Jump()
    {
        if (currentJumpCount == maxJumpCount)
            return;

        currentJumpCount += 1;

        playerCharacter.Jump(Vector2.up * jumpForce);
    }

    private void Move()
    {
        playerCharacter.Move(moveDir * moveSpeed);
    }

    private void UsePush()
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

    public void HitStun()
    {
        //Complete after.
    }
}
