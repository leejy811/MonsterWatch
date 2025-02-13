using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    public float speed;

    private int damage;
    private Vector3 dir = Vector3.zero;

    private void Update()
    {
        transform.position += dir * speed * Time.deltaTime;
    }

    public void Shoot(int _damage, Vector3 _dir)
    {
        damage = _damage;
        dir = _dir;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
            //Player.OnHit(damage);
        }
    }
}
