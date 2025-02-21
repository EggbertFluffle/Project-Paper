using System;
using System.Collections.Generic;
using UnityEngine;

public class SaveData {
    public List<BodyPartRef> Inventory;

    public List<BodyPartRef> EquippedParts;

    public SaveData() {
        Inventory = new List<BodyPartRef>();
        EquippedParts = new List<BodyPartRef>();
    }
}
