using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadManager : MonoBehaviour
{
    private string sceneCancelLoad;

    public void LoadScene(string SceneToLoad)
    {
        if (SceneManager.GetSceneByName(SceneToLoad) != null)
        {
            sceneCancelLoad = SceneToLoad;

            StartCoroutine(LoadSceneAsync());
        }
        else
        {
            Debug.Log("Ошибка в обнаружении сцены!");
        }
    }

    private IEnumerator LoadSceneAsync()
    {
        Debug.Log("Yo");

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneCancelLoad);
        while (!asyncOperation.isDone)
        {
            yield return null;
        }

        SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneCancelLoad));
    }
}
