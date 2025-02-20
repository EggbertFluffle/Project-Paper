using System;
using UnityEngine;
using System.Collections.Generic;

[Serializable]
public class Cutscene
{
    [Serializable]
    public class Event
    {
        public string Text;
        public List<int> ActiveImages;
    }

    public List<Event> AllEvents;

    public List<GameObject> AllImages;
    public List<TextPrompt> AllTextBoxes;

    public void Play(int index)
    {

    }

    public void Finish()
    {
        
    }

}
