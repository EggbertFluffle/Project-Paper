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
    public List<GravePickerButton> LimbButtons;

    private void Awake() {
        if(Instance == null) Instance = this;   
    }

    public void OpenGrave(Grave g) {
        CurrentGrave = g;

        // Go though grave contents and change grave picker UI
        for(int i = 0; i < 4; i++) {
            LimbButtons[i].UseBodyPart(g.BodyParts[i]);
        }
    }

    public void HandleLimbPick(BodyPartRef _bodyPart) {
        // GameManager.ActiveSave.Inventory.Add(_bodyPart);
        ClosePicker();
    }

    public void ClosePicker() {
        if(!CameraManager.Instance.IsTransitioning()) {
            GRManager.Instance.HideUI();
        }
    }
}
