using UnityEngine;
using System.Collections;

public class controlDog : MonoBehaviour {

    GameObject stageManager;
    int miniStageNum;
    float speed = 0.0f;
    public bool canGo;
    public GameObject forStage3;
    public GameObject clearSignAtFirst;
    public GameObject barkBark;
    Animator dogAni;

    //효과음
    GameObject audioManager;
    AudioClip dogBark;
    AudioClip clearDoorSound;
    AudioClip forColor;
    AudioSource audioChange;
    AudioClip gogoPast;

    //moveDelay
    bool firstStand = false;


    // Use this for initialization
    void Start()
    {

        stageManager = GameObject.Find("MainCamera");      
        miniStageNum = PlayerPrefs.GetInt("MiniStage");
        dogAni = GetComponent<Animator>();

        //Sound
        audioManager = GameObject.Find("soundManager");
        audioChange = audioManager.GetComponent<AudioSource>();
        dogBark = audioManager.GetComponent<audioControl>().dogBark;
        clearDoorSound = audioManager.GetComponent<audioControl>().clearDoorSound;
        forColor = audioManager.GetComponent<audioControl>().getColor;
        gogoPast = audioManager.GetComponent<audioControl>().gogoPast;

    }

    // Update is called once per frame
    void Update()
    {
        if (canGo)
        {
            dogAni.SetBool("canGo", true);
            if(!firstStand)
            {
                firstStand = true;
                StartCoroutine(forStandUp(1.1f));
                
            }
            moveDog();
        }

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


    void OnMouseDown()
    {
        //개짖는 소리
        StartCoroutine(bark());
    }



    public IEnumerator bark()
    {
        AudioSource.PlayClipAtPoint(dogBark, new Vector3(0, 0, -5));
        barkBark.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        barkBark.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
 
        if (other.CompareTag("Door"))
        {
            Debug.Log("WHHHHHYYYY???");
            canGo = false;
            dogAni.SetBool("canGo", false);
            if (miniStageNum + 1 != 3)
            {
                Debug.Log("not last");
                StartCoroutine(clearEffect());
               
            }
            else
            {
                //만약 첫 클리어면 효과 함수 호출하기!
                //화면 옆으로 넘겨서 액자 보여주고, 메인으로 쫓겨난다. 지금은 메인으로만
                if (PlayerPrefs.GetInt("MainStage") <= 0)
                {
                    PlayerPrefs.SetInt("MainStage", 1);
                    PlayerPrefs.SetInt("MiniStage", 0);
                    StartCoroutine(testEffect(2.0f));
                }
                else
                {
                    stageManager.GetComponent<stageManager>().clearStage();
                }
            }
        }

    }

    IEnumerator clearEffect()
    {
        AudioSource.PlayClipAtPoint(clearDoorSound, new Vector3(0, 0, -5));
        yield return new WaitForSeconds(1.2f);
        stageManager.GetComponent<stageManager>().clearStage();
    }

    //3단계 함수
    IEnumerator testEffect(float time)
    {
        float timeValue = 1 / time;
        Vector3 movingVector = new Vector3(timeValue * -11, 0, 0);
        float countTime = 0.0f;

        //개가 색깔 무는 거
        audioChange.mute = true;
        //StartCoroutine(audioManager.GetComponent<audioControl>().downVolume());       
        //yield return new WaitForSeconds(0.5f);
        clearSignAtFirst.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        AudioSource.PlayClipAtPoint(forColor, new Vector3(0, 0, -5));
        StartCoroutine(this.GetComponent<clearSign>().coloredSign());

        while (countTime < time)
        {
            yield return new WaitForEndOfFrame();
            countTime += Time.deltaTime;
            forStage3.transform.position = forStage3.transform.position + movingVector * Time.deltaTime;

        }
        clearSignAtFirst.SetActive(false);
        yield return new WaitForSeconds(0.7f);
        AudioSource.PlayClipAtPoint(gogoPast, new Vector3(0, 0, -5),0.5f);
        yield return new WaitForSeconds(2.0f);
        Application.LoadLevel("Loading");

    }
}
