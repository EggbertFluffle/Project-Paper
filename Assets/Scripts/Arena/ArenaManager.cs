using UnityEngine.Events;
using UnityEngine;

public class ArenaManager : SceneLoader {
    public static ArenaManager Instance;
    public UnityEngine.UI.Image MouseBlock;

    public GameState currentGameState;

    public static readonly UnityEvent OnPlayerTurn = new UnityEvent();
    public static readonly UnityEvent OnBossTurn = new UnityEvent();

    public static readonly UnityEvent OnPlayerWin = new UnityEvent();
    public static readonly UnityEvent OnBossWin = new UnityEvent();

    public bool bossHasBeenBeat = false;

    public static GameState CurrentGameState {
        get => Instance.currentGameState;

        set {
            switch (value) {
                case GameState.PlayerTurn:
                    Cursor.lockState = CursorLockMode.None;
                    OnPlayerTurn.Invoke();
                    break;
                case GameState.BossTurn:
                    Cursor.lockState = CursorLockMode.Locked;
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

        OnPlayerTurn.Invoke();
    }
}
