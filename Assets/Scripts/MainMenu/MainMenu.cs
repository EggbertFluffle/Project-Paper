using UnityEngine;

public class MainMenu : SceneLoader {
    public static MainMenu Instance;

    public Cutscene OpeningCutscene;

    private int cutsceneIndex = 0;

    public Animator BackgroundAnimator;

    public void LoadScene() => LoadScene("Grave_Robbing");

    private void Awake() {
        if(Instance == null) Instance = this;
        OpeningCutscene.ParentObject.SetActive(false);
    }

    public void Start() {
        AudioManager.PlayMusic("Main Menu");
    }

    public void PlayCutscene() {
        if (cutsceneIndex < OpeningCutscene.AllEvents.Count) {
            OpeningCutscene.Play(cutsceneIndex);
            cutsceneIndex++;
        } else
            OpeningCutscene.Finish(false);
    }

    public void Update() {
        if(Random.Range(0.0f, 1.0f) > 0.999f) {
            BackgroundAnimator.Play("MainMenu");
        }
    }
}
