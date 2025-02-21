using System;
using System.Collections.Generic;
using UnityEngine;

public class Grave : MonoBehaviour {
    public List<BodyPartRef> BodyParts;

    public bool Robbed = false;
    
    public Sprite RobbedGraveSprite;
    public SpriteRenderer HighlightRenderer;

    public ParticleSystem DigParticles;

    private SpriteRenderer SpriteRenderer;

    private void Start() {
        BodyParts = new List<BodyPartRef>();
        SpriteRenderer = GetComponent<SpriteRenderer>();

        GenerateLimbs();
    }

    private void GenerateLimbs() {
        BodyPart[] allArms = Resources.LoadAll<BodyPart>("BodyParts/Arms");
        BodyPart[] allLegs = Resources.LoadAll<BodyPart>("BodyParts/Legs");

        for(int i = 0; i < 4; i++) {
            if(UnityEngine.Random.Range(0.0f, 1.0f) < GRManager.NoLimbChance) {
                // Limb did not make it lol
                BodyParts.Add(null);
            } else {
                // Set the body part to a arm or leg depending on limb slot
                BodyPart b = i < 2 ? 
                    allArms[UnityEngine.Random.Range(0, allArms.Length)] :
                    allLegs[UnityEngine.Random.Range(0, allLegs.Length)];
                BodyParts.Add(new BodyPartRef(b));
            }
        }
    }

    private void OnMouseDown() {
        GRManager.Instance.HandleGraveClick(this);
    }

    public void Rob() {
        SpriteRenderer.sprite = RobbedGraveSprite;
        DigParticles.Play();
        Robbed = true;
    }

    // Only highlight the grave if the camera is not transitioning
    private void OnMouseEnter() {
        if(GRManager.Instance.GraveRobs != 0)
            HighlightRenderer.enabled = true;
    }

    private void OnMouseExit() {
        HighlightRenderer.enabled = false;
    }
}
