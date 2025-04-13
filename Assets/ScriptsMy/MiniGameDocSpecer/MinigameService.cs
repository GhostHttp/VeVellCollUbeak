using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class MinigameService : MonoBehaviour
{
    [SerializeField]
    private GameObject _miniGameManager;
    [SerializeField]
    private List<RectTransform> _spawnPoint = new List<RectTransform>();
    [SerializeField]
    private List<PaperSO> _paperList = new List<PaperSO>();
    [SerializeField]
    private List<GameObject> _createdObject = new List<GameObject>();
    [SerializeField]
    private int _secoundsToEnd = 30;
    [SerializeField]
    private TMP_Text _timerText;

    private void Start()
    {
        StartSpawn();
        StartCoroutine(MinigameTime(_secoundsToEnd));
        _timerText.text = "Осталось времени " + _secoundsToEnd.ToString();
    }

    public void StartSpawn()
    {
        StartCoroutine(SpawnDelay(Random.Range(0.25f, 2)));
    }

    private void MakePaper()
    {
        int numberEtype = Random.Range(0, 2);
        var randomSpawnPointIndex = Random.Range(0, _spawnPoint.Count);
       GameObject objectPaper = Instantiate(_paperList[numberEtype].prefab, _spawnPoint[randomSpawnPointIndex]);
        _createdObject.Add(objectPaper);
    }

    private IEnumerator SpawnDelay(float second)
    {
        yield return new WaitForSeconds(second);
        MakePaper();
        StartSpawn();
    }

    private IEnumerator MinigameTime(float second)
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            second--;
            _timerText.text = "Осталось времени " + second.ToString();
            if (second <= 0)
            {
                StopSpawn();
            }
        }
    }

    public void StopSpawn()
    {
        StopAllCoroutines();
        foreach (var item in _createdObject)
        {
            Destroy(item);
        }
    }
}