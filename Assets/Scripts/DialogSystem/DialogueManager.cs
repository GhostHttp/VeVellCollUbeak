using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private DialogueData[] dialogueData;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private GameObject noInvitesPanel;
    [SerializeField] private TextMeshProUGUI questionText;
    [SerializeField] private Button[] answerButtons;
    [SerializeField] private JobBoardManager _boardManager;
    [Space]
    [SerializeField] private int currentActiveCalls = 0;

    private int currentNodeIndex = 0;
    private int currentDialogeData = 0;

    private void Start()
    {
        _boardManager.OnVacancyResponded += AddAlert;
        if (dialogueData == null || currentDialogeData > dialogueData.Length)
        {
            Debug.LogError("Dialogue data is missing or empty!");
            return;
        }     
    }
    private void AddAlert(bool isSuccess)
    {
        if (isSuccess)
        {
            currentActiveCalls++;
        }
    }

    public void StartDialogue()
    {
        if (currentActiveCalls > 0)
        {
            ShowDialogueNode(0);
            dialoguePanel.SetActive(true);
        }
        else
        {
            noInvitesPanel.SetActive(true);
        }
    }

    public void ShowDialogueNode(int nodeIndex)
    {
        var randomDialoge = Random.Range(0, dialogueData.Length);
        if (currentDialogeData > dialogueData.Length -1)
        {
            return;
        }
        if (nodeIndex < 0 || nodeIndex >= dialogueData[randomDialoge].dialogueNodes.Length)
        {
            Debug.LogError("Invalid node index!");
            return;
        }

        currentNodeIndex = nodeIndex;
        var currentNode = dialogueData[randomDialoge].dialogueNodes[nodeIndex];

        questionText.text = currentNode.question;

        foreach (var button in answerButtons)
        {
            button.gameObject.SetActive(false);
        }

        for (int i = 0; i < currentNode.answers.Length && i < answerButtons.Length; i++)
        {
            answerButtons[i].gameObject.SetActive(true);
            answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = currentNode.answers[i];

            answerButtons[i].onClick.RemoveAllListeners();
            int answerIndex = i;
            answerButtons[i].onClick.AddListener(() => OnAnswerSelected(answerIndex, randomDialoge));
        }

        dialoguePanel.SetActive(true);
    }

    private void OnAnswerSelected(int answerIndex, int dialogueIndex)
    {

        Debug.Log($"Selected answer: {answerIndex} for question: {currentNodeIndex}");

        int nextNodeIndex = currentNodeIndex + 1;
        if (nextNodeIndex < dialogueData[dialogueIndex].dialogueNodes.Length)
        {
            ShowDialogueNode(nextNodeIndex);
        }
        else
        {
            EndDialogue();
        }
    }

    private void EndDialogue()
    {
        currentActiveCalls--;
        dialoguePanel.SetActive(false);
        Debug.Log("Dialogue ended");
    }
}