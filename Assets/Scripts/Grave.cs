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
        GravePicker.Instance.SelectGrave(this);
    }

    public void SetBodyPart(int i, BodyPart b) {
        BodyParts[i] = b;
    }

    public void DenyGraveRob() {
        // Do some shit to deny the grave rob
        // Shake the grave and make a "no" noise
        Debug.Log("Out of grave robs grave robs");
    }
}
