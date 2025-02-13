using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillUI : MonoBehaviour
{
    public Transform[] skillTrans;
    public float tweenSecond;
    public float selectScale;
    public Vector3 moveVec;

    private int skillID;
    private bool isTweening;

    public void ChangeSkill()
    {
        isTweening = true;

        int id = skillID;
        int id0 = (skillID + 1) % skillTrans.Length;
        int id1 = (skillID + 2) % skillTrans.Length;

        Vector3 pos = skillTrans[id].localPosition;
        Vector3 pos0 = skillTrans[id0].localPosition;
        Vector3 pos1 = skillTrans[id1].localPosition;

        skillTrans[id].DOLocalMove(pos + moveVec, tweenSecond);
        skillTrans[id0].DOLocalMove(pos, tweenSecond);
        skillTrans[id1].DOLocalMove(pos0, tweenSecond);

        skillTrans[id0].DOScale(selectScale, tweenSecond).OnComplete(() =>
        {
            skillTrans[id].SetAsFirstSibling();
            skillTrans[id].DOScale(1.0f, tweenSecond);
            skillTrans[id].DOLocalMove(pos1, tweenSecond).OnComplete(() =>
            {
                isTweening = false;
            });
        });

        skillID = (skillID + 1) % skillTrans.Length;
    }
}
