using UnityEngine;
using System.Collections;

public class endingControl : MonoBehaviour {

    public GameObject[] backGround;
    public GameObject[] objs;
    Animator dogAni;
    float speed;

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

    //죽은 후에
    bool plusAct;
    bool noCall;
    //고고
    bool act2;
    bool act1;
    bool act3;
    bool act4;
    bool canDown;
    bool canGo;
    //6번째 공 먹은 이후의 연출

    public bool lastAtc1;
    bool lastAtc_Coroutine;
    bool lastShake;
    bool canDown2;  //공 떨어지는 Shaking
    public bool lastAtc2;
    bool lastAtc_Coroutine2;

    //Sound
    GameObject audioManager;
    AudioSource audioChange;
    AudioClip byebye;
    AudioClip tikTik;
    AudioClip tokTok;
    AudioClip getgetColor;

    void Awake()
    {


        dogAni = GameObject.Find("dog3").GetComponent<Animator>();

        for (int i = 0; i < backGround.Length; i++) backGround[i].SetActive(false);
        for (int i = 0; i < objs.Length; i++) objs[i].SetActive(false);


        plusAct = false;
        noCall = false;

        speed =0;
        act1 = false;
        act2= false;
        act3 = false;
        act4 = false;

        lastAtc1 = false;
        lastAtc_Coroutine = false;
        canDown2 = false;
        lastShake = false;
        lastAtc2 = false;
        lastAtc_Coroutine2 = false;

        audioManager = GameObject.Find("soundManager");
        audioChange = audioManager.GetComponent<AudioSource>();
        byebye = audioManager.GetComponent<audioControl>().forStage3;
        tikTik = audioManager.GetComponent<audioControl>().tikTik;
        tokTok = audioManager.GetComponent<audioControl>().tokTok;
        getgetColor = audioManager.GetComponent<audioControl>().foundKey;
        audioChange.loop = false;

    }

	// Use this for initialization
	void Start () {

        //DetectShake
        accelerometerUpdateInterval = 1.0f / 60.0f;
        lowPassKernelWidthInSeconds = 1.0f;
        shakeDetectionThreshold = 2.0f;
        lowPassKernelWidthInSeconds = accelerometerUpdateInterval / lowPassKernelWidthInSeconds;
        lowPassValue = Vector3.zero;
        shakeDetectionThreshold *= shakeDetectionThreshold;
        lowPassValue = Input.acceleration;
        starttime = Time.time;


        StartCoroutine(setEnding(backGround[0], objs[0], 1.2f));
	}


