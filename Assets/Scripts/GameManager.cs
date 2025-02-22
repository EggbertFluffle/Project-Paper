using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;
using System;

using LimbType = BodyPart.LimbType;
using System.Collections;

public class GameManager : MonoBehaviour {
    public static SaveData ActiveSave => instance.activeSave;

    public static BossBattle CurrentBossBattle => instance.currentBossBattle;

    public SaveData activeSave = new SaveData();

    public static List<BodyPart> AllArms => instance.allArms.Values.ToList();
    public static List<BodyPart> AllLegs => instance.allLegs.Values.ToList();

    private readonly Dictionary<string, BodyPart> allArms = new Dictionary<string, BodyPart>();
    private readonly Dictionary<string, BodyPart> allLegs = new Dictionary<string, BodyPart>(); 
    
    private static GameManager instance;

    private List<BossBattle> bossBattles;

    private BossBattle currentBossBattle;

    private readonly List<string> bossOrder = new List<string> {
        "Maria Sheila",
        "Frank N. Stein",
        "Heckyll and Jibe",
        "Hazabe",
        "Chimera"
    };

    private void OnEnable()
    {
        AssignConstants();
    }

    private void Awake() {
        if (instance == null) 
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } 
        else 
            Destroy(gameObject);

        foreach (BodyPart arm in Resources.LoadAll<BodyPart>("BodyParts/Arms")) 
            allArms[arm.Name] = arm;
        
        foreach (BodyPart leg in Resources.LoadAll<BodyPart>("BodyParts/Legs")) 
            allLegs[leg.Name] = leg;
        
        
        bossBattles = bossOrder.Select(bossName => Resources.Load<BossBattle>($"Arena/{bossName}")).ToList();
        currentBossBattle = bossBattles[0];
    }

    private void NextBossInstance() {
        
    }

    public static void NextBoss() {
        instance.NextBossInstance();
    }

    public BodyPart GetBodyPart(string key) => allArms.TryGetValue(key, out var arm) ? arm : allLegs.TryGetValue(key, out var leg) ? leg : null;

    private void AssignConstants() {
        foreach (BodyPartRef bodyPartRef in instance.activeSave.EquippedParts) {
            if(bodyPartRef != null) {
                Debug.Log(GetBodyPart(bodyPartRef.Name) == null);
                Debug.Log(JsonUtility.ToJson(bodyPartRef));
                bodyPartRef.SetConstants(GetBodyPart(bodyPartRef.Name));
            }
        }

        foreach (BodyPartRef bodyPartRef in instance.activeSave.Inventory) {
            Debug.Log("is BodyPart null in inventory: " + GetBodyPart(bodyPartRef.Name) == null);
            bodyPartRef.SetConstants(GetBodyPart(bodyPartRef.Name));
        }
    }



    // private void SaveInstance() {
    //     string json = JsonUtility.ToJson(instance.activeSave);
    //     Debug.Log(json);
    //     PlayerPrefs.SetString("Save", json);
    // }

    // public static void Save() => instance.SaveInstance();

    // private void LoadInstance() {
    //     string json = PlayerPrefs.GetString("Save");
    //     Debug.Log("Loading Data");
    //     instance.activeSave = string.IsNullOrEmpty(json) ? new SaveData() : JsonUtility.FromJson<SaveData>(json);
    //     AssignConstants();
    // }

    // public static void Load() => instance.LoadInstance();

    // public void DeleteSave() {
    //     Debug.Log("Deleting Save.");
    //     PlayerPrefs.DeleteKey("Save");
    // }



    private void OnApplicationQuit() {
        // SaveInstance();
    }

    private void Update() {
        // if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.Period)) {
        //     DeleteSave();
        // }
    }
}