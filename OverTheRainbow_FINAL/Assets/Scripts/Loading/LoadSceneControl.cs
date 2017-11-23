using UnityEngine;
using System.Collections;

public class LoadSceneControl : MonoBehaviour {

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

    //Start Game 
    // 후에 터치 입력 들어오면 이동하게 만들기 추가. 리소스 필요 0720
    GameObject loadingScene;
    GameObject startScene;
    public GameObject[] selectStage = new GameObject[7];
    GameObject audioManager;

    //sound
    AudioClip dogBark;
    bool nowStart;


    void Awake()
    {
        PlayerPrefs.SetInt("CurrentStage", 0);
        PlayerPrefs.SetInt("retryNow", 0);
        PlayerPrefs.SetInt("CurrentMiniStage", 0);

        Debug.Log(PlayerPrefs.GetInt("MainStage"));
        //외부에서 수정이 가해질 경우 초기화
        if (PlayerPrefs.GetInt("MainStage") < 0 || PlayerPrefs.GetInt("MainStage") > 5)
        {
            PlayerPrefs.SetInt("MainStage", 0);
            PlayerPrefs.SetInt("MiniStage", 0);
            Application.LoadLevel("Loading");
        }

        //가장 처음 시작할 때의 연출. Loading 씬 생략하고 튜토리얼로 감. 
        if (PlayerPrefs.GetInt("MainStage") <= 0)
        {
            Application.LoadLevel("Stage1");
        }

        //startGame
        startScene = GameObject.Find("background");
        loadingScene = GameObject.Find("shakePicture");
        for(int j=0; j<selectStage.Length; j++)
        {
            selectStage[j] = GameObject.Find(j+1+"");
            if(j>= PlayerPrefs.GetInt("MainStage"))
            {
                selectStage[j].SetActive(false);
            }
        }

        //shake로 깨야하는 곳으로 들어가고, 클릭으로 예전 stage 다시 함
        //색을 칠하자!
        switch (PlayerPrefs.GetInt("MainStage"))
        {
            //Stage2 플레이 중
            case 1:
                if (PlayerPrefs.GetInt("MiniStage") == 1)
                {
                    selectStage[1].SetActive(true);

                }

                break;
           //Stage3 플레이 중.
            case 2:              
                for (int j = 0; j < 3; j++)
                {
                    selectStage[j].SetActive(true);
                }
                if (PlayerPrefs.GetInt("MiniStage") == 1)
                    selectStage[3].SetActive(true);

                break;
            case 3: //stage4 플레이 중
                for (int j = 0; j < 5; j++)
                {
                    selectStage[j].SetActive(true);
                }
                break;
            case 4:
                for (int j = 0; j < 6; j++)
                {
                    selectStage[j].SetActive(true);
                }
                break;

            case 5:
                for (int j = 0; j < selectStage.Length; j++)
                {
                    selectStage[j].SetActive(true);
                }
                    break;
            default: break;
        }
        if(PlayerPrefs.GetInt("happyEnding")==427) selectStage[6].SetActive(true);

        loadingScene.SetActive(false);
        
        Debug.Log("Mainstage " + PlayerPrefs.GetInt("MainStage"));
        Debug.Log("Ministage " + (PlayerPrefs.GetInt("MiniStage")+1));
        nowStart = false;

    }


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
        audioManager = GameObject.Find("soundManager");
        StartCoroutine(audioManager.GetComponent<audioControl>().UpVolume());

        dogBark = audioManager.GetComponent<audioControl>().dogBark;

    }
	
	void Update () {

        //안드로이드 뒤로 가기 버튼
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKeyDown(KeyCode.Escape))  //안드로이드에서 뒤로가기 버튼
                Application.Quit();
        }

        //강아지 누워있는 화면 떠야만 Shake 인식하게
        if (loadingScene.active) DetectShake();

    }


    void DetectShake()
    {
        float timecount = Time.time - starttime;
        acceleration = Input.acceleration;
        lowPassValue = Vector3.Lerp(lowPassValue, acceleration, lowPassFilterFactor);
        deltaAcceleration = acceleration - lowPassValue;
        if (deltaAcceleration.sqrMagnitude >= shakeDetectionThreshold && timecount > 0.8f)
        {
            if (!nowStart)
            {
                nowStart = true;
                StartCoroutine(dogDog());
            }
        }
    }


    public void Load()
    {
        startScene.SetActive(false);
        loadingScene.SetActive(true);
    }


    public void LoadingScene()
    {
        //Shake 하면 지금 깨야하는 Stage로 가고, 클릭을 하면 해당하는 stage로 감.
        
        switch (PlayerPrefs.GetInt("MainStage")+1)
        {

            //메인+1이 가야 할 스테이지임. 
            case 0:
            case 1:  //Tutorial
                Application.LoadLevel("Stage1");
                break;
            case 2:
                Application.LoadLevel("Stage2");
                break;
            case 3:
                Application.LoadLevel("Stage3");
                break;
            case 5:  //다 깨면 5가 됨 
            case 4:  //Last
                Application.LoadLevel("Stage4");
                break;
            default: //악의적 이용. 초기화 시켜버림.
                PlayerPrefs.SetInt("MainStage", 0);
                PlayerPrefs.SetInt("MiniStage", 0);
                Application.LoadLevel("Stage1");
                break;
        }
    }

    //버튼 인식.
    IEnumerator dogDog()
    {
        AudioSource.PlayClipAtPoint(dogBark, new Vector3(0, 0, -5));
        yield return new WaitForSeconds(0.5f);
        LoadingScene();
    }



}
