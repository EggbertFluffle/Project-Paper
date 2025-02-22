using UnityEngine;

public class LabButton : MonoBehaviour {
    public void HandleRelease() {
        GRManager.Instance.LoadScene("Laboratory");
    }

    public void OnClick() {
        AudioManager.PlaySFX("Button Press");
    }
}
