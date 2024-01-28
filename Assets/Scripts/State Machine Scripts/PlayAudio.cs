using UnityEngine;

public class PlayAudio : StateOneShot
{
    [SerializeField]
    private AudioSource audioSource;

    public float loopDelay = 1;

    private float timer = 0f;

    private void Awake()
    {
        if (audioSource == null)
        {
            audioSource = GetComponentInChildren<AudioSource>();
        }
        audioSource.loop = false;
    }

    public override void OnStateTrigger(CurrentStateData stateData)
    {
        // if (timer > loopDelay)
        // {
        audioSource.Play();
        Debug.Log("Playing audio");
        // }
    }

    public override void OnStateUpdate(CurrentStateData stateData)
    {
        timer += Time.deltaTime;
        if (timer > loopDelay)
        {
            audioSource.Play();
            timer = 0f;
        }

        // if (audioSource.time > audioSource.clip.length)
        // {
        //     audioSource.Stop();
        // }

    }
}