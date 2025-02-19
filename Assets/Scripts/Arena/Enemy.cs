using UnityEngine;
using UnityEngine.U2D.IK;

public class Enemy : MonoBehaviour {
    public static Enemy Instance;

    public float Health;

    public SpriteRenderer[] Limbs;

    private void Awake() {
        if(Instance == null) Instance = this;
    }

    private void LoadLimbs(BodyPart[] bodyParts) {
        Limbs[0].sprite = bodyParts[0].FrontLimbSprite;
        Limbs[1].sprite = bodyParts[1].BackLimbSprite;
        Limbs[2].sprite = bodyParts[2].GraveLimbSprite;
        Limbs[3].sprite = bodyParts[3].GraveLimbSprite;
    }
}
