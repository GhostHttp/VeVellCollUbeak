using TMPro;
using UnityEngine;

public class ScoreGame : MonoBehaviour
{
    public int Score;
    [SerializeField]

    private TMP_Text _textScoreTMPro;

    void Update()
    {
        _textScoreTMPro.text = $"Score:{Score}";
    }
}
