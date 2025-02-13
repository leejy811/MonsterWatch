using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BuffType { Health, AttackSpeedUp, AttackSizeUp }

public class Well : InteractableObject
{
    public BuffType buffType;
    public int[] buffValues;
    public Sprite offSprite;

    protected override void OnInteraction(GameObject gameObject)
    {
        PlayerController.instance.GetBuff(buffType, buffValues[(int)buffType]);

        interactable = false;
        gameObject.GetComponent<SpriteRenderer>().sprite = offSprite;
    }
}
