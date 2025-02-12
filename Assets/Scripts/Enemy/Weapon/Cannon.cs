using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public float shootForce;
    public float yOffest;

    private CircleCollider2D circleCollider;
    private Rigidbody2D rigid;
    private int damage;
    private Transform target;

    private void Awake()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();
        circleCollider = gameObject.GetComponent<CircleCollider2D>();
        circleCollider.enabled = false;
    }

    public void Shoot(int _damage, Transform _target)
    {
        damage = _damage;
        target = _target;

        rigid.AddForce(new Vector2(0, shootForce), ForceMode2D.Impulse);
    }

    public void Fall()
    {
        float ranX = Random.Range(-1f, 1f);
        transform.position = new Vector3(target.position.x + ranX, yOffest, transform.position.z);
        circleCollider.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
            //Player.OnHit(damage);
        }
        else if (collision.CompareTag("Ground"))
            Destroy(gameObject);
    }
}
