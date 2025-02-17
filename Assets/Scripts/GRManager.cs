using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GRManager : MonoBehaviour {

    public static GRManager Instance;
    
    public float NoLimbChance = 0.15f;

    public int GraveRobs = 2;
    public int LimbSelections = 2;

    public List<BodyPart> AllArms;
    public List<BodyPart> AllLegs;

    public Grave[] Graves;

    public Animator[] GraveUIAnimations;

    private bool graveUIActive = false;

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


    public void ShowUI()
    {
        foreach (Animator anim in GraveUIAnimations) 
        {
            anim.Play(graveUIActive ? "ClosePanel" : "OpenPanel");
        }

        graveUIActive = !graveUIActive;
    }

    public void HandleGraveClick(Grave g) 
    {
        ShowUI();

        if(g.State == Grave.GraveState.Robbed) {
            g.Rob();
        } else {
            if(GraveRobs == 0) {
                g.DenyGraveRob();
            } else {
                GraveRobs--;
                g.Rob();
            }
        }
    }
}