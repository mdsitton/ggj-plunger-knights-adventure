using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public States currentState;

    private void Update()
    {
        RunStateMachine();
    }

    private void RunStateMachine()
    {
        States nextState = currentState?.RunCurrentState();

        if(nextState != null)
        {
            SwitchToTheNextState(nextState);
        }

        
    }

    private void SwitchToTheNextState(States nextState)
    {
        currentState = nextState;
    }

    
    
}
