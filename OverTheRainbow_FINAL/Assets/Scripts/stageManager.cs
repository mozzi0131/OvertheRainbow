using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


//Stage1을 처음으로 클리어하면, MainStage==1 즉, 자기가 끝가지 클리어한 스테이지의 최대수를 나타냄.
public class stageManager : MonoBehaviour {

    //각 배경 object를 관리하기 위한 배열
    public GameObject[] backObject;

    //했던 stage 또 하는 중
    public GameObject forStage2;

    //BGMControl;
    GameObject audioManager;
    AudioSource audioChange;


    void Awake()
    {
        //악의적인 변경시
        if (PlayerPrefs.GetInt("retryNow") >= 2)
        {
            PlayerPrefs.SetInt("MainStage", 0);
            PlayerPrefs.SetInt("MiniStage", 0);
        }

        string sceneName = SceneManager.GetActiveScene().name;
        int stageType = PlayerPrefs.GetInt("MainStage");
        Debug.Log(sceneName);
        Debug.Log("CurrentMiniStage"+PlayerPrefs.GetInt("CurrentMiniStage"));
        Debug.Log("Mainstage " + PlayerPrefs.GetInt("MainStage"));
        Debug.Log("Ministage " + (PlayerPrefs.GetInt("MiniStage") + 1));


        //그 씬의 오브젝트를 받아오는 걸 관리 
        switch (sceneName)
        {
            case "Stage1":
                backObject = new GameObject[3];
                PlayerPrefs.SetInt("CurrentStage", 1);
                break;
            case "Stage2":
                backObject = new GameObject[2];
                PlayerPrefs.SetInt("CurrentStage", 2);
                break;
            case "Stage3": //여기서부터는 기획 없어서 임의
                backObject = new GameObject[2];
                PlayerPrefs.SetInt("CurrentStage", 3);
                break;
            case "Stage4":
                backObject = new GameObject[1];
                PlayerPrefs.SetInt("CurrentStage", 4);
                break;
            case "Ending":
                PlayerPrefs.SetInt("CurrentStage", 5);
                break;

        }

        //배경 오브젝트 전부 지정.
        for (int j = 0; j < backObject.Length; j++)
        {
            backObject[j] = GameObject.Find("miniStage" + (j + 1));
        }

        //지정된 배경 오브젝트 외에는 전부 꺼버림

        int nowStageIn = PlayerPrefs.GetInt("CurrentStage");



        //retryNow==1일 때 재도전이라는 뜻. retryNow==ture 라궁...
        if (nowStageIn <= stageType)
        {
            Debug.Log("retryNow And ReplayNow");
            for (int j = 0; j < backObject.Length; j++)
            {
                if (j == PlayerPrefs.GetInt("CurrentMiniStage")) backObject[j].SetActive(true);
                else backObject[j].SetActive(false);
            }
        }
        else  //한번도 안 깬 곳 깨는 중 일 때
        {
            PlayerPrefs.SetInt("CurrentMiniStage", 0);
            for (int j = 0; j < backObject.Length; j++)
            {
                if (j == PlayerPrefs.GetInt("MiniStage")) backObject[j].SetActive(true);
                else backObject[j].SetActive(false);
            }
        }


        //stage2에서 액자 켜고 시작
        if (nowStageIn == 2)
        {
            forStage2 = GameObject.Find("defaultWall");
            backObject[0].SetActive(false);
            backObject[1].SetActive(false);
        }

        audioManager = GameObject.Find("soundManager");

        if (PlayerPrefs.GetInt("AudioNum") != nowStageIn || (stageType == 0 && PlayerPrefs.GetInt("MiniStage") == 0))
            StartCoroutine(audioManager.GetComponent<audioControl>().UpVolume());

    }


    void Update()
    {
        //컴퓨터 테스트용
        if (Input.GetKey(KeyCode.RightArrow)) clearStage();
    }


    //스테이지 클리어시 호출할 함수. 
    public void clearStage()
    {

        string sceneName = SceneManager.GetActiveScene().name;
        int stageType = PlayerPrefs.GetInt("MainStage");
        int miniNum = PlayerPrefs.GetInt("MiniStage");
        int currentMiniNum = PlayerPrefs.GetInt("CurrentMiniStage");


        //최종 미니 스테이지까지 깬 상태 or 악의적인 변경시
        //후에 튜토리얼 효과때문에라도 Stage1에 관한건 수정해야함 => 다 깨면 Loading씬 가야함.

        if (PlayerPrefs.GetInt("CurrentStage") == 2)
        {
            if (stageType == 1) //Stage2 깨는 중.
            {
                PlayerPrefs.SetInt("retryNow", 0);
                if (miniNum == 0)
                {

                    PlayerPrefs.SetInt("MiniStage", 1);
                    Application.LoadLevel(sceneName);
                }else if (miniNum == 1 && backObject[0].active)
                {
                    PlayerPrefs.SetInt("MiniStage", 1);
                    Application.LoadLevel(sceneName);
                }
                else
                {
                    PlayerPrefs.SetInt("MiniStage", 0); //미니스테이지 초기화해야댐
                    PlayerPrefs.SetInt("MainStage", stageType + 1); //mainStage도 하나 올림
                    Application.LoadLevel("Stage3");
                }
            }
            else  //다시 깨는 중
            {
                Debug.Log("Now On rePlaying");

                if (currentMiniNum == 0)
                {
                    PlayerPrefs.SetInt("CurrentMiniStage", currentMiniNum + 1);
                    Application.LoadLevel(sceneName);
                }else if(currentMiniNum == 1 && backObject[0].active)
                {
                    Application.LoadLevel(sceneName);
                }
                else
                {
                    PlayerPrefs.SetInt("CurrentMiniStage", 0);
                    Application.LoadLevel("Stage3");
                }

            }
           

        }
        else if (PlayerPrefs.GetInt("CurrentStage") > stageType)
        {
            Debug.Log("Now On Playing");
            if (miniNum + 1 >= backObject.Length)
            {
                PlayerPrefs.SetInt("retryNow", 0);
                PlayerPrefs.SetInt("MiniStage", 0); //미니스테이지 초기화해야댐
                PlayerPrefs.SetInt("MainStage", stageType + 1); //mainStage도 하나 올림

                switch (sceneName)
                {
                    case "Stage1":
                        Application.LoadLevel("Stage2"); break;
                    case "Stage2":
                        Application.LoadLevel("Stage3"); break;
                    case "Stage3":
                        Application.LoadLevel("Stage4"); break;
                    case "Stage4":
                        Application.LoadLevel("Ending"); break;
                    case "Ending":
                        Application.LoadLevel("Loading"); break;
                }
            }
            else
            {
                PlayerPrefs.SetInt("retryNow", 0);
                PlayerPrefs.SetInt("MiniStage", miniNum + 1);
                Application.LoadLevel(sceneName);
            }
        }
        else {
            Debug.Log("Now On rePlaying");
            if (currentMiniNum + 1 >= backObject.Length)
            {
                PlayerPrefs.SetInt("CurrentMiniStage", 0);

                switch (sceneName)
                {
                    case "Stage1":
                        Application.LoadLevel("Stage2"); break;
                    case "Stage2":
                        Application.LoadLevel("Stage3"); break;
                    case "Stage3":
                        Application.LoadLevel("Stage4"); break;
                    case "Stage4":
                        Application.LoadLevel("Ending"); break;
                    case "Ending":
                        Application.LoadLevel("Loading"); break;
                }
            }
            else
            {
                PlayerPrefs.SetInt("CurrentMiniStage", currentMiniNum + 1);
                Application.LoadLevel(sceneName);
            }
        }

    }
}
