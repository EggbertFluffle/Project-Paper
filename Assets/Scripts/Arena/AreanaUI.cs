using System;
using UnityEngine;

public class AreanaUI : MonoBehaviour {
    public static AreanaUI Instance;

    private void Awake() {
        if(Instance == null) Instance = this;
    }

    public void LoadAttackOptions(BodyPart[] bodyParts) {
       // Attacks will be present on body parts 
    }
}
