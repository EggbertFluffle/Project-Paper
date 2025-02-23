using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;
using TMPro;
using System.Linq;

public class Player : MonoBehaviour {
    public static Player Instance;

    public int MaxHealth = 100;
    public int Health;
    public float TotalEvasion;

    public TextMeshProUGUI PlayerHealthCount;
    public TextMeshProUGUI PlayerHealthMax;

    public AttackButtonContainer LeftArm;
    public AttackButtonContainer RightArm;

    public SpriteRenderer[] Arms;
    public Slider PlayerHealthBar;
    public float HealthLerpSpeed = 0.1f;
    public Animator PlayerAnimator;

    public bool Flexed = false;

    public enum PlayerState { NotTurn, Wait, SelectAttack, PrimaryAttack, SecondaryAttack, Dead };
    public PlayerState State = PlayerState.SelectAttack;

    private Dictionary<string, Vector2> rightArmPositions = new Dictionary<string, Vector2>{
        ["Athlete Arm"] = new Vector2(2.63f, 1.51f),
        ["Chainsaw Arm"] = new Vector2(3.37f, 0.0f),
        ["Chicken Wing"] = new Vector2(2.94f, 0.43f),
        ["Crab Arm"] = new Vector2(2.79f, 1.72f),
        ["Gorilla Arm"] = new Vector2(3.19f, 1.44f),
        ["Human Arm"] = new Vector2(2.38f, 1.72f),
        ["Sexy Arm"] = new Vector2(2.51f, 1.66f),
        ["Pejhon"] = new Vector2(2.99f, 0.83f),
    };

    private Dictionary<string, Vector2> leftArmPositions = new Dictionary<string, Vector2>{
        ["Athlete Arm"] = new Vector2(-2.78f, 1.46f),
        ["Chainsaw Arm"] = new Vector2(-3.61f, 0.0f),
        ["Chicken Wing"] = new Vector2(-2.93f, 0.4f),
        ["Crab Arm"] = new Vector2(-2.96f, 1.64f),
        ["Gorilla Arm"] = new Vector2(-3.26f, 1.43f),
        ["Human Arm"] = new Vector2(-2.73f, 1.58f),
        ["Sexy Arm"] = new Vector2(-2.75f, 1.56f),
        ["Pejhon"] = new Vector2(-2.96f, 0.88f),
    };

    private void Awake() {
        if(Instance == null) Instance = this;
    }

    public void Update() {
        if(!ArenaUI.Instance.HasTextPrompt()) {
            switch(State) {
                case PlayerState.Wait:
                    State = PlayerState.SelectAttack;
                    break;
                case PlayerState.Dead:
                    ArenaManager.Instance.PlayerWin();
                    State = PlayerState.NotTurn;
                    break;
            }
        }
    }

    public void Start() {
        DebugManager.Instance.Load();
        SetupAttacks(GameManager.ActiveSave.EquippedParts);
        AlignArms(GameManager.ActiveSave.EquippedParts);
        SetupLegStats(GameManager.ActiveSave.EquippedParts);

        Health = MaxHealth;
        SetHealth();
    }

    public void Heal() {
        // TODO: add indicator for healing
        // Heal 50 percent of health
        Health += (int)Mathf.Floor(0.5f * MaxHealth);
        SetHealth();
    }

    public void HandlePrimaryAttack(BodyPartRef bodyPart) {
        Debug.Log("Primary move cast");
        if(Random.Range(0.0f, 1.0f) > 0.9f) {
            ArenaUI.Instance.MakeTextPrompt("Attack missed!");
        } else {
            if(!Flexed) {
                ArenaUI.Instance.MakeTextPrompt($"Used {bodyPart.PrimaryAttack}!");
                Boss.Instance.SendAttack(bodyPart.Strength + (int)Mathf.Floor(bodyPart.Strength * 0.5f));
            } else {
                ArenaUI.Instance.MakeTextPrompt($"Used strength infused {bodyPart.PrimaryAttack}!");
                Boss.Instance.SendAttack(bodyPart.Strength);
            }
        }
    }
    
    public void HandleSecondaryAttack(BodyPartRef bodyPart) {
        Debug.Log("Secondary move cast");
        if(Random.Range(0.0f, 1.0f) > 0.9f) {
            ArenaUI.Instance.MakeTextPrompt("Attack missed!");
        } else {
            ArenaUI.Instance.MakeTextPrompt(bodyPart.SecondaryAttackUse);
            switch(bodyPart.Name) {
                case "Athlete Arm":
                    Flexed = true;
                    break;
                case "Chicken Arm":
                    Boss.Instance.SendBleed(10);
                    break;
                case "Sexy Arm":
                    Health += (int)Mathf.Floor(MaxHealth * 0.3f);
                    break;
            }
        }
    }

    public void SendAttack(int damage) {
        if(UnityEngine.Random.Range(0.0f, 1.0f) < TotalEvasion) {
            ArenaUI.Instance.MakeTextPrompt("Attack evaded");
        } else {
            TakeDamage(damage);
        }
        State = PlayerState.Wait;
    }

    public void TakeDamage(int dmg) {
        Health -= dmg;
        SetHealth();
        if(Health <= 0.0f) {
            Kill();
        }
    }

    public void SetHealth() {
        // Somehow lerp this
        PlayerHealthBar.value = (float)Health / MaxHealth;
        PlayerHealthCount.text = Health.ToString();
        PlayerHealthMax.text = MaxHealth.ToString();
    }

    public void Kill() {
        ArenaManager.Instance.PlayerLose();
    }

    public void SetupAttacks(List<BodyPartRef> bodyParts) {
        LeftArm.SetArm(bodyParts[0], 0);
        RightArm.SetArm(bodyParts[1], 1);
    }

    public void AlignArms(List<BodyPartRef> bodyParts) {
        // Set arm sprites according to equipped parts
        if (bodyParts[0] != null) {
            Arms[0].sprite = bodyParts[0].BackLimbSprite;
            Arms[0].transform.localPosition = new Vector3(
                leftArmPositions[bodyParts[0].Name].x,
                leftArmPositions[bodyParts[0].Name].y,
                0
            );
        }

        if (bodyParts[1] != null) {
            Arms[1].sprite = bodyParts[1].BackLimbSprite;
            Arms[1].transform.localPosition = new Vector3(
                rightArmPositions[bodyParts[1].Name].x,
                rightArmPositions[bodyParts[1].Name].y,
                0
            );
        }
    }

    public void SetupLegStats(List<BodyPartRef> bodyParts) {
        MaxHealth = 100;

        if (bodyParts[2] != null) {
            MaxHealth += bodyParts[2].HP;
            TotalEvasion += bodyParts[2].Evasion;
        }

        if(bodyParts[3] != null) {
            MaxHealth += bodyParts[3].HP;
            TotalEvasion += bodyParts[3].Evasion;
        }
    }
}
