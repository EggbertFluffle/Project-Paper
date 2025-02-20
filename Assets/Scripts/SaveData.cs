using System;
using System.Collections.Generic;
using UnityEngine;

public class SaveData
{
    public List<BodyPartRef> Inventory;

    public List<BodyPartRef> EquippedParts;

    public List<bool> Checkpoints;

    public void SetCheckpoint(string checkpointName, bool value)
    {
        int checkpointValue = checkpointName switch 
        {
            "Boss 1" => 0,
            "Boss 2" => 1,
            "Boss 3" => 2,
            "Boss 4" => 3,
            "Boss 5" => 4,
            _ => -1,
        };

        Checkpoints[checkpointValue] = value;
    }

    public SaveData()
    {
        Inventory = new List<BodyPartRef>();
        EquippedParts = new List<BodyPartRef>();

        Checkpoints = new List<bool>
        {
            false,
            false,
            false,
            false,
            false
        };
    }
}
