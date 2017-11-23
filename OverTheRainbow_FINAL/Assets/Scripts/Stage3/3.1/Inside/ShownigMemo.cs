using UnityEngine;
using System.Collections;

public class ShownigMemo : MonoBehaviour {
    public GameObject camera;
    Camera cameraControl;
    bool zoomIn;
    Vector3 firstPos;
    public GameObject note;

    // Use this for initialization
    void Start ()
    {
        note = GameObject.Find("note");
        camera = GameObject.Find("MainCamera");
        cameraControl = camera.GetComponent<Camera>();
        firstPos = new Vector3(20, 0, -10);

        note.SetActive(false);
	}

    void OnMouseDown()
    {
        if (gameObject.transform.position.y<=-4.3f)
        {
            if (!zoomIn)
            {
                note.SetActive(true);
                zoomIn = true;
                //zoomInFunc();
            }
            else
            {
                note.SetActive(false);
                zoomIn = false;
                //zoomOutFunc();
            }
        }
       
    }

    void zoomInFunc()
    {
        cameraControl.orthographicSize = 1.5f;
        camera.transform.position = new Vector3(20, 0, -10);
        zoomIn = true;
    }

    void zoomOutFunc()
    {
        cameraControl.orthographicSize = 5;
        camera.transform.position = firstPos;
        zoomIn = false;
    }
}
