using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BadCancelGame : MonoBehaviour
{
    [SerializeField] GameObject EndScene;
    [SerializeField] string _sceneName;

    void Start()
    {
        EndScene.SetActive(false);
        StartCoroutine(SecondTime(900));
    }

    private IEnumerator SecondTime(int second)
    {
        yield return new WaitForSeconds(second);
    }

    private void TheEndGame()
    {
        EndScene.SetActive(true);
        StartCoroutine(SecondTime(15));
        SceneManager.LoadScene(_sceneName);
    }
}
