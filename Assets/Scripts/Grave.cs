using System;
using UnityEngine;

public class Grave : MonoBehaviour {
    public BodyPart[] BodyParts = new BodyPart[4];

    public enum GraveState { Untouched, Robbed };
    public GraveState State;
    
    public SpriteRenderer GraveHighlight;
    public Sprite RobbedGraveSprite;

    private SpriteRenderer graveSpriteRenderer;

    private void Start() {
        State = GraveState.Untouched;
        graveSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnMouseDown() {
        GRManager.Instance.HandleGraveClick(this);
    }

    private void OnMouseEnter() {
        GraveHighlight.enabled = true;
    }

    private void OnMouseExit() {
        GraveHighlight.enabled = false;
    }

    public void Rob() {
        State = GraveState.Robbed;
        graveSpriteRenderer.sprite = RobbedGraveSprite;
        GravePicker.Instance.SelectGrave(this);
    }

    public void SetBodyPart(int i, BodyPart b) {
        BodyParts[i] = b;
    }

    public void DenyGraveRob() {
        // Do some shit to deny the grave rob
        // Shake the grave and make a "no" noise
        Debug.Log("Out of grave robs");
    }
}
