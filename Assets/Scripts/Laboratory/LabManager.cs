using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LabManager : MonoBehaviour {
    public GameObject InventorySlot;
    public GameObject DraggableArm;
    public GameObject DraggableLeg;

    public Transform ArmContainer;
    public Transform LegContainer;

    public void Start() {
        // Seperate each inventory limb into their own inventories
        foreach(BodyPartRef bp in GameManager.ActiveSave.Inventory) {
            if(bp.LimbConstants.Limb == BodyPart.LimbType.Arm) {
                AddToArmContainer(bp);
            } else {
                // AddToLegContainer(bp);
            }
        }
    }

    public void AddToArmContainer(BodyPartRef bodyPart) {
        GameObject newSlot = Instantiate(InventorySlot);
        Transform newSlotTransform = newSlot.GetComponent<Transform>();
        newSlotTransform.SetParent(ArmContainer);
        newSlot.GetComponent<GridLayoutGroup>().cellSize = new Vector2(200, 400);
        newSlot.GetComponent<InventorySlot>().armSlot = true;

        GameObject newArm = Instantiate(DraggableArm);
        newArm.GetComponent<Transform>().SetParent(newSlotTransform);
        newArm.GetComponent<DraggableItem>().SetLimb(bodyPart);
    }

    public void AddToLegContainer(BodyPartRef bodyPart) {
        GameObject newSlot = Instantiate(InventorySlot);
        Transform newSlotTransform = newSlot.GetComponent<Transform>();
        newSlotTransform.SetParent(LegContainer);
        newSlot.GetComponent<GridLayoutGroup>().cellSize = new Vector2(200, 600);
        newSlot.GetComponent<InventorySlot>().armSlot = false;

        GameObject newLeg = Instantiate(DraggableLeg);
        newLeg.GetComponent<Transform>().SetParent(newSlotTransform);
        newLeg.GetComponent<DraggableItem>().SetLimb(bodyPart);
    }
}
