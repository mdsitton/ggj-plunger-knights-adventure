using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode]
public class TestZeldaBar : MonoBehaviour
{
    public int total;
    public float doDamage;

    public Player player;
    private void Start()
    {
        ConvertHealth();
        ZeldaStyleHealthBar.instance.RemoveHearths(doDamage);
    }

    private void Update()
    {
        ZeldaStyleHealthBar.instance.SetCurrentHealth((float)player.Health / total);
    }

    void ConvertHealth()
    {
        ZeldaStyleHealthBar.instance.SetupHearts(player.Health / total); 
    }
}
