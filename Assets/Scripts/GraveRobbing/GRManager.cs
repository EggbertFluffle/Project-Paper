using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

using Rarity = BodyPart.Rarity;

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

    private int mythicThreshold = 0;
    private int rareThreshold = 5;

    private void Awake() {
        if(Instance == null) 
            Instance = this;

        for (int i = 0; i < Graves.Length; i++)
            Graves[i].GraveIndex = i;
    }

    public void Start() {
        Cursor.lockState = CursorLockMode.None;
        GraveRobs = GameManager.ActiveSave.CurrentBoss == 0 ? 4 : 2;
        GraveRobMax.text = GraveRobs.ToString();
        GraveRobsLeft.text = GraveRobs.ToString();

        AudioManager.PlayMusic("Graveyard");

        PickedLimbs = GraveRobs;

        AudioManager.PlayMusic("Main Menu");
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
            // GameObject txt = Instantiate(TextPrompt, Canvas.transform);
            // txt.GetComponent<TextPrompt>().contents = "You can't rob anymore graves!";
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

    public bool InRangeInclusive(int target, int min, int max) => target >= min && target <= max;

    public BodyPartRef RandomArmWithRarity()
    {
        List<BodyPart> bodyPartRange = new List<BodyPart>();

        // Pity: If by the Last boss you dont have a mythic arm, you are guarenteed one.
        if (GameManager.ActiveSave.CurrentBoss == 4 && !GameManager.ActiveSave.MythicArmPulled)
        {
            bodyPartRange.AddRange(GameManager.AllArms.Where(bp => bp.LimbRarity == Rarity.Mythic));
            GameManager.ActiveSave.MythicArmPulled = true;

            return new BodyPartRef(bodyPartRange[Random.Range(0, bodyPartRange.Count)]);
        }

        int rng = Random.Range(0, 101);
        if (rng == mythicThreshold)
        {
            bodyPartRange.AddRange(GameManager.AllArms.Where(bp => bp.LimbRarity == Rarity.Mythic));
            GameManager.ActiveSave.MythicArmPulled = true;
        }
        else if (InRangeInclusive(rng, mythicThreshold + 1, mythicThreshold + rareThreshold))
        {
            bodyPartRange.AddRange(GameManager.AllArms.Where(bp => bp.LimbRarity == Rarity.Rare));
        }
        else if (InRangeInclusive(rng, mythicThreshold + rareThreshold + 1, mythicThreshold + rareThreshold + 15))
        {
            bodyPartRange.AddRange(GameManager.AllArms.Where(bp => bp.LimbRarity == Rarity.Uncommon));
        }
        else
        {
            bodyPartRange.AddRange(GameManager.AllArms.Where(bp => bp.LimbRarity == Rarity.Common));
        }

        return new BodyPartRef(bodyPartRange[Random.Range(0, bodyPartRange.Count)]);
    }

    public BodyPartRef RandomLegWithRarity()
    {
        int rng = Random.Range(0, 101);
        List<BodyPart> bodyPartRange = new List<BodyPart>();

        if (InRangeInclusive(rng, 0, rareThreshold))
        {
            bodyPartRange.AddRange(GameManager.AllLegs.Where(bp => bp.LimbRarity == Rarity.Rare));
        }
        else if (InRangeInclusive(rng, rareThreshold + 1, rareThreshold + 15))
        {
            bodyPartRange.AddRange(GameManager.AllLegs.Where(bp => bp.LimbRarity == Rarity.Uncommon));
        }
        else
        {
            bodyPartRange.AddRange(GameManager.AllLegs.Where(bp => bp.LimbRarity == Rarity.Common));
        }

        return new BodyPartRef(bodyPartRange[Random.Range(0, bodyPartRange.Count)]);
    }
}