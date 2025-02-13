using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;  // EventTrigger 관련 기능을 사용하기 위한 네임스페이스

public class SkillTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // 스킬 설명 텍스트
    public TextMeshProUGUI skillDescriptionText;

    // 설명할 텍스트 (예시로 넣은 텍스트, 필요에 맞게 수정)
    public string skillDescription = "부적을 투척";

    private void Start()
    {
        // 초기 상태에서는 텍스트를 숨깁니다.
        skillDescriptionText.gameObject.SetActive(false);
    }

    // 마우스가 이미지 위에 올라갔을 때 호출됩니다.
    public void OnPointerEnter(PointerEventData eventData)
    {
        // 스킬 설명 텍스트를 보이게 합니다.
        skillDescriptionText.text = skillDescription;
        skillDescriptionText.gameObject.SetActive(true);
    }

    // 마우스가 이미지 밖으로 나갔을 때 호출됩니다.
    public void OnPointerExit(PointerEventData eventData)
    {
        // 스킬 설명 텍스트를 숨깁니다.
        skillDescriptionText.gameObject.SetActive(false);
    }
}
