using UnityEngine;
using System.Collections;

public class stage1Controller : MonoBehaviour {

    GameObject dog;
    int miniStageNum;


    //1.1
    GameObject door1;
    public float ZoomDist;
    private BoxCollider2D doorCollider;
    public bool[] gettouch = new bool[2];
    bool nowAct = false;

    //1.2
    GameObject door2;


    //1.3 Shake
    float accelerometerUpdateInterval;
    // The greater the value of LowPassKernelWidthInSeconds, the slower the filtered value will converge towards current input sample (and vice versa).
    float lowPassKernelWidthInSeconds;
    // This next parameter is initialized to 2.0 per Apple's recommendation, or at least according to Brady! ;)
    float shakeDetectionThreshold;
    private float lowPassFilterFactor;
    private Vector3 lowPassValue;
    private Vector3 acceleration;
    private Vector3 deltaAcceleration;
    private float starttime;
    GameObject color;
    Rigidbody2D colorRb;
    bool firstShake=false;


    void Awake()
    {



        dog = GameObject.Find("Dog");
        door1 = GameObject.Find("door1");
        gettouch[0] = false;
        gettouch[1] = false;
        
        

            
    }

    // Use this for initialization
    void Start () {

        //1.3 shake
        accelerometerUpdateInterval = 1.0f / 60.0f;
        lowPassKernelWidthInSeconds = 1.0f;
        shakeDetectionThreshold = 2.0f;
        lowPassKernelWidthInSeconds = accelerometerUpdateInterval / lowPassKernelWidthInSeconds;
        lowPassValue = Vector3.zero;
        shakeDetectionThreshold *= shakeDetectionThreshold;
        lowPassValue = Input.acceleration;
        starttime = Time.time;

        //판정을 위해 설정해두는게 좋은!
        if (PlayerPrefs.GetInt("MainStage") == 0) miniStageNum = PlayerPrefs.GetInt("MiniStage");
        else miniStageNum = PlayerPrefs.GetInt("CurrentMiniStage");

        //1.3
        if (miniStageNum + 1 == 3)
        {
            color = GameObject.Find("color");
            colorRb = color.GetComponent<Rigidbody2D>();
        }

    }
	
	// Update is called once per frame
	void Update () {

        //1.1
        if (miniStageNum + 1 == 1)
        {
            if (!nowAct)
            {
                nowAct = true;
                expandSomething(door1);
            }
            setScale(door1);
        }

        //1.3
        if (miniStageNum + 1 == 3)
        {
            if (!firstShake)
            {
                detectShake(2.0f);

                //컴퓨터 테스팅 용
                if (Input.GetKey(KeyCode.LeftArrow)) colorDown();
            }
        }

    }


    void expandSomething(GameObject object1)
    {
       
            //터치를 두개 이상 받으면
            if (Input.touchCount == 2)
        {
            if (object1.transform.localScale.x < 0.8 && object1.transform.localScale.y < 0.8)
            {

                // Store both touches.
                Touch touchZero = Input.GetTouch(0);
                Touch touchOne = Input.GetTouch(1);

                Vector3 touch0 = Camera.main.ScreenToWorldPoint(new Vector3(touchZero.position.x, touchZero.position.y, 0));
                Vector3 touch1 = Camera.main.ScreenToWorldPoint(new Vector3(touchOne.position.x, touchOne.position.y, 0));

                Vector3 middletouch = (touch0 + touch1) / 2;
                Vector2 centerPos = new Vector2(middletouch.x, middletouch.y);

                // TouchText.text = "collider bounds"+ gameObject.GetComponent<BoxCollider2D>().bounds+ "text1" + touch0 + "text2" + touch1 + "middel" + centerPos;

                if (object1.GetComponent<BoxCollider2D>().bounds.Contains(centerPos))
                {
                    // TouchText.text = "gettouched!";
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
                        object1.transform.localScale += new Vector3(change * Time.deltaTime, change * Time.deltaTime, 0);
                    }
                }
            }
            if (object1.transform.localScale.x >= 0.8f && object1.transform.localScale.y >= 0.8f)
            {
                dog.GetComponent<controlDog>().canGo = true;
                object1.GetComponent<doorKnob>().canOpen = true;
            }
        }
       


        nowAct = false;
    }


    void setScale(GameObject object1)
    {
        if (object1.transform.localScale.x > 0.9f)
        {
            object1.transform.localScale = new Vector3(0.9f, 0.9f, 1);
        }
    }

    void detectShake(float setTime)
    {
        float timecount = Time.time - starttime;
        acceleration = Input.acceleration;
        lowPassValue = Vector3.Lerp(lowPassValue, acceleration, lowPassFilterFactor);
        deltaAcceleration = acceleration - lowPassValue;
        if (deltaAcceleration.sqrMagnitude >= shakeDetectionThreshold && timecount > setTime)
        {
            colorDown();
        }
    }

    void colorDown()
    {
        firstShake = true;
        colorRb.isKinematic = false;
    }
}
