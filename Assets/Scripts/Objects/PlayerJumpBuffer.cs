using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpBuffer : MonoBehaviour
{
    [SerializeField] private Player player;

    private void Start()
    {
        if (player == null)
            player = GetComponentInParent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<PlatformGround>() != null)
        {
            Debug.Log("Can queue jump.");
            player.CanQueueJump = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlatformGround>() != null)
        {
            player.CanQueueJump = false;
        }
    }
}
