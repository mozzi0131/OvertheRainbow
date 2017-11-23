using UnityEngine;
using System.Collections;

public class DragHammer : MonoBehaviour {
    GameObject board;
    GameObject lyingboard;
    private Vector3 screenPoint;

    // Use this for initialization
    void Start ()
    {
        board = GameObject.Find("WoodBoard");
        lyingboard = GameObject.Find("lyingwoodboard");

        lyingboard.SetActive(false);
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
    // Update is called once per frame
    void OnTriggerEnter2D (Collider2D other)
    {
        if(other.gameObject==board)
        {
            other.gameObject.SetActive(false);
            lyingboard.SetActive(true);
        }
	
	}
}
