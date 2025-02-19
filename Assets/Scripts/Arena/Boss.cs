using UnityEngine;

public class Boss : MonoBehaviour {
    public static Boss Instance;

    public void OnAwake() {
        if(Instance == null) Instance = this;
    }
}
