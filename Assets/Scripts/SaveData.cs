using System;
using System.Collections.Generic;
using UnityEngine;

public class SaveData
{
    [Serializable]
    public class BodyPartRef
    {
        public BodyPartRef(BodyPart bodyPart)
        {
            Name = bodyPart.Name;
            LimbType = bodyPart.Limb;
        }

        public string Name;
        public int Durability;

        public BodyPart.LimbType LimbType;
    }

    public List<BodyPartRef> BodyPartInventory;

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
            _ => -1,
        };

        Checkpoints[checkpointValue] = value;
    }

    public SaveData()
    {
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
