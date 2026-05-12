using UnityEngine;

public class DialogChoice: MonoBehaviour
{
    [Header ("Emotion")] // This is just for the editor, it doesn't do anything
    public EmoteTypes emoteType;
    public int emoteValue = 1;

    //[HideInInspector]
    public DialogueManager dialogueManager;

    public void MakeChoice() // This method will be called when the player clicks on this choice
    {
       dialogueManager.ChooseAnswer(this);
    }

}
