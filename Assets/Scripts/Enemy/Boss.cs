using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.Burst;
using UnityEngine;

public enum BossSkillType { Butble, Wave, Air }

[System.Serializable]
public class BossSkill
{
    public BossSkillType skillType;
    public int skillDamage;
    public float skillTime;
    [Range(0.0f, 1.0f)] public float skillProb;
    public GameObject skillPrefab;
    public Transform skillParent;
}

public class Boss : Enemy
{
    [Header("Skill")]
    public List<BossSkill> skills;
    public Vector2Int restRange;

    public bool isStart;

    private float curAttackTime;
    private int curRestTime;

    #region Init

    protected override void Awake()
    {
        //Components Initialize
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    protected override void Start()
    {
        base.Start();
        InitProb();
    }

    private void Update()
    {
        //if (!isStart) return;

        curAttackTime += Time.deltaTime;

        if(curAttackTime > attackCool)
        {
            Think();
            curAttackTime = 0.0f;
        }
    }

    private void InitProb()
    {
        float totalProb = 0.0f;

        foreach (BossSkill skill in skills)
        {
            totalProb += skill.skillProb;
        }

        foreach (BossSkill skill in skills)
        {
            skill.skillProb /= totalProb;
        }
    }
    #endregion

    #region AttackPattern
    public void BurblePattern()
    {
        BossSkill skill = skills[(int)BossSkillType.Butble];
        GameObject burbleObject = Instantiate(skill.skillPrefab, transform.position, Quaternion.identity, transform);
        burbleObject.GetComponent<Burble>().Shoot(skill.skillDamage, target);
    }

    public void WavePattern()
    {
        BossSkill skill = skills[(int)BossSkillType.Wave];
        GameObject WaveLeft = Instantiate(skill.skillPrefab, transform.position, Quaternion.identity, transform);
        GameObject WaveRight = Instantiate(skill.skillPrefab, transform.position, Quaternion.identity, transform);
        WaveLeft.GetComponent<Wave>().Shoot(skill.skillDamage, Vector3.left);
        WaveRight.GetComponent<Wave>().Shoot(skill.skillDamage, Vector3.right);
    }

    #endregion

    private void Think()
    {
        float probSum = 0.0f;
        float ran = UnityEngine.Random.Range(0.0f, 1.0f);
        int i = 0;

        for (i = 0; i < skills.Count; i++)
        {
            probSum += skills[i].skillProb;
            if(ran <= probSum)
                break;
        }

        attackCool = skills[i].skillTime;
        damage = skills[i].skillDamage;

        animator.SetInteger("SkillIdx", i);
        animator.SetTrigger("DoAttack");
    }
}
