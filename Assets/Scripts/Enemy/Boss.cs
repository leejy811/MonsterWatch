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

    public Action skillAction;
}

public class Boss : Enemy
{
    [Header("Skill")]
    public List<BossSkill> skills;
    public Vector2Int restRange;

    private float curAttackTime;
    private int curRestTime;

    #region Init

    protected override void Awake()
    {
        //Components Initialize
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        InitProb();
        InitAction();
    }

    private void Update()
    {
        
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

    private void InitAction()
    {
        skills[(int)BossSkillType.Butble].skillAction += BurblePattern;
        skills[(int)BossSkillType.Wave].skillAction += WavePattern;
        skills[(int)BossSkillType.Air].skillAction += AirPattern;
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

    public void AirPattern()
    {
        BossSkill skill = skills[(int)BossSkillType.Air];
        StartCoroutine(PlayAirPattern(skill));
    }

    IEnumerator PlayAirPattern(BossSkill skill)
    {
        float prevGravity = rb.gravityScale;
        rb.gravityScale = 0.0f;

        transform.DOMoveY(4.0f, 0.5f).SetEase(Ease.OutBack);
        yield return new WaitForSeconds(0.5f);

        animator.SetFloat("AttackSpeed", 0.0f);
        skill.skillPrefab.SetActive(true);
        yield return new WaitForSeconds(skill.skillTime - 1f);
        skill.skillPrefab.SetActive(false);
        animator.SetFloat("AttackSpeed", 1.0f);

        rb.gravityScale = prevGravity;
    }

    #endregion

    public override void Attack()
    {
        base.Attack();

        Think();
    }

    protected override IEnumerator PlayAttack()
    {
        yield return null;
    }

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
        skills[i].skillAction.Invoke();
    }
}
