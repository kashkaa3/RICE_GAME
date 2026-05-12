using UnityEngine;
using TMPro;
using Ink.Runtime;

public class DialogueManager : MonoBehaviour
{
    [Header("Ink")]
    public TextAsset inkJSON;
    Story story;

    [Header ("UI")]
    public GameObject DialoguePanel;
    public TextMeshProUGUI DialogueText;
    public TextMeshProUGUI NameText;

    [Header ("Choices")]
    public GameObject angrySphere;
    public GameObject happySphere;
    public GameObject sadsphere;
    public GameObject silentSphere;


    [Header ("Spawn Points")]
    public Transform[] SpawnPoints;

    [Header("Systems")]
    public EmotionSystem emotionSystem;


    private void Start()
    {
        // HideChoices();
        DialoguePanel.SetActive(false);
        //story = new Story(inkJSON.text);
        HideChoices();
        StartDialogue();
    }

    public void StartDialogue() // This method will be called to start a dialogue
    {
        Debug.Log("Starting Dialogue");
        story = new Story(inkJSON.text);
        DialoguePanel.SetActive(true); // Show the dialogue panel
        //NameText.text = name; // Set the name text
        //DialogueText.text = line; // reference to the dialogue system, you can replace this with your own dialogue system
        //RandomizeChoicePos();
        //ShowChoices();
        
        ContinueStory();
    }

    public void ContinueStory() // This method will be called to continue the story
    {
        Debug.Log("Continuing Story");
        if (story.canContinue)
        {
            string text = story.Continue(); // Get the next line of dialogue from the story
            Debug.Log (text);
            string[] parts = text.Split(':'); // Split the text into parts using the colon as a delimiter

            if (parts.Length >= 2) { 
                NameText.text = parts[0];
                DialogueText.text = parts[1];
            }
            else
            {
                NameText.text = ""; // If there is no colon, clear the name text
                DialogueText.text = text; // If there is no colon, just display the text as dialogue
            }
        }
        ShowChoices();
    }

    //public void ChooseAnswer(DialogChoice choice) // This method will be called when a choice is made
    //{
    //    switch (choice.emoteType) // Update the emotion points based on the choice made
    //    {
    //        case EmoteTypes.Angry:
    //            emotionSystem.angryPoints += choice.emoteValue;
    //            break;
    //        case EmoteTypes.Happy:
    //            emotionSystem.happyPoints += choice.emoteValue;
    //            break;
    //        case EmoteTypes.Sad:
    //            emotionSystem.sadPoints += choice.emoteValue;
    //            break;
    //        case EmoteTypes.Silent:
    //            emotionSystem.silentPoints += choice.emoteValue;
    //            break;
    //    }
    //    Debug.Log("Angry: " + emotionSystem.angryPoints);
    //    Debug.Log("Happy: " + emotionSystem.happyPoints);
    //    Debug.Log("Sad: " + emotionSystem.sadPoints);
    //    Debug.Log("Silent: " + emotionSystem.silentPoints);
    //    //HideChoices();
    //}

    void ShowChoices() { 
        //foreach (GameObject obj in Choices)
        //{
        //    obj.SetActive(true);
        //}
        HideChoices();
        RandomizeChoicePos();
        foreach (Choice choice in story.currentChoices)
        {
            if (choice.text == "Angry") 
                angrySphere.SetActive(true);
            if (choice.text == "Happy")
                happySphere.SetActive(true);
            if (choice.text == "Sad")
                sadsphere.SetActive(true);
            if (choice.text == "Silent")
                silentSphere.SetActive(true);
        }
    }

    void HideChoices() {
        angrySphere.SetActive(false);
        happySphere.SetActive(false);
        sadsphere.SetActive(false); 
        silentSphere.SetActive(false);
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

        GameObject[] activeChoices = {
            angrySphere,
            happySphere,
            sadsphere,
            silentSphere
        };

        for (int i = 0; i < activeChoices.Length; i++) // Set the position of each choice to the position of the corresponding shuffled spawn point
        {
            activeChoices[i].transform.position = shuffledPoints[i].position;
        }
    }
    
    public void ChooseEmotion(string emotionName) // This method will be called to continue the dialogue based on the choice made
    {
        for (int i =0; i < story.currentChoices.Count; i++)
        {
            if (story.currentChoices[i].text == emotionName) // Find the choice that matches the emotion name
            {
                story.ChooseChoiceIndex(i); // Choose the choice at the corresponding index
                ContinueStory();
                break; // Exit the loop once the choice is found and chosen
            }
        }
    }
}
