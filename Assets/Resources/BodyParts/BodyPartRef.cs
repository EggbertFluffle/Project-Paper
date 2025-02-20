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
        public Sprite GraveLimbSprite;
        public Sprite BackLimbSprite;
        public Sprite FrontLimbSprite;

        public int Accuracy;
        public int Evasion;
        public int Speed;

        public string ActiveName;
    }

    public string Name;

    public int HP;

    public int Strength;

    public Constants LimbConstants;

    public BodyPartRef(BodyPart bodyPart) {
        Name = bodyPart.Name;

        // I think this is changing based of rarity
        HP = bodyPart.HP + UnityEngine.Random.Range(-bodyPart.HPDeviation, bodyPart.HPDeviation);
        Strength = bodyPart.Strength + UnityEngine.Random.Range(-bodyPart.AttackDeviation, bodyPart.AttackDeviation);

        SetConstants(bodyPart);
    }

    public void SetConstants(BodyPart bodyPart)
    {
        LimbConstants = new Constants
        {
            Description = bodyPart.Description,
            Limb = bodyPart.Limb,

            // Legs only
            Ability = bodyPart.LegAbility,
            GraveLimbSprite = bodyPart.GraveLimbSprite,
            BackLimbSprite = bodyPart.BackLimbSprite,
            FrontLimbSprite = bodyPart.FrontLimbSprite,

            Accuracy = bodyPart.Accuracy,
            Evasion = bodyPart.Evasion,
            Speed = bodyPart.Speed,
        };

    }
}
