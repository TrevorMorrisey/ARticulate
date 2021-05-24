using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

// From https://developer.vuforia.com/forum/unity-extension-technical-discussion/blurry-camera-image
// This was needed on a Samsung Galaxy S21 and not needed on a Samsung Galaxy S7
public class VuforiaFocus : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        var vuforia = VuforiaARController.Instance;
        vuforia.RegisterVuforiaStartedCallback(OnVuforiaStarted);
        vuforia.RegisterOnPauseCallback(OnPaused);
    }

    private void OnVuforiaStarted()
    {
        CameraDevice.Instance.SetFocusMode(
        CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
    }

    private void OnPaused(bool paused)
    {

        if (!paused) // resumed
        {
            // Set again autofocus mode when app is resumed
            CameraDevice.Instance.SetFocusMode(
            CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
        }
    }
}