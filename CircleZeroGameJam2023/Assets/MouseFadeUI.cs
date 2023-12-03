using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MouseFadeUI : MonoBehaviour
{
    [SerializeField]
    private float fadeDistance = 100f; // Distance within which the object starts fading
    [SerializeField]
    private float minFadeDistance = 50f; // Minimum distance for the object to be fully faded out
    [SerializeField]
    private Canvas canvas; // Reference to the canvas

    private RectTransform rectTransform;
    private Image imageComponent;
    private Color originalColor;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        imageComponent = GetComponent<Image>();
        if (imageComponent != null)
        {
            originalColor = imageComponent.color;
        }
    }

    private void Update()
    {
        Vector2 mousePosition = Input.mousePosition;
        Vector2 uiElementPosition = RectTransformUtility.WorldToScreenPoint(canvas.worldCamera, rectTransform.position);
        float distance = Vector2.Distance(mousePosition, uiElementPosition);

        UpdateFade(distance);
    }

    private void UpdateFade(float distance)
    {
        // Ensure minFadeDistance is not greater than fadeDistance
        minFadeDistance = Mathf.Min(minFadeDistance, fadeDistance);

        float effectiveDistance = Mathf.Clamp(distance, minFadeDistance, fadeDistance);
        float alpha = Mathf.Clamp01(1 - (fadeDistance - effectiveDistance) / (fadeDistance - minFadeDistance));
        imageComponent.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
    }
}