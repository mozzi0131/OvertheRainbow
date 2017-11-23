//더 고민해봐야 하는 코드임
//이건 무시해주세영...

using UnityEngine;
using System.Collections;

public class CallMiniStage : MonoBehaviour {
    static public CallMiniStage instance;
    //static형 startcoroutine에 대한 검색들. 
    //그리고 이것만 아니더라도 이 코드에 대해 고민좀 더 해봐야함
    //http://answers.unity3d.com/questions/140107/public-static-ienumerator.html
    //https://community.unity.com/t5/Scripting/C-Coroutines-in-static-functions/td-p/1038934

    public static GameObject[] LoadImage = new GameObject[5];
    public static GameObject[] Ministage = new GameObject[5];
    public static int i = 0;
    public static float delay;
    public static bool check;

	// Use this for initialization
	void Start () {
        LoadImage[0].SetActive(true);
        Ministage[0].SetActive(true);
        for(i=1;i<5;i++)
        {
            LoadImage[i].SetActive(false);
            Ministage[i].SetActive(false);
        }
	}

    //게임 진행시 문에서 collision이 감지되면 changestage()를 호출함 
    //그림 호출, 게임 object들 호출 간 delay를 위해 IEnumerator를 사용함
    //아직 어떻게 쓸지 고민 못해서 코드가 개판임
    public static void ChageStage () {
        check = false;
        //parameter가 함수에 delay를 넣는 것에 관한 링크
        //http://answers.unity3d.com/questions/926236/c-invoke-function-with-params-and-delay.html
        instance.StartCoroutine(LoadingImage(false, 2.0f));
        
        Ministage[i].SetActive(false);
        i++;
        LoadImage[i].SetActive(true);
        Ministage[i].SetActive(true);

    }

    public static IEnumerator LoadingImage(bool check,float delay)
    {
        yield return new WaitForSeconds(delay);
        LoadImage[i].SetActive(check);
    }

    void LoadingMiniStage(bool check)
    {
        Ministage[i].SetActive(check);
    }
}
