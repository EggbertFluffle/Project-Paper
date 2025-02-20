using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.UI;
using UnityEngine;

public class Player : MonoBehaviour {
    public static Player Instance;

    public float MaxHealth = 100;
    public float Health = 100;
    public SpriteRenderer[] Arms;
    public Slider PlayerHealthBar;
    public Animator PlayerAnimator;

    private void Awake() {
        if(Instance == null) Instance = this;
        PlayerHealthBar.value = 0.5f;
    }

    private void LoadLimbs(BodyPart[] bodyParts) {
        Arms[0].sprite = bodyParts[0].BackLimbSprite;
        Arms[1].sprite = bodyParts[1].BackLimbSprite;
    }

    public void AttackButtonHandle(int buttonNum) {
        Enemy.Instance.SendAttack(new ArenaManager.Attack(10.0f, 1.0f));
        PlayerAnimator.SetTrigger("RunAttack");
    }

    public void SendAttack(ArenaManager.Attack atk) {
        float rng = Random.Range(0.0f, 1.0f);
        if(rng > atk.accuracy) {
            // Attack missed
            Debug.Log("Attack Missed");
            return;
        }

        TakeDamage(atk.damage);
    }

    public void TakeDamage(float dmg) {
        // Factor in defence?
        Health -= dmg;
        SetHealth();
        if(Health <= 0.0f) {
            Kill();
        }
    }

    public void SetHealth() {
        PlayerHealthBar.value = Health / MaxHealth;
    }

    public void Kill() {
        Debug.Log("You lost!");
    }
}
