using UnityEngine;
using System.Collections;

public class MakeBridge : MonoBehaviour {
    private Vector3 screenPoint;
    // Use this for initialization
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

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.name=="bigholeground")
        {
            gameObject.transform.localPosition = new Vector3(4.37f, -4.5f, 0);
        }
    }
}
