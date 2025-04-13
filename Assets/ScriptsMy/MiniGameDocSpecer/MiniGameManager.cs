using UnityEngine;

public class MiniGameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _TMPObjectFail;
    [SerializeField]
    private ScoreGame _scoreGame;

    void Update()
    {
        if(_scoreGame.Score == 0)
        {
            _TMPObjectFail.SetActive(true);
        }
    }
}
