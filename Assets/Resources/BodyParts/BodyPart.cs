using UnityEngine;

[CreateAssetMenu(fileName = "New Body Part", menuName = "Body Part")]
public class BodyPart : ScriptableObject {
    public LimbType Limb;
    public string Name;
    
    [TextArea]
    public string Description;
    public Sprite GraveLimbSprite;
    public Sprite BackLimbSprite;
    public Sprite FrontLimbSprite;
    public int Durability;

    [Range(50, 100), Header("Arms Only")]
    public int Accuracy;
    public int Strength;

    [Range(0, 100), Header("Legs Only")]
    public int Evasion;

    public enum LimbType {
         Arm, Leg
    }
}
