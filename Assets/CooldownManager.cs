using System.Collections;
using UnityEngine;
using UnityEngine.UI;  // Image를 사용하기 위해 필요

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
    // Cooldown UI 이미지들
    //public Image Skill0CooldownImage;  dash slot 삭제 2월 13일 오후 5:23
    public Image skill1CooldownImage;
    public Image skill2CooldownImage;
    public Image skill3CooldownImage;

    // 스킬 쿨타임 시간
    public float skill1CooldownTime = 5f;  // 스킬 1 쿨타임 (초)
    public float skill2CooldownTime = 3f;  // 스킬 2 쿨타임 (초)
                                           // 스킬 3 독이라서 쿨타임 없음

    // 스킬 쿨타임 이미지를 위한 스프라이트
    public Sprite[] skillSprites;

    // 스킬 사용 중인지 체크
    private bool isSkill1OnCooldown = false;
    private bool isSkill2OnCooldown = false;
    private bool isSkill3OnCooldown = true; //독은 패시브 이므로

    // Start()는 처음 시작될 때 한 번 호출됩니다.
    private void Start()
    {
        // 스프라이트 할당
        //skill1CooldownImage.sprite = skillcool;
        //skill2CooldownImage.sprite = skillcool;

        // 쿨타임 이미지를 기본값으로 설정
        skill1CooldownImage.fillAmount = 1f;
        skill2CooldownImage.fillAmount = 1f;
        //독은 쿨타임을 가지지 않으므로 생략
    }

    private void Update()
    {
        // 키 입력을 받아 스킬을 발동시킴
        if (Input.GetKeyDown(KeyCode.Alpha1))  // 1번 키를 눌렀을 때 스킬 1 투척
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

        if (Input.GetKeyDown(KeyCode.Alpha2))  // 2번 키를 눌렀을 때 스킬 2 무적
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

    // 스킬 1 사용
    private void UseSkill1()
    {
        Debug.Log("Skill 1 used!");
        isSkill1OnCooldown = true;  // 스킬 1 쿨타임 시작
        StartCoroutine(StartSkillCooldown(skill1CooldownImage, false, skill1CooldownTime));
    }

    // 스킬 2 사용
    private void UseSkill2()
    {
        Debug.Log("Skill 2 used!");
        isSkill2OnCooldown = true;  // 스킬 2 쿨타임 시작
        StartCoroutine(StartSkillCooldown(skill2CooldownImage, false, skill2CooldownTime));
    }

    public void ChangeSkill(int skillIdx)
    {
        //TODO : 연출 추가
        skill2CooldownImage.sprite = skillSprites[skillIdx];
    }

    // 쿨타임 코루틴
    public IEnumerator StartSkillCooldown(Image skillCooldownImage, bool isDash, float cooldownTime)
    {
        Debug.Log(skillCooldownImage);
        float elapsedTime = 0f;  // 경과 시간

        // 쿨타임 동안 `elapsedTime`을 증가시키면서 UI 이미지의 fillAmount를 갱신
        while (elapsedTime < cooldownTime)
        {
            elapsedTime += Time.deltaTime;  // 프레임당 경과 시간 추가
            skillCooldownImage.fillAmount = 1 - (elapsedTime / cooldownTime);  // 1에서 경과 비율을 빼서 감소하는 효과 적용
            yield return null;  // 다음 프레임까지 대기
        }

        // 쿨타임이 끝난 후에는 이미지가 완전히 차게 설정 (0에서 1로 채우기)
        skillCooldownImage.fillAmount = 1f;

        // 쿨타임 종료 후 스킬이 다시 사용 가능하도록 설정
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
