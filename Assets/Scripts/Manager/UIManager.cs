using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public HealthUI healthUI;
    public SkillUI skillUI;

    public void UpdateHealth()
    {
        healthUI.UpdateLife();
    }

    public void ChangeSkill()
    {
        skillUI.ChangeSkill();
    }
}
