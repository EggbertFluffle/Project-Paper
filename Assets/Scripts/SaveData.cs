using System.Collections.Generic;

public class SaveData {

    public int CurrentBoss;

    public bool MythicArmPulled;

    public List<BodyPartRef> Inventory;
    public List<BodyPartRef> EquippedParts;

    public SaveData() {
        Inventory = new List<BodyPartRef>();
        EquippedParts = new List<BodyPartRef>{null, null, null, null};
    }
}
