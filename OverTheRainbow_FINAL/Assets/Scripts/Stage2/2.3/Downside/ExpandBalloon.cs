using UnityEngine;
using System.Collections;

public class ExpandBalloon : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.name == "Balloon2" && gameObject.GetComponent<BalloonControl>().enoughsize == false)
        {
            if (gameObject.transform.localScale.x < 1 && gameObject.transform.localScale.y < 1)
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
                            gameObject.transform.localScale += new Vector3(change * Time.deltaTime, change * Time.deltaTime, 0);
                        }
                    }
                }
            }
            else if (gameObject.transform.localScale.x >= 1 && gameObject.transform.localScale.y >= 1)
                gameObject.GetComponent<BalloonControl>().enoughsize = true;
        }
    }
}
