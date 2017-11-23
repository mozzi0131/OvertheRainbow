using UnityEngine;
using System.Collections;

public class SecondArrowScript : MonoBehaviour {

    public GameObject camera;
    public GameObject CameraMoving;
    public GameObject Dog;

    // Use this for initialization
    void Start()
    {
        camera = GameObject.Find("MainCamera");
        CameraMoving = GameObject.Find("CameraMoving");
        Dog = GameObject.Find("Dog");
    }


    void OnMouseDown()
    {
        Debug.Log(gameObject.name);
        if (gameObject.name == "Left")
        {
            Debug.Log("leftarrow picked");
            // StartGame.transform.Translate(-20, 0, 0);
            camera.transform.Translate(-20, 0, 0);
            Dog.transform.Translate(-20, 0, 0);
            gameObject.transform.parent.Translate(-20, 0, 0);
            CameraMoving.GetComponent<SecondCameraMoving>().Setting();
        }
        if (gameObject.name == "Right")
        {
            Debug.Log("rightarrow picked!");
            //StartGame.transform.Translate(20, 0, 0);
            camera.transform.Translate(20, 0, 0);
            Dog.transform.Translate(20, 0, 0);
            gameObject.transform.parent.Translate(20, 0, 0);
            CameraMoving.GetComponent<SecondCameraMoving>().Setting();
        }
    }
}
