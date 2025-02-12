using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeManager : MonoBehaviour
{

    // 목숨을 나타내는 오브젝트의 프리팹
    public GameObject lifePrefab;

    // 목숨을 관리할 동적 배열 List
    private List<GameObject> lives = new List<GameObject>();

    public int maxLives = 3; // 최대 목숨 개수


    // Start is called before the first frame update
    void Start()
    {
        // 게임 시작 시 목숨을 설정 (예: 3개의 목숨 추가)
        for (int i = 0; i < maxLives; i++)
        {
            AddLife();
        }
    }

    // 목숨 추가 함수
    void AddLife()
    {
        // 프리팹을 생성하고, List에 추가
        GameObject newLife = Instantiate(lifePrefab, new Vector3(2 * lives.Count, 0, 0), Quaternion.identity);
        lives.Add(newLife);
    }

    // 목숨을 잃을 때 호출되는 함수
    public void LoseLife()
    {
        if (lives.Count > 0)
        {
            GameObject lifeToRemove = lives[lives.Count - 1];
            lives.RemoveAt(lives.Count - 1);
            Destroy(lifeToRemove);  // 오브젝트 제거
        }
    }
}

