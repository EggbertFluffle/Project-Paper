using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public static SaveData ActiveSave => instance.activeSave;

    public static UnityEvent OnSaveDataLoaded = new UnityEvent();

    private static GameManager instance;
    private SaveData activeSave;

    public List<BodyPart> BodyPartInventory;

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

        Debug.Log("The string is " + json.Length + "chars long.");

        PlayerPrefs.SetString("Save", json);
    }

    public static void Load()
    {
        string json = PlayerPrefs.GetString("Save");
        instance.activeSave = string.IsNullOrEmpty(json) ? new SaveData() : JsonUtility.FromJson<SaveData>(json);
    }

    public static void LoadScene(string sceneName) => SceneManager.LoadScene(sceneName);
}
