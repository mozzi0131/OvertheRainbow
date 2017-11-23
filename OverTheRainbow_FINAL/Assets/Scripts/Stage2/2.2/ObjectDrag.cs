using UnityEngine;
using System.Collections;

public class ObjectDrag: MonoBehaviour {
    private Vector3 screenPoint;

    void OnMouseDown()
    {
        gameObject.transform.parent = null;
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
    }

    void OnMouseDrag()
    {
        Vector3 currentScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 currentPos = Camera.main.ScreenToWorldPoint(currentScreenPoint);
        transform.position = currentPos;
    }
}
