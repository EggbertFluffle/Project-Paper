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

    private SpriteRenderer SpriteRenderer;

    private void Start() {
        BodyParts = new List<BodyPartRef>();
        SpriteRenderer = GetComponent<SpriteRenderer>();

        GenerateLimbs();
    }

    private void GenerateLimbs() 
    {
        for(int i = 0; i < 4; i++) 
        {
            if (GameManager.ActiveSave.CurrentBoss == 0)
            {
                // The top graves should produce 2 random arms, and the bottom graves should have 2 random legs

                // Top Graves
                if (GraveIndex < 2 && i < 2)
                {
                    BodyParts.Add(new BodyPartRef(GameManager.AllArms[Random.Range(0, GameManager.AllArms.Count)]));
                }
                // Bottom Graves
                else if (GraveIndex > 1 && i > 1)
                {
                    BodyParts.Add(new BodyPartRef(GameManager.AllLegs[Random.Range(0, GameManager.AllLegs.Count)]));
                }
                else
                {
                    BodyParts.Add(null);
                }
            }
            else
            {
                BodyParts.Add(Random.Range(0.0f, 1.0f) < GRManager.NoLimbChance
                    // Limb did not make it lol
                    ? null
                    // Set the body part to a arm or leg depending on limb slot
                    : new BodyPartRef(i < 2 
                        ? GameManager.AllArms[Random.Range(0, GameManager.AllArms.Count)] 
                        : GameManager.AllLegs[Random.Range(0, GameManager.AllLegs.Count)]));
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
