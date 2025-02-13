using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    public Transform lifeParent;
    public GameObject lifePrefab;
    public List<GameObject> lives = new List<GameObject>();

    private int maxLive;

    void Start()
    {
        maxLive = PlayerController.instance.maxHP;
    }

    void AddLife()
    {
        GameObject newLife = Instantiate(lifePrefab, lifeParent);
        lives.Add(newLife);
    }

    public void UpdateLife()
    {
        int curHP = PlayerController.instance.curHP;
        int tempHP = PlayerController.instance.tempHP;

        if(tempHP <= 0)
        {
            for (int i = 0; i < maxLive; i++)
            {
                lives[i].SetActive(i < curHP);
            }
        }
        else
        {
            int count = lives.Count - maxLive;
            for (int i = 0; i < count; i++)
            {
                Destroy(lives[maxLive]);
                lives.RemoveAt(maxLive);
            }

            for (int i = 0; i < tempHP; i++)
            {
                AddLife();
            }
        }
    }
}
