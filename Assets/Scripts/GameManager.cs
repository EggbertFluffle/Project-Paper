using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;
using System;

public class GameManager : MonoBehaviour
{
    public static SaveData ActiveSave => instance.activeSave;

    public static BossBattle CurrentBossBattle => instance.currentBossBattle;

    public SaveData activeSave;

    private static GameManager instance;

    private List<BossBattle> bossBattles;

    private BossBattle currentBossBattle;

    private readonly List<string> bossOrder = new List<string>
    {
        "Maria Sheila",
        "Frank N. Stein",
        "Heckyll and Jibe",
        "Hazabe",
        "Chimera"
    };

    private void OnEnable()
    {
        LoadInstance();
        if (instance.activeSave != null) {
            Debug.Log("Successfully loaded save.");
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
        
        bossBattles = bossOrder.Select(bossName => Resources.Load<BossBattle>($"Arena/{bossName}")).ToList();
        currentBossBattle = bossBattles[0];
    }

    private void NextBossInstance() {
        
    }

    public static void NextBoss() {
        instance.NextBossInstance();
    }

    private void SaveInstance() {
        string json = JsonUtility.ToJson(instance.activeSave);
        Debug.Log("The string is " + json.Length + "chars long.");
        PlayerPrefs.SetString("Save", json);
    }

    public static void Save() => instance.SaveInstance();

    private void LoadInstance() {
        Debug.Log("Instance: " + instance == null);
        string json = PlayerPrefs.GetString("Save");
        instance.activeSave = string.IsNullOrEmpty(json) ? new SaveData() : JsonUtility.FromJson<SaveData>(json);
    }

    public static void Load() => instance.LoadInstance();

    public static void LoadScene(string sceneName)
    {
        instance.SaveInstance();
        SceneManager.LoadScene(sceneName);
    }

    private void OnApplicationQuit()
    {
        SaveInstance();
    }
}
