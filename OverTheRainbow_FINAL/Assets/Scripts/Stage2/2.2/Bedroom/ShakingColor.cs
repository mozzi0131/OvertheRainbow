using UnityEngine;
using System.Collections;

public class ShakingColor : MonoBehaviour {
    //색깔 먹는 것 판단용 변수들
    GameObject dog;
    bool canGo;
    Animator dogAni;
    GameObject camera;
    GameObject miniStage1;

    //테스트용
    public bool canshake;

    //흔들기 위한 변수들
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
    bool firstStand = false;

    // Use this for initialization
    void Awake()
    {
        canshake = false;
        firstStand = false;
        dog = GameObject.Find("Dog");
        dogAni = dog.GetComponent<Animator>();
        canGo = false;
        camera = GameObject.Find("MainCamera");
        miniStage1 = GameObject.Find("miniStage1");

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
        //색깔이 -4 이상에 있으면(바닥에 떨어지지 않았으면) 흔들기가 가능하다
        if(gameObject.transform.position.y>-4)
        {
            float timecount = Time.time - starttime;
            acceleration = Input.acceleration;
            lowPassValue = Vector3.Lerp(lowPassValue, acceleration, lowPassFilterFactor);
            deltaAcceleration = acceleration - lowPassValue;

            if (deltaAcceleration.sqrMagnitude >= shakeDetectionThreshold && timecount > 1.5f)
            {
                gameObject.transform.DetachChildren();
                gameObject.transform.Translate(0, -5 * Time.deltaTime, 0);
            }
        }

        //색깔이 바닥으로 떨어지면 강아지 이동시키기
        if(gameObject.transform.position.y<=-4)
        {
                dogAni.SetBool("canGo", true);
                if (!firstStand)
                {
                    firstStand = true;
                    StartCoroutine(forStandUp(1.1f));

                }
                moveDog();
            }
        }



    void moveDog()
    {
        dog.transform.Translate(new Vector3(5 * Time.deltaTime, 0, 0));
    }

    IEnumerator forStandUp(float time)
    {
        yield return new WaitForSeconds(time);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.name=="Dog")
        {
            camera.GetComponent<stageManager>().clearStage();
        }
    }

}

