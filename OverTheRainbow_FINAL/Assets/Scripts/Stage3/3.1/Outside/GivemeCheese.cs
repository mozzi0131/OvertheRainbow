using UnityEngine;
using System.Collections;

//쥐에게 붙이는 메세지 인쇄하는 코드 + 색 떨구고 강아지가 색을 먹는 것 까지 정리
public class GivemeCheese : MonoBehaviour
{
    GameObject colorball_1;
    GameObject Dog;
    GameObject bigger;
    GameObject cheese;
    GameObject balloon;
    GameObject camera;
    GameObject getcheese;

    AudioClip mousetalking;
    GameObject audioManager;


    Animator DogAni;

    bool canshake;

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
        colorball_1 = GameObject.Find("colorball_1");
        Dog = GameObject.Find("Dog");
        bigger = GameObject.Find("bigger");
        cheese = GameObject.Find("cheese_m");
        balloon = GameObject.Find("balloon2");
        camera = GameObject.Find("MainCamera");
        getcheese = GameObject.Find("cheese");

        DogAni = Dog.GetComponent<Animator>();

        accelerometerUpdateInterval = 1.0f / 60.0f;
        lowPassKernelWidthInSeconds = 1.0f;
        shakeDetectionThreshold = 2.0f;
        lowPassKernelWidthInSeconds = accelerometerUpdateInterval / lowPassKernelWidthInSeconds;
        lowPassValue = Vector3.zero;
        shakeDetectionThreshold *= shakeDetectionThreshold;
        lowPassValue = Input.acceleration;
        starttime = Time.time;

        audioManager = GameObject.Find("soundManager");
        mousetalking = audioManager.GetComponent<audioControl>().mouse;

    }

    void Start()
    {
        colorball_1.SetActive(false);
        bigger.SetActive(false);
        cheese.SetActive(false);
        balloon.SetActive(false);

        canshake = false;
    }

    void Update()
    {
        if (canshake == true && colorball_1.transform.localPosition.y >= 0)
        {
            float timecount = Time.time - starttime;
            acceleration = Input.acceleration;
            lowPassValue = Vector3.Lerp(lowPassValue, acceleration, lowPassFilterFactor);
            deltaAcceleration = acceleration - lowPassValue;

            if (deltaAcceleration.sqrMagnitude >= shakeDetectionThreshold && timecount > 1.5f)
            {
                colorball_1.transform.Translate(0, -5 * Time.deltaTime, 0);
            }
        }

        if (colorball_1.transform.localPosition.y <= 0)
        {
            canshake = false;
            DogAni.SetBool("canGo", true);
            if (!firstStand)
            {
                firstStand = true;
                StartCoroutine(forStandUp(1.1f));

            }
            moveDog();
        }
    }

    // Update is called once per frame
    void OnMouseDown()
    {
        AudioSource.PlayClipAtPoint(mousetalking, new Vector3(0, 0, -5));
        /*//치즈가 있고+커지지 않은 경우
        if (cheese.GetComponent<CheeseScript>().getCheese == true && cheese.GetComponent<CheeseScript>().bigenough == false)
        {
            StartCoroutine(Showingtalk(bigger));
        }*/  // 필요없을 것 같아서 일단 지움(밑에 드래그로 판단하는 코드 잇음)


        //치즈가 없을 경우
        if (getcheese.GetComponent<CheeseScript>().getCheese == false)
        {
            StartCoroutine(cheesetalk());
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {//치즈가 있고 커져서 만난 경우
        if (other.name == "cheese")
        {

            if (getcheese.GetComponent<CheeseScript>().bigenough == true)
            {
                AudioSource.PlayClipAtPoint(mousetalking, new Vector3(0, 0, -5));
                colorball_1.transform.DetachChildren();
                colorball_1.SetActive(true);
                canshake = true;

            }
            //커지지 않고 만난 경우
            if (getcheese.GetComponent<CheeseScript>().takecheese == true)
            {

                AudioSource.PlayClipAtPoint(mousetalking, new Vector3(0, 0, -5));
                StartCoroutine(biggertalk());
            }
        }

        if (other.name == "Dog")
        {
            camera.GetComponent<stageManager>().clearStage();
        }
    }

    void moveDog()
    {
        Dog.transform.Translate(new Vector3(5 * Time.deltaTime, 0, 0));
    }

    IEnumerator forStandUp(float time)
    {
        yield return new WaitForSeconds(time);
    }

    

    IEnumerator cheesetalk()
    {
        cheese.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        cheese.SetActive(false);
    }

    IEnumerator biggertalk()
    {
        bigger.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        bigger.SetActive(false);
    }
}
