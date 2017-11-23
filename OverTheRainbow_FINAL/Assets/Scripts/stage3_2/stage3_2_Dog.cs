using UnityEngine;
using System.Collections;

public class stage3_2_Dog : MonoBehaviour {


    stage3_2_Manager checkBools;
    stageManager stageM;

    public bool canGo;
    float speed;
    bool firstStand = false;
    Animator dogAni;

    public GameObject colorBall;
    public GameObject[] sleepingGun = new GameObject[3];
    bool shootDog;

    //Sound
    GameObject audioManager;
    AudioSource audioChange;
    AudioClip barkBark;
    AudioClip gun1;
    AudioClip gun2;
    AudioClip colorGet;
    AudioClip byebye;

    //BARK
    public GameObject meong;

    void Awake()
    {
        checkBools = GameObject.Find("miniStage2").GetComponent<stage3_2_Manager>();
        speed = 0;
        dogAni = GetComponent<Animator>();
        stageM = GameObject.Find("MainCamera").GetComponent<stageManager>();

        if (sleepingGun[0] != null)
        {
            sleepingGun[0].SetActive(true);
            sleepingGun[1].SetActive(false);
            sleepingGun[2].SetActive(false);
        }
        shootDog = false;

        audioManager = GameObject.Find("soundManager");
        audioChange = audioManager.GetComponent<AudioSource>();
        barkBark= audioManager.GetComponent<audioControl>().dogBark;
        colorGet = audioManager.GetComponent<audioControl>().getColor;
        byebye = audioManager.GetComponent<audioControl>().forStage3;
        gun1 = audioManager.GetComponent<audioControl>().gun1;
        gun2 = audioManager.GetComponent<audioControl>().gun2;
    }

    // Use this for initialization
    void Start () {
	

	}

    void Update()
    {
        if (canGo)
        {
            dogAni.SetBool("canGo", true);

            if (!firstStand)
            {
                firstStand = true;
                StartCoroutine(forStandUp(1.1f));

            }

            if (this.transform.position.x >= -6.6f &&!checkBools.stage3_2_1Clear)
            {
                setStop();

            }
            else if (checkBools.stage3_2_1Clear && this.transform.position.x > 13) //3-2-1 클리어
            {

                setStop();
                this.transform.position = new Vector3(-15, this.transform.position.y,this.transform.position.z);

                //3-2-2 로딩 
                checkBools.stage3_2_1Clear = true;
                checkBools.stageObj[1].SetActive(true);
                Destroy(checkBools.stageObj[0]);
                checkBools.stage3_2_2Bird = true;

            }else if (checkBools.stageObj[0] == null && this.transform.position.x >= -6.6f&&!checkBools.stage3_2_2Clear)  //파괴는 후에 
            {
                setStop();
            }
            else moveDog();

        }

        if (shootDog)
        {
            if(sleepingGun[1] !=null)
            sleepingGun[1].transform.position += new Vector3(13.0f * Time.deltaTime, 0, 0);
        }

    }

    void setStop()
    {
        canGo = false;
        firstStand = false;
        dogAni.SetBool("canGo", false);
        speed = 0;
    }

    void moveDog()
    {
        this.transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
    }

    IEnumerator forStandUp(float time)
    {
        yield return new WaitForSeconds(time);
        speed = 5.5f;
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.name == "colorball")
        {
            //색 먹고 소리 없애자
            setStop(); 
            //먹은 코루틴 
            
            StartCoroutine(getColor());
            StartCoroutine(readyGun(3.0f));
            //느낌표 뜨고 하는거....!!!!  색 사라지고

        }else if(other.name == "trueBall")
        {
            setStop();
            StartCoroutine(shootGoGo());
            //사이드에서 마취총 나와서 마취총이 개에 부딪치면, 화면 암전 되고, 클리어 함수 호출! 
        }

        if (other.name == "sleepingdart")
        {
            Destroy(other.gameObject);
            StartCoroutine(sleepSleep());
        }

    }

    IEnumerator readyGun(float setTime)
    {
        float countTime = 0;
        Vector3 firstPos = sleepingGun[0].transform.position;
        float magnitude = 6.5f / setTime * Time.deltaTime;
        yield return new WaitForSeconds(1.0f);

        AudioSource.PlayClipAtPoint(gun1, new Vector3(0, 0, -5));
        while (countTime < setTime)
        {
            countTime += Time.deltaTime/setTime;
            yield return new WaitForEndOfFrame();
            sleepingGun[0].transform.position = Vector3.Lerp(firstPos, new Vector3(-7.5f, firstPos.y, firstPos.z), countTime);
        }

        sleepingGun[0].transform.position = new Vector3(-7.5f, firstPos.y, firstPos.z);

    }

    void OnMouseDown()
    {
        if (!canGo)
        {
            StartCoroutine(bark());
        }
    }

    public IEnumerator bark()
    {
        AudioSource.PlayClipAtPoint(barkBark, new Vector3(0, 0, -5));
        meong.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        meong.SetActive(false);
    }

    IEnumerator shootGoGo()
    {
        yield return new WaitForSeconds(0.9f);
        sleepingGun[1].SetActive(true);
        shootDog = true;
        AudioSource.PlayClipAtPoint(gun2, new Vector3(0, 0, -5),0.5f);

    }

    IEnumerator getColor()
    {
        AudioSource.PlayClipAtPoint(colorGet, new Vector3(0, 0, -5));
        yield return new WaitForSeconds(3.0f);      
        //이펙트 넣을 거면 넣자 
        colorBall.SetActive(false);
        canGo = true;
    }

    IEnumerator sleepSleep()
    {
        audioChange.mute = true; 
        Debug.Log("클리어될거야. ");
        sleepingGun[2].SetActive(true);
        AudioSource.PlayClipAtPoint(byebye, new Vector3(0, 0, -5));
        //콜라이더 건드리지 않게 뮤트 시켜두자. 
        yield return new WaitForSeconds(6.0f);
        
        stageM.clearStage();
    }
}
