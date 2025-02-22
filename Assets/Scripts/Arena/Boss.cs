using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour {
    public static Boss Instance;

    public int MaxHealth;
    public int Health;
    public Slider BossHealthBar;
    public TextMeshProUGUI BossTextName;
    public SpriteRenderer BossSprite;
    public Animator BossAnimator;

    private BossBattle currentBossBattle;

    private bool bleeding = false; 
    private int bleedOut;
    private int bleedTimer = 0;

    public enum BossState { NotTurn, Bleeding, Wait, Attack, Dead };
    public BossState State;

    private void Awake() {
        if(Instance == null) Instance = this;
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
        State = BossState.NotTurn;
    }

    public void Update() {
        if(!ArenaUI.Instance.HasTextPrompt()) {
            switch(State) {
                case BossState.Wait:
                    if(bleeding) TakeBleed();
                    State = BossState.Bleeding;
                    break;
                case BossState.Bleeding:
                    Taunt();
                    State = BossState.Attack;
                    break;
                case BossState.Attack:
                    State = BossState.NotTurn;
                    TakeTurn();
                    break;
                case BossState.Dead:
                    ArenaManager.Instance.PlayerWin();
                    State = BossState.NotTurn;
                    break;
            }
        }
    }

    public void Taunt() {
        // TODO: Add boss taunts
        ArenaUI.Instance.MakeTextPrompt("I am taunting you");
    }

    public void TakeTurn() {
        Player.Instance.SendAttack(currentBossBattle.Damage);
    }

    public void TakeBleed() {
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
        TakeDamage(damage);
    }

    public void TakeDamage(int dmg) {
        Health -= dmg;
        SetHealth();
        if(Health <= 0.0f) {
            Kill();
        }
        // TODO: Play some sort of animation
        // TODO: Play some sort of sound
    }

    public void SetHealth() {
        BossHealthBar.value = (float)Health / MaxHealth;
    }

    public void Kill() {
        State = BossState.Dead;
        ArenaUI.Instance.MakeTextPrompt(currentBossBattle.Name + " has fallen!");
    }
}
