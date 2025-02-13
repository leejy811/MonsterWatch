using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public IEnumerator LoadScene(string sceneName, float fadeSecond)
    {
        StartCoroutine(PostProcessManager.instance.FadeInOut(fadeSecond, true));
        StartCoroutine(PostProcessManager.instance.VignetteInOut(fadeSecond, 1.0f));

        yield return new WaitForSeconds(fadeSecond);

        SceneManager.LoadScene(sceneName);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
