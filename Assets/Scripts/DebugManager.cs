using UnityEngine;
using System.Linq;

public class DebugManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        Debug.Log((GameManager.ActiveSave == null));

        GameManager.ActiveSave.Inventory.AddRange(GameManager.AllArms.Select(arm => new BodyPartRef(arm)));
        GameManager.ActiveSave.Inventory.AddRange(GameManager.AllLegs.Select(leg => new BodyPartRef(leg)));
        GameManager.ActiveSave.Inventory.AddRange(GameManager.AllArms.Select(arm => new BodyPartRef(arm)));
        GameManager.ActiveSave.Inventory.AddRange(GameManager.AllLegs.Select(leg => new BodyPartRef(leg)));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
