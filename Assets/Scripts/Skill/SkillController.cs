using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Collections;
using UnityEngine;

public enum SkillList
{
    Shoot,
    Poison,
    Invincibility
}

public class SkillController : MonoBehaviour
{
    delegate void UseSkillDelegate();
    UseSkillDelegate[] useSkill;

    public SkillList curSkill = 0;
    public float[] skillCoolTime;
    public bool isChangable;

    [Header("Shoot Skill Info")]
    public GameObject shootSkillPrefabs;
    public int shootNum;
    public float shootDist;
    public float shootDelay;

    [Header("Poison Skill Info")]
    public float poisonTime;

    [Header("Invincibility Skill Info")]
    public float invincibilityTime;

    void Awake()
    {
        useSkill = new UseSkillDelegate[3] { ShootSkill, Poison, Invincibility };
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            useSkill[(int)curSkill]();
        }

        if(Input.GetKeyDown(KeyCode.Tab) && isChangable)
        {
            curSkill++;
            if ((int)curSkill > 2)
                curSkill = 0;
        }
    }

    void ShootSkill()
    {
        StartCoroutine(ShootCoroutine(shootNum));
    }

    IEnumerator ShootCoroutine(int shootNum)
    {
        while(shootNum > 0)
        {
            shootNum--;
            Vector3 initPos;
            initPos.x = this.transform.position.x + this.transform.localScale.x * -shootDist;
            initPos.y = this.transform.position.y;
            initPos.z = this.transform.position.z;

            GameObject amulet = Instantiate(shootSkillPrefabs, initPos, Quaternion.identity);

            ShootSkillObject obj = amulet.GetComponent<ShootSkillObject>();
            if (obj != null)
                obj.Initialize(initPos - this.transform.position);

            yield return new WaitForSeconds(shootDelay);
        }
    }

    void Poison()
    {
        StartCoroutine(PoisonCoroutine());
    }

    IEnumerator PoisonCoroutine()
    {
        PlayerController.instance.isPoison = true;
        yield return new WaitForSeconds(poisonTime);
        PlayerController.instance.isPoison = false;
    }

    void Invincibility()
    {
        StartCoroutine(InvincibilityCoroutine());
    }

    IEnumerator InvincibilityCoroutine()
    {
        PlayerController.instance.isInvincibility = true;
        yield return new WaitForSeconds(invincibilityTime);
        PlayerController.instance.isInvincibility = false;
    }
}
