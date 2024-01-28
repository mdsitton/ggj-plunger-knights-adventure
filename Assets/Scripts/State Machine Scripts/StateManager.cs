using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public enum AiState
{
    Idle,
    Search,
    Attack,
    Dead
}

public interface IStateSystem
{
    (AiState nextState, float delayTime) OnIdleState(AiState previousState);
    (AiState nextState, float delayTime) OnSearchState(AiState previousState);
    (AiState nextState, float delayTime) OnAttackState(AiState previousState);
    (AiState nextState, float delayTime) OnDeadState(AiState previousState);
}

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
        // Debug.Log($"{currentStateData.previousState} -> {currentStateData.newState}");

        foreach (var state in stateCallbacks[(int)currentStateData.newState])
        {
            state?.Invoke(currentStateData);
        }
    }

    public void ChangeState(AiState newState)
    {
        currentStateData.newState = newState;
    }

}
