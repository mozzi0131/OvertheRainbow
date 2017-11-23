using UnityEngine;
using System.Collections;

public class controlCamera : MonoBehaviour {

    public GameObject camera;
    Camera cameraControl;
    bool zoomIn = false;
    bool zoomOut = false;
    Vector3 firstPos;
    float lerpTime1=0.0f;
    public GameObject[] books = new GameObject[2];
    int touchCount = 0;
    GameObject shelfObj;
    bool actNow = false;

    //Drag
    public bool canDrag = false;
    private Vector3 screenPoint;

    GameObject key;

    //Sound
    GameObject audioManager;
    AudioClip foundKey;
    AudioClip bookSlap;

    // Use this for initialization
    void Start () {

        camera = GameObject.Find("MainCamera");
        cameraControl = camera.GetComponent<Camera>();
        firstPos = camera.transform.position;

        key = GameObject.Find("Key");

        audioManager = GameObject.Find("soundManager");
        foundKey = audioManager.GetComponent<audioControl>().foundKey;
        bookSlap = audioManager.GetComponent<audioControl>().bookSlap;
    }


    void OnMouseDown()
    {

        if (!zoomIn)
        {
            zoomInFunc();

        }
        else
        {
            if (key != null)
            {
                if (!key.GetComponent<forKey>().canDrag)
                {
                    touchCount++;
                    StartCoroutine(books[0].GetComponent<shakeSomething>().shake(0.2f));
                    if (touchCount >= 3 && !actNow)
                    {
                        actNow = true;

                        StartCoroutine(downKey(key));
                    }
                }
                else
                {
                    zoomOutFunc();
                }
            }
        }

    }

    void zoomInFunc()
    {
        cameraControl.orthographicSize = 1;
        camera.transform.position = new Vector3(0, 3, -10);
        zoomIn = true;
    }

    void zoomOutFunc()
    {
        cameraControl.orthographicSize = 5;
        camera.transform.position = firstPos;
        zoomIn = false;
    }


    //열쇠 누르면 떨어지게 하는거
    IEnumerator downKey(GameObject obj)
    {
        float countTime = 0.0f;
        Vector3 forceVector = new Vector3(0, -7.37f,-0.99f);
        AudioSource.PlayClipAtPoint(foundKey, new Vector3(0, 0, -10), 0.4f);
        books[0].SetActive(false);
        AudioSource.PlayClipAtPoint(bookSlap, new Vector3(0, 0, -5),0.9f);
        books[1].SetActive(true);
        while (countTime < 1.0f)
        {
            countTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
            obj.transform.position += forceVector * Time.deltaTime;
        }
       // obj.transform.position += new Vector3(0, 0, -0.99f);
        key.GetComponent<forKey>().canDrag = true;
        actNow = false;
    }
}
