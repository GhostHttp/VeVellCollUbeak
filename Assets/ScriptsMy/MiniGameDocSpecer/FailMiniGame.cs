using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.EventSystems;

public class FailButtonMiniGame : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private ScoreGame _scoreGame;
    [SerializeField] private GameObject _miniGameFailScene;

    public void OnPointerClick(PointerEventData eventData)
    {
        _miniGameFailScene.SetActive(false);
        _scoreGame.Score = 1;
    }
}
