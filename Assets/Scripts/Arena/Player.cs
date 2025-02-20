using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;

public class Player : MonoBehaviour {
    public static Player Instance;

    public int MaxHealth;
    public int Health;
    public float TotalEvasion;

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
        Arms[0].sprite = BodyParts[0].LimbConstants.BackLimbSprite;
        Arms[1].sprite = BodyParts[1].LimbConstants.BackLimbSprite;

        // Take into account leg stats
        TotalEvasion = BodyParts[2].LimbConstants.Evasion + BodyParts[3].LimbConstants.Evasion;
        MaxHealth = 100 + BodyParts[2].HP + BodyParts[3].HP;
        Health = MaxHealth;
    }

    public void SetupAttacks() {
        // Make all buttons say the names of arms
        // Make all special moves have the correct names
    }

    public void AttackButtonHandle(int buttonNum) {
        if(buttonNum == 0 || buttonNum == 2) {
            if(Random.Range(0.0f, 1.0f) > 0.9f) {
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
        if(Random.Range(0.0f, 1.0f) < TotalEvasion) {
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
    }

    public void Kill() {
        Debug.Log("You lost!");
    }
}
