using System;
using UnityEngine;

using LimbType = BodyPart.LimbType;
using PassiveAbility = BodyPart.PassiveAbility;

[Serializable]
public class BodyPartRef 
{
    public class Constants
    {
        public LimbType Limb;
        public PassiveAbility Ability;

        public string Description;
        public string SpeicialMoveName;
        public Sprite GraveLimbSprite;
        public Sprite BackLimbSprite;
        public Sprite FrontLimbSprite;

        public float Evasion;
        public int Speed;

        public string ActiveName;
    }

    public string Name;

    public int Strength;
    public int Durability;

    public int HP;

    public Constants LimbConstants;

    public bool IsArm() => LimbConstants.Limb == BodyPart.LimbType.Arm;
    public bool IsLeg() => LimbConstants.Limb == BodyPart.LimbType.Leg;

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
        LimbConstants = new Constants
        {
            Description = bodyPart.Description,
            SpeicialMoveName = bodyPart.SpeicialMoveName,
            Limb = bodyPart.Limb,

            // Legs only
            Ability = bodyPart.LegAbility,
            GraveLimbSprite = bodyPart.GraveLimbSprite,
            BackLimbSprite = bodyPart.BackLimbSprite,
            FrontLimbSprite = bodyPart.FrontLimbSprite,

            Evasion = bodyPart.Evasion,
            Speed = bodyPart.Speed,
        };
    }
}
