using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burble : MonoBehaviour
{
    public float speed;

    private int damage;
    private Vector3 dir = Vector3.zero;

    private void Update()
    {
        transform.position += dir * speed * Time.deltaTime;
    }

    public void Shoot(int _damage, Transform _target)
    {
        damage = _damage;
        dir = _target.position.x > transform.position.x ? Vector3.right : Vector3.left;
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
