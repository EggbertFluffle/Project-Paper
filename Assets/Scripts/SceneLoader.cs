using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class SceneLoader : MonoBehaviour
{
    public Animator Crossfade;
    public float TransitionTime = 1;

    private void Start() {
        Crossfade.SetTrigger("Start");
    }

    public void LoadScene(string sceneName) => StartCoroutine(StartSceneTransition(sceneName));

    private IEnumerator StartSceneTransition(string name) {
        Debug.Log("Inventory size: " + GameManager.ActiveSave.Inventory.Count);
        Crossfade.SetTrigger("ChangeScene");
        yield return new WaitForSeconds(TransitionTime);

        SceneManager.LoadScene(name);
    }
}
