using UnityEngine;

public class PlayAudio : StateOneShot
{
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponentInChildren<AudioSource>();
    }

    public override void OnStateTrigger(CurrentStateData stateData)
    {
        audioSource.Play();
    }

    public override void OnStateUpdate(CurrentStateData stateData)
    {
        // throw new System.NotImplementedException();
    }
}