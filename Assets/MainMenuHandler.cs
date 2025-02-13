using UnityEngine;

public class MainMenuHandler : MonoBehaviour
{
    public GameObject MainMenu; // ���� �޴� �г��� ����
    private bool isPaused = false;

    void Update()
    {
        // Escape Ű�� ������ ��
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMainMenu();
        }
    }

    void ToggleMainMenu()
    {
        isPaused = !isPaused; // paused ���¸� ���

        if (isPaused)
        {
            // ���� �޴� ǥ��
            MainMenu.SetActive(true);
            Time.timeScale = 0f; // ���� �Ͻ� ����

            
        }
        else
        {
            // ���� �޴� �����
            MainMenu.SetActive(false);
            Time.timeScale = 1f; // ���� �簳
        }
    }
}
