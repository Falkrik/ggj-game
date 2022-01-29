using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Player : MonoBehaviour
{
    [SerializeField] private Vector2 moveDir;

    [SerializeField] private GameObject characterPrefab;
    [SerializeField] private ControlScheme controls;
    [SerializeField] private Vector2 spawnPosition;
    [SerializeField] private float groundSpeedMax;
    [SerializeField] private float groundMoveAcceleration;
    [SerializeField] private float groundMoveDeceleration;
    [SerializeField] private float airMoveSpeedMax;
    [SerializeField] private float airMoveAcceleration;
    [SerializeField] private float airMoveDeceleration;
    [SerializeField] private float jumpForce;
    [SerializeField] private float coyoteTime;
    [SerializeField] private float hitStunTime;
    [SerializeField] private float pushForce;
    [SerializeField] private float pushDuration;
    [SerializeField] private float pushCooldown;

    private int playerNumber;
    private int maxJumpCount = 2;
    private int currentJumpCount = 0;
    private bool isHitStun = false;
    private Character playerCharacter;
    private float speedLimit;
    private float speedAcceleration;
    private float speedDeceleration;

    private bool canQueueJump = false;
    private bool jumpQueued = false;
    private bool canCoyoteJump = false;
    private bool isGrounded = false;
    private float coyoteTimeStart;
    private float hitStunTimeStart;
    private float pushCooldownStart;

    public Vector2 SpawnPosition { get => spawnPosition; set => spawnPosition = value; }
    public int PlayerNumber { get => playerNumber; }
    public bool CanQueueJump { get => canQueueJump; set => canQueueJump = value; }
    public Vector2 MoveDirection { get => moveDir; }
    public float GroundSpeedMax { get => groundSpeedMax; }
    public float GroundMoveAcceleration { get => GroundMoveAcceleration; }
    public float GroundMoveDeceleration { get => groundMoveDeceleration; }
    public float AirMoveSpeedMax { get => airMoveSpeedMax; }
    public float AirMoveAcceleration { get => airMoveAcceleration; }
    public float AirMoveDeceleration { get => AirMoveDeceleration; }
    public float JumpForce { get => jumpForce; }
    public float SpeedLimit { get => speedLimit; }
    public float AccelerationSpeed { get => speedAcceleration; }
    public float DecelerationSpeed { get => speedDeceleration; }

    [ContextMenu("Spawn")]
    public void SpawnCharacter(Vector2 spawnPos)
    {
        SpawnPosition = spawnPos;
        transform.position = SpawnPosition;

        playerCharacter = Instantiate(characterPrefab, this.transform).GetComponent<Character>();
        playerCharacter.CharacterPlayer = this;

        ResetJumpCount();
        moveDir = Vector2.zero;
    }

    public void Die()
    {
        //Complete after.
    }

    public void HitStun()
    {
        //Complete after.
    }

    private void Update()
    {
        if (playerCharacter != null)
        {
            if (!isHitStun)
                GetInput();

            TimerChecks();
        }
    }

    private void TimerChecks()
    {
        CheckCoyoteTime();
        //CheckPushCooldown();
        //CheckHitStunRecovery();
        return;
    }

    private void CheckCoyoteTime()
    {
        if (!canCoyoteJump)
            return;
        if ((Time.timeSinceLevelLoad - coyoteTimeStart) > coyoteTime)
            canCoyoteJump = false;
    }

    private void GetInput()
    {
        if (controls == ControlScheme.WASD)
            ListenWASDInput();

        if (controls == ControlScheme.Arrows)
            ListenARROWInput();
    }

    private void ListenWASDInput()
    {
        if (Input.GetKeyDown(KeyCode.W))
            Jump();

        if (Input.GetKeyDown(KeyCode.A))
            moveDir = Vector2.left;
        if (Input.GetKeyDown(KeyCode.D))
            moveDir = Vector2.right;

        if (Input.GetKeyUp(KeyCode.A))
            moveDir = Vector2.zero;
        if (Input.GetKeyUp(KeyCode.D))
            moveDir = Vector2.zero;
    }

    private void ListenARROWInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
            Jump();

        if (Input.GetKeyDown(KeyCode.LeftArrow))
            moveDir = Vector2.left;
        if (Input.GetKeyDown(KeyCode.RightArrow))
            moveDir = Vector2.right;


        if (Input.GetKeyUp(KeyCode.LeftArrow))
            moveDir = Vector2.zero;
        if (Input.GetKeyUp(KeyCode.RightArrow))
            moveDir = Vector2.zero;
    }

    private void Jump()
    {
        if (currentJumpCount >= maxJumpCount && !canQueueJump)
            return;

        if (isGrounded)
        {
            currentJumpCount += 1;

            playerCharacter.IsJumping = true;
            return;
        }

        if (!isGrounded && canQueueJump)
        {
            jumpQueued = true;
            canQueueJump = false;
            return;
        }

        if (!isGrounded && !canQueueJump && currentJumpCount < maxJumpCount)
        {
            if (canCoyoteJump)
            {
                currentJumpCount += 1;
                canCoyoteJump = false;

                playerCharacter.IsJumping = true;
                return;
            }

            currentJumpCount = maxJumpCount;
            playerCharacter.IsJumping = true;
        }
    }

    private void UsePush()
    {
        //Complete after.
    }

    private void UseDuality()
    {
        //Complete after.
    }

    //Changes grounding status. If the player is not grounded, we set the player's movement stats 
    //to reflect air movespeed. We also check if the player has jumped. If they have not jumped 
    //and they are not grounded, it means the platform disappeared, and so we start our coyoteTime.
    public void ChangePlayerGrounding(bool newGrounding)
    {
        isGrounded = newGrounding;

        if (!isGrounded)
        {
            speedLimit = airMoveSpeedMax;
            speedDeceleration = airMoveDeceleration;
            speedAcceleration = airMoveAcceleration;

            if (currentJumpCount == 0)
            {
                coyoteTimeStart = Time.timeSinceLevelLoad;
                canCoyoteJump = true;
            }
        }

        if (isGrounded)
        {
            speedLimit = groundSpeedMax;
            speedDeceleration = groundMoveDeceleration;
            speedAcceleration = groundMoveAcceleration;

            ResetJumpCount();
        }

        if (jumpQueued && isGrounded)
        {
            jumpQueued = false;
            Jump();
        }

    }

    private void ResetJumpCount() => currentJumpCount = 0;

    //public void HitStun()
    //{

    //}
}
