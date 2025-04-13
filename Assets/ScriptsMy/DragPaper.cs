using UnityEngine;
using UnityEngine.EventSystems;

public class DragPaper : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private RectTransform dragRectT;
    [SerializeField] private Canvas canvas;
    private Rigidbody2D rb;
    private float gravityScale;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        gravityScale = rb.gravityScale;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        rb.gravityScale = 0f;
        rb.linearVelocity = Vector3.zero;
    }

    public void OnDrag(PointerEventData eventData)
    {
        dragRectT.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        rb.gravityScale = gravityScale;
    }
}
