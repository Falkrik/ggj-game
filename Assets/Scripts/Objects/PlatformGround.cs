using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGround : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Character>() != null)
            collision.gameObject.GetComponent<Character>().ChangePlayerGrounding(true);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Character>() != null)
            collision.gameObject.GetComponent<Character>().ChangePlayerGrounding(false);
    }
}
