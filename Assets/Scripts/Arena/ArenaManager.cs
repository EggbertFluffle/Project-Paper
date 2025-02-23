using System.Collections.Generic;
using UnityEngine;

public class ArenaManager : SceneLoader {
    public static ArenaManager Instance;

    public bool PlayerTurn;

    private void Awake() {
        if(Instance == null) Instance = this;
    }

    public void Start() {
        switch(GameManager.ActiveSave.CurrentBoss) {
            case 0:
                AudioManager.PlayMusic($"Battle05");
                break;
            case 1:
                AudioManager.PlayMusic($"Battle01");
                break;
            case 2:
                AudioManager.PlayMusic($"Battle02");
                break;
            case 3:
                AudioManager.PlayMusic($"Battle03");
                break;
            case 4:
                AudioManager.PlayMusic($"Battle04");
                break;
        }
        PlayerTurn = true;
    }

    public void PlayerWin() {
        AudioManager.StopMusic();
        LoadScene("Grave_Robbing");
    }

    public void PlayerLose() {
        GameManager.Restart();
        AudioManager.StopMusic();
        LoadScene("Main_Menu");
    }
}
