using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DownScaleKey : MonoBehaviour {
    public float ZoomDist;
    GameObject Lock;
    private Vector3 screenPoint;
    public bool DownsizeKey;
   // public Text TouchText;

    void Start()
    {
        DownsizeKey = false;
        Lock = GameObject.Find("ClosedLock");

    }

    void Update()
    {
          //오브젝트 크기가 작을 때만 작동
            if (gameObject.transform.localScale.x > 0.5 && gameObject.transform.localScale.y > 0.5)
            {
                //터치를 두개 이상 받으면
                if (Input.touchCount == 2)
                {
                    // Store both touches.
                    Touch touchZero = Input.GetTouch(0);
                    Touch touchOne = Input.GetTouch(1);

                   // Vector3 touch0 = Camera.main.ScreenToWorldPoint(new Vector3(touchZero.position.x, touchZero.position.y, 0));
                    //Vector3 touch1 = Camera.main.ScreenToWorldPoint(new Vector3(touchOne.position.x, touchOne.position.y, 0));

                   // Vector3 middletouch = (touch0 + touch1) / 2;
                    //Vector2 centerPos = new Vector2(middletouch.x, middletouch.y);

                   // TouchText.text = "collider bounds" + gameObject.GetComponent<BoxCollider2D>().bounds + "text1" + touch0 + "text2" + touch1 + "middel" + centerPos;
                    
                    //if (gameObject.GetComponent<BoxCollider2D>().bounds.Contains(centerPos))
                    //{
                        //TouchText.text = "gettouched!";
                        // Find the position in the previous frame of each touch.
                        Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
                        Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

                        // Find the magnitude of the vector (the distance) between the touches in each frame.
                        float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
                        float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

                        // Find the difference in the distances between each frame.
                        float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;
                        if (deltaMagnitudeDiff > 0.4f) deltaMagnitudeDiff = 0.8f;
                        float change = -deltaMagnitudeDiff;//deltaMagnitudeDiff는 손가락 사이의 벌어짐을 -값으로 받아온다.
                        if (deltaMagnitudeDiff > 0)
                        {
                            gameObject.transform.localScale += new Vector3(change * Time.deltaTime, change * Time.deltaTime, 0);
                            Lock.GetComponent<Unlock>().canOpen = true;
                        }
                    //}
                }
            }
        if (gameObject.transform.localScale.x <= 0.55 && gameObject.transform.localScale.y <= 0.55)
        {
            DownsizeKey = true;
            Lock.GetComponent<Unlock>().canOpen = true;
        }
    }

    void OnMouseDown()
    {
        if(DownsizeKey == true)
        {
            screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        }
    }

    void OnMouseDrag()
    {
        if(DownsizeKey == true)
        {
            Vector3 currentScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            Vector3 currentPos = Camera.main.ScreenToWorldPoint(currentScreenPoint);
            transform.position = currentPos;
        } 
    }
}
