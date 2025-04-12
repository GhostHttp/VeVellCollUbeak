using UnityEngine;

public class TMPAuthors : MonoBehaviour
{
    public float speed;

    private void Update()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        float positionX = rectTransform.position.x;
        float positionY = rectTransform.position.y;
        Vector3 positionObject = new Vector3(positionX, positionY+speed, 0);
        rectTransform.position = positionObject;
    }
}