    void Update()
    {
        if (plusAct&&!noCall)
        {
            if(Input.GetMouseButtonDown(0))
            {
                noCall = true;
                StartCoroutine(plusEnding4());
            }
        }

        //개가 걷는 것부터 시작 
        if (act1)
        {
            dogAni.SetBool("canGo", true);
            if (!act2)
            {
                act2 = true;
                StartCoroutine(forStandUp(1.1f));

            }
            if (objs[3].transform.position.x < 9.0f && !act3)
                moveDog(objs[3]);
            else
            {
                act3 = true; act1 = false;
            }
        }

        if (act3)
        {
            speed = -5.5f;


            if (objs[3].transform.position.x > -4.0f)
            {
                moveDog(objs[3]);
            }
            if (objs[2].transform.position.x > -9.0f) moveDog(objs[2]);
            else { act3 = false; dogAni.SetBool("canGo", false); act4 = true; }

            

        }

        if (act4)
        {
            if (Input.GetKey(KeyCode.DownArrow))
            {
                canDown = true;
                act4 = false;
            }
                DetectShake();
        }

        if (canDown&&objs[4]!=null) {  //색깔 떨어짐
            if (objs[4].transform.position.y > -3.5f) moveDown(objs[4]);
            else { canDown = false; if(objs[3] !=null) objs[3].GetComponent<endingDog>().canGo = true; act4 = false; }
        }

        //여기부터는 색 주우러 가고 효과 나오고...

        if (lastAtc1)
        {
            if (!lastAtc_Coroutine) { lastAtc_Coroutine = true; StartCoroutine(lastDoggy()); }
        }

        //마지막 색 떨어지는 거
        if (lastShake&&!canDown2)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                canDown2 = true;
                
            }
            DetectShake();
        }

        if (canDown2 &&objs[9] !=null)
        {
            if (objs[9].transform.position.y > -3.5f) moveDown(objs[9]);
            else { AudioSource.PlayClipAtPoint(getgetColor, new Vector3(0, 0, -5),0.5f); lastShake = false; canDown2 = false; lastAtc2 = true; }
        }

        //라스트! 
        if(lastAtc2)
        {
            if (!lastAtc_Coroutine2) { lastAtc_Coroutine2 = true; StartCoroutine(happyEnding()); }
        }
    }

   IEnumerator lastDoggy()
    {
        yield return new WaitForSeconds(3.0f);
        objs[6].SetActive(true); //fadeInEffect;
        backGround[1].SetActive(true);  //선만 있는 그림 
        objs[5].SetActive(true);  //중앙에 있을 강아지   
        AudioSource.PlayClipAtPoint(tikTik, new Vector3(0, 0, -5));
        yield return new WaitForSeconds(2.0f);

        float countTime = 0;

        while (countTime < 3.3f)
        {
            countTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
            objs[6].transform.localScale += new Vector3(3.0f*Time.deltaTime, 3.0f * Time.deltaTime, 0);
        }
        
        yield return new WaitForSeconds(0.2f);
        objs[6].SetActive(false);
        backGround[1].SetActive(false);  //선만 있는 그림 
        objs[5].SetActive(false);
        AudioSource.PlayClipAtPoint(tikTik, new Vector3(0, 0, -5));
        AudioSource.PlayClipAtPoint(tokTok, new Vector3(0, 0, -5));
        ///////////////
        yield return new WaitForSeconds(1.0f);
        backGround[1].SetActive(true);
        objs[5].SetActive(true);
        objs[7].SetActive(true);  //주인 나타남
        AudioSource.PlayClipAtPoint(tikTik, new Vector3(0, 0, -5));
        yield return new WaitForSeconds(1.0f);
        backGround[1].SetActive(false);
        objs[5].SetActive(false);
        objs[7].SetActive(false);
        AudioSource.PlayClipAtPoint(tikTik, new Vector3(0, 0, -5));
        AudioSource.PlayClipAtPoint(tokTok, new Vector3(0, 0, -5));
        yield return new WaitForSeconds(1.5f);
        backGround[1].SetActive(true);
        objs[8].SetActive(true);
        objs[7].SetActive(true);
        AudioSource.PlayClipAtPoint(tikTik, new Vector3(0, 0, -5));
        yield return new WaitForSeconds(1.0f);
        backGround[1].SetActive(false);
        objs[8].SetActive(false);
        objs[7].SetActive(false);
        AudioSource.PlayClipAtPoint(tikTik, new Vector3(0, 0, -5));
        AudioSource.PlayClipAtPoint(tokTok, new Vector3(0, 0, -5));
        yield return new WaitForSeconds(2.0f);
        backGround[1].SetActive(true);
        objs[8].SetActive(true);
        objs[7].SetActive(true);
        objs[9].SetActive(true); //colorBall
        AudioSource.PlayClipAtPoint(tikTik, new Vector3(0, 0, -5));
        lastShake = true;
    }


    IEnumerator happyEnding()
    {
        yield return new WaitForSeconds(0.5f);
        //색 봤다고 또 느낌표 띄울까
        //브금 브금~
        backGround[1].SetActive(false);
        objs[8].SetActive(false);
        objs[7].SetActive(false);
        objs[9].SetActive(false);
        AudioSource.PlayClipAtPoint(tikTik, new Vector3(0, 0, -5));
        AudioSource.PlayClipAtPoint(tokTok, new Vector3(0, 0, -5));
        yield return new WaitForSeconds(2.0f);
        audioChange.clip = audioManager.GetComponent<audioControl>().forHappyEnding;
        audioChange.Play();
        audioChange.loop = true;
        backGround[2].SetActive(true);
        objs[10].SetActive(true);
        objs[8].SetActive(true);
        objs[7].SetActive(true);
        AudioSource.PlayClipAtPoint(tikTik, new Vector3(0, 0, -5));
        yield return new WaitForSeconds(1.5f);
        objs[10].SetActive(false); //구름 끄고
        objs[11].SetActive(true);  // 글자 켜고 
        PlayerPrefs.SetInt("happyEnding", 427);

    }


    void moveDown(GameObject obj)
    {
        obj.transform.Translate(new Vector3(0, speed * Time.deltaTime, 0));
    }

    void moveDog(GameObject obj)
    {
       obj.transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
    }

    IEnumerator forStandUp(float time)
    {
        yield return new WaitForSeconds(time);
        speed = 5.5f;
    }


    void DetectShake()
    {
        float timecount = Time.time - starttime;
        acceleration = Input.acceleration;
        lowPassValue = Vector3.Lerp(lowPassValue, acceleration, lowPassFilterFactor);
        deltaAcceleration = acceleration - lowPassValue;
        if (deltaAcceleration.sqrMagnitude >= shakeDetectionThreshold && timecount > 1.0f&&!canDown)
        {
            canDown = true;
            act4 = false;
            if (lastAtc1) canDown2 = true;
        }
    }


    IEnumerator setEnding(GameObject back, GameObject obj,float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        AudioSource.PlayClipAtPoint(tikTik, new Vector3(0, 0, -5));
        back.SetActive(true);
        obj.SetActive(true);

        yield return new WaitForSeconds(waitTime);
        back.SetActive(false);
        obj.SetActive(false);
        AudioSource.PlayClipAtPoint(tikTik, new Vector3(0, 0, -5));
        AudioSource.PlayClipAtPoint(tokTok, new Vector3(0, 0, -5));
        StartCoroutine(setEnding2(backGround[0],objs[1],waitTime));
    }


    IEnumerator setEnding2(GameObject back, GameObject obj, float waitTime)
    {
        float setTime = 0.7f;
        float startTime = 0;
        float power = 1.2f*Time.deltaTime;
        yield return new WaitForSeconds(waitTime);


        back.SetActive(true);
        obj.SetActive(true);
        AudioSource.PlayClipAtPoint(tikTik, new Vector3(0, 0, -5));
        while (startTime < setTime)
        {
            yield return new WaitForEndOfFrame();
            startTime += Time.deltaTime;
            obj.transform.position -= new Vector3(power, 0, 0);
        }

        back.SetActive(false);
        obj.SetActive(false);
        AudioSource.PlayClipAtPoint(tikTik, new Vector3(0, 0, -5));
        AudioSource.PlayClipAtPoint(tokTok, new Vector3(0, 0, -5));
        StartCoroutine(setEnding3(backGround[0], objs[1], waitTime));
    }

    IEnumerator setEnding3(GameObject back, GameObject obj, float waitTime)
    {
        float setTime = 1.5f;
        float startTime = 0;
        float power = 2.0f * Time.deltaTime;
        yield return new WaitForSeconds(waitTime);



        back.SetActive(true);
        obj.SetActive(true);
        AudioSource.PlayClipAtPoint(tikTik, new Vector3(0, 0, -5));
        while (startTime < setTime)
        {
            yield return new WaitForEndOfFrame();
            startTime += Time.deltaTime;
            power += 0.1f * Time.deltaTime;
            obj.transform.position -= new Vector3(power, 0, 0);
        }

        yield return new WaitForSeconds(1.0f);
 
        back.SetActive(false);
        obj.SetActive(false);
        AudioSource.PlayClipAtPoint(tikTik, new Vector3(0, 0, -5));
        AudioSource.PlayClipAtPoint(tokTok, new Vector3(0, 0, -5));
        yield return new WaitForSeconds(0.8f);
        AudioSource.PlayClipAtPoint(byebye, new Vector3(0, 0, -5));
       
        //개가 잡혀감 
       
        //여기다 효과음 같은거...? 아니면 이전에 개가 잡혀가는 부분 
        yield return new WaitForSeconds(4.5f);
        plusAct = true;
        Debug.Log("plz Touch");

    }

    IEnumerator plusEnding4()
    {
        AudioSource.PlayClipAtPoint(tikTik, new Vector3(0, 0, -5));

        objs[2].SetActive(true);
        objs[3].SetActive(true);
        objs[4].SetActive(true);


        yield return new WaitForSeconds(2.5f);
        act1 = true;
    }


}
