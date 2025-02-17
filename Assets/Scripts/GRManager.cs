using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GRManager : MonoBehaviour {
    public float NoLimbChance = 0.15f;

    public int GraveRobs = 2;
    public int LimbSelections = 2;

    public List<BodyPart> AllArms;
    public List<BodyPart> AllLegs;

    public Grave[] Graves;

    public static GRManager Instance;

    private void Awake() {
        if(Instance == null) Instance = this;

        AllArms = Resources.LoadAll<BodyPart>("BodyParts/Arms").ToList();
        AllLegs = Resources.LoadAll<BodyPart>("BodyParts/Legs").ToList();
    } 

    private void Start() {
        GenerateGraves();
    }

    private void GenerateGraves() {
        foreach(Grave g in Graves) {
            for(int i = 0; i < 4; i++) {
                if(Random.Range(0.0f, 1.0f) < NoLimbChance) {
                    g.SetBodyPart(i, null);
                } else {
                    BodyPart b = i < 2 ? 
                        AllArms[Random.Range(0, AllArms.Count)] :
                        AllLegs[Random.Range(0, AllLegs.Count)];
                    g.SetBodyPart(i, b);
                }
            }
        }
    }

    public void HandleGraveClick(Grave grave) {
        if(GraveRobs == 0) {
            Debug.Log("Cannot rob anymore graves!");
        } else {
            GraveRobs--;
            grave.Rob();
        }
    }
}