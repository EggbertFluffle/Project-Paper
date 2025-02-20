using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LimbToolTip : MonoBehaviour {
    public static LimbToolTip Instance;

    public bool IsActive;
    public BodyPartRef CurrentBodyPart;
    public Image Image;
    public TextMeshProUGUI Title;
    public TextMeshProUGUI Content;

    public void Awake() {
        if(Instance == null) Instance = this;
    }

    public void LateUpdate() {
        if(IsActive) {
            // Follow the mouse with some stuff
        }   
    }

    public void DismissTooltip() {
        IsActive = false;
        Image.enabled = false;
    }

    public void RequestTooltip(BodyPartRef bodyPart) {
        IsActive = true;
        Image.enabled = true;
        CurrentBodyPart = bodyPart;

        if(CurrentBodyPart.LimbConstants.Limb == BodyPart.LimbType.Arm) {
            Title.text = CurrentBodyPart.Name + " + Arm\n";
        } else if(CurrentBodyPart.LimbConstants.Limb == BodyPart.LimbType.Leg) {
            Title.text = CurrentBodyPart.Name + " + Leg\n";
        }
    }
}