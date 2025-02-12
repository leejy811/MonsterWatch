using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CooldownManager : MonoBehaviour
{
    public Image skill1CooldownImage;  // ��ų 1 ��Ÿ�� �̹���
    public Image skill2CooldownImage;  // ��ų 2 ��Ÿ�� �̹���

    public float skill1CooldownTime = 5f;  // ��ų 1 ��Ÿ�� �ð� (��)
    public float skill2CooldownTime = 3f;  // ��ų 2 ��Ÿ�� �ð� (��)

    private void Start()
    {
        // ���� �� 2���� ��ų ��Ÿ�� �̹����� �ʱ�ȭ
        skill1CooldownImage.fillAmount = 0f;
        skill2CooldownImage.fillAmount = 0f;
    }

    void Update() //Ʈ����
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))  // Ű 1�� ������ ��
        {
            UseSkill1();  // ��ų 1 ���
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))  // Ű 2�� ������ ��
        {
            UseSkill2();  // ��ų 2 ���
        }
    }


    // ��ų 1�� ��Ÿ���� �����ϴ� �Լ�
    public void UseSkill1()
    {
        StartCoroutine(StartCooldown(skill1CooldownImage, skill1CooldownTime));
    }

    // ��ų 2�� ��Ÿ���� �����ϴ� �Լ�
    public void UseSkill2()
    {
        StartCoroutine(StartCooldown(skill2CooldownImage, skill2CooldownTime));
    }

    // ��Ÿ���� ó���ϴ� �ڷ�ƾ �Լ�
    private IEnumerator StartCooldown(Image skillImage, float cooldownTime)
    {
        float elapsedTime = 0f;

        // ��Ÿ�� ���� ��
        while (elapsedTime < cooldownTime)
        {
            elapsedTime += Time.deltaTime;  // ��� �ð� ����
            skillImage.fillAmount = elapsedTime / cooldownTime;  // ä������ ���� ������Ʈ
            yield return null;  // �� ������ ��ٸ�
        }

        // ��Ÿ�� ������ �̹����� �ٽ� 0���� ����
        skillImage.fillAmount = 0f;
    }
}