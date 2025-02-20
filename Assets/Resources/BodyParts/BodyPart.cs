using UnityEngine;

[CreateAssetMenu(fileName = "New Body Part", menuName = "Body Part")]
public class BodyPart : ScriptableObject {
    public LimbType Limb;
    public Rarity LimbRarity;
    public string Name;
    
    [TextArea]
    public string Description;
    [TextArea]
    public string SpeicialMoveName;
    public Sprite GraveLimbSprite;
    public Sprite BackLimbSprite;
    public Sprite FrontLimbSprite;
    
    public int Durability;
    public int DurabilityDeviation;

    public int Strength;
    public int AttackDeviation;
    

    public int HP;
    public int HPDeviation;

    //public string PrimaryAttack;
    //public string SecondaryAttack;

    public float Evasion;
    public int Speed;
    public PassiveAbility LegAbility;

    // Legs should have passive stat

    public enum PassiveAbility
    {
        None, MoreHP, DamageReduction, MoreEvasion, 
    }

    public enum Rarity
    {
        Common, Uncommon, Rare
    }

    public enum LimbType 
    {
         Arm, Leg
    }
}
