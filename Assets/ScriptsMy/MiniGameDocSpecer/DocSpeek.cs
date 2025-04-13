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
    private List<RectTransform> _spawnPoint = new List<RectTransform>();
    [SerializeField]
    private List<PaperSO> _paperList = new List<PaperSO>();
    [SerializeField]

    private void Start()
    {
        MakePaper();
        StartCoroutine(SecondTime(Random.Range(0.25f, 2)));
    }

    private void StartSpawn()
    {
        StartCoroutine(SecondTime(Random.Range(0.25f, 2)));
    }

    private void MakePaper()
    {
        int numberEtype = Random.Range(0, 2);
        var randomSpawnPointIndex = Random.Range(0, _spawnPoint.Count);
       GameObject objectPaper = Instantiate(_paperList[numberEtype].prefab, _spawnPoint[randomSpawnPointIndex]);
    }

    private IEnumerator SecondTime(float second)
    {
        yield return new WaitForSeconds(second);
        MakePaper();
        StartSpawn();
    }
}