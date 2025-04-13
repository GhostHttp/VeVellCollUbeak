using UnityEngine;
using UnityEngine.EventSystems;

public class DragPaper : MonoBehaviour, IDragHandler
{
    [SerializeField] private RectTransform dragRectT;

    public void OnDrag(PointerEventData eventData)
    {
        dragRectT.anchoredPosition += eventData.delta;
    }
}
