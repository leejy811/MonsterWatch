using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutSceneUI : MonoBehaviour
{
    public Image image;
    public Sprite[] sprites;

    private int curIdx = 0;

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            curIdx++;

            if(curIdx == sprites.Length)
            {
                image.DOFade(0.0f, 1.0f).OnComplete(() =>
                {
                    gameObject.SetActive(false);
                });
                return;
            }

            image.sprite = sprites[curIdx];
        }
    }
}
