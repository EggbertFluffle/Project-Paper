using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public static SaveData ActiveSave => Instance.activeSave;

    public static UnityEvent OnSaveDataLoaded = new UnityEvent();

    public SaveData activeSave;

    private List<string> bossOrder = new List<string>{
        "Maria Sheila",
        "Frank N. Stein",
        "Heckyll and Jibe",
        "Hazabe",
        "Chimera"
    };
    private List<BossBattle> bossBattles;
    public BossBattle CurrentBossBattle;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
        
        foreach(string bossName in bossOrder) {
            bossBattles.Add(Resources.Load<BossBattle>("Arena/" + bossName));
        }
        CurrentBossBattle = bossBattles[0];
    }

    private void NextBossInstance() {
        
    }

    public static void NextBoss() {
        Instance.NextBossInstance();
    }

    public static void Save()
    {
        string json = JsonUtility.ToJson(Instance.activeSave);

        Debug.Log("The string is " + json.Length + "chars long.");

        PlayerPrefs.SetString("Save", json);
    }

    public static void Load()
    {
        string json = PlayerPrefs.GetString("Save");
        Instance.activeSave = string.IsNullOrEmpty(json) ? new SaveData() : JsonUtility.FromJson<SaveData>(json);
    }

    public static void LoadScene(string sceneName) => SceneManager.LoadScene(sceneName);
}
