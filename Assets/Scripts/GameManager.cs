using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public static SaveData ActiveSave => instance.activeSave;

    private static GameManager instance;
    private SaveData activeSave;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void Save()
    {
        string json = JsonUtility.ToJson(instance.activeSave);
        PlayerPrefs.SetString("Save", json);
    }

    public static void Load()
    {
        string json = PlayerPrefs.GetString("Save");
        instance.activeSave = string.IsNullOrEmpty(json) ? new SaveData() : JsonUtility.FromJson<SaveData>(json);
    }

    public BodyPart[] GetLimbsFromInventory()
    {
        if (GRManager.Instance)
        {
            // Change this bullshit immediatly
            // Also persist durability when converting from BodyRefs to BodyParts
            return instance.activeSave.BodyPartInventory.Select(bodyPartRef => bodyPartRef.LimbType == BodyPart.LimbType.Arm
                    ? GRManager.Instance.AllArms.Find(arm => arm.Name.Equals(bodyPartRef.Name))
                    : GRManager.Instance.AllLegs.Find(leg => leg.Name.Equals(bodyPartRef.Name))).ToArray();
        }
        else
        {
            Debug.LogError("GRManager needs to exist in order to use this method.");
            return null;
        }
    }

    public BodyPart[] GetEquippedLimbs()
    {
        if (GRManager.Instance)
        {
            return instance.activeSave.EquippedParts.Select(bodyPartRef => bodyPartRef.LimbType == BodyPart.LimbType.Arm
                    ? GRManager.Instance.AllArms.Find(arm => arm.Name.Equals(bodyPartRef.Name))
                    : GRManager.Instance.AllLegs.Find(leg => leg.Name.Equals(bodyPartRef.Name))).ToArray();
        }
        else
        {
            Debug.LogError("GRManager needs to exist in order to use this method.");
            return null;
        }
    }

    public static void LoadScene(string sceneName) => SceneManager.LoadScene(sceneName);
}
