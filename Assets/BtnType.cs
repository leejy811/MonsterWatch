using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BtnType : MonoBehaviour, IPointerEnterHandler , IPointerExitHandler
{
    public BTNType currentType;
    public Transform buttonScale;
    Vector3 defaultScale;

    private void Start()
    {
        defaultScale = transform.localScale;
    }

    public void OnBtnClick()
    {
        switch (currentType)
        {
            case BTNType.New:
                Debug.Log("시작하기");
                break;
            case BTNType.Continue:
                Debug.Log("이어하기");
                    break;
            case BTNType.Quit:
                Debug.Log("종료하기");
                    break;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonScale.localScale = defaultScale * 1.2f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonScale.localScale = defaultScale;
    }
}
