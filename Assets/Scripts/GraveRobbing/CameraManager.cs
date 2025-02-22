using System;
using UnityEngine;

public class CameraManager : MonoBehaviour {
    public static CameraManager Instance;

    public Animator CameraAnimator;

    private void Awake() {
        if(Instance == null) Instance = this;
    }

    public bool IsOpen() {
        string currentClipName = CameraAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.name;
        return currentClipName == "Opened";
    }
    
    public bool IsClosed() {
        string currentClipName = CameraAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.name;
        return currentClipName == "Closed";
    }

    // Ensure the camera is not moving in or out
    public bool IsTransitioning() {
        string currentClipName = CameraAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.name;
        return currentClipName == "ClosePanel" && currentClipName == "OpenPanel";
    }
}
