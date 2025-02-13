using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BuffType { Health, AttackSpeed }

public class Well : InteractableObject
{
    public BuffType buffType;
    public int[] buffValues;

    protected override void OnInteraction(GameObject gameObject)
    {
        //Player player = gameObject.GetComponent<Player>();
        //player.GetBuff(buffType, buffValues[(int)buffType]);
    }
}
