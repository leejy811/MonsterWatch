using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;  // EventTrigger ���� ����� ����ϱ� ���� ���ӽ����̽�

public class SkillTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // ��ų ���� �ؽ�Ʈ
    public TextMeshProUGUI skillDescriptionText;

    // ������ �ؽ�Ʈ (���÷� ���� �ؽ�Ʈ, �ʿ信 �°� ����)
    public string skillDescription = "������ ��ô";

    private void Start()
    {
        // �ʱ� ���¿����� �ؽ�Ʈ�� ����ϴ�.
        skillDescriptionText.gameObject.SetActive(false);
    }

    // ���콺�� �̹��� ���� �ö��� �� ȣ��˴ϴ�.
    public void OnPointerEnter(PointerEventData eventData)
    {
        // ��ų ���� �ؽ�Ʈ�� ���̰� �մϴ�.
        skillDescriptionText.text = skillDescription;
        skillDescriptionText.gameObject.SetActive(true);
    }

    // ���콺�� �̹��� ������ ������ �� ȣ��˴ϴ�.
    public void OnPointerExit(PointerEventData eventData)
    {
        // ��ų ���� �ؽ�Ʈ�� ����ϴ�.
        skillDescriptionText.gameObject.SetActive(false);
    }
}
