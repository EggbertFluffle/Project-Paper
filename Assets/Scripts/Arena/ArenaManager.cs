using UnityEngine;

public class ArenaManager : MonoBehaviour {
    public static ArenaManager Instance;

    public bool PlayerTurn = true;

    private void Awake() {
        if(Instance == null) Instance = this;
    }

    public void SetUpBattle() {
        
    }
}
