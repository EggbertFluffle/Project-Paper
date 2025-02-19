using UnityEngine;

public class ArenaManager : MonoBehaviour {
    public static ArenaManager Instance;

    public void OnAwake() {
        if(Instance == null) Instance = this;
    }
}
