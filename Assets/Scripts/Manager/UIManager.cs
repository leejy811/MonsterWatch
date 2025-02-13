using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public HealthUI healthUI;
    public SkillUI skillUI;
    public GameObject menuPanel;

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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            menuPanel.SetActive(!menuPanel.activeSelf);
            if (menuPanel.activeSelf) Time.timeScale = 0.0f;
            else Time.timeScale = 1.0f;
        }
    }

    public void UpdateHealth()
    {
        healthUI.UpdateLife();
    }

    public void ChangeSkill()
    {
        skillUI.ChangeSkill();
    }

    public void OnClickContinue()
    {
        menuPanel.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void OnClickLobby()
    {
        GameManager.instance.LoadScene("LobbyScene", 2f);
    }

    public void OnClickExit()
    {
        GameManager.instance.ExitGame();
    }
}
