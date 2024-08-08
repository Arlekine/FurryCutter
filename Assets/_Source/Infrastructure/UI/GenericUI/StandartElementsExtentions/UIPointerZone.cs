using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class UIPointerZone : MonoBehaviour
{
    public bool IsPointerInZone
    {
        get
        {
            PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
            eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

            foreach (var raycastResult in results)
            {
                if (raycastResult.gameObject == gameObject)
                    return true;
            }

            return false;
        }
    }

    public bool IsScreenPositionInZone(Vector2 screenPosition)
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = screenPosition;
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

        foreach (var raycastResult in results)
        {
            if (raycastResult.gameObject == gameObject)
                return true;
        }

        return false;
    }
}