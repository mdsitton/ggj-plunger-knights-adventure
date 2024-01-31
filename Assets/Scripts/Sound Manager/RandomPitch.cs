using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class RandomPitch : MonoBehaviour
{
    private void Awake()
    {
        this.GetComponent<AudioSource>().pitch = Random.Range(-2.5f, 2.5f);
    }

}
