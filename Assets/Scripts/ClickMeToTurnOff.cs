using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickMeToTurnOff : MonoBehaviour
{
    private void OnMouseUp()
    {
        this.gameObject.SetActive(false);
    }
}
