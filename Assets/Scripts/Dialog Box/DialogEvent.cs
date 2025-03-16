using UnityEngine;

[System.Serializable]
// This is used to edit the dialog in the inspector
public class DialogEvent
{
    public string[] messages;
    public AnimationTriggerType animationTrigger;
}


// Trigger Names

public enum AnimationTriggerType
{
    DialogBox,
    WavingAndTalking,
    Talking
}