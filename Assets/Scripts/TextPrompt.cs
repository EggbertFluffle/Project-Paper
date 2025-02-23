using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class TextPrompt : MonoBehaviour {
    public Button Button;
    public Image TextPanel;
    public TextMeshProUGUI Text;

    public UnityEvent OnClicked = new UnityEvent();

    [TextArea]
    public string contents;

    [Space(10)]
    public int waitedFrames = 0;
    public int frameWait = 20;
    bool typewriting = true;

    public void Start() {
        Text.text = string.Empty;
    }

    public void FixedUpdate() {
        if(typewriting) {
            if(waitedFrames >= frameWait) {
                waitedFrames = 0;
                TypeWrite();
            }
            waitedFrames++;
        }
    }

    public void TypeWrite() {
        Text.text = contents.Substring(0, Text.text.Length + 1);
        if(Text.text.Length == contents.Length) {
            typewriting = false;
        }
    }

    public void HandleClick() {
        OnClicked.Invoke();

        if(typewriting) {
            typewriting = false;
            Text.text = contents;
        } else {
            Destroy(gameObject);
        }
    }
}
