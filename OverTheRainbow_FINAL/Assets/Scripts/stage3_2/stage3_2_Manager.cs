using UnityEngine;
using System.Collections;

public class stage3_2_Manager : MonoBehaviour
{


    public GameObject[] stageObj = new GameObject[2];
    public GameObject ball1;
    GameObject dogDog;
    public GameObject[] fallenfallen;

    float countTime;
    Vector3 firstPos;
    Vector3 firstPos2;

    //Stage3_2_1 control Bool
    public bool gogoDoggy;
    public bool needHandkerchief;  //차에 탄 사람이 똥 떨어지면
    public bool downHandkerchief;
    public bool handkerchiefOn;
    public bool canDragCable;
    public bool canOnElectricity;
    public bool electricityOn;
    public bool stage3_2_1Clear;

    //Stage3_2_2 contorl Boll
    public bool stage3_2_2Bird;  //새를 다스리는 변수
    public bool getSeed;
    public bool dirtGetSeed;
    public bool getWater;
    public bool canWatering;
    public bool canDoongZi;
    public bool birdInTree;
    public bool canGetColor;
    public bool downdownBall;
    public bool stage3_2_2Clear;


    //Shake
    //DetectShake
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


    void Awake()
    {
        //first
        stageObj[0].SetActive(true);
        stageObj[1].SetActive(false);
        dogDog = GameObject.Find("dog_2");


        countTime = 0.0f;
        firstPos = ball1.transform.position;


        //3-2-1
        needHandkerchief = false;
        downHandkerchief = false;
        handkerchiefOn = false;
        canDragCable = false;
        canOnElectricity = false;
        electricityOn = false;
        stage3_2_1Clear = false;

        //3-2-2
        stage3_2_2Bird = false;
        getSeed = false;
        getWater = false;
        canWatering = false;
        canDoongZi = false;
        canGetColor = false;
        dirtGetSeed = false;
        downdownBall = false;
        birdInTree = false;
        stage3_2_2Clear = false;

        //개님 움직이게 하는 거
        gogoDoggy = true;
    }

    // Use this for initialization
    void Start()
    {

        //DetectShake
        accelerometerUpdateInterval = 1.0f / 60.0f;
        lowPassKernelWidthInSeconds = 1.0f;
        shakeDetectionThreshold = 2.0f;
        lowPassKernelWidthInSeconds = accelerometerUpdateInterval / lowPassKernelWidthInSeconds;
        lowPassValue = Vector3.zero;
        shakeDetectionThreshold *= shakeDetectionThreshold;
        lowPassValue = Input.acceleration;
        starttime = Time.time;

    }

    // Update is called once per frame
    void Update()
    {


        if (ball1 != null)
        {
            countTime += Time.deltaTime / 5.0f;
            if (ball1.transform.position.x >= 11) { Destroy(ball1); countTime = 0; }
            else
            {
                ball1.transform.position = Vector3.Lerp(firstPos, new Vector3(11, firstPos.y, firstPos.z), countTime);
                ball1.transform.Rotate(new Vector3(0, 0, 1), -500 * Time.deltaTime);
            }
        }
        else
        {
            if (gogoDoggy)
            {
                gogoDoggy = false;
                dogDog.GetComponent<stage3_2_Dog>().canGo = true;
            }
        }

        if (canGetColor&&!downdownBall)
        {
            DetectShake();
        }




    }


    void DetectShake()
    {
        float timecount = Time.time - starttime;
        acceleration = Input.acceleration;
        lowPassValue = Vector3.Lerp(lowPassValue, acceleration, lowPassFilterFactor);
        deltaAcceleration = acceleration - lowPassValue;
        if (deltaAcceleration.sqrMagnitude >= shakeDetectionThreshold && timecount > 1.0f ||Input.GetKey(KeyCode.DownArrow))
        {
            
            fallenfallen[0].GetComponent<Rigidbody2D>().isKinematic = false;
            fallenfallen[1].GetComponent<Rigidbody2D>().isKinematic = false;
            downdownBall = true;
        }
    }
}
