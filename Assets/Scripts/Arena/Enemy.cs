using UnityEngine;

public class Enemy : MonoBehaviour {
    public static Enemy Instance;

    public SpriteRenderer[] Limbs;

    public void OnAwake() {
        if(Instance == null) Instance = this;
    }
}
