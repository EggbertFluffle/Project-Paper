using NUnit.Framework;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Boss : MonoBehaviour {
    public static Boss Instance;

    public int MaxHealth;
    public float Health;
    public Slider BossHealthBar;
    public TextMeshProUGUI BossTextName;
    public SpriteRenderer BossSprite;
    public Animator BossAnimator;

    private BossBattle currentBossBattle;

    private bool bleeding = false; 
    private int bleedOut;
    private int bleedTimer = 0;

    private void Awake() 
    {
        if(Instance == null) 
            Instance = this;
    }

    private void OnEnable()
    {
        ArenaManager.OnBossTurn.AddListener(TakeBleed);
        ArenaManager.OnBossTurn.AddListener(TakeRandomAction);
    }

    private void OnDisable()
    {
        ArenaManager.OnBossTurn.RemoveListener(TakeBleed);
        ArenaManager.OnBossTurn.RemoveListener(TakeRandomAction);
    }

    public void Start() {
        // Get current boss from the GameManager
        currentBossBattle = GameManager.CurrentBossBattle;
        ArenaUI.Instance.MakeTextPrompt(currentBossBattle.BattleStartText);
        transform.localScale = new Vector3(currentBossBattle.Scale, currentBossBattle.Scale, currentBossBattle.Scale);
        BossSprite.sprite = currentBossBattle.BossSprite;
        MaxHealth = currentBossBattle.Health;
        Health = MaxHealth;
        BossTextName.text = currentBossBattle.Name;
    }

    public void TakeRandomAction()
    {
        Player.Instance.SendAttack(currentBossBattle.Damage, currentBossBattle.Name);
    }

    public void Taunt() {
        // TODO: Add boss taunts
        ArenaUI.Instance.MakeTextPrompt("I am taunting you");
    }

    public void TakeBleed() 
    {
        bleedTimer--;
        TakeDamage(bleedOut);
        bleeding = bleedTimer != 0;
        ArenaUI.Instance.MakeTextPrompt($"{currentBossBattle.Name} is bleeding");
    }

    public void SendBleed(int _bleedOut) {
        bleeding = true;
        bleedOut = _bleedOut;
        bleedTimer = 5;
    }

    public void SendAttack(int damage) {
        if(damage != 0) 
            TakeDamage(damage);
    }

    public void TakeDamage(int dmg) 
    {
        if(Health - dmg <= 0.0f) 
        {
            Kill();
        }

        SetHealth(dmg);
    }

    public void SetHealth(int dmg) 
    {
        StartCoroutine(LerpHealthBar(Health, Health - dmg));
        Health -= dmg;
    }

    private IEnumerator LerpHealthBar(float currentHP, float targetHP)
    {
        float elapsedTime = 0;
        while (Health != targetHP && elapsedTime < 1)
        {
            elapsedTime += Time.deltaTime;
            float newHP = Mathf.Lerp(currentHP, targetHP, elapsedTime);

            Health = newHP;
            BossHealthBar.value = Health / MaxHealth;
            yield return null;
        }

        Health = targetHP;
    }

    public void Kill() {
        ArenaUI.Instance.ClearTextPrompts();
        TextPrompt prompt = ArenaUI.Instance.MakeTextPrompt(currentBossBattle.Name + " has fallen!");
        prompt.OnClicked.AddListener(() => ArenaManager.CurrentGameState = ArenaManager.GameState.PlayerWin);
    }
}
