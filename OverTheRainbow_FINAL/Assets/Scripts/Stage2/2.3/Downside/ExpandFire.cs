using UnityEngine;
using System.Collections;

public class ExpandFire : MonoBehaviour {

    public float ZoomDist;
    private GameObject MatchLight;
    bool bright;
    Vector3 screenPoint;
    private BoxCollider2D objectcollider;
    public bool[] gettouch = new bool[2];
    GameObject camera;
    bool play;

    //audio
    AudioClip fire;
    AudioClip water;
    GameObject audioManager;

    void Start()
    {
        gettouch[0] = false;
        gettouch[1] = false;
        play = false;
        bright = false;
        camera = GameObject.Find("MainCamera");
        MatchLight = GameObject.Find("MatchLight");
        MatchLight.GetComponent<Light>().intensity = 6;
        MatchLight.GetComponent<Light>().range = 2.5f;
        objectcollider = gameObject.GetComponent<BoxCollider2D>();

        audioManager = GameObject.Find("soundManager");
        fire = audioManager.GetComponent<audioControl>().fire;
        water = audioManager.GetComponent<audioControl>().Water;
    }

    void Update()
    {
        if(camera.transform.position.y<-5)
        {
            if (play == false)
            {
                AudioSource.PlayClipAtPoint(fire, new Vector3(0, 0, -15));
                AudioSource.PlayClipAtPoint(water, new Vector3(0, 0, -10));
                play = true;
            }

        }

        //오브젝트 크기가 작을 때만 작동
        if (MatchLight.GetComponent<Light>().intensity==6 && MatchLight.GetComponent<Light>().range == 2.5)
        {
            //터치를 두개 이상 받으면
            if (Input.touchCount == 2)
            {
                Touch touchZero = Input.GetTouch(0);
                Touch touchOne = Input.GetTouch(1);

                Vector3 touch0 = Camera.main.ScreenToWorldPoint(new Vector3(touchZero.position.x, touchZero.position.y, 0));
                Vector3 touch1 = Camera.main.ScreenToWorldPoint(new Vector3(touchOne.position.x, touchOne.position.y, 0));

                Vector3 middletouch = (touch0 + touch1) / 2;
                Vector2 centerPos = new Vector2(middletouch.x, middletouch.y);

                if (gameObject.GetComponent<BoxCollider2D>().bounds.Contains(centerPos))
                {
                    // Find the position in the previous frame of each touch.
                    Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
                    Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

                    // Find the magnitude of the vector (the distance) between the touches in each frame.
                    float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
                    float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

                    // Find the difference in the distances between each frame.
                    float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;
                    float change = -deltaMagnitudeDiff;//deltaMagnitudeDiff는 손가락 사이의 벌어짐을 -값으로 받아온다.
                    if (deltaMagnitudeDiff < 0)
                    {
                        MatchLight.GetComponent<Light>().intensity = 6;
                        MatchLight.GetComponent<Light>().range = 5f;
                    }
                }
            }
        }
        else if (MatchLight.GetComponent<Light>().intensity == 6 && MatchLight.GetComponent<Light>().range == 5)
           bright = true;
    }

    void OnMouseDown()
    {
        if(bright == true)
            screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
    }

    void OnMouseDrag()
    {
        if(bright==true)
        {
            Vector3 currentScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            Vector3 currentPos = Camera.main.ScreenToWorldPoint(currentScreenPoint);
            transform.position = currentPos;
        }
    }
}
