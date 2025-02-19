using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static SaveData ActiveSave => instance.activeSave;

    private static GameManager instance;
    private SaveData activeSave;

    private void Awake()
    {
        if (instance == null)
            instance = this;
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
        instance.activeSave = JsonUtility.FromJson<SaveData>(PlayerPrefs.GetString("Save"));
    }

    public static void LoadScene(string sceneName) => SceneManager.LoadScene(sceneName);
}
