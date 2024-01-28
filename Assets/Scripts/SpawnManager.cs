using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] keepTrackOfEnemiesList;
    public bool[] enemiesAlive;
    // Start is called before the first frame update

    private void Awake()
    {
        enemiesAlive = new bool[keepTrackOfEnemiesList.Length];
    }

    private void LateUpdate()
    {
        if (keepTrackOfEnemiesList[0].gameObject.active == false)
        {
            enemiesAlive[0] = keepTrackOfEnemiesList[0].gameObject.active;
        }
    }
}
