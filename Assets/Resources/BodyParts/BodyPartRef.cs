using System;
using UnityEngine;

using LimbType = BodyPart.LimbType;
using PassiveAbility = BodyPart.PassiveAbility;

[Serializable]
public class BodyPartRef 
{
    // Constants ---------------------------------------------------

    public PassiveAbility Ability { get; private set; }

    public string Description { get; private set; }
    public string SpecialMoveName { get; private set; }
    public Sprite GraveLimbSprite { get; private set; }
    public Sprite BackLimbSprite { get; private set; }
    public Sprite FrontLimbSprite { get; private set; }

    public float Evasion { get; private set; }
    public int Speed { get; private set; }

    public string ActiveName;

    // --------------------------------------------------------------

    public string Name;
    public LimbType Limb;

    public int Strength;
    public int Durability;

    public int HP;

    public bool IsArm() => Limb == LimbType.Arm;
    public bool IsLeg() => Limb == LimbType.Leg;

    public BodyPartRef(BodyPart bodyPart) {
        Name = bodyPart.Name;

        // I think this is changing based of rarity
        HP = bodyPart.HP + UnityEngine.Random.Range(-bodyPart.HPDeviation, bodyPart.HPDeviation);
        Strength = bodyPart.Strength + UnityEngine.Random.Range(-bodyPart.AttackDeviation, bodyPart.AttackDeviation);
        Durability = bodyPart.Durability + UnityEngine.Random.Range(-bodyPart.DurabilityDeviation, bodyPart.DurabilityDeviation);

        SetConstants(bodyPart);
    }

    public void SetConstants(BodyPart bodyPart)
    {
        Description = bodyPart.Description;
        SpecialMoveName = bodyPart.SpecialMoveName;

        // Legs only
        Ability = bodyPart.LegAbility;
        GraveLimbSprite = bodyPart.GraveLimbSprite;
        BackLimbSprite = bodyPart.BackLimbSprite;
        FrontLimbSprite = bodyPart.FrontLimbSprite;

        Evasion = bodyPart.Evasion;
        Speed = bodyPart.Speed;
    }
}
