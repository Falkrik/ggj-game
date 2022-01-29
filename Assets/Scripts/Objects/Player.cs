using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Player : MonoBehaviour
{
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
    [SerializeField] private float runJumpModifier;
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
    private Vector2 moveDir;
    private float speedLimit;
    private float speedLimitDeceleration;

    private bool canQueueJump = false;
    private bool jumpQueued = false;
    private bool canCoyoteJump = false;
    private bool isGrounded = false;
    private bool isJumping = false;
    private float coyoteTimeStart;
    private float hitStunTimeStart;
    private float pushCooldownStart;

    public Vector2 SpawnPosition { get => spawnPosition; set => spawnPosition = value; }
    public int PlayerNumber { get => playerNumber; }
    public bool CanQueueJump { get => canQueueJump; set => canQueueJump = value; }

    [ContextMenu("Spawn")]
    public void SpawnCharacter(Vector2 spawnPos)
    {
        SpawnPosition = spawnPos;
        playerCharacter = Instantiate(characterPrefab, this.transform).GetComponent<Character>();
        transform.position = SpawnPosition;
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
        if(playerCharacter != null)
        {
            if(!isHitStun)
                GetInput();

            SpeedLimitCheck();
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

    private void SpeedLimitCheck()
    {
        Vector2 velocity = new Vector2(playerCharacter.CharacterRigidBody.velocity.x, 0);
        if (Mathf.Abs(velocity.magnitude) <= speedLimit)
            return;

        playerCharacter.CharacterRigidBody.velocity = Vector2.ClampMagnitude(playerCharacter.CharacterRigidBody.velocity, speedLimit);
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
            
        if (Input.GetKey(KeyCode.A))
            moveDir = Vector2.left;
        if (Input.GetKey(KeyCode.D))
            moveDir = Vector2.right;

        if (Input.GetKeyUp(KeyCode.A))
            moveDir = Vector2.zero;
        if (Input.GetKeyUp(KeyCode.D))
            moveDir = Vector2.zero;

        MovePlayer();
    }

    private void ListenARROWInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
            Jump();

        if (Input.GetKey(KeyCode.LeftArrow))
            moveDir = Vector2.left;
        if (Input.GetKey(KeyCode.RightArrow))
            moveDir = Vector2.right;



        if (Input.GetKey(KeyCode.LeftArrow))
            moveDir -= Vector2.zero;
        if (Input.GetKey(KeyCode.RightArrow))
            moveDir -= Vector2.zero;

        MovePlayer();
    }

    private void Jump()
    {
        #region DetermineJump
        void DetermineJump()
        {
            if (moveDir != Vector2.zero)
                playerCharacter.Jump(Vector2.up * runJumpModifier * jumpForce);
            if (moveDir == Vector2.zero)
                playerCharacter.Jump(Vector2.up * jumpForce);
        }
        #endregion

        if (currentJumpCount >= maxJumpCount && !canQueueJump)
            return;

        isJumping = true;

        if (isGrounded)
        {
            currentJumpCount += 1;
            
            DetermineJump();
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

                DetermineJump();
                return;
            }
            currentJumpCount = maxJumpCount;
            DetermineJump();
        }
    }

    private void MovePlayer()
    {
        if(moveDir != Vector2.zero && playerCharacter.CharacterRigidBody.velocity.magnitude < speedLimit)
        {
            if (isGrounded)
                playerCharacter.MoveCharacter(moveDir * groundMoveAcceleration);
            else
                playerCharacter.MoveCharacter(moveDir * airMoveAcceleration);
            return;
        }

        if (moveDir != Vector2.zero && playerCharacter.CharacterRigidBody.velocity.magnitude >= speedLimit)
        {
            return;
        }

        else
        {
            if (isGrounded)
                playerCharacter.MoveCharacter(-playerCharacter.CharacterRigidBody.velocity * groundMoveDeceleration);
            if (!isGrounded && isJumping)
                playerCharacter.MoveCharacter(-playerCharacter.CharacterRigidBody.velocity * airMoveDeceleration);
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
            speedLimitDeceleration = airMoveDeceleration;

            if(currentJumpCount == 0)
            {
                coyoteTimeStart = Time.timeSinceLevelLoad;
                canCoyoteJump = true;
            }
        }
        
        if(isGrounded)
        {
            speedLimit = groundSpeedMax;
            speedLimitDeceleration = groundMoveDeceleration;
            isJumping = false;

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
