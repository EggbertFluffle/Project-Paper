using UnityEngine;

public class ArenaUI : MonoBehaviour {
    public static ArenaUI Instance;

    public GameObject TextPromptPrefab;
    public Transform PromptContiainer;

    private void Awake() {
        if(Instance == null) Instance = this;
    }

    public void MakeTextPrompt(string text) {
        GameObject prompt = Instantiate(TextPromptPrefab, PromptContiainer);
        prompt.GetComponent<TextPrompt>().contents = text;
    }

    public bool HasTextPrompt() {
        return PromptContiainer.childCount != 0;
    }

    public void ClearTextPrompts() {
        foreach(Transform prompt in PromptContiainer) {
            Destroy(prompt.gameObject);
        }
    }
}
