using UnityEngine;
using System.Collections;

public class selectStageInLoading : MonoBehaviour {

    GameObject audioManager;
	// Use this for initialization
	void Start () {
        audioManager = GameObject.Find("soundManager");
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseDown()
    {  //이건 무조건 재클리어 할 때만 이렇게 입장 함.
  
        switch (this.name)
        {
            case "1":
                Application.LoadLevel("Stage1");
                break;
            case "2":
                Application.LoadLevel("Stage2");
                break;
            case "3": //두번째 액자
                PlayerPrefs.SetInt("CurrentMiniStage", 1);
                Application.LoadLevel("Stage2");
                break;
            case "4":
                Application.LoadLevel("Stage3");
                break;
            case "5":
                PlayerPrefs.SetInt("CurrentMiniStage", 1);
                Application.LoadLevel("Stage3");
                break;
            case "6":
                Application.LoadLevel("Stage4");
                break;
            default:
                Application.LoadLevel("Loading");
                break;

        }

    }

    IEnumerator test()
    {
        audioManager.GetComponent<audioControl>().plzDown = true;
        yield return new WaitForSeconds(1.0f);
       
    }
}
