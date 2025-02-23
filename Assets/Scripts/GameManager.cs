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
        "Hekyll and Jibe",
        "Hazabe",
        "Chimera"
    };

    private void OnEnable() {
        AssignConstants();

        bossBattles = new List<BossBattle>();
        foreach(string bossName in bossOrder) {
            BossBattle boss = Resources.Load<BossBattle>($"Arena/{bossName}");
            bossBattles.Add(boss);
        }
        currentBossBattle = bossBattles[0];
    }

    private void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else 
            Destroy(gameObject);

        foreach (BodyPart arm in Resources.LoadAll<BodyPart>("BodyParts/Arms")) 
            allArms[arm.Name] = arm;
        
        foreach (BodyPart leg in Resources.LoadAll<BodyPart>("BodyParts/Legs")) 
            allLegs[leg.Name] = leg;
    }

    private void NextBossInstance() {
        Debug.Log("NEXT BOSS IS CALLED");
        activeSave.CurrentBoss++;
        currentBossBattle = bossBattles[activeSave.CurrentBoss];
    }

    public static void NextBoss() {
        instance.NextBossInstance();
    }

    public BodyPart GetBodyPart(string key) => allArms.TryGetValue(key, out var arm) ? arm : allLegs.TryGetValue(key, out var leg) ? leg : null;

    private void AssignConstants() {
        foreach(BodyPartRef bodyPartRef in instance.activeSave.EquippedParts) {
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

    public static void Restart() {
        instance.RestartInstance();
    }

    private void RestartInstance() {
        activeSave = new SaveData();
        currentBossBattle = bossBattles[activeSave.CurrentBoss];
    }   
}