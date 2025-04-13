using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class nextScene : MonoBehaviour
{
    [SerializeField] private string nameNextScene;

    void Start()
    {
        StartCoroutine(nextSceneLoad());
    }

    private IEnumerator nextSceneLoad()
    {
        yield return new WaitForSeconds(15);
        SceneManager.LoadScene(nameNextScene);
    }
}
