using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LabManager : SceneLoader {
    public GameObject InventorySlot;
    public GameObject DraggableArm;
    public GameObject DraggableLeg;

    public Transform ArmContainer;
    public Transform LegContainer;

    public InventorySlot[] BodySlots;

    public void Start() 
    {
        // Seperate each inventory limb into their own inventories

        foreach (BodyPartRef bp in GameManager.ActiveSave.Inventory) 
        {
            if (bp == null) 
                continue;

            if (bp.IsArm()) 
                AddPartToContainer(bp, ArmContainer);
            else if (bp.IsLeg()) 
                AddPartToContainer(bp, LegContainer);
        }

        for (int i = 0; i < GameManager.ActiveSave.EquippedParts.Count; i++) 
        {
            BodyPartRef bp = GameManager.ActiveSave.EquippedParts[i];
            if (bp == null) 
                continue;

            if (i < 2) {
                GameObject newArm = Instantiate(DraggableArm, BodySlots[i].transform);
                DraggableItem draggableItem = newArm.GetComponent<DraggableItem>();
                draggableItem.isArm = true;
                draggableItem.SetLimb(bp);

                draggableItem.Zero(BodySlots[i].Reversed);
                draggableItem.SetScale(0.28f);
            } 
            else 
            {
                GameObject newLeg = Instantiate(DraggableLeg, BodySlots[i].transform);
                DraggableItem draggableItem = newLeg.GetComponent<DraggableItem>();
                draggableItem.isArm = false;
                draggableItem.SetLimb(bp);

                draggableItem.Zero(BodySlots[i].Reversed);
                draggableItem.SetScale(0.26f);
            }
        }
    }

    public void AddPartToContainer(BodyPartRef bodyPart, Transform Container) {
        if(bodyPart != null) {
            // Create new inventory slot to hold the limb
            GameObject newSlot = Instantiate(InventorySlot);
            newSlot.transform.SetParent(Container);
            newSlot.transform.localScale = Vector3.one;
            newSlot.GetComponent<InventorySlot>().armSlot = bodyPart.IsArm();

            // Add the limb to the inventory slot
            GameObject newLimb = Instantiate(bodyPart.IsArm() ? DraggableArm : DraggableLeg, newSlot.transform);
            DraggableItem draggableItem = newLimb.GetComponent<DraggableItem>();
            draggableItem.SetLimb(bodyPart);
        }
    }

    public void StartFight() {
        GameManager.ActiveSave.Inventory.Clear();
        GameManager.ActiveSave.EquippedParts = new List<BodyPartRef>{null, null, null, null};
        
        for(int i = 0; i < BodySlots.Length; i++) {
            // sets equipped limbs
            Debug.Log("accessing index: " + i);
            GameManager.ActiveSave.EquippedParts[i] = BodySlots[i].transform.childCount == 0 ? null : BodySlots[i].GetComponentInChildren<DraggableItem>().GetBodyPart();
        }

        foreach(Transform armInventorySlot in ArmContainer) {
            if (armInventorySlot.childCount == 0)
                continue;

            Transform draggable = armInventorySlot.GetChild(0);

            if(draggable == null) 
                continue;

            GameManager.ActiveSave.Inventory.Add(draggable.GetComponent<DraggableItem>().GetBodyPart());
        }

        foreach(Transform armInventorySlot in LegContainer.transform) {
            if (armInventorySlot.childCount == 0)
                continue;

            Transform draggable = armInventorySlot.GetChild(0);

            if(draggable == null) 
                continue;
            GameManager.ActiveSave.Inventory.Add(draggable.GetComponent<DraggableItem>().GetBodyPart());
        }
        AudioManager.PlaySFX("Button Press");
        AudioManager.StopMusic(1);

        GetComponent<Animator>().enabled = true;
        Invoke(nameof(LoadFightScene), 3.5f);
    }

    private void LoadFightScene() => LoadScene("Battle");
    private void StopLightning() => AudioManager.StopSFX();

    public void PlayLightning()
    {
        AudioManager.PlaySFX("Electric Shock");
        Invoke(nameof(StopLightning), 2.4f);
    }
}
