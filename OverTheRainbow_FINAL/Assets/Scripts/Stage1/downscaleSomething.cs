using UnityEngine;
using System.Collections;

public class downscaleSomething : MonoBehaviour
{


    //드래그와 축소
    public float ZoomDist;
    public bool canDrag = false;
    private Vector3 screenPoint;

    //1.2
    bool openTheDoor = false;
    public bool actNow = false;
    bool canDownscale = true;
    public bool onKey = false;
    public bool first = false;
    public bool second = false;
    float scaleValue=0.0f;
    GameObject door;
    public doorKnob doorControl;
    GameObject dog;

    void Start()
    {
        door = GameObject.Find("door2");
        doorControl = door.GetComponent<doorKnob>();
        dog = GameObject.Find("Dog");
    }

    void Update()
    {
        
        if (transform.localScale.x <= 0.4 && !(doorControl.canOpen)&& transform.localScale.y <= 0.4)
        {
            canDownscale = false;
            setScale(this.gameObject);
            canDrag = true;
            
        }
        //오브젝트 크기가 작을 때만 작동
        if (canDownscale && !actNow)
        {
            if (!first)
            {
                downScaleThis();
                if (actNow)
                    StartCoroutine(downscaleTest(0.2f));
            }else if (!second)
            {
                downScaleThis();
                if (actNow) transform.localScale = new Vector3(0.35f, 0.35f, 1);
            }
        }

        checkCanClear();


        setScale(this.gameObject);

        //컴에서
        //if (Input.GetKey(KeyCode.UpArrow))
        //{
            
        //    StartCoroutine(downscaleTest(0.2f));
        //}

    }

    void checkCanClear()
    {
        if (!canDrag && second)
        {
            transform.localScale = new Vector3(0.35f, 0.35f, 1);
        }
    }

void downScaleThis()
    {
        if (gameObject.transform.localScale.x > 0.2 && gameObject.transform.localScale.y > 0.2)
        {
            //터치를 두개 이상 받으면
            if (Input.touchCount == 2)
            {
                // Store both touches.
                Touch touchZero = Input.GetTouch(0);
                Touch touchOne = Input.GetTouch(1);

                Vector3 touch0 = Camera.main.ScreenToWorldPoint(new Vector3(touchZero.position.x, touchZero.position.y,0));
                Vector3 touch1 = Camera.main.ScreenToWorldPoint(new Vector3(touchOne.position.x, touchOne.position.y,0));

                Vector3 middletouch = (touch0 + touch1) / 2;
                Vector2 centerPos = new Vector2(middletouch.x, middletouch.y);

                // TouchText.text = "collider bounds" + gameObject.GetComponent<BoxCollider2D>().bounds + "text1" + touch0 + "text2" + touch1 + "middel" + centerPos;

                
                if (onKey)
                {
                    //TouchText.text = "gettouched!";
                    // Find the position in the previous frame of each touch.
                    Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
                    Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

                    // Find the magnitude of the vector (the distance) between the touches in each frame.
                    float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
                    float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

                    // Find the difference in the distances between each frame.
                    float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;
                    //if (deltaMagnitudeDiff > 1.0f) deltaMagnitudeDiff = 0.6f;
                    float change = -deltaMagnitudeDiff * Time.deltaTime;//deltaMagnitudeDiff는 손가락 사이의 벌어짐을 -값으로 받아온다.
                    if (gameObject.transform.localScale.x < 0.6f)
                        change = change / 3;

                    if (deltaMagnitudeDiff > 0&&canDownscale)
                    {
                        if (!actNow && first)
                        {
                            actNow = true;
                            second = true;
                        }
                        if (!first)
                        {
                            first = true;
                            actNow = true;
                        }
                        
                    }
                }

            }
        }
        else
        {
            canDownscale = false;

        }

        
    }

    IEnumerator downscaleTest(float setTime)
    {
        float countTime = 0.0f;
        float ndvalue = -0.5f * Time.deltaTime * 1/ setTime;

        while (countTime < setTime)
        {
            yield return new WaitForEndOfFrame();
            countTime += Time.deltaTime;            
            if (canDownscale)
            {               
                    gameObject.transform.localScale += new Vector3(ndvalue, ndvalue, 0);
            }
            if (gameObject.transform.localScale.x <= 0.35 && gameObject.transform.localScale.y <= 0.35)
            {
                canDownscale = false;
            }
        }
        actNow = false;
    }

    void OnMouseDown()
    {
 
            onKey = true;
        if (canDrag)
        {
            screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        }
    }


    void OnMouseDrag()
    {
        onKey = true;
        if (canDrag)
        {
            Vector3 currentScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            Vector3 currentPos = Camera.main.ScreenToWorldPoint(currentScreenPoint);
            transform.position = currentPos;
        }

    }

    void OnMouseUp()
    {
        onKey = false;
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Lock") && !doorControl.canOpen)
        {
            if (transform.localScale.x > 0.2f && transform.localScale.y > 0.2f)
            {
                doorControl.canOpen = true;
                canDrag = false;
                dog.GetComponent<controlDog>().canGo = true;
            }
        }
    }

    void setScale(GameObject object1)
    {
        if (object1.transform.localScale.x < 0.35f || object1.transform.localScale.y < 0.35f)
        {
            object1.transform.localScale = new Vector3(0.35f, 0.35f, 1);
        }
    }
}
