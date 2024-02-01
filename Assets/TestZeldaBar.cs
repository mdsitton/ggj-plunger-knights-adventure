using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode]
public class TestZeldaBar : MonoBehaviour
{
    public int total;
    public float doDamage;

    private void Start()
    {
        ZeldaStyleHealthBar.instance.SetupHearts(total);
        ZeldaStyleHealthBar.instance.RemoveHearths(doDamage);
    }

}
