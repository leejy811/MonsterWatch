using System.Collections;
using UnityEngine;
using UnityEngine.UI;  // Image�� ����ϱ� ���� �ʿ�

/*
 public enum SkillList
{
    Shoot,
    Poison,
    Invincibility
}
 */

public class CooldownManager : MonoBehaviour
{
    // Cooldown UI �̹�����
    //public Image Skill0CooldownImage;  dash slot ���� 2�� 13�� ���� 5:23
    public Image skill1CooldownImage;
    public Image skill2CooldownImage;
    public Image skill3CooldownImage;

    // ��ų ��Ÿ�� �ð�
    public float skill1CooldownTime = 5f;  // ��ų 1 ��Ÿ�� (��)
    public float skill2CooldownTime = 3f;  // ��ų 2 ��Ÿ�� (��)
                                           // ��ų 3 ���̶� ��Ÿ�� ����

    // ��ų ��Ÿ�� �̹����� ���� ��������Ʈ
    public Sprite[] skillSprites;

    // ��ų ��� ������ üũ
    private bool isSkill1OnCooldown = false;
    private bool isSkill2OnCooldown = false;
    private bool isSkill3OnCooldown = true; //���� �нú� �̹Ƿ�

    // Start()�� ó�� ���۵� �� �� �� ȣ��˴ϴ�.
    private void Start()
    {
        // ��������Ʈ �Ҵ�
        //skill1CooldownImage.sprite = skillcool;
        //skill2CooldownImage.sprite = skillcool;

        // ��Ÿ�� �̹����� �⺻������ ����
        skill1CooldownImage.fillAmount = 1f;
        skill2CooldownImage.fillAmount = 1f;
        //���� ��Ÿ���� ������ �����Ƿ� ����
    }

    private void Update()
    {
        // Ű �Է��� �޾� ��ų�� �ߵ���Ŵ
        if (Input.GetKeyDown(KeyCode.Alpha1))  // 1�� Ű�� ������ �� ��ų 1 ��ô
        {
            if (!isSkill1OnCooldown)
            {
                UseSkill1();
            }
            else
            {
                Debug.Log("Skill 1 is on cooldown!");
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))  // 2�� Ű�� ������ �� ��ų 2 ����
        {
            if (!isSkill2OnCooldown)
            {
                UseSkill2();
            }
            else
            {
                Debug.Log("Skill 2 is on cooldown!");
            }
        }
    }

    // ��ų 1 ���
    private void UseSkill1()
    {
        Debug.Log("Skill 1 used!");
        isSkill1OnCooldown = true;  // ��ų 1 ��Ÿ�� ����
        StartCoroutine(StartSkillCooldown(skill1CooldownImage, false, skill1CooldownTime));
    }

    // ��ų 2 ���
    private void UseSkill2()
    {
        Debug.Log("Skill 2 used!");
        isSkill2OnCooldown = true;  // ��ų 2 ��Ÿ�� ����
        StartCoroutine(StartSkillCooldown(skill2CooldownImage, false, skill2CooldownTime));
    }

    public void ChangeSkill(int skillIdx)
    {
        //TODO : ���� �߰�
        skill2CooldownImage.sprite = skillSprites[skillIdx];
    }

    // ��Ÿ�� �ڷ�ƾ
    public IEnumerator StartSkillCooldown(Image skillCooldownImage, bool isDash, float cooldownTime)
    {
        Debug.Log(skillCooldownImage);
        float elapsedTime = 0f;  // ��� �ð�

        // ��Ÿ�� ���� `elapsedTime`�� ������Ű�鼭 UI �̹����� fillAmount�� ����
        while (elapsedTime < cooldownTime)
        {
            elapsedTime += Time.deltaTime;  // �����Ӵ� ��� �ð� �߰�
            skillCooldownImage.fillAmount = 1 - (elapsedTime / cooldownTime);  // 1���� ��� ������ ���� �����ϴ� ȿ�� ����
            yield return null;  // ���� �����ӱ��� ���
        }

        // ��Ÿ���� ���� �Ŀ��� �̹����� ������ ���� ���� (0���� 1�� ä���)
        skillCooldownImage.fillAmount = 1f;

        // ��Ÿ�� ���� �� ��ų�� �ٽ� ��� �����ϵ��� ����
        if (skillCooldownImage == skill1CooldownImage)
        {
            isSkill1OnCooldown = false;
        }
        else if (skillCooldownImage == skill2CooldownImage)
        {
            isSkill2OnCooldown = false;
        }
    }
}
