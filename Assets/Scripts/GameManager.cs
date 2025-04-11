using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private JobBoardManager _boardManager;
    [SerializeField] private DialogueManager _dialogueManager;
    private int alertCounter = 0; 


    private void Start()
    {
        _boardManager.OnVacancyResponded += AddAlert;
    }

    private void Update()
    {
        
    }

    private void AddAlert(bool isSuccess)
    {
        if (isSuccess)
        {
            alertCounter++;
        }
    } 
}
