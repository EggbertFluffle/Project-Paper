using UnityEngine;
using UnityEngine.U2D.IK;
using UnityEngine.Video;

public class Enemy : MonoBehaviour {
    public static Enemy Instance;

    public float MaxHealth = 100.0f;
    public float Health;
    public UnityEngine.UI.Slider EnemyHealthBar;
    public SpriteRenderer[] Limbs;

    private void Awake() {
        if(Instance == null) Instance = this;
        Health = MaxHealth;
    }

    private void LoadLimbs(BodyPart[] bodyParts) {
        Limbs[0].sprite = bodyParts[0].FrontLimbSprite;
        Limbs[1].sprite = bodyParts[1].FrontLimbSprite;
        Limbs[2].sprite = bodyParts[2].GraveLimbSprite;
        Limbs[3].sprite = bodyParts[3].GraveLimbSprite;
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
        EnemyHealthBar.value = Health / MaxHealth;
    }

    public void Kill() {
        
    }
}
