using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] keepTrackOfEnemiesList;
    public bool[] enemiesAlive;
    // Start is called before the first frame update

    private void Awake()
    {
        //Create a bool to track all enemies in List then we set if alive or not
        enemiesAlive = new bool[keepTrackOfEnemiesList.Length];
        for (int i = 0; i < keepTrackOfEnemiesList.Length; i++)
        {
            enemiesAlive[i] = keepTrackOfEnemiesList[i].activeSelf;
        }
    }

    private void LateUpdate()//No need check every Update
    {
        // Debug.Log("Are All Enimes being Track Dead = " + NoEnemiesAlive().ToString());
        //if Enimes all Die and Keeping Track List is not Empty
        if (NoEnemiesAlive() == true && keepTrackOfEnemiesList[0] != null)
        {
            // Debug.Log("Are All Enimes being Track Finally Died = " + NoEnemiesAlive().ToString());
            // Debug.Log("Game Over or Player Wins");
            SceneManager.LoadScene(2);
        }
    }

    private bool NoEnemiesAlive()
    {
        bool allEnemiesDied = true;
        //Check the list of Enemies to see if still in game if not set in Array
        for (int i = 0; i < keepTrackOfEnemiesList.Length; i++)
        {
            enemiesAlive[i] = keepTrackOfEnemiesList[i].activeSelf;
        }

        //if any enemy is still alive we set set bool to false
        foreach (var enemy in enemiesAlive)
        {
            if (enemy != false)
            {
                allEnemiesDied = false;
                break;
            }
        }
        return allEnemiesDied;

        //This returs game wincodintion if any of Monster are dead
        // bool CurrentCondition = enemiesAlive.All(x => x);
        // return CurrentCondition;
    }
}
