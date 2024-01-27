using UnityEngine;

/// <summary>
/// A timer that triggers every interval
/// </summary>
public class Repeater
{
    private float timeRemaining;
    private float timeInterval;
    private bool hasLooped = false;
    private bool paused = false;

    public Repeater(float interval)
    {
        timeRemaining = interval;
        timeInterval = interval;
    }

    /// <summary>
    /// Updates the timer. Call this once per frame.
    /// </summary>
    public void Update()
    {
        if (hasLooped)
        {
            hasLooped = false;
        }

        if (!paused)
        {
            timeRemaining -= Time.deltaTime;
        }

        if (timeRemaining <= 0)
        {
            timeRemaining += timeInterval;
            hasLooped = true;
        }
    }

    public void Pause()
    {
        paused = true;
    }

    /// <summary>
    /// Returns true if the timer has looped this frame
    /// </summary>
    /// <returns></returns>
    public bool HasTriggered() => hasLooped;

    /// <summary>
    /// Resets the timer to its initial state
    /// </summary>
    public void Reset()
    {
        timeRemaining = timeInterval;
        hasLooped = false;
    }
}