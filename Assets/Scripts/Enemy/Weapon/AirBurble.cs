using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirBurble : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerController.instance.OnHit(transform.position, 1);
        }
    }
}
