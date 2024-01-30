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
        this.stateData = stateData;
    }

    public abstract void OnStateTrigger(CurrentStateData stateData);
    public abstract void OnStateUpdate(CurrentStateData stateData);

    private void Update()
    {
        if (stateData != null && stateData.newState == runAtState)
        {
            OnStateUpdate(stateData);
        }
    }
}
