using UnityEngine;

public class ArenaManager : SceneLoader {
    public static ArenaManager Instance;

    public bool PlayerTurn;

    private void Awake() {
        if(Instance == null) Instance = this;
    }

    public void Start() {
        AudioManager.PlayMusic("Battle Music");
        PlayerTurn = true;
    }
}
