using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.EventSystems;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

public class StartGame : MonoBehaviour, IPointerClickHandler
{
    public string sceneToLoad;
    public Slider progressBar;

    public void OnPointerClick(PointerEventData eventData)
    {
        StartCoroutine(LoadSceneAsync());
    }

    private IEnumerator LoadSceneAsync()
    {
        Debug.Log("Yo");
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneToLoad);
        asyncOperation.allowSceneActivation = false;
        while(!asyncOperation.isDone)
        {
            float progress = Mathf.Clamp01(asyncOperation.progress / 0.9f);

            progressBar.value = progress;

            yield return null;
        }

        SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneToLoad));
    }
}
