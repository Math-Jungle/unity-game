using UnityEngine;
using UnityEngine.EventSystems;

public class AppleDragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector2 offset;

    private void Awake()
    {
        canvas = GetComponentInParent<Canvas>();
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        offset = rectTransform.anchoredPosition - eventData.position / canvas.scaleFactor;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 0.6f; // Make the apple semi-transparent while dragging
        canvasGroup.blocksRaycasts = false; // Allow the apple to be dragged over other UI elements
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition = eventData.position / canvas.scaleFactor + offset;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f; // Restore the apple's opacity
        canvasGroup.blocksRaycasts = true; // Allow the apple to be dropped on UI elements
    }
}
