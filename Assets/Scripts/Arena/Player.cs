using UnityEngine;

public class Player : MonoBehaviour {
    public static Player Instance;

    public SpriteRenderer[] Arms;

    private void Awake() {
        if(Instance == null) Instance = this;
    }

    private void LoadLimbs(BodyPart[] bodyParts) {
        Arms[0].sprite = bodyParts[0].FrontLimbSprite;
        Arms[1].sprite = bodyParts[1].BackLimbSprite;
    }
}
