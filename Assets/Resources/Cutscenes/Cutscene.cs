using System;
using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public class Cutscene
{
    [Serializable]
    public class Event
    {
        public int ActiveTextBox;

        [TextArea]
        public string Text;

        public List<int> ActiveImages;
    }

    public GameObject ParentObject;

    [Space(10)]
    public List<Event> AllEvents;

    public List<GameObject> AllImages;
    public List<TextPrompt> AllTextBoxes;

    [Space(10)]
    public UnityEvent OnCutsceneFinished;

    public void Play(int eventIndex) {
        ParentObject.SetActive(true);
        AudioManager.PlaySFX("Paper Rustle");

        Event currentEvent = AllEvents[eventIndex];

        foreach (var prompt in AllTextBoxes.Where(p => p != null))
        {
            prompt.gameObject.SetActive(false);
        }

        for (int i = 0; i < AllImages.Count; i++)
        {
            AllImages[i].SetActive(currentEvent.ActiveImages.Contains(i));
        }

        AllTextBoxes[currentEvent.ActiveTextBox].gameObject.SetActive(true);
        AllTextBoxes[currentEvent.ActiveTextBox].contents = currentEvent.Text;
    }

    public void Finish(bool hideImages = true)
    {
        foreach (var prompt in AllTextBoxes.Where(p => p != null))
        {
            prompt.gameObject.SetActive(false);
        }

        if (hideImages)
        {
            for (int i = 0; i < AllImages.Count; i++)
            {
                AllImages[i].SetActive(false);
            }
        }

        ParentObject.SetActive(!hideImages);
        OnCutsceneFinished.Invoke();
    }

}
