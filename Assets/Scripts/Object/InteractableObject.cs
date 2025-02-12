using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public bool interactable;

    protected virtual void OnInteraction(GameObject gameObject)
    {

    }

    protected void OnTriggerStay2D(Collider2D collision)
    {
        if (!interactable) return;

        if (collision.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                OnInteraction(collision.gameObject);
            }
        }
    }
}
