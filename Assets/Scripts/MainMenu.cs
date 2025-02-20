using UnityEngine;

public class MainMenu : MonoBehaviour {
    public Animator BackgroundAnimator;
    public Animator FadeToBlack;

    public void PlayFadeToBlack() {
        FadeToBlack.Play("FadeToBlack");
    }

    public void Start() {
        AudioManager.PlayMusic("Main Menu");
    }

    public void FixedUpdate() {
        if(Random.Range(0.0f, 1.0f) > 0.999f) {
            BackgroundAnimator.Play("MainMenu");
        }

        // Exit the scene once the fade to black is over
        if(FadeToBlack.GetCurrentAnimatorStateInfo(0).IsName("Finished")) {
            GameManager.LoadScene("Grave_Robbing");
            AudioManager.StopMusic(2);
        }
    }
}
