using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SetOfGroups : MonoBehaviour
{
    public GameObject[] group;
    public int countLimit;
    private int loopDynamicNumber;

    private void Start()
    {       
        Reset();
        LoopAndTurnOff();
//      Debug.Log("SET UP DOORS");
    }

    private void Reset()
    {
        loopDynamicNumber = countLimit;
        for (int i = 0; i < group.Length; i++)
        {
            group[i].gameObject.SetActive(true);
        }
//      Debug.Log("We Reset");
    }
    private void LoopAndTurnOff()
    {       
 //     Debug.Log("we chose pokemman" + loopDynamicNumber);
        for (int i = 0; i < loopDynamicNumber; i++)
        {
           int randomNumber = Random.Range(0, group.Length);

            if (group[randomNumber].activeInHierarchy == true)
            {
                group[randomNumber].gameObject.SetActive(false);
            }
            else
            {
                loopDynamicNumber++;
//              Debug.Log("ARE YOU READDDDDY");
            }
        }
    }
}
