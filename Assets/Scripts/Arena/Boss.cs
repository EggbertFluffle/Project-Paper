using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour {
    public static Boss Instance;

    public int MaxHealth;
    public int Health;
    public Slider BossHealthBar;
    public Animator BossAnimator;

    private BossBattle currentBossBattle;
    private Transform transform;

    // TODO: Give an indicator for bleeding
    private bool bleeding = false; 
    private int bleedOut;
    private int bleedTimer = 0;

    private void Awake() {
        if(Instance == null) Instance = this;
    }

    public void Start() {
        // Get current boss from the GameManager
        currentBossBattle = GameManager.CurrentBossBattle;
        transform.localScale = new Vector3(currentBossBattle.Scale, currentBossBattle.Scale, currentBossBattle.Scale);
        MaxHealth = currentBossBattle.Health;
        Health = MaxHealth;
    }

    public void TakeTurn() {
        if(bleeding) {
            bleedTimer--;
            TakeDamage(bleedOut);
            bleeding = bleedTimer != 0;
        }

        Player.Instance.SendAttack(currentBossBattle.Damage);
    }

    public void TakeBleed(int _bleedOut) {
        bleeding = true;
        bleedOut = _bleedOut;

        // How long should the bleed last
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
    }

    public void SetHealth() {
        BossHealthBar.value = (float)Health / MaxHealth;
    }

    public void Kill() {
        // Implement win
        // IMPLEMENT ME
        // IMPLEMENT ME
        // IMPLEMENT ME
        // IMPLEMENT ME
        Debug.Log("Boss has been killed");
    }
}
