using UnityEngine;

#if UNITY_EDITOR
#endif

public abstract class StateOneShot : MonoBehaviour
{
    public AiState runAtState;
    private CurrentStateData stateData;

    private bool isRunning = false;

    public void OnStateDelegate(CurrentStateData stateData)
    {
        OnStateTrigger(stateData);
        if (stateData.newState == runAtState)
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }
    }

    public abstract void OnStateTrigger(CurrentStateData stateData);
    public abstract void OnStateUpdate(CurrentStateData stateData);

    private void Update()
    {
        if (isRunning)
        {
            OnStateUpdate(stateData);
        }
    }
}
