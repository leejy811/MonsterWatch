using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyUIManager : MonoBehaviour
{
    public void OnClickStartGame()
    {
        StartCoroutine(GameManager.instance.LoadScene("InGameScene", 2f));
    }

    public void OnClickExitGame()
    {
        GameManager.instance.ExitGame();
    }
}
