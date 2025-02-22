using System.Collections.Generic;

public class SaveData {
    public List<BodyPartRef> Inventory;
    public List<BodyPartRef> EquippedParts;

    public SaveData() {
        Inventory = new List<BodyPartRef>();
        EquippedParts = new List<BodyPartRef>{null, null, null, null};
    }
}
