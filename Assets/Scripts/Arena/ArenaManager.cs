using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class ArenaManager : SceneLoader {
    public static ArenaManager Instance;

    public GameState currentGameState;

    public static readonly UnityEvent OnPlayerTurn = new UnityEvent();
    public static readonly UnityEvent OnBossTurn = new UnityEvent();

    public static readonly UnityEvent OnPlayerWin = new UnityEvent();
    public static readonly UnityEvent OnBossWin = new UnityEvent();

    public static GameState CurrentGameState
    {
        get => Instance.currentGameState;

        set
        {
            switch (value)
            {
                case GameState.PlayerTurn:
                    OnPlayerTurn.Invoke();
                    break;
                case GameState.BossTurn:
                    OnBossTurn.Invoke();
                    break;
                case GameState.PlayerWin:
                    OnPlayerWin.Invoke();
                    break;
                case GameState.BossWin:
                    OnBossWin.Invoke();
                    break;
            }

            Instance.currentGameState = value;
        }
    }

    public enum GameState
    { PlayerTurn, BossTurn, PlayerWin, BossWin }

    private void Awake() {
        currentGameState = GameState.PlayerTurn;

        if(Instance == null) 
            Instance = this;
    }

    public void Start() {
        AudioManager.PlayMusic($"Battle0{GameManager.ActiveSave.CurrentBoss + 1}");
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
