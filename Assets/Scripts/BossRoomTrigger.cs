using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoomTrigger : MonoBehaviour
{
    public Boss boss;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision .CompareTag("Player"))
        {
            boss.isStart  = true;
            SoundManager.instance.PlaySFX("Shouting");
        }
    }
}
