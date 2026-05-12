using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [Header ("UI")]
    public GameObject DialoguePanel;
    public TextMeshProUGUI DialogueText;
    public TextMeshProUGUI NameText;

    [Header ("Choices")]
    public GameObject[] Choices;

    [Header ("Spawn Points")]
    public Transform[] SpawnPoints;

    [Header("Systems")]
    public EmotionSystem emotionSystem;

    private void Start()
    {
        HideChoices();

        StartDialogue("Passanger", "Hello");
    }

    public void StartDialogue(string name, string line) // This method will be called to start a dialogue
    {
        DialoguePanel.SetActive(true); // Show the dialogue panel
        NameText.text = name; // Set the name text
        DialogueText.text = line; // reference to the dialogue system, you can replace this with your own dialogue system
        RandomizeChoicePos();
        ShowChoices();
    }
    
    public void ChooseAnswer(DialogChoice choice) // This method will be called when a choice is made
    {
        switch (choice.emoteType) // Update the emotion points based on the choice made
        {
            case EmoteTypes.Angry:
                emotionSystem.angryPoints += choice.emoteValue;
                break;
            case EmoteTypes.Happy:
                emotionSystem.happyPoints += choice.emoteValue;
                break;
            case EmoteTypes.Sad:
                emotionSystem.sadPoints += choice.emoteValue;
                break;
            case EmoteTypes.Silent:
                emotionSystem.silentPoints += choice.emoteValue;
                break;
        }
        Debug.Log("Angry: " + emotionSystem.angryPoints);
        Debug.Log("Happy: " + emotionSystem.happyPoints);
        Debug.Log("Sad: " + emotionSystem.sadPoints);
        Debug.Log("Silent: " + emotionSystem.silentPoints);
        //HideChoices();
    }

    void ShowChoices() { 
        foreach (GameObject obj in Choices)
        {
            obj.SetActive(true);
        }
    }

    void HideChoices() {
        foreach (GameObject obj in Choices)
        {
            obj.SetActive(false);
        }
    }

    void RandomizeChoicePos() {
        Transform[] shuffledPoints = (Transform[]) SpawnPoints.Clone(); // Clone the spawn points array to avoid modifying the original

        for (int i = 0; i < shuffledPoints.Length; i++) // Shuffle the spawn points
        {
            Transform temp = shuffledPoints[i]; // Store the current spawn point in a temporary variable
            int randomIndex = Random.Range(i, shuffledPoints.Length); // Generate a random index from the current index to the end of the array
            shuffledPoints[i] = shuffledPoints[randomIndex]; // Swap the current spawn point with the spawn point at the random index
            shuffledPoints[randomIndex] = temp; // Swap the spawn point at the random index with the current spawn point (using the temporary variable)
        }
        for (int i = 0; i < Choices.Length; i++) // Set the position of each choice to the position of the corresponding shuffled spawn point
        {
            Choices[i].transform.position = shuffledPoints[i].position;
        }
    }
}
