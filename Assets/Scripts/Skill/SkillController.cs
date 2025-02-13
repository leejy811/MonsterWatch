using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    public bool[] skillUsable;
    public bool isChangable;

    [Header("Shoot Skill Info")]
    public GameObject shootSkillPrefabs;
    public float shootDist;
    public float shootDelay;

    [Header("Poison Skill Info")]
    public float poisonTime;

    [Header("Invincibility Skill Info")]
    public float invincibilityTime;
    public GameObject invincibilityBubbleObject;

    void Awake()
    {
        useSkill = new UseSkillDelegate[3] { ShootSkill, Poison, Invincibility };
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && skillUsable[(int)curSkill])
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
        StartCoroutine(ShootCoroutine());
        StartCoroutine(CoolDownCoroutine());
    }

    IEnumerator ShootCoroutine()
    {
        PlayerController.instance.animator.SetBool("IsShootSkill", true);
        int playerFacingDir = PlayerController.instance.facingDir;
        Vector3 initPos;
        initPos.x = this.transform.position.x + playerFacingDir * shootDist;
        initPos.y = this.transform.position.y;
        initPos.z = this.transform.position.z;

        GameObject amulet = Instantiate(shootSkillPrefabs, initPos, Quaternion.identity);

        ShootSkillObject obj = amulet.GetComponent<ShootSkillObject>();
        if (obj != null)
            obj.Initialize(new Vector2(playerFacingDir, 0));

        yield return new WaitForSeconds(shootDelay);
        PlayerController.instance.animator.SetBool("IsShootSkill", false);
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
        StartCoroutine(CoolDownCoroutine());
    }

    IEnumerator InvincibilityCoroutine()
    {
        PlayerController.instance.isInvincibility = true;
        PlayerController.instance.isInputEnabled = false;
        PlayerController.instance.animator.SetBool("IsInvincibility", true);
        GameObject obj = Instantiate(invincibilityBubbleObject, this.transform.position, Quaternion.identity, PlayerController.instance.transform);

        float timer = invincibilityTime;
        while(timer > 0.0f)
        {
            timer -= Time.deltaTime;
            if (PlayerController.instance.isGrounded)
                PlayerController.instance.rb.velocity = Vector2.zero;
            yield return null;
        }

        PlayerController.instance.isInvincibility = false;
        PlayerController.instance.isInputEnabled = true;
        PlayerController.instance.animator.SetBool("IsInvincibility", false);
        Destroy(obj);
    }

    IEnumerator CoolDownCoroutine()
    {
        skillUsable[(int)curSkill] = false;
        yield return new WaitForSeconds(skillCoolTime[(int)curSkill]);
        skillUsable[(int)curSkill] = true;
    }
}
