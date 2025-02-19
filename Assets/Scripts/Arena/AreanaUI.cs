using UnityEngine;

public class AreanaUI : MonoBehaviour {
    public static AreanaUI Instance;

    public void OnAwake() {
        if(Instance == null) Instance = this;
    }
}
