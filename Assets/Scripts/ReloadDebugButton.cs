using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Composites;
using UnityEngine.SceneManagement;

public class ReloadDebugButton : MonoBehaviour
{
    private void OnGUI()
    {
        if (GUI.Button(new Rect(25,25,200,100), "RESET ME"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Debug.Log("Screen Was Reset");
        }

        if (GUI.Button(new Rect(Screen.width-85, 20, 75, 75), "Exit"))
        {
            Application.Quit();
            Debug.Log("Quite App make my my own scrit later");
        }
    }
}
