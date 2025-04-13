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
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    public void OnDrag(PointerEventData eventData)
    {
        dragRectT.anchoredPosition += eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        rb.gravityScale = gravityScale;
        rb.bodyType = RigidbodyType2D.Dynamic;
    }
}
