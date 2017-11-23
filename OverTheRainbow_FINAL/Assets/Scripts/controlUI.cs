using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class controlUI : MonoBehaviour {

    bool optionOn = false;
    public GameObject[] optionUI;
    GameObject stageManager;

	// Use this for initialization
	void Start () {

        stageManager = GameObject.Find("MainCamera");

    }
	
	// Update is called once per frame
	void Update () {

        //안드로이드 뒤로 가기 버튼
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKeyDown(KeyCode.Escape))  //안드로이드에서 뒤로가기 버튼
            {
                if (optionOn)  //UI가 이미 켜진 상태일 때 UI를 끔
                {
                    for(int i=0;i< optionUI.Length;i++)
                    optionUI[i].SetActive(false);
                    optionOn = false;
                }
                else
                {

                    Application.LoadLevel("Loading");   //뒤로
                }
            }
        }

    }


    public void touchOption()
    {

        if (!optionOn)
        {
            for (int i = 0; i < optionUI.Length; i++)
                optionUI[i].SetActive(true);
            optionOn = true;

        }else
        {
            for (int i = 0; i < optionUI.Length; i++)
                optionUI[i].SetActive(false);
            optionOn = false;
        }
    }

    //지금 씬 다시
    public void reStart()
    {
        //잠깐 잠가둠
        Scene thisScene = SceneManager.GetActiveScene();
        string sceneName = thisScene.name;

        Application.LoadLevel(sceneName);
      

    }

    public void allReStart()
    {
        PlayerPrefs.SetInt("MainStage",0);
        PlayerPrefs.SetInt("MiniStage",0);
        PlayerPrefs.SetInt("CurrentMiniStage", 0);
        PlayerPrefs.SetInt("happyEnding", 0);

        //다시 시작. 씬 다시 로드. 
        switch (PlayerPrefs.GetInt("MainStage"))
        {
            case 0:
            case 1:
                Application.LoadLevel("Stage1");
                break;
            case 2:
                Application.LoadLevel("Stage2");
                break;
            case 3:
                Application.LoadLevel("Stage3");
                break;
            case 4:
                Application.LoadLevel("Stage4");
                break;
        }

    }

    public void nextStage()
    {
        stageManager.GetComponent<stageManager>().clearStage();
    }

}
