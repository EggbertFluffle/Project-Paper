using UnityEngine;

public class Grave : MonoBehaviour {
    public void OnMouseDown() {
        GRManager.Instance.HandleGraveClick(this);
    }

    public void Rob() {
        Debug.Log("I was robbed, oh no!");
    }
}
