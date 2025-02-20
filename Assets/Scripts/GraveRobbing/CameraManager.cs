using System;
using UnityEngine;

public class CameraManager : MonoBehaviour {
    public static CameraManager Instance;

    public Animator CameraAnimator;

    private void Awake() {
        if(Instance == null) Instance = this;
    }

    public bool IsOpen() {
        String currentClipName = CameraAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.name;
        return currentClipName == "Opened";
    }
    
    public bool IsClosed() {
        String currentClipName = CameraAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.name;
        return currentClipName == "Closed";
    }

    // Ensure the camera is not moving in or out
    public bool IsTransitioning() {
        String currentClipName = CameraAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.name;
        return currentClipName == "ClosePanel" && currentClipName == "OpenPanel";
    }
}
