using UnityEngine;
using System.Collections;

//치즈에게 붙는 스크립트(확대, 드래그 등)
public class CheeseScript : MonoBehaviour
{
    GameObject Dog;
    GameObject camera;
    GameObject outSide;
    GameObject mouse;

    Vector3 screenPoint;

    public bool getCheese;
    public bool takecheese;
    public bool bigenough;


    // Use this for initialization
    void Start()
    {
        Dog = GameObject.Find("Dog");
        camera = GameObject.Find("MainCamera");
        outSide = GameObject.Find("Outside");
        mouse = GameObject.Find("mouse");

        getCheese = false;
        takecheese = false;//해당 화면에 cheese를 가져왔냐를 판단하는 변수
        bigenough = false; // 확대를 했는가?
    }


    void Update()
    {
        if (getCheese == true)
        {
            //오브젝트 크기가 작을 때만 작동
            if (gameObject.transform.localScale.x < 2 && gameObject.transform.localScale.y < 2)
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
            else if (gameObject.transform.localScale.x >= 2 && gameObject.transform.localScale.y >= 2)
                bigenough = true;
        }
    }

    // Update is called once per frame
    void OnMouseDown()
    {
        if (getCheese == false)//부엌에서 처음 발견했을때
        {
            gameObject.transform.parent = Dog.transform;
            gameObject.transform.localPosition = new Vector3(1.2f, 0.7f, 1);
            getCheese = true;
        }
        else if (getCheese == true)//쥐에게 드래그할때?
        {
            if (camera.transform.position.x == 40)
            {
                gameObject.transform.parent = outSide.transform;
                screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
            }
        }
    }

    void OnMouseDrag()//제대로된 장소+치즈가 있는거 아니면 아니면 못 끈다
    {
        if (getCheese == true && camera.transform.position.x == 40)
        {
            takecheese = true;
            Vector3 currentScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            Vector3 currentPos = Camera.main.ScreenToWorldPoint(currentScreenPoint);
            transform.position = currentPos;
        }
    }



}
