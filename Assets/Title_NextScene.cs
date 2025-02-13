using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour 
{
    public string SceneToLoad;

    public void LoadGame()
    {
        SceneManager.LoadScene(SceneToLoad); //SceneToLoad 라는 string에 다음에 실행될 scene의 이름 넣기
    }


}