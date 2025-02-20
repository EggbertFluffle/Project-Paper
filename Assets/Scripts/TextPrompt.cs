using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextPrompt : MonoBehaviour {
    public Button Button;
    public Image TextPanel;
    public TextMeshProUGUI Text;

    public string contents;
    public int waitedFrames = 0;
    public int frameWait = 20;
    bool typewriting = true;

    public void Start() {
        Text.text = "";
    }

    public void FixedUpdate() {
        if(typewriting) {
            if(waitedFrames >= frameWait) {
                waitedFrames = 0;
            }
            frameWait++;
        }
    }

    public void TypeWrite() {
        Text.text = contents.Substring(0, Text.text.Length);
        if(Text.text.Length == contents.Length) {
            typewriting = false;
        }
    }

    public void HandleClick() {
        if(typewriting) {
            typewriting = false;
            Text.text = contents;
        } else {
            Destroy(gameObject);
        }
    }
}
