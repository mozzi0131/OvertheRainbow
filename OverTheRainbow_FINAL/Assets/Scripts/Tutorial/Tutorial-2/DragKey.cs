using UnityEngine;
using System.Collections;

public class DragKey : MonoBehaviour
{

    private Vector3 screenPoint;

    void Update()
    {

    }


    void OnMouseDown()
    {

        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
    }

    void OnMouseDrag()
    {
        Vector3 currentScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 currentPos = Camera.main.ScreenToWorldPoint(currentScreenPoint);
        transform.position = currentPos;
    }
}

   
