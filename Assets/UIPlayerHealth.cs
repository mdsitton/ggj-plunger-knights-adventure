using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerHealth : MonoBehaviour
{
    public Player trackPlayerInGame;
    public Slider sliderTracker;
    private float currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = trackPlayerInGame.Health;
        if (!sliderTracker)
        {
            this.gameObject.GetComponent<Slider>();
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        currentHealth = trackPlayerInGame.Health;
        sliderTracker.value = currentHealth;
    }
}
