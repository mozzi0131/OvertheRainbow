using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
    public static int MainStage = 0;
    public static int MiniStage = 0;

	//데이터 값을 저장할 함수 save
	public static void Save () {
        PlayerPrefs.SetInt("MainStage", MainStage);
        PlayerPrefs.SetInt("MiniStage", MiniStage);
    }

    //데이터 값을 불러올 함수 load
    public static void Load()
    {
        PlayerPrefs.GetInt("MainStage");
        PlayerPrefs.GetInt("MiniStage");
    }
}
