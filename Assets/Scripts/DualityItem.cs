using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DualityItem : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Character>() != null)
        {
            collision.gameObject.GetComponent<Character>().CharacterPlayer.GainDuality();
            Destroy(this.gameObject);
        }
    }
}
