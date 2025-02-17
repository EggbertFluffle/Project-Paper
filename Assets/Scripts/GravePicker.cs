using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using Unity.VisualScripting;
using UnityEngine;


public class GravePicker : MonoBehaviour {
    public static GravePicker Instance;

    public enum GravePickerState { Unselected, Selected };
    public GravePickerState State = GravePickerState.Unselected;

    public Grave CurrentGrave = null;

    public List<Image> BodyPartImage;

    private void Awake() {
        if(Instance == null) Instance = this;   
    }

    public void SelectGrave(Grave g) {
        CurrentGrave = g;
        State = GravePickerState.Selected;
    }

    public void LeavePicker() {
        State = GravePickerState.Unselected;
    }
}
