using UnityEngine;
using UnityEngine.UI;

public class ArenaUI : MonoBehaviour {
    public static ArenaUI Instance;

    public Button[] PlayerActions;

    public GameObject TextPromptPrefab;
    public Transform PromptContiainer;

    private void Awake() {
        if(Instance == null) Instance = this;
    }

    private void OnEnable() => ArenaManager.OnPlayerTurn.AddListener(() => SetButtonsInteractable(true));

    private void OnDisable() => ArenaManager.OnPlayerTurn.RemoveAllListeners();

    public void SetButtonsInteractable(bool interactable)
    {
        foreach (var button in PlayerActions)
        {
            button.gameObject.SetActive(interactable);
        }
    }

    public TextPrompt MakeTextPrompt(string text) {
        GameObject promptObj = Instantiate(TextPromptPrefab, PromptContiainer);
        TextPrompt prompt = promptObj.GetComponent<TextPrompt>();

        prompt.contents = text;
        return prompt;
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
