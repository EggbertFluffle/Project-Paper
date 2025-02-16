using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.LightTransport;

public class Movement : MonoBehaviour {
    public Transform transform;
    public float speed = 100.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
    }

    // Update is called once per frame
    void Update() {
        float horiz_dir = speed * ((Input.GetKeyDown("a") ? -1 : 0) + (Input.GetKeyDown("d") ? 1 : 0));
        transform.position = new Vector2(transform.position.x + horiz_dir, 0);
    }
}
