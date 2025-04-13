using UnityEngine;
using UnityEngine.EventSystems;

public class ExitTheGame : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] GameObject MiniGameOther;

    public void OnPointerClick(PointerEventData eventData)
    {
        MiniGameOther.SetActive(false);
    }
}
