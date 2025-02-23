using System.Collections.Generic;
using UnityEngine;

public class ArenaManager : SceneLoader {
    public static ArenaManager Instance;

    public bool PlayerTurn;

    private void Awake() {
        if(Instance == null) Instance = this;
    }

    public void Start() {
        AudioManager.PlayMusic($"Battle0{GameManager.ActiveSave.CurrentBoss + 1}");
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
