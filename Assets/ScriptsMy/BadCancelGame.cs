using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BadCancelGame : MonoBehaviour
{
    [SerializeField] private string _endScene;
    [SerializeField] private TMP_Text secondToBadEnd;

    void Start()
    {
        StartCoroutine(SecondTime(900));
    }

    private IEnumerator SecondTime(int second)
    {
        yield return new WaitForSeconds(second);
        TheEndGame();
    }

    private void TheEndGame()
    {
        SceneManager.LoadScene(_endScene);
    }
}
