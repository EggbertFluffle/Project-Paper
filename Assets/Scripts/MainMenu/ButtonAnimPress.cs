using UnityEngine;

public class ButtonAnimPress : MonoBehaviour {
    public void HandleRelease() {
        AudioManager.PlaySFX("Paper Rustle");
        MainMenu.Instance.PlayCutscene();
    }

    public void OnClick() {
        AudioManager.PlaySFX("Button Press");
    }
}
