using UnityEngine;
using UnityEngine.UI;

public class MainMenu : SceneLoader {
    public static MainMenu Instance;

    public Cutscene OpeningCutscene;
    public Image Credits;

    private int cutsceneIndex = 0;

    public Animator BackgroundAnimator;

    public void LoadScene() {
        AudioManager.StopMusic();
        LoadScene("Grave_Robbing");
    }

    private void Awake() {
        if(Instance == null) Instance = this;
        OpeningCutscene.ParentObject.SetActive(false);
    }

    public void Start() {
        AudioManager.PlayMusic("Main_Menu");
    }

    public void PlayCutscene() {
        if (cutsceneIndex < OpeningCutscene.AllEvents.Count) {
            OpeningCutscene.Play(cutsceneIndex);
            cutsceneIndex++;
        } else {
            OpeningCutscene.Finish(false);
        }
    }

    public void RollTheCredits() {
        Credits.gameObject.SetActive(true);
    }

    public void CloseCredits() {
        Credits.gameObject.SetActive(false);
    }

    public void Update() {
        if(Random.Range(0.0f, 1.0f) > 0.999f) {
            BackgroundAnimator.Play("MainMenu");
        }
    }
}
