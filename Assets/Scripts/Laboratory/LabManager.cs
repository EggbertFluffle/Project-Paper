using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class LabManager : MonoBehaviour {
    public GameObject InventorySlot;
    public GameObject DraggableArm;
    public GameObject DraggableLeg;

    public Transform ArmContainer;
    public Transform LegContainer;

    public InventorySlot[] BodySlots;

    public void Start() {
        // Seperate each inventory limb into their own inventories
        foreach(BodyPartRef bp in GameManager.ActiveSave.Inventory) {
            if(bp == null) continue;
            Debug.Log(bp);
            if(bp.IsArm()) {
                // AddToArmContainer(bp);
            } else if(bp.IsLeg()) {
                // AddToLegContainer(bp);
            }
        }

        for (int i = 0; i < 4; i++) {
            BodyPartRef bodyPart = GameManager.ActiveSave.EquippedParts[i];

            if (bodyPart.LimbConstants.Limb == BodyPart.LimbType.Arm) {
                GameObject newArm = Instantiate(DraggableArm, BodySlots[i].transform);
                newArm.GetComponent<DraggableItem>().SetLimb(bodyPart);
            } else if (bodyPart.LimbConstants.Limb == BodyPart.LimbType.Leg) {
                GameObject newLeg = Instantiate(DraggableLeg, BodySlots[i].transform);
                newLeg.GetComponent<DraggableItem>().SetLimb(bodyPart);
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

    public void StartFight() {
        for(int i = 0; i < BodySlots.Length; i++) {
            GameManager.ActiveSave.EquippedParts[i] = 
                BodySlots[i].transform.GetChild(0).GetComponent<DraggableItem>().GetBodyPart();
        }

        // Add all the other slots to the save data
    }
}
