using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CooldownManager : MonoBehaviour
{
    public Image skill1CooldownImage;  // 스킬 1 쿨타임 이미지
    public Image skill2CooldownImage;  // 스킬 2 쿨타임 이미지

    public float skill1CooldownTime = 5f;  // 스킬 1 쿨타임 시간 (초)
    public float skill2CooldownTime = 3f;  // 스킬 2 쿨타임 시간 (초)

    private void Start()
    {
        // 시작 시 2개의 스킬 쿨타임 이미지를 초기화
        skill1CooldownImage.fillAmount = 0f;
        skill2CooldownImage.fillAmount = 0f;
    }

    void Update() //트리거
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))  // 키 1을 눌렀을 때
        {
            UseSkill1();  // 스킬 1 사용
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))  // 키 2을 눌렀을 때
        {
            UseSkill2();  // 스킬 2 사용
        }
    }


    // 스킬 1의 쿨타임을 시작하는 함수
    public void UseSkill1()
    {
        StartCoroutine(StartCooldown(skill1CooldownImage, skill1CooldownTime));
    }

    // 스킬 2의 쿨타임을 시작하는 함수
    public void UseSkill2()
    {
        StartCoroutine(StartCooldown(skill2CooldownImage, skill2CooldownTime));
    }

    // 쿨타임을 처리하는 코루틴 함수
    private IEnumerator StartCooldown(Image skillImage, float cooldownTime)
    {
        float elapsedTime = 0f;

        // 쿨타임 진행 중
        while (elapsedTime < cooldownTime)
        {
            elapsedTime += Time.deltaTime;  // 경과 시간 누적
            skillImage.fillAmount = elapsedTime / cooldownTime;  // 채워지는 비율 업데이트
            yield return null;  // 한 프레임 기다림
        }

        // 쿨타임 끝나면 이미지를 다시 0으로 설정
        skillImage.fillAmount = 0f;
    }
}