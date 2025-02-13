using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public Transform respawnPoint;
    public int damage;

    private IEnumerator Respawn(GameObject gameObject)
    {
        PlayerController.instance.OnHit(transform.position, damage);

        yield return StartCoroutine(PostProcessManager.instance.FadeInOut(1f, true));

        gameObject.transform.position = respawnPoint.position;

        yield return StartCoroutine(PostProcessManager.instance.FadeInOut(1f, false));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Respawn(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Enemy")
        {
            //StartCoroutine(collision.gameObject.GetComponent<Enemy>().OnDie());
        }
    }

}
