using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class GravePicker : MonoBehaviour {
    public static GravePicker Instance;

    public Grave CurrentGrave = null;
    public bool Opened = false;

    // Use new tool tipped limbs
    public UnityEngine.UI.Image[] BodyPartImages;

    private void Awake() {
        if(Instance == null) Instance = this;   
    }

    public void OpenGrave(Grave g) {
        CurrentGrave = g;

        // Go though grave contents and change grave picker UI
        // Implement me
        // Implement me
        // Implement me
        // Implement me
        // Implement me
        // Implement me
        // for(int i = 0; i < 4; i++) {
        //     BodyPartImages[i].enabled = true;
        //     if(g.BodyParts[i] == null) {
        //         BodyPartImages[i].enabled = false;
        //     } else {
        //         BodyPartImages[i].sprite = g.BodyParts[i].GraveLimbSprite;
        //         BodyPartImages[i].SetNativeSize();
        //     }
        // }
    }

    public void HandleLimbPick(BodyPartRef _bodyPart) {
        ClosePicker();
    }

    public void ClosePicker() {
        if(!CameraManager.Instance.IsTransitioning()) {
            GRManager.Instance.HideUI();
        }
    }
}
