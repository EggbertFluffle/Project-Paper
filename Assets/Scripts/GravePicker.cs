using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class GravePicker : MonoBehaviour {
    public static GravePicker Instance;

    public enum GravePickerState { Unselected, Selected };
    public GravePickerState State = GravePickerState.Unselected;

    public Grave CurrentGrave = null;

    public UnityEngine.UI.Image[] BodyPartImages;

    private void Awake() {
        if(Instance == null) Instance = this;   
    }

    public void SelectGrave(Grave g) {
        CurrentGrave = g;
        State = GravePickerState.Selected;

        // Go though grave contents and change grave picker UI
        for(int i = 0; i < 4; i++) {
            Debug.Log("Body part length: " + g.BodyParts.Length);
            Debug.Log("Images length: " + BodyPartImages.Length);
            
            BodyPartImages[i].enabled = true;
            if(g.BodyParts[i] == null) {
                BodyPartImages[i].enabled = false;
            } else {
                BodyPartImages[i].sprite = g.BodyParts[i].LimbSprite;
            }
        }
    }

    public void LeavePicker() {
        State = GravePickerState.Unselected;
    }
}
