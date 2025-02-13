using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeManager : MonoBehaviour
{

    // ����� ��Ÿ���� ������Ʈ�� ������
    public GameObject lifePrefab;

    // ����� ������ ���� �迭 List
    private List<GameObject> lives = new List<GameObject>();

    public int maxLives = 3; // �ִ� ��� ����


    // Start is called before the first frame update
    void Start()
    {
        // ���� ���� �� ����� ���� (��: 3���� ��� �߰�)
        for (int i = 0; i < maxLives; i++)
        {
            AddLife();
        }
    }

    // ��� �߰� �Լ�
    void AddLife()
    {
        // �������� �����ϰ�, List�� �߰�
        GameObject newLife = Instantiate(lifePrefab, new Vector3(2 * lives.Count, 0, 0), Quaternion.identity);
        lives.Add(newLife);
    }

    // ����� ���� �� ȣ��Ǵ� �Լ�
    public void LoseLife()
    {
        if (lives.Count > 0)
        {
            GameObject lifeToRemove = lives[lives.Count - 1];
            lives.RemoveAt(lives.Count - 1);
            Destroy(lifeToRemove);  // ������Ʈ ����
        }
    }
}

