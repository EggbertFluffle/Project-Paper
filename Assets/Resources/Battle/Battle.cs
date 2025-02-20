using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[CreateAssetMenu(fileName = "Create Arena Battle", menuName = "Battle")]
public class Battle : ScriptableObject {
    public string Name;

    public bool BossFight = true;

    [Range(0, 100), Header("Difficulty")]
    public int difficulty = 0;

    public void GenerateLimbs() {
        List<BodyPart> AllArms = Resources.LoadAll<BodyPart>("BodyParts/Arms").ToList();
        List<BodyPart> AllLegs = Resources.LoadAll<BodyPart>("BodyParts/Legs").ToList();


    }
}
