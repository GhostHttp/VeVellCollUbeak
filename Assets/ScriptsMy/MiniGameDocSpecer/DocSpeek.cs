using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;

public class DocSpeek : MonoBehaviour
{
    [SerializeField]
    private GameObject _miniGameManager;
    [SerializeField]
    private RectTransform[] _spawnPoint;
    [SerializeField] 
    private ScoreGame _scoreGame;
    [SerializeField]
    private List<PaperSO> _paperList = new List<PaperSO>();
    [SerializeField]

    private void Start()
    {
        _scoreGame.Score += 1;
        MakePaper();
        StartCoroutine(SecondTime(Random.Range(2, 5)));
    }

    private void StartSpawn()
    {
        if(_scoreGame.Score > 0)
        {
           StartCoroutine(SecondTime(Random.Range(2, 5)));
        }
    }

    private void MakePaper()
    {
        int numberEtype = Random.Range(0, 2);
        if ((int)(_paperList[numberEtype].etypePaper) == numberEtype)
        {
            GameObject objectPaper = Instantiate(_paperList[numberEtype].prefab, _spawnPoint[Random.Range(0, _spawnPoint.Length)]);
        }
    }

    private IEnumerator SecondTime(float second)
    {
        yield return new WaitForSeconds(second);
        MakePaper();
        StartSpawn();
    }
}