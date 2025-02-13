using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ShootSkillObject : MonoBehaviour
{
    public float lifeTime = 0.0f;
    public float lifeTimer;
    public float moveSpeed;
    public Vector2 dir;

    public Rigidbody2D rb;
    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    public void Initialize(Vector2 _dir)
    {
        dir = _dir;
        rb.velocity = dir * moveSpeed;
        lifeTimer = lifeTime;

    }

    // Update is called once per frame
    void Update()
    {
        lifeTimer -= Time.deltaTime;

        Vector2 newVel;
        newVel.x = Mathf.Exp((dir * moveSpeed).x * (lifeTime - lifeTimer)*0.1f);
        newVel.y = 0.0f;
        rb.velocity = newVel;

        
        if (lifeTimer < 0.0f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject);
    }
}
