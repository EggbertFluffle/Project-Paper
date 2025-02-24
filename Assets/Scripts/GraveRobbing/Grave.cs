using System.Collections.Generic;
using UnityEngine;

public class Grave : MonoBehaviour {
    public List<BodyPartRef> BodyParts;

    public bool Robbed = false;

    [HideInInspector]
    public int GraveIndex;

    public Sprite RobbedGraveSprite;
    public SpriteRenderer HighlightRenderer;

    public ParticleSystem DigParticles;
    public Animator Animator;

    private SpriteRenderer SpriteRenderer;

    private void Start() {
        BodyParts = new List<BodyPartRef>();
        SpriteRenderer = GetComponent<SpriteRenderer>();

        GenerateLimbs();
    }

    private void GenerateLimbs() {
        for(int i = 0; i < 4; i++) {
            if (GameManager.ActiveSave.CurrentBoss == 0) {
                // The top graves should produce 2 random arms, and the bottom graves should have 2 random legs

                // Top Graves
                if (GraveIndex < 2 && i < 2) {
                    BodyParts.Add(GRManager.Instance.RandomArmWithRarity());
                }
                // Bottom Graves
                else if (GraveIndex > 1 && i > 1) {
                    BodyParts.Add(GRManager.Instance.RandomLegWithRarity());
                } else {
                    BodyParts.Add(null);
                }
            } else { BodyParts.Add(Random.Range(0.0f, 1.0f) < GRManager.NoLimbChance
                    // Limb did not make it lol
                    ? null
                    // Set the body part to a arm or leg depending on limb slot
                    : i < 2
                        ? GRManager.Instance.RandomArmWithRarity() 
                        : GRManager.Instance.RandomLegWithRarity());
            }
        }
    }

    private void OnMouseDown() {
        if(!Robbed) GRManager.Instance.HandleGraveClick(this);
    }

    public void Rob() {
        SpriteRenderer.sprite = RobbedGraveSprite;
        DigParticles.Play();
        Animator.SetTrigger("Shake");
        AudioManager.PlaySFX("Dig");
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
