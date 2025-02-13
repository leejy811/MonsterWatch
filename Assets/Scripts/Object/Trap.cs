using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public Transform respawnPoint;
    public int damage;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().OnHit(transform.position, damage);
        }
        else if (collision.gameObject.tag == "Enemy")
        {
            //StartCoroutine(collision.gameObject.GetComponent<Enemy>().OnDie());
        }
    }
}
