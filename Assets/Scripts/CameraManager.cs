using UnityEngine;

public class CameraManager : MonoBehaviour {
    public static CameraManager Instance;

    private void Awake() {
        if(Instance == null) Instance = this;
    }
}
