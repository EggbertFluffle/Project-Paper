using UnityEngine;

public class Player : MonoBehaviour {
    public static Player Instance;

    public SpriteRenderer[] Arms;

    public void OnAwake() {
        if(Instance == null) Instance = this;
    }
}
