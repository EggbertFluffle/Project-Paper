using UnityEngine;

public class MainMenu : SceneLoader {

    public Cutscene OpeningCutscene;

    private int cutsceneIndex = 0;

    public Animator BackgroundAnimator;
    public Animator FadeToBlack;

    public void LoadScene() => LoadScene("Grave_Robbing");

    private void Awake() => OpeningCutscene.ParentObject.SetActive(false);

    public void Start() {
        AudioManager.PlayMusic("Main Menu");
    }

    public void PlayCutscene()
    {
        if (cutsceneIndex < OpeningCutscene.AllEvents.Count)
        {
            OpeningCutscene.Play(cutsceneIndex);
            cutsceneIndex++;
        }
        else
            OpeningCutscene.Finish(false);
    }

    public void Update() {
        if(Random.Range(0.0f, 1.0f) > 0.999f) {
            BackgroundAnimator.Play("MainMenu");
        }

        // Exit the scene once the fade to black is over
        if (FadeToBlack.GetCurrentAnimatorStateInfo(0).IsName("Finished")) {
            AudioManager.StopMusic(2);
            LoadScene("Grave_Robbing");
        }
    }
}
