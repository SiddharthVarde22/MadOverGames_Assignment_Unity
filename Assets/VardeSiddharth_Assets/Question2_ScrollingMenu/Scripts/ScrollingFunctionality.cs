using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ScrollingFunctionality : MonoBehaviour, IBeginDragHandler
{
    [SerializeField]
    private ScrollRect scrollRectOfUpperSection;
    [SerializeField]
    private float scrollSpeed = 20;

    private float scrollDifference = 0.25f;
    private float targetedPosition = 0;

    // Update is called once per frame
    private void Update()
    {
        targetedPosition = Mathf.Clamp(targetedPosition, 0, 1);

        if (scrollRectOfUpperSection.horizontalNormalizedPosition != targetedPosition)
        {
            AnimateTheScroll();
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (eventData.delta.x > 0)
        {
            ChangeTargetPosition(targetedPosition -= scrollDifference);
        }
        else if(eventData.delta.x < 0)
        {
            ChangeTargetPosition(targetedPosition += scrollDifference);
        }
    }

    public void ChangeTargetPosition(float position)
    {
        targetedPosition = position;
    }

    private void AnimateTheScroll()
    {
        scrollRectOfUpperSection.horizontalNormalizedPosition =
                Mathf.Lerp(scrollRectOfUpperSection.horizontalNormalizedPosition, targetedPosition, scrollSpeed * Time.deltaTime);
    }
}
