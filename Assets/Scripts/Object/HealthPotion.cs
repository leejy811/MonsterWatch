using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : Item
{
    protected override void GetItem(GameObject gameObject)
    {
        PlayerController player = gameObject.GetComponent<PlayerController>();

        //player.GetHP(value);
    }
}
