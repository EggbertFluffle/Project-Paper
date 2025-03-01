using UnityEngine;
using System.Linq;

public class DebugManager : MonoBehaviour {
    public static DebugManager Instance;

    public void Awake() {
        if(Instance == null) Instance = this;
    }

    public void Load() {
        GameManager.ActiveSave.EquippedParts[0] = new BodyPartRef(GameManager.AllArms[0]);
        GameManager.ActiveSave.EquippedParts[1] = new BodyPartRef(GameManager.AllArms[0]);
        GameManager.ActiveSave.EquippedParts[2] = new BodyPartRef(GameManager.AllLegs[0]);
        GameManager.ActiveSave.EquippedParts[3] = new BodyPartRef(GameManager.AllLegs[0]);
    }

    private void Start()
    {
        GameManager.ActiveSave.Inventory.AddRange(GameManager.AllArms.Select(arm => new BodyPartRef(arm)));
        GameManager.ActiveSave.Inventory.AddRange(GameManager.AllLegs.Select(leg => new BodyPartRef(leg)));
        GameManager.ActiveSave.Inventory.AddRange(GameManager.AllArms.Select(arm => new BodyPartRef(arm)));
        GameManager.ActiveSave.Inventory.AddRange(GameManager.AllLegs.Select(leg => new BodyPartRef(leg)));
    }
}
