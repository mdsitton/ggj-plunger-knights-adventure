using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class AIManagerStates : MonoBehaviour
{
    public States[] actionList;

    void Awake()
    {
        Debug.Log("This is a action test" + actionList[0]);
    }

}
