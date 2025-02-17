using System;
using UnityEngine;

public class Grave : MonoBehaviour {
    public BodyPart[] BodyParts = new BodyPart[4];

    public enum GraveState { Untouched, Robbed };
    public GraveState State;

    private void Start() {
        State = GraveState.Untouched;
    }

    public void OnMouseDown() {
        GRManager.Instance.HandleGraveClick(this);
    }

    public void Rob() {
        State = GraveState.Robbed;
        for(int i = 0; i < BodyParts.Length; i++) {
            if(BodyParts[i] != null) {
                Debug.Log("My " + i + "th body part is " + BodyParts[i].Name);
            }
        }
    }

    public void SetBodyPart(int i, BodyPart b) {
        BodyParts[i] = b;
    }
}
