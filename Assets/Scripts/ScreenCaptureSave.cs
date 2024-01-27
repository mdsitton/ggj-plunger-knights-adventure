using UnityEngine;

// Generate a screenshot and save it to disk with the name SomeLevel.png.

public class ScreenCaptureSave : MonoBehaviour
{
    void OnMouseDown()
    {
        ScreenCapture.CaptureScreenshot("SomeLevel.png");
        Debug.Log("shoots");
    }
}