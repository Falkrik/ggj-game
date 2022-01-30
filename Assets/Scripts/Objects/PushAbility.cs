using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushAbility : MonoBehaviour
{
    private float pushForce;
    private Vector2 playerPosition;

    public float PushForce { set => pushForce = value; }
    public Vector2 PlayerPosition { set => playerPosition = value; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Character>() != null)
        {
            GameObject enemyObject = collision.gameObject.GetComponent<Character>().gameObject;
            Character enemy = enemyObject.GetComponent<Character>();

            Vector2 direction = transform.position - enemyObject.transform.position;
            enemy.GetPushed(pushForce, direction);

        }
    }
}
