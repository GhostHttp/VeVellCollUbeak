using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;

public class DocSpeek : MonoBehaviour
{
    [SerializeField]
    private RectTransform[] _spawnPoint;
    [SerializeField]
    private List<PaperSO> _paperList = new List<PaperSO>();
    [SerializeField]

    private void Start()
    {
        StartSpawn();
    }

    private void StartSpawn()
    {
        StartCoroutine(SecondTime(Random.Range(2, 5)));
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
