using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;
using TMPro;
using System.Collections;

using GameState = ArenaManager.GameState;

public class Player : MonoBehaviour {
    public static Player Instance;

    public int MaxHealth = 100;

    private float health;
    public float Health {
        get => health;
        set {
            if (value > MaxHealth) {
                health = MaxHealth;
            } else if (value < 0) {
                health = 0;
            } else {
                health = value;
            }
        }
    }

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
    public bool Charged = false;

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

    private void OnEnable() {
        ArenaManager.OnBossWin.AddListener(OnPlayerLose);
        ArenaManager.OnPlayerWin.AddListener(OnPlayerWin);
        ArenaManager.OnPlayerTurn.AddListener(HasArms);
    }

    private void OnDisable() {
        ArenaManager.OnBossWin.RemoveListener(OnPlayerLose);
        ArenaManager.OnPlayerWin.RemoveListener(OnPlayerWin);
        ArenaManager.OnPlayerTurn.RemoveListener(HasArms);
    }

    public void Start() {
        SetupAttacks(GameManager.ActiveSave.EquippedParts);
        AlignArms(GameManager.ActiveSave.EquippedParts);
        SetupLegStats(GameManager.ActiveSave.EquippedParts);

        Health = MaxHealth;
        SetHealth(0);
    }

    public void Heal() {
        // TODO: add indicator for healing
        // Heal 50 percent of health
        SetHealth((int)Mathf.Floor(0.5f * MaxHealth));
    }

    public void HandlePrimaryAttack(BodyPartRef bodyPart) {
        TextPrompt prompt;

        if(Random.Range(0.0f, 1.0f) > 0.9f) {
            prompt = ArenaUI.Instance.MakeTextPrompt("Attack missed!");
        } else {
            AudioManager.PlaySFX($"Hurt{Random.Range(1, 4)}");

            if (Flexed) {
                prompt = ArenaUI.Instance.MakeTextPrompt($"Used strength infused {bodyPart.PrimaryAttack}!");
                Boss.Instance.SendAttack(bodyPart.Strength + (int)Mathf.Floor(bodyPart.Strength * 0.5f));
                Flexed = false;
            } else if (Charged) {
                prompt = ArenaUI.Instance.MakeTextPrompt($"RIP AND TEAR!!!! {bodyPart.PrimaryAttack}!!!!");
                Boss.Instance.SendAttack(bodyPart.Strength + (int)Mathf.Floor(bodyPart.Strength * 0.9f));
                Charged = false;
            } else {
                prompt = ArenaUI.Instance.MakeTextPrompt($"Used {bodyPart.PrimaryAttack}!");
                Boss.Instance.SendAttack(bodyPart.Strength);
            }
        }

        prompt.OnClicked.AddListener(() => ArenaManager.CurrentGameState = GameState.BossTurn);
    }
    
    public void HandleSecondaryAttack(BodyPartRef bodyPart) {
        TextPrompt prompt;

        if(Random.Range(0.0f, 1.0f) > 0.9f) {
            prompt = ArenaUI.Instance.MakeTextPrompt("Attack missed!");
        } else {
            switch (bodyPart.Name) {
                case "Athlete Arm":
                    Flexed = true;
                    break;
                case "Chainsaw Arm":
                    SetHealth((int)(MaxHealth * 0.8f < 1 ? 1 : MaxHealth * 0.2f));
                    Charged = true;
                    break;
                case "Sexy Arm":
                    SetHealth(-(int)Mathf.Floor(MaxHealth * 0.3f));
                    break;
                case "Chicken Wing":
                    Boss.Instance.SendBleed(10);
                    break;
            }
            prompt = ArenaUI.Instance.MakeTextPrompt(bodyPart.SecondaryAttackUse);
        }
        prompt.OnClicked.AddListener(() => ArenaManager.CurrentGameState = GameState.BossTurn);
    }

    public void SendAttack(int damage, string attacker) {
        TextPrompt prompt;
        if(Random.Range(0.0f, 1.0f) < TotalEvasion) {
            prompt = ArenaUI.Instance.MakeTextPrompt("Attack evaded!");
        } else {
            prompt = ArenaUI.Instance.MakeTextPrompt($"{attacker} attacked!");
            TakeDamage(damage);
        }

        prompt.OnClicked.AddListener(() => ArenaManager.CurrentGameState = ArenaManager.GameState.PlayerTurn);
    }

    public void TakeDamage(int dmg) 
    {
        if(Health - dmg <= 0.0f) 
        {
            TextPrompt prompt = ArenaUI.Instance.MakeTextPrompt("Your monster fainted!");
            prompt.OnClicked.AddListener(() => 
            { 
                ArenaManager.CurrentGameState = GameState.BossWin;
                ArenaUI.Instance.ClearTextPrompts();
            });

        }

        SetHealth(dmg);
    }

    public void SetHealth(int dmg) {
        StartCoroutine(LerpHealthBar(Health, Health - dmg));
        Health -= dmg;

        PlayerHealthCount.text = ((int)Health).ToString();
        PlayerHealthMax.text = MaxHealth.ToString();
    }

    public void HasArms() {
        if(GameManager.ActiveSave.EquippedParts[0] == null && GameManager.ActiveSave.EquippedParts[1] == null) 
            ArenaUI.Instance.MakeTextPrompt("Arms are broken!").OnClicked.AddListener(() => ArenaManager.CurrentGameState = GameState.BossTurn);
    }

    private IEnumerator LerpHealthBar(float currentHP, float targetHP) {
        float elapsedTime = 0;
        while (Health != targetHP && elapsedTime < 1) {
            elapsedTime += Time.deltaTime;
            float newHP = Mathf.Lerp(currentHP, targetHP, elapsedTime);

            Health = newHP;
            PlayerHealthBar.value = Health / MaxHealth;
            yield return null;
        }

        Health = targetHP;
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

    public void OnPlayerWin() {
        GameManager.NextBoss();
        AudioManager.StopMusic(0.5f);
        ArenaManager.Instance.LoadScene("Grave_Robbing");
    }

    public void OnPlayerLose() {
        GameManager.Restart();
        AudioManager.StopMusic(0.5f);
        ArenaManager.Instance.LoadScene("Main_Menu");
    }
}
