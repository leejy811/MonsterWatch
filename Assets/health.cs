using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{

    public Image[] healthgauge0;  // ü�¹ٸ� �����ϴ� ���� Image��

    public int maxHealth = 3;   // �ִ� ü��
    public int currentHealth;     // ���� ü��


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth; // �ʱ� ü�� ����
        UpdateHealth();         // ü�¹� �ʱ�ȭ
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // ü�� ���� �� ȣ��
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0) currentHealth = 0;
        UpdateHealth();  // ü�¹� ������Ʈ
    }

    // ü�� ȸ�� �� ȣ��
    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth) currentHealth = maxHealth;
        UpdateHealth();  // ü�¹� ������Ʈ
    }

    void UpdateHealth()
    {
        for (int i = 0; i < currentHealth; i++)
        {
            if (i < currentHealth)  // ü�¿� �ش��ϴ� �̹��� Ȱ��ȭ
            {
                healthgauge0[i].enabled = true;
            }
            else  // ü���� ������ �̹��� ��Ȱ��ȭ
            {
                healthgauge0[i].enabled = false;
            }
        }

    }
}
