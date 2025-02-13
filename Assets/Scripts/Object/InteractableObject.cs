using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public bool interactable;

    private bool inObject;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && inObject)
        {
            OnInteraction(PlayerController.instance.gameObject);
        }
    }

    protected virtual void OnInteraction(GameObject gameObject)
    {

    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (!interactable) return;

        if (collision.tag == "Player")
        {
            inObject = true;
        }
    }

    protected void OnTriggerExit2D(Collider2D collision)
    {
        if (!interactable) return;

        if (collision.tag == "Player")
        {
            inObject = false;
        }
    }
}
