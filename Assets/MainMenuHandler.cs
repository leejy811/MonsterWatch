using UnityEngine;

public class MainMenuHandler : MonoBehaviour
{
    public GameObject MainMenu; // 메인 메뉴 패널을 참조
    private bool isPaused = false;

    void Update()
    {
        // Escape 키가 눌렸을 때
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMainMenu();
        }
    }

    void ToggleMainMenu()
    {
        isPaused = !isPaused; // paused 상태를 토글

        if (isPaused)
        {
            // 메인 메뉴 표시
            MainMenu.SetActive(true);
            Time.timeScale = 0f; // 게임 일시 정지

            
        }
        else
        {
            // 메인 메뉴 숨기기
            MainMenu.SetActive(false);
            Time.timeScale = 1f; // 게임 재개
        }
    }
}
