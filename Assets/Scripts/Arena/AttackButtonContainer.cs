using TMPro;
using UnityEngine;

public class AttackButtonContainer : MonoBehaviour {
    public BodyPartRef BodyPart;
    public int ArmIndex;

    public TextMeshProUGUI ArmInfo;

    public TextMeshProUGUI PrimaryAttackName;
    public TextMeshProUGUI PrimaryAttackCost;

    public GameObject SecondaryAttackButton;
    public TextMeshProUGUI SecondaryAttackName;
    public TextMeshProUGUI SecondaryAttackCost;

    public void SetArm(BodyPartRef bodyPart, int armIndex) {
        gameObject.SetActive(bodyPart != null);
        if(bodyPart == null) return;
        
        BodyPart = bodyPart;
        ArmIndex = armIndex;
        UpdateDurability(0);

        PrimaryAttackName.text = BodyPart.PrimaryAttack;
        PrimaryAttackCost.text = $"-{BodyPart.PrimaryAttackDurabilityCost}";

        if(BodyPart.HasSecondaryAttack) {
            SecondaryAttackName.text = BodyPart.SecondaryAttack;
            SecondaryAttackCost.text = $"-{BodyPart.SecondaryAttackDurabilityCost}";
        } else {
            SecondaryAttackButton.SetActive(false);
        }
    }

    public void UpdateDurability(int cost) {
        BodyPart.Durability -= cost;
        ArmInfo.text = $"{BodyPart.Name}: {BodyPart.Durability}";
    }

    public void HandlePrimary() {
        AudioManager.PlaySFX("Button Press");
        BodyPart.Durability -= BodyPart.PrimaryAttackDurabilityCost;
        UpdateArmInfo();
        if(BodyPart.Durability <= 0) {
            GameManager.ActiveSave.EquippedParts[0] = null;
            BodyPart = null;
            SetArm(null, 0);
        }
        Player.Instance.HandlePrimaryAttack(BodyPart);
    }

    public void HandleSecondary() {
        AudioManager.PlaySFX("Button Press");
        BodyPart.Durability -= BodyPart.SecondaryAttackDurabilityCost;
        UpdateArmInfo();
        if(BodyPart.Durability <= 0) {
            GameManager.ActiveSave.EquippedParts[1] = null;
            BodyPart = null;
            SetArm(null, 0);
        }
        Player.Instance.HandleSecondaryAttack(BodyPart);
    }

    public void UpdateArmInfo() {
        PrimaryAttackName.text = BodyPart.PrimaryAttack;
        PrimaryAttackCost.text = $"-{BodyPart.PrimaryAttackDurabilityCost}";

        if(BodyPart.HasSecondaryAttack) {
            SecondaryAttackName.text = BodyPart.SecondaryAttack;
            SecondaryAttackCost.text = $"-{BodyPart.SecondaryAttackDurabilityCost}";
        }
    }

    public void BreakArm() {
        // Play some arm breaking audio
        // Probably display a text prompt
        GameManager.ActiveSave.EquippedParts[ArmIndex] = null;
    }
}
