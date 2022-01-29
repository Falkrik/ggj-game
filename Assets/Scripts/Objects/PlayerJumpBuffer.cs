using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpBuffer : MonoBehaviour
{
    [SerializeField] private Player player;

    private void Start()
    {
        if (player == null)
            GetComponentInParent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<PlatformGround>() != null)
        {
            player.CanQueueJump = true;
        }
    }
}
