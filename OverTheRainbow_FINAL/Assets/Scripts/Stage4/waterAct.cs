using UnityEngine;
using System.Collections;

public class waterAct : MonoBehaviour {

    stage4Manager checkBools;
    bool sumyeonZe;
    public GameObject[] cupState;


    GameObject audioManager;
    AudioSource audioChange;
    AudioClip sumyeon;

    void Awake()
    {
        checkBools = GameObject.Find("MainCamera").GetComponent<stage4Manager>();


        audioManager = GameObject.Find("soundManager");
        audioChange = audioManager.GetComponent<AudioSource>();
       sumyeon = audioManager.GetComponent<audioControl>().sumyeon;
    }


    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (checkBools.dDay == 4 && !cupState[3].active)
        {
            cupState[1].SetActive(false);
            cupState[3].SetActive(true);
        }

	}
    
    void OnMouseDown()
    {
        //수면제 넣어벌임. 상태도 바뀌어야 함. 
        if (checkBools.sumyeon&&checkBools.dDay==6)
        {
            sumyeonZe = true;
            AudioSource.PlayClipAtPoint(sumyeon, new Vector3(0, 0, -5));
            Debug.Log("수면제넣음");
            cupState[0].SetActive(false);
            cupState[1].SetActive(true);
            checkBools.dDay--; //5일 남아벌임 + 효과도 보임! 이거 함수 넣어.
        }
    }




}
