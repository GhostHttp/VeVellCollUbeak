using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private DialogueData dialogueData;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI questionText;
    [SerializeField] private Button[] answerButtons;

    private int currentNodeIndex = 0;

    private void Start()
    {
        if (dialogueData == null || dialogueData.dialogueNodes.Length == 0)
        {
            Debug.LogError("Dialogue data is missing or empty!");
            return;
        }

        ShowDialogueNode(0);
    }

    public void ShowDialogueNode(int nodeIndex)
    {
        if (nodeIndex < 0 || nodeIndex >= dialogueData.dialogueNodes.Length)
        {
            Debug.LogError("Invalid node index!");
            return;
        }

        currentNodeIndex = nodeIndex;
        var currentNode = dialogueData.dialogueNodes[nodeIndex];

        // ������������� ������
        questionText.text = currentNode.question;

        // �������� ��� ������ �������
        foreach (var button in answerButtons)
        {
            button.gameObject.SetActive(false);
        }

        // ������������� ������
        for (int i = 0; i < currentNode.answers.Length && i < answerButtons.Length; i++)
        {
            answerButtons[i].gameObject.SetActive(true);
            answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = currentNode.answers[i];

            // ������� ���������� ����������� � ��������� �����
            answerButtons[i].onClick.RemoveAllListeners();
            int answerIndex = i;
            answerButtons[i].onClick.AddListener(() => OnAnswerSelected(answerIndex));
        }

        dialoguePanel.SetActive(true);
    }

    private void OnAnswerSelected(int answerIndex)
    {
        // ����� ����� �������� ������ �������� � ���������� ���� �������
        // ��������, ����� ������������ answerIndex ��� ����������� ���������� ����

        Debug.Log($"Selected answer: {answerIndex} for question: {currentNodeIndex}");

        // ������� ������ - ������� � ���������� �������
        int nextNodeIndex = currentNodeIndex + 1;
        if (nextNodeIndex < dialogueData.dialogueNodes.Length)
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
        dialoguePanel.SetActive(false);
        Debug.Log("Dialogue ended");
    }
}