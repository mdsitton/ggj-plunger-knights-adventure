using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] keepTrackOfEnemiesList;
    public bool[] enemiesAlive;
    // Start is called before the first frame update

    private void Awake()
    {
        enemiesAlive = new bool[keepTrackOfEnemiesList.Length];
        foreach (GameObject inGameEnimies in keepTrackOfEnemiesList)
        {
            enemiesAlive = new bool[keepTrackOfEnemiesList.Length];
        }
    }
    

    private void LateUpdate()
    {
        if (!keepTrackOfEnemiesList[0].gameObject.activeSelf)
        {
            enemiesAlive[0] = false;
        }
        else
        {
            enemiesAlive[0] = true;
        }

        if (!enemiesAlive[0])
        {
            Debug.Log("Game Over");
            SceneManager.LoadScene(2);
        }
    }
}
