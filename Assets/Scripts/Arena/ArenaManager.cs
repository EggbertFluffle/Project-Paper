using UnityEngine;

public class ArenaManager : MonoBehaviour {
    public static ArenaManager Instance;

    public bool PlayerTurn = true;

    public class Attack {
        public float damage;
        public float accuracy;

        public Attack(float _damage, float _accuracy) {
            damage = _damage;
            accuracy = _accuracy;
        }
    }

    private void Awake() {
        if(Instance == null) Instance = this;
    }

    public void SetUpBattle() {
        
    }
}
