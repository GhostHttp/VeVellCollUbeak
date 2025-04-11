using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class standartButton : MonoBehaviour, IPointerClickHandler
{
    public string sceneActivatedName;

    public void OnPointerClick(PointerEventData eventData)
    {
        SceneManager.LoadScene(sceneActivatedName);
    }
}
