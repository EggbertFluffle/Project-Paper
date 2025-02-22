using UnityEngine;

[CreateAssetMenu(fileName = "Create Boss", menuName = "Boss")]
public class BossBattle : ScriptableObject {
    public string Name;
    
    public Sprite BossSprite;
    public float Scale;
    public int Damage;
    public int Health;
    public float Accuracy;
}
