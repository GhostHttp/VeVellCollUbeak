using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

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
    [SerializeField] private Image callbackground;
    [SerializeField] private RectTransform callPersona;
    [SerializeField] private AudioSource RightAnswer;
    [SerializeField] private AudioSource BadAnswer;

    private int currentNodeIndex = 0;
    private int currentDialogeData = 0;

    public event Action<bool> OnAnswerSelected;
    public event Action OnDialogueEnded;
    private GameObject currentPersona;

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
            var randomDialoge = UnityEngine.Random.Range(0, dialogueData.Length);
            ShowDialogueNode(0, randomDialoge);
            dialoguePanel.SetActive(true);
        }
        else
        {
            noInvitesPanel.SetActive(true);
        }
    }

    public void ShowDialogueNode(int nodeIndex, int dialogeIndex)
    {
        if (currentDialogeData > dialogueData.Length -1)
        {
            return;
        }
        if (nodeIndex < 0 || nodeIndex >= dialogueData[dialogeIndex].dialogueNodes.Length)
        {
            Debug.LogError("Invalid node index!");
            return;
        }

        currentNodeIndex = nodeIndex;
        var currentNode = dialogueData[dialogeIndex].dialogueNodes[nodeIndex];

        questionText.text = currentNode.question;

        var currentBackground = dialogueData[dialogeIndex].DialogeBackground;
        var persona = dialogueData[dialogeIndex].DialogePesona;

        callbackground.sprite = currentBackground;
        if (!currentPersona)
        {
            currentPersona = Instantiate(persona, callPersona);
        }


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
            answerButtons[i].onClick.AddListener(() => SelectAnswer(answerIndex, dialogeIndex));
        }

        dialoguePanel.SetActive(true);
    }

    private void SelectAnswer(int answerIndex, int dialogueIndex)
    {

        Debug.Log($"Selected answer: {answerIndex} for question: {currentNodeIndex}");
        if (answerIndex == dialogueData[dialogueIndex].dialogueNodes[currentNodeIndex].rightAnswerIndex)
        {
            Debug.Log("Правильный ответ");
            RightAnswer.PlayOneShot(RightAnswer.clip);
            OnAnswerSelected?.Invoke(true);
        }
        else 
        {
            BadAnswer.PlayOneShot(BadAnswer.clip);
            OnAnswerSelected?.Invoke(false);
        }

        int nextNodeIndex = currentNodeIndex + 1;
        if (nextNodeIndex < dialogueData[dialogueIndex].dialogueNodes.Length)
        {
            ShowDialogueNode(nextNodeIndex, dialogueIndex);
        }
        else
        {
            EndDialogue();
        }
    }

    private void EndDialogue()
    {
        Destroy(currentPersona);
        OnDialogueEnded?.Invoke();
        currentActiveCalls--;
        dialoguePanel.SetActive(false);
        Debug.Log("Dialogue ended");
    }
}