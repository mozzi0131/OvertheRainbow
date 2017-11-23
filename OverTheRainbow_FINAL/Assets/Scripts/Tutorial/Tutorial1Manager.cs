//스테이지1 튜토리얼 진행 관련 코드임

using UnityEngine;
using System.Collections;

public class Tutorial1Manager : MonoBehaviour {
    //tutorial 1,2를 관리하기 위한 배열 tutorial 선언

    GameObject[] Tutorial = new GameObject[2];



	void Start ()
    {
        PlayerPrefs.SetInt("MainStage", 0);
        Debug.Log(Tutorial.Length);
        Debug.Log(PlayerPrefs.GetInt("MiniStage"));

        int miniNum = PlayerPrefs.GetInt("MiniStage");

        Tutorial[0] = GameObject.Find("1.1");
        Tutorial[1] = GameObject.Find("1.2");
        //Tutorial[2] = GameObject.Find("1.3");

        //Object의 Active 조정//
        for (int i = 0; i < Tutorial.Length; i++)
        {
            if (i == miniNum)
             Tutorial[i].SetActive(true); 
            else
              Tutorial[i].SetActive(false);
        }
    }

    void Update()
    {
        //컴 테스트용
       // if (Input.GetKey(KeyCode.RightArrow))   ChangeTutorial();
    }

	//각 세부 튜토리얼에서 끝날 때마다 호출되는 함수
    //UI의 Restart나, stage 조정에 있어서 이전 방식이 안좋아서 임의로 수정했어요. 미안해요.
	public void ChangeTutorial () {
        
       
        //마지막 MiniStage일 경우 지금은 튜토리얼이 2개뿐임.
        if (PlayerPrefs.GetInt("MiniStage") >= 1)
        {
            //튜토리얼은 다 클리어 했으니 miniStage는 0으로 해줌.
            PlayerPrefs.SetInt("MiniStage", 0);
            //MainStage는 다음으로 넘어가야함. 
            //근데 사실상... 우리는 씬이 4개라 MainStage 변수는 지금은 필요 없어보임.
            //후에 이전 Stage로 돌아갈 수있게 만들거라면 필요함.
            //이경우 MiniStage 관련 변수를 세분화하는 게 관리하기 편함.
            PlayerPrefs.SetInt("MainStage", PlayerPrefs.GetInt("MainStage")+1);
            Application.LoadLevel("Stage1");
        }
        else  //아닐 경우
        {
            PlayerPrefs.SetInt("MiniStage", PlayerPrefs.GetInt("MiniStage")+1);
            Application.LoadLevel("Tutorial");
        }
	}
}
