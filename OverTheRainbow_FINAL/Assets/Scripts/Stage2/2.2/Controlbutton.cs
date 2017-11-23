using UnityEngine;
using System.Collections;

public class Controlbutton : MonoBehaviour {
    public GameObject Camera;
    GameObject cameramoving;
    GameObject Dog;

    // Use this for initialization
    void Start()
    {
        Camera = GameObject.Find("MainCamera");
        cameramoving = GameObject.Find("CameraMoving");
        Dog = GameObject.Find("Dog");
    }

    // Update is called once per frame
    void OnMouseDown()
    {
        if (gameObject.name == "Right")
        {
            Camera.transform.position = new Vector3(0, 0, -10);
            Dog.transform.Translate(20, 0, 0);
            cameramoving.GetComponent<CameraMoving>().SetButton();
            
        }
        if (gameObject.name == "Left")
        {
            Camera.transform.position = new Vector3(-20, 0, -10);
            Dog.transform.Translate(-20, 0, 0);
            cameramoving.GetComponent<CameraMoving>().SetButton();
        }
    }
}
