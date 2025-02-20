using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GRManager : MonoBehaviour {

    public static GRManager Instance;
    public static float NoLimbChance = 0.15f;

    public int GraveRobs = 2;
    public int LimbSelections = 2;

    public Grave[] Graves;

    public Animator[] GraveUIAnimations;

    private bool graveUIActive = false;

    private void Awake() {
        if(Instance == null) Instance = this;
    } 

    public void ShowUI() {
        foreach (Animator anim in GraveUIAnimations) {
            anim.Play("OpenPanel");
        }
    }

    public void HideUI() {
        foreach (Animator anim in GraveUIAnimations) {
            anim.Play("ClosePanel");
        }
    }

    public void HandleGraveClick(Grave g) {
        if(CameraManager.Instance.IsClosed() && GraveRobs > 0) {
            ShowUI();
            g.Rob();
            GravePicker.Instance.OpenGrave(g);
            GraveRobs--;
        } else if(GraveRobs == 0) {
            // Maybe display a text box complaining
        }
    }
}