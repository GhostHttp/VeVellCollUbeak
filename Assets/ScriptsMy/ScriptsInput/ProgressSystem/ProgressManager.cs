using UnityEngine;
using UnityEngine.UI;

public class ProgressManager : MonoBehaviour
{
    [SerializeField] private JobBoardManager _jobBoardManager;
    [SerializeField] private DialogueManager _dialogManager;
    [SerializeField] private Image _progressBar;
    [SerializeField] private float _progressPercent;
    private void Awake()
    {
        _jobBoardManager.OnVacancyResponded += ChangeProgressApply;
        _dialogManager.OnAnswerSelected += ChangeProgressAnswer;
        _progressBar.fillAmount = 0;
    }
    
    private void ChangeProgressApply(bool isSuccess)
    {
        _progressPercent += 0.5f;
        _progressBar.fillAmount = _progressPercent / 100;
    }

    private void ChangeProgressAnswer(bool isRight)
    {
        if (isRight)
        {
            _progressPercent += 1f;
        }
        else 
        {
            _progressPercent -= 1f;
        }
        _progressBar.fillAmount = _progressPercent / 100;
    }
}
