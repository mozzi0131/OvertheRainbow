using UnityEngine;
using System.Collections;

public class Stage1Manager : MonoBehaviour {

    public GameObject[] Stage1 = new GameObject[5];
    //public int setMiniNum;
   
	// Use this for initialization
	void Start ()
    {
        
        Debug.Log(PlayerPrefs.GetInt("MiniStage"));
        for (int i = 0; i < Stage1.Length; i++)
        {
            if (i == PlayerPrefs.GetInt("MiniStage"))
            //if(i== setMiniNum) //컴 테스팅 용
                Stage1[i].SetActive(true);
            else
                Stage1[i].SetActive(false);
        }
	}

    public void ChangeStage1 () {
        if(PlayerPrefs.GetInt("MiniStage") >= 4)
        {
            PlayerPrefs.SetInt("MiniStage", 0);
            PlayerPrefs.SetInt("MainStage", PlayerPrefs.GetInt("MainStage") + 1);
            Application.LoadLevel("Stage2");
        }
        else
        {
            PlayerPrefs.SetInt("MiniStage", PlayerPrefs.GetInt("MiniStage") + 1);
            Application.LoadLevel("Stage1");
        } 
    }
}
