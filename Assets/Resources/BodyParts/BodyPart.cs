using UnityEngine;

[CreateAssetMenu(fileName = "New Body Part", menuName = "Body Part")]
public class BodyPart : ScriptableObject {
    public LimbType Limb;
    public Rarity LimbRarity;
    public string Name;
    
    [TextArea]
    public string Description;
    public Sprite GraveLimbSprite;
    public Sprite BackLimbSprite;
    public Sprite FrontLimbSprite;
    
    // HP of the limb. When this depletes, the arm is no longer usable.
    public int Durability;

    // Deviation of the HP value.
    [Range(0, 10)]
    public int HPDeviation;

    [Range(0, 10)]
    public int AttackDeviation;

    // HP that the limb gives to the player
    public int HP;

    [Range(50, 100), Header("Arms Only")]
    public int Accuracy;
    public int Strength;
    // Arms should have an attack

    //public string PrimaryAttack;
    //public string SecondaryAttack;

    [Range(0, 100), Header("Legs Only")]
    public int Evasion;
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
