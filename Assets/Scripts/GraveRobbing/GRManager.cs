using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GRManager : SceneLoader {
    public static GRManager Instance;
    public static float NoLimbChance = 0.15f;
    public GameObject TextPrompt;
    public Canvas Canvas;
    public TextMeshProUGUI GraveRobsLeft;
    public TextMeshProUGUI GraveRobMax;

    public Button LabButton;

    public int GraveRobs;

    [HideInInspector]
    public int PickedLimbs;
    public int LimbSelections = 2;

    public Grave[] Graves;
    public Animator[] GraveUIAnimations;

    private void Awake() {
        if(Instance == null) 
            Instance = this;

        for (int i = 0; i < Graves.Length; i++)
            Graves[i].GraveIndex = i;
    }

    public void Start() {
        GraveRobs = GameManager.ActiveSave.CurrentBoss == 0 ? 4 : 2;
        GraveRobMax.text = GraveRobs.ToString();
        GraveRobsLeft.text = GraveRobs.ToString();

        PickedLimbs = GraveRobs;
    }

    public void ShowUI() {
        foreach (Animator anim in GraveUIAnimations) 
            anim.Play("OpenPanel");
        
    }

    public void HideUI() {
        foreach (Animator anim in GraveUIAnimations) 
            anim.Play("ClosePanel");
    }

    public void HandleGraveClick(Grave g) {
        if(CameraManager.Instance.IsClosed() && GraveRobs > 0) {
            ShowUI();
            g.Rob();
            GravePicker.Instance.OpenGrave(g);
            GraveRobs--;
            GraveRobsLeft.text = GraveRobs.ToString();
        } else if(GraveRobs == 0) {
            GameObject txt = Instantiate(TextPrompt, Canvas.transform);
            txt.GetComponent<TextPrompt>().contents = "You can't rob anymore graves!";
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