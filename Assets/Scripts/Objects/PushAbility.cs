using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushAbility : MonoBehaviour
{
    private float pushForce;

    public float PushForce { set => pushForce = value; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Character>() != null)
            collision.gameObject.GetComponent<Character>().GetPushed(pushForce);
    }
}
