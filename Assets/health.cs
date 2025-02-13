using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{

    public Image[] healthgauge0;  // 체력바를 구성하는 여러 Image들

    public int maxHealth = 3;   // 최대 체력
    public int currentHealth;     // 현재 체력


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth; // 초기 체력 설정
        UpdateHealth();         // 체력바 초기화
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 체력 변경 시 호출
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0) currentHealth = 0;
        UpdateHealth();  // 체력바 업데이트
    }

    // 체력 회복 시 호출
    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth) currentHealth = maxHealth;
        UpdateHealth();  // 체력바 업데이트
    }

    void UpdateHealth()
    {
        for (int i = 0; i < currentHealth; i++)
        {
            if (i < currentHealth)  // 체력에 해당하는 이미지 활성화
            {
                healthgauge0[i].enabled = true;
            }
            else  // 체력이 없으면 이미지 비활성화
            {
                healthgauge0[i].enabled = false;
            }
        }

    }
}
