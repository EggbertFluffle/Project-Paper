using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArenaUI : MonoBehaviour {
    public static ArenaUI Instance;
    public List<AttackButton> AttackButtons;
    public GameObject TextPrompt;

    private void Awake() {
        if(Instance == null) Instance = this;
    }

    public void LoadAttackOptions(List<BodyPartRef> bodyParts) {
        // Put the correct sprites onto player arms
        AttackButtons[0].Text.text = bodyParts[0].Name;
        AttackButtons[1].Text.text = bodyParts[1].Name;
        AttackButtons[2].Text.text = bodyParts[0].SpecialMoveName;
        AttackButtons[3].Text.text = bodyParts[1].SpecialMoveName;
    }

    public void MakeTextPrompt(string text) {
        GameObject prompt = Instantiate(TextPrompt, transform);
        prompt.GetComponent<TextPrompt>().contents = text;
    }
}
