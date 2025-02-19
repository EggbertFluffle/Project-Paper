using System;
using UnityEngine;

public class CameraManager : MonoBehaviour {
    public static CameraManager Instance;

    public Animator CameraAnimator;

    private void Awake() {
        if(Instance == null) Instance = this;
    }

    public bool CanTransition() {
        AnimatorClipInfo[] currentClips = CameraAnimator.GetCurrentAnimatorClipInfo(0);
        String currentClipName = currentClips[0].clip.name;
        return currentClipName != "ClosePanel" && currentClipName != "OpenPanel";
    }
}
