using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GRManager : SceneLoader {
    public static GRManager Instance;
    public static float NoLimbChance = 0.15f;

    public Button LabButton;

    public int GraveRobs = 2;
    public int PickedLimbs;
    public int LimbSelections = 2;

    public Grave[] Graves;
    public Animator[] GraveUIAnimations;

    private void Awake() {
        if(Instance == null) Instance = this;
    }

    public void Start() {
        PickedLimbs = GraveRobs;
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

    public void HandleLimbPick(BodyPartRef _bodyPart) {
        GameManager.ActiveSave.Inventory.Add(_bodyPart);
        PickedLimbs--;
        if(PickedLimbs == 0) {
            ShowLabButton();
        }
    }

    public void ShowLabButton() {
        LabButton.enabled = true;
        LabButton.gameObject.GetComponent<Image>().enabled = true;
        LabButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().enabled = true;
    }
}