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
    }
}
