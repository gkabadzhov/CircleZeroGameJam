using OTBG.Utilities.General;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseAvoidUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private float moveThreshold = 100f; // Distance within which the object starts moving away
    [SerializeField]
    private float maxMoveDistance = 50f; // Maximum distance the object can move away from its original position
    [SerializeField]
    private float moveSpeed = 5f; // Speed at which the object moves away from the mouse

    private RectTransform rectTransform;
    private Vector3 originalPosition;
    private bool isMouseOver = false;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        originalPosition = rectTransform.position;
    }

    private void Update()
    {
        if (isMouseOver)
        {
            MoveAwayFromMouse();
        }
        else
        {
            ReturnToOriginalPosition();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isMouseOver = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isMouseOver = false;
    }

    private void MoveAwayFromMouse()
    {
        Vector2 mousePosition = Input.mousePosition;
        Vector2 direction = (rectTransform.position - Vector3Utils.ConvertVector2To3(mousePosition)).normalized;
        float distance = Vector2.Distance(mousePosition, rectTransform.position);

        if (distance < moveThreshold)
        {
            Vector3 newPosition = rectTransform.position + (Vector3)direction * moveSpeed * Time.deltaTime;
            if (Vector3.Distance(newPosition, originalPosition) <= maxMoveDistance)
            {
                rectTransform.position = newPosition;
            }
        }
    }

    private void ReturnToOriginalPosition()
    {
        rectTransform.position = Vector3.Lerp(rectTransform.position, originalPosition, Time.deltaTime * 5f);
    }
}
