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
    [SerializeField] private float coyoteTime;
    [SerializeField] private float hitStunTime;
    [SerializeField] private float pushForce;
    [SerializeField] private float pushDuration;
    [SerializeField] private float pushCooldown;

    private int playerNumber;
    private int maxJumpCount = 2;
    [SerializeField] private int currentJumpCount = 0;
    private bool isHitStun = false;
    private Character playerCharacter;
    [SerializeField] private Vector2 moveDir;
    private float speedLimit;
    private float speedLimitDeceleration;

    private bool canQueueJump = false;
    private bool jumpQueued = false;
    private bool canCoyoteJump = false;
    private bool isGrounded = false;
    private float coyoteTimeStart;
    private float hitStunTimeStart;
    private float pushCooldownStart;

    public Vector2 SpawnPosition { get => spawnPosition; set => spawnPosition = value; }
    public int PlayerNumber { get => playerNumber; }


    /// <summary>
    /// Instatiates prefab of the character 
    /// </summary>
    [ContextMenu("Spawn Character")]
    public void SpawnCharacter()
    {
        playerCharacter = Instantiate(characterPrefab, this.transform).GetComponent<Character>();
        transform.position = SpawnPosition;
        ResetJumpCount();
        moveDir = Vector2.zero;
    }

    public void ResetJumpCount() => currentJumpCount = 0;

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
        if (Mathf.Abs(playerCharacter.CharacterRigidBody.velocity.magnitude) <= speedLimit)
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
        if (Input.GetKeyDown(KeyCode.A))
            moveDir += Vector2.left;
        if (Input.GetKeyDown(KeyCode.D))
            moveDir += Vector2.right;
        //if (Input.GetKeyDown(KeyCode.S))
        //    return;

        if (Input.GetKeyUp(KeyCode.A))
            moveDir -= Vector2.left;
        if (Input.GetKeyUp(KeyCode.D))
            moveDir -= Vector2.right;
        //if (Input.GetKeyUp(KeyCode.S))
        //    return;

        MovePlayer();
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


        if (Input.GetKeyUp(KeyCode.LeftArrow))
            moveDir -= Vector2.left;
        if (Input.GetKeyUp(KeyCode.RightArrow))
            moveDir -= Vector2.right;
        //if (Input.GetKeyUp(KeyCode.DownArrow))
        //    moveDir -= Vector2.down;

        MovePlayer();
    }

    private void Jump()
    {
        if (currentJumpCount == maxJumpCount && !canQueueJump)
            return;

        if(isGrounded)
        {
            currentJumpCount += 1;

            playerCharacter.Jump(Vector2.up * jumpForce);
            return;
        }

        if (!isGrounded && canQueueJump)
        {
            jumpQueued = true;
            return;
        }

        //if (!isGrounded && !canQueueJump && currentJumpCount < maxJumpCount)
        //{            
        //    if(canCoyoteJump)
        //    {
        //        currentJumpCount += 1;
        //        playerCharacter.Jump(Vector2.up * jumpForce);
        //        canCoyoteJump = false;
                
        //        return;
        //    }

        //    playerCharacter.Jump(Vector2.up * jumpForce);
        //    currentJumpCount = maxJumpCount;
        //}
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
            if (!isGrounded)
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
        Debug.Log("Current Grounding: " + isGrounded);

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

            currentJumpCount = 0;
        }

        if (jumpQueued && isGrounded)
            Jump();
    }

    //public void HitStun()
    //{

    //}
}
