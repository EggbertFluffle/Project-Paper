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
            Image.rectTransform.position = Input.mousePosition;
        }   
    }

    public void DismissTooltip() {
        Hide();
    }

    public void RequestTooltip(BodyPartRef bodyPart) {
        CurrentBodyPart = bodyPart;
        Show();

        if(CurrentBodyPart.IsArm()) {
            Title.text = CurrentBodyPart.Name;
            Content.text = 
                "Strength: " + CurrentBodyPart.Strength + "\n" + 
                "Durability: " + CurrentBodyPart.Durability + "\n\n" +
                "Secondary Move: " + CurrentBodyPart.SecondaryAttack + "\n" + 
                CurrentBodyPart.SecondaryAttackDescription;
        } else if(CurrentBodyPart.IsLeg()) {
            Title.text = CurrentBodyPart.Name;
            Content.text = 
                "HP: " + CurrentBodyPart.HP + "\n" + 
                "Evasion: +" + (int)(CurrentBodyPart.Evasion * 100) + "%";
        }
    }
    
    public void Show() {
        IsActive = true;
        Image.enabled = true;
        Title.enabled = true;
        Content.enabled = true;
    }

    public void Hide() {
        IsActive = false;
        Image.enabled = false;
        Title.enabled = false;
        Content.enabled = false;
    }
}