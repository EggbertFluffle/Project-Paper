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
        Debug.Log("Inventory size: " + GameManager.ActiveSave.Inventory.Count);

        // Seperate each inventory limb into their own inventories
        foreach(BodyPartRef bp in GameManager.ActiveSave.Inventory) {
            if(bp == null) continue;
            if(bp.IsArm()) {
                Debug.Log("Add arm");
                AddPartToContainer(bp, ArmContainer);
            } else if(bp.IsLeg()) {
                Debug.Log("Add leg");
                AddPartToContainer(bp, LegContainer);
            }
        }

        for (int i = 0; i < BodySlots.Length; i++) {
            if (i < 2) {
                GameObject newArm = Instantiate(DraggableArm, BodySlots[i].transform);
                newArm.GetComponent<DraggableItem>().SetLimb(null);
            } else {
                GameObject newLeg = Instantiate(DraggableLeg, BodySlots[i].transform);
                newLeg.GetComponent<DraggableItem>().SetLimb(null);
            }
        }
    }

    public void AddPartToContainer(BodyPartRef bodyPart, Transform Container) {
        if(bodyPart != null) {
            GameObject newSlot = Instantiate(InventorySlot);
            Transform newSlotTransform = newSlot.transform;
            newSlotTransform.SetParent(Container);
            newSlot.GetComponent<InventorySlot>().armSlot = bodyPart.IsArm();

            GameObject newLimb = Instantiate(
                bodyPart.IsArm() ?  DraggableArm : DraggableLeg, 
                newSlotTransform);
            newLimb.GetComponent<DraggableItem>().SetLimb(bodyPart);
        }
    }

    public void StartFight() {
        for(int i = 0; i < BodySlots.Length; i++) {
            GameManager.ActiveSave.EquippedParts[i] = 
                BodySlots[i].transform.GetChild(0).GetComponent<DraggableItem>().GetBodyPart();
        }

        // Add all the other slots to the save data
    }
}
