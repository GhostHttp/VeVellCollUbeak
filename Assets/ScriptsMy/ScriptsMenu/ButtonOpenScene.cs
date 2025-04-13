using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.EventSystems;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

public class buttoOpenScene : MonoBehaviour, IPointerClickHandler
{
    public string nameSceneToLoad;
    public SceneLoadManager loadManager;

    private void Start()
    {
        GameObject sceneManager = GameObject.Find("SceneMenuManager");
        loadManager = sceneManager.GetComponent<SceneLoadManager>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        loadManager.LoadScene(nameSceneToLoad);
    }
}
