using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PaperScripts : MonoBehaviour
{
    [SerializeField]
    private GameObject _prefabGoodPaper;
    [SerializeField] 
    private GameObject _prefabBadPaper;
    [SerializeField]
    private ScoreGame _scoreGame;
    [SerializeField]
    private int _DeScorGood;
    [SerializeField]
    private int _DeScorBad;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PaperScripts>() == null)
        {

                if (collision.tag == "BadPaper")
                {
                    _scoreGame.Score += _DeScorBad;
                    Object.Destroy(collision.gameObject);
                }
                else if (collision.tag == "GoodPaper")
                {
                    _scoreGame.Score += _DeScorGood;
                    Object.Destroy(collision.gameObject);
                }
                else
                {
                    Debug.Log("Ошибка произошла при проверке и сравнении объектов!");
                    Debug.Log(collision.gameObject == _prefabGoodPaper);
                    Debug.Log(collision.gameObject == _prefabBadPaper);
                }
        }
    }
}
