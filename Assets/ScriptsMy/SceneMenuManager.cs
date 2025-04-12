using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEditor.SearchService;

public class SceneLoadManager : MonoBehaviour
{
    private string sceneCancelLoad;
    public Slider progressBar;
    public GameObject sceneLoad;

    void Start()
    {
        sceneLoad.SetActive(false);
    }

    public void LoadScene(string SceneToLoad)
    {
        if (SceneManager.GetSceneByName(SceneToLoad) != null)
        {
            sceneCancelLoad = SceneToLoad;
            sceneLoad.SetActive(true);

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
            float progress = Mathf.Clamp01(asyncOperation.progress / 0.9f);

            progressBar.value = progress;

            yield return null;
        }

        SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneCancelLoad));
    }
}
