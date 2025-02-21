using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class Player : MonoBehaviour {
    public static Player Instance;

    public int MaxHealth;
    public int Health;
    public float TotalEvasion;

    public TextMeshProUGUI PlayerHealthCount;
    public TextMeshProUGUI PlayerHealthMax;

    public SpriteRenderer[] Arms;
    public Slider PlayerHealthBar;
    public Animator PlayerAnimator;

    // List of BodyPartRef
    public List<BodyPartRef> BodyParts;

    private void Awake() {
        if(Instance == null) Instance = this;
    }

    public void Start() {
        BodyParts = GameManager.ActiveSave.EquippedParts;

        // Set arm sprites according to equipped parts
        if (BodyParts[0] != null) Arms[0].sprite = BodyParts[0].BackLimbSprite;
        if (BodyParts[1] != null) Arms[1].sprite = BodyParts[1].BackLimbSprite;

        // Take into account leg stats
        MaxHealth = 100;

        // Load leg stats if applicable
        if (BodyParts[2] != null) {
            MaxHealth += BodyParts[2].HP;
            TotalEvasion += BodyParts[2].Evasion;
        }

        if(BodyParts[3] != null) {
            MaxHealth += BodyParts[3].HP;
            TotalEvasion += BodyParts[3].Evasion;
        }

        Health = MaxHealth;
        SetHealth();
        PlayerHealthCount.text = Health.ToString();
        PlayerHealthMax.text = MaxHealth.ToString();
    }

    public void SetupAttacks() {
        ArenaUI.Instance.LoadAttackOptions(BodyParts);
    }

    public void Heal() {
        // TODO: add indicator for healing
        // Heal 50 percent of health
        Health += (int)Mathf.Floor(0.5f * MaxHealth);
        SetHealth();
    }

    public void AttackButtonHandle(int buttonNum) {
        if(buttonNum == 0 || buttonNum == 1) {
            if(UnityEngine.Random.Range(0.0f, 1.0f) > 0.9f) {
                // Attack misses
                // IMPLEMENT ME
                // IMPLEMENT ME
                // IMPLEMENT ME
                // IMPLEMENT ME
                // IMPLEMENT ME
                // IMPLEMENT ME
            } else {
                int damage = BodyParts[buttonNum].Strength;
                Boss.Instance.SendAttack(damage);
                PlayerAnimator.SetTrigger("RunAttack");
            }
        } else {
            HandleSpecialAttack(buttonNum);
        }
    }
    
    public void HandleSpecialAttack(int buttonNum) {
        // Take care of the special attacks
        // IMPLEMENT ME
        // IMPLEMENT ME
        // IMPLEMENT ME
        // IMPLEMENT ME
        // IMPLEMENT ME
    }

    public void SendAttack(int damage) {
        if(UnityEngine.Random.Range(0.0f, 1.0f) < TotalEvasion) {
            // Take care of evading the attack
            // IMPLEMENT ME
            // IMPLEMENT ME
            // IMPLEMENT ME
            // IMPLEMENT ME
            // IMPLEMENT ME
        } else {
            TakeDamage(damage);
        }
    }

    public void TakeDamage(int dmg) {
        Health -= dmg;
        SetHealth();
        if(Health <= 0.0f) {
            Kill();
        }
    }

    public void SetHealth() {
        PlayerHealthBar.value = (float)Health / MaxHealth;
        PlayerHealthCount.text = "" + Health;
    }

    public void Kill() {
        Debug.Log("You lost!");
    }
}
