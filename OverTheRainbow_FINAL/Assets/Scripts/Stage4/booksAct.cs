using UnityEngine;
using System.Collections;

public class booksAct : MonoBehaviour {

    stage4Manager checkBools;
    bool nowActCoroutine;
    guardAct wakeUP;

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

    //책이 바닥에 떨어집니다~!
    GameObject booksOnGround;
    int countTouch;


    GameObject audioManager;
    AudioSource audioChange;
    AudioClip downBook;

    void Awake()
    {
        booksOnGround = GameObject.Find("booksonground");
        wakeUP = GameObject.Find("Guard").GetComponent<guardAct>();
        checkBools = GameObject.Find("MainCamera").GetComponent<stage4Manager>();      
        booksOnGround.SetActive(false);
        countTouch = 0;

        //BGM
        audioManager = GameObject.Find("soundManager");
        audioChange = audioManager.GetComponent<AudioSource>();
        downBook = audioManager.GetComponent<audioControl>().bookSlap;
    }


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
    void Update () {

        DetectShake();


        //컴퓨터에서 테스팅
        if (Input.GetKey(KeyCode.UpArrow)) byebyeBooks();


    }

    void OnMouseDown()
    {
        //자고 있는 중이 아닐 때.
        if (!checkBools.nowSleeping)
        {
            wakeUP.nowSleep = false;
            Debug.Log("now not Sleeping!!!");
        }
        else
        {
            countTouch++;
            if(countTouch>5) byebyeBooks();
        }
    }

    void DetectShake()
    {
        float timecount = Time.time - starttime;
        acceleration = Input.acceleration;
        lowPassValue = Vector3.Lerp(lowPassValue, acceleration, lowPassFilterFactor);
        deltaAcceleration = acceleration - lowPassValue;
        if (deltaAcceleration.sqrMagnitude >= shakeDetectionThreshold && timecount > 1.0f)
        {
            if (!checkBools.nowSleeping)
            {
                if (!nowActCoroutine)
                {
                    //책 각자 흔들리게 합시다.
                    //코루틴 안에 코루틴을 하나 더 불러서 거기서 불값 관리하자!
                    wakeUP.nowSleep = false;
                }
            }
            else   //경비원이 자는 중이면 책이 떨어진다! 
            {
                if(!checkBools.noBooks)
                {
                    byebyeBooks();

                }

            }


        }
    }

    void byebyeBooks()
    {
        checkBools.noBooks = true;
        AudioSource.PlayClipAtPoint(downBook, new Vector3(0, 0, -5));
        booksOnGround.SetActive(true);
        Destroy(this.gameObject);

    }

   
}
