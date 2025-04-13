using System;
using UnityEngine;
using UnityEngine.UI;

public class ProgressManager : MonoBehaviour
{
    [SerializeField] private JobBoardManager _jobBoardManager;
    [SerializeField] private DialogueManager _dialogManager;
    [SerializeField] private Image _progressBar;
    [SerializeField] private float _progressPercent;
    [SerializeField] private GameObject _winWindow;
    [SerializeField] private AudioSource _winsound;

    private event Action<float> OnProgressCahnged;
    private bool _win = false;

    private void Awake()
    {
        _jobBoardManager.OnVacancyResponded += ChangeProgressApply;
        _dialogManager.OnAnswerSelected += ChangeProgressAnswer;
        _progressBar.fillAmount = 0;      
    }

    private void Update()
    {
        if (_progressPercent > 100 && !_win) 
        {
            _win = true;
            _winWindow.SetActive(true);
            _winsound.PlayOneShot(_winsound.clip);
        }
    }

    private void ChangeProgressApply(bool isSuccess)
    {
        _progressPercent += 3f;
        _progressBar.fillAmount = _progressPercent / 100;
        OnProgressCahnged?.Invoke(_progressPercent);
    }

    public void AddProgress()
    {
        _progressPercent += 3f;
        _progressBar.fillAmount = _progressPercent / 100;
        OnProgressCahnged?.Invoke(_progressPercent);
    }

    private void ChangeProgressAnswer(bool isRight)
    {
        if (isRight)
        {
            _progressPercent += 5f;
        }
        else 
        {
            _progressPercent -= 5f;
        }
        _progressBar.fillAmount = _progressPercent / 100;
        OnProgressCahnged?.Invoke(_progressPercent);
    }
}
