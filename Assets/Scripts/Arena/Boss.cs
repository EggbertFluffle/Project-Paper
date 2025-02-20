using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour {
    public static Boss Instance;

    public int MaxHealth;
    public int Health;
    public Slider BossHealthBar;
    public Animator BossAnimator;

    private BossBattle currentBossBattle;

    private void Awake() {
        if(Instance == null) Instance = this;
    }

    public void Start() {
        // Get current boss from the GameManager
        // currentBossBattle = GameManager.CurrentBoss;
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
