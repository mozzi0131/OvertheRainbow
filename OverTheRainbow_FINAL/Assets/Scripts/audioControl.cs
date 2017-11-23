using UnityEngine;
using System.Collections;

public class audioControl : MonoBehaviour {

    private int loadIndex;
    private static int loadCount;

    //////Dog Sound////
    public AudioClip dogBark;
    public AudioClip yipyap;
    public AudioClip bigDog;
    public AudioClip forStage3;


    //////Stage1Effect////////
    public AudioClip candleFlame;
    public AudioClip candleFlameOff;
    public AudioClip doorLocked;
    public AudioClip doorOpen;
    public AudioClip foundKey;
    public AudioClip rotateBlock;
    public AudioClip clearDoorSound;
    public AudioClip getColor;
    public AudioClip wrongFrame;
    public AudioClip gogoPast;
    public AudioClip bookSlap;

    /////Stage2Effect////// //suhyun
    public AudioClip coffeepotWater;
    public AudioClip potWater;
    public AudioClip cupOnCoaster;
    public AudioClip closeCabinet;
    public AudioClip openCabinet;
    public AudioClip boilCoffee;
    public AudioClip coffeeInCup;
    public AudioClip finToast;
    public AudioClip inputToaster;
    public AudioClip ingToaster;
    public AudioClip sinkSound;
    public AudioClip chimSound;


    //stage2.2
    public AudioClip fire;
    public AudioClip Water;

    //stage3.1suhyun
    public AudioClip doorbutton;
    public AudioClip mouse;

    //Stage3-2 JY
    public AudioClip bird1;
    public AudioClip bird2;
    public AudioClip carHorn;
    public AudioClip spark1;
    public AudioClip watering;
    public AudioClip lightSwitch;
    public AudioClip faucet;
    public AudioClip gun1;
    public AudioClip gun2;


    //Stage4
    public AudioClip openC;
    public AudioClip closeC;
    public AudioClip lockedLock;
    public AudioClip monitorN;
    public AudioClip getSomething;
    public AudioClip metalgate;
    public AudioClip getgetKey;
    public AudioClip cardReader;
    public AudioClip snoring;
    public AudioClip sumyeon;
    public AudioClip paper;
    public AudioClip paperBag;
    public AudioClip toyBall;


    //endign
    public AudioClip tikTik;
    public AudioClip tokTok;

    ////////BGM/////////
    public AudioClip firstTitle;
    public AudioClip Stage1BGM;
    public AudioClip Stage2BGM;
    public AudioClip trueBGM;
    public AudioClip endingBGM;
    public AudioClip Stage4BGM;
    public AudioClip forHappyEnding;

    //suhyun
    public AudioClip Stage3BGM;
    

    AudioSource audioChange;
    int bgmNum=0;
    public bool plzDown = false;
    bool nowAct = false;

    void Awake()
    {
        bgmNum = PlayerPrefs.GetInt("CurrentStage");
        loadIndex = loadCount;
        loadCount++;
        if (loadIndex == 0)
        {
            DontDestroyOnLoad(gameObject); //Audio Source용 오브젝트 하나로 씀
        }
        else
        {
            Destroy(gameObject);
        }

        audioChange = this.GetComponent<AudioSource>();

        if (PlayerPrefs.GetInt("MainStage") == 0) audioChange.clip = Stage1BGM;
        else if (PlayerPrefs.GetInt("happyEnding") == 427) audioChange.clip = trueBGM;
        else audioChange.clip = firstTitle;

        audioChange.Play();


    }



    // Use this for initialization
    void Start () {



    }
	
	// Update is called once per frame
	void Update () {

        if (plzDown)
        {
            Debug.Log("canDown");
            if (!nowAct)
            {
                nowAct = true;
                StartCoroutine(downVolume());
            }
        }


	}

    public IEnumerator downVolume()
    {
        float countTime = 0.0f;

        Debug.Log("ccall");
        while (countTime < 4.0f)
        {
            countTime += Time.deltaTime;
            audioChange.volume -= 0.1f*Time.deltaTime;
        }
        audioChange.volume = 0;
       // plzDown = false;
        yield return new WaitForSeconds(0.1f);
      //  nowAct = false;
    }

    //이건 StageManager에서 쓰임. 
    public IEnumerator UpVolume()
    {
        float countTime = 0.0f;
        audioChange.mute = false;
        audioChange.volume = 0.8f;
        Debug.Log("UPPPccall");
        bgmNum = PlayerPrefs.GetInt("CurrentStage");
        PlayerPrefs.SetInt("AudioNum", bgmNum);
        switch (bgmNum)
        {
                    
            case 0:
                if (PlayerPrefs.GetInt("happyEnding") == 427)  //엔딩 봤다는 증거여요
                    audioChange.clip = trueBGM; 
                else audioChange.clip = firstTitle; break;
            case 1: audioChange.clip = Stage1BGM; break;
            case 2: audioChange.clip = Stage2BGM; break;
            case 3: audioChange.clip = Stage3BGM; break;//suhyun
            case 4: audioChange.clip = Stage4BGM; break;
            case 5: audioChange.clip = endingBGM; break;
            default: audioChange.clip = trueBGM; break;
        }

        audioChange.Play();
        //audioChange.volume = 0.0f;
        //while (countTime < 3.0f)
        //{
        //    countTime += Time.deltaTime;
        //    audioChange.volume += 0.3f * Time.deltaTime;
        //}

        yield return new WaitForSeconds(0.2f);
    }
}
