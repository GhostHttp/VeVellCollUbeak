using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreView : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _scoreText;
    [SerializeField]
    private int _currentScore = 0;
    [SerializeField]
    private List<PaperScripts> _paperScipts = new List <PaperScripts>();

    [SerializeField]
    private ProgressManager _progressManager;

    private void Awake()
    {
        _scoreText.text = _currentScore.ToString();
        foreach (var script in _paperScipts)
        {
            script.OnScoreChanged += ChangePoints;
        }
    }
    private void ChangePoints(int score)
    {
        _currentScore = _currentScore + score;
        _progressManager.AddProgress();
        if (_currentScore <= 0) 
        {
            _currentScore = 0;
        }
        _scoreText.text = _currentScore.ToString();
    }
}
