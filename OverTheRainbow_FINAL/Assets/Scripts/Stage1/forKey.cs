using UnityEngine;
using System.Collections;

public class forKey : MonoBehaviour {

    //clearCheck
    GameObject door;
    public doorKnob doorControl;
    GameObject dog;

    public bool canDrag = false;
    private Vector3 screenPoint;



    // Use this for initialization
    void Start () {
        door = GameObject.Find("door3");
        doorControl = door.GetComponent<doorKnob>();
        dog = GameObject.Find("Dog");
    }
	
	// Update is called once per frame
	void Update () {

    }

    void OnMouseDown()
    {
        if (canDrag)
        {
            screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);

        }
    }

    void OnMouseDrag()
    {
        if (canDrag)
        {
            Vector3 currentScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            Vector3 currentPos = Camera.main.ScreenToWorldPoint(currentScreenPoint);
            transform.position = currentPos;
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Lock") && !doorControl.canOpen)
        {
            doorControl.canOpen = true;
            dog.GetComponent<showHint>().nowAct = true;  //문 못 쓰게 만들어벌임
            canDrag = false;
            dog.GetComponent<controlDog>().canGo = true;
            Destroy(this.gameObject);
        }
    }
}
