using UnityEngine;
using System.Collections;

public class ShowMemo : MonoBehaviour {
    public GameObject camera;
    Camera cameraControl;
    bool zoomIn;
    Vector3 firstPos;

    void Start ()
    {
        camera = GameObject.Find("MainCamera");
        cameraControl = camera.GetComponent<Camera>();
        firstPos = camera.transform.position;
        zoomIn = false;

    }
	
	// Update is called once per frame
	void OnMouseDown ()
    {
        if (!zoomIn)
            zoomInFunc();
        else
            zoomOutFunc();
	}

    void zoomInFunc()
    {
        cameraControl.orthographicSize = 1.5f;
        camera.transform.position = new Vector3(-1, 2, -10);
        zoomIn = true;
    }

    void zoomOutFunc()
    {
        cameraControl.orthographicSize = 5;
        camera.transform.position = firstPos;
        zoomIn = false;
    }
}
