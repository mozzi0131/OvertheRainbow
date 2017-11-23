using UnityEngine;
using System.Collections;

//문에 붙이는 스크립트
public class Password : MonoBehaviour {
    GameObject keypad;
    GameObject camera;
    GameObject Dog;
    GameObject StartGame;
    public bool unlock;
    private int password;
    public int[] inputnum = new int[4];
    int comparenum;

    Camera cameraControl;
    bool zoomIn;
    Vector3 firstPos;

    // Use this for initialization
    void Start ()
    {
        Dog = GameObject.Find("Dog");
        camera = GameObject.Find("MainCamera");
        StartGame = GameObject.Find("StartGame");
        cameraControl = camera.GetComponent<Camera>();
        firstPos = new Vector3(20,0,-10);
        zoomIn = false;

        unlock = true;
        password = 4091;
        for (int i = 0; i < inputnum.Length; i++)
        {
            inputnum[i] = 0;
        }
    }

    void OnMouseDown()
    {
        if(unlock)//unlock이 true라면
        {
            if (!zoomIn)
                zoomInFunc();
            else
                zoomOutFunc();
        }

        else if(unlock == false)
        {
            if(cameraControl.orthographicSize != 5)
            {
                zoomOutFunc();
            }
            else if(cameraControl.orthographicSize == 5)
            {
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
                StartGame.GetComponent<CameraChanging>().Setting();
                Debug.Log("setting called");
            }
        }
    }

    public void Compare()
    {
        Debug.Log(inputnum[0]+"1"+inputnum[1]+"2"+inputnum[2]+"3"+inputnum[3]);
        if (inputnum[0]==4&&inputnum[1]==0&&inputnum[2]==9&&inputnum[3]==10)
        {
            Debug.Log("unlock checked");
            unlock = false;
            Debug.Log(unlock);
        }
        else
        {
            for (int i = 0; i < inputnum.Length; i++)
            {
                inputnum[i] = 0;
            }
        }
    }

    void zoomInFunc()
    {
        cameraControl.orthographicSize = 1.5f;
        camera.transform.position = new Vector3(25.5f, -0.1f, -10);
        zoomIn = true;
    }

    void zoomOutFunc()
    {
        cameraControl.orthographicSize = 5;
        camera.transform.position = firstPos;
        zoomIn = false;
    }
}
