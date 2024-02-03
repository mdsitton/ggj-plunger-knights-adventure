using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class CurrentStateData
{
    public AiState previousState;
    public AiState newState;
    public IEntity entity;
    public float idleDelay;
}

[RequireComponent(typeof(IStateSystem))]
public class StateManager : MonoBehaviour
{
    private delegate void OnStateDelegate(CurrentStateData stateData);

    IStateSystem stateSystem;

    List<List<OnStateDelegate>> stateCallbacks = new();

    private CurrentStateData currentStateData;

    float idleTimer = 0f;

    public CurrentStateData CurrentStateData => currentStateData;

    private void Start()
    {
        currentStateData = new CurrentStateData();

        var components = GetComponents<StateOneShot>();
        stateSystem = GetComponent<IStateSystem>();
        currentStateData.entity = GetComponent<IEntity>();

        // ensure slots for all states exists
        foreach (var @enum in Enum.GetValues(typeof(AiState)))
        {
            stateCallbacks.Add(new List<OnStateDelegate>());
        }

        foreach (var component in components)
        {
            stateCallbacks[(int)component.runAtState].Add(component.OnStateDelegate);
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (currentStateData == null)
        {
            return;
        }
        GUIStyle style = new GUIStyle();
        style.normal.textColor = Color.green;
        style.fontSize = 40;
        style.fontStyle = FontStyle.Bold;
        style.alignment = TextAnchor.MiddleCenter;
        Handles.Label(transform.position + (-transform.up * 0.3f), $"{currentStateData.newState} {idleTimer:0.00}", style);
    }
#endif

    private void Update()
    {
        idleTimer += Time.deltaTime;

        if (idleTimer >= currentStateData.idleDelay)
        {
            idleTimer = 0f;
            RunStateMachine();
        }
    }

    private void RunStateMachine()
    {
        currentStateData.previousState = currentStateData.newState;
        // use the idle state if a state doesn't exist

        switch (currentStateData.newState)
        {
            case AiState.Idle:
                (currentStateData.newState, currentStateData.idleDelay) = stateSystem.OnIdleState(currentStateData.previousState);
                break;
            case AiState.Search:
                (currentStateData.newState, currentStateData.idleDelay) = stateSystem.OnSearchState(currentStateData.previousState);
                break;
            case AiState.Attack:
                (currentStateData.newState, currentStateData.idleDelay) = stateSystem.OnAttackState(currentStateData.previousState);
                break;
            case AiState.Dead:
                (currentStateData.newState, currentStateData.idleDelay) = stateSystem.OnDeadState(currentStateData.previousState);
                break;
        }

        foreach (var state in stateCallbacks[(int)currentStateData.newState])
        {
            state?.Invoke(currentStateData);
        }
    }

    public void ChangeState(AiState newState)
    {
        currentStateData.newState = newState;
        Debug.Log($"Changing state to {newState}");
    }

}
