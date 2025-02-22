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
        BodyPart = bodyPart;
        ArmIndex = armIndex;
        UpdateDurability(0);

        PrimaryAttackName.text = BodyPart.PrimaryAttack;
        PrimaryAttackCost.text = $"-{BodyPart.PrimaryAttackDurabilityCost}";

        if(bodyPart.HasSecondaryAttack) {
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

    }

    public void HandleSecondary() {

    }

    public void BreakArm() {
        // Play some arm breaking audio
        // Probably display a text prompt
        GameManager.ActiveSave.EquippedParts[ArmIndex] = null;
    }
}
