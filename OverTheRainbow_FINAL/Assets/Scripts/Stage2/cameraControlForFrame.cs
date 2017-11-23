using UnityEngine;
using System.Collections;

public class cameraControlForFrame : MonoBehaviour {

    //Get camera Info
    public GameObject camera;
    Camera cameraControl;
    Vector3 firstPos;
    bool zoomIn = false;
    bool nowSelect = false;  //Stage 진입중일 때

    int currentMininum = 0;
    int mainStage = 0;

    public GameObject[] backGround = new GameObject[2];
    public GameObject wallFrame;

    //Sound
    GameObject audioManager;
    AudioClip wrongFrame;
    AudioClip gogoPast;

    // Use this for initialization
    void Start () {

        mainStage = PlayerPrefs.GetInt("MainStage");


        if (mainStage <= 1)
            currentMininum = PlayerPrefs.GetInt("MiniStage");
        else
            currentMininum = PlayerPrefs.GetInt("CurrentMiniStage");

        camera = GameObject.Find("MainCamera");
        cameraControl = camera.GetComponent<Camera>();
        firstPos = camera.transform.position;

        //backGround[0] = GameObject.Find("miniStage1");
        //backGround[1] = GameObject.Find("miniStage2");
        wallFrame = GameObject.Find("defaultWall");

        //Sound
        audioManager = GameObject.Find("soundManager");
        wrongFrame = audioManager.GetComponent<audioControl>().wrongFrame;
        gogoPast = audioManager.GetComponent<audioControl>().gogoPast;


    }
	
	// Update is called once per frame
	void Update () {
	
	}



    void OnMouseDown()
    {
        
        if (!zoomIn&&!nowSelect)
        {
            zoomInFunc();

        }
        else
        {
            //Stage1클리어 했고, 2-1깨야 할 차례일 때
            if (this.name=="frame2"&& mainStage==1&&currentMininum < 1)  //여기가 다 클리어 안 한 Stage
                StartCoroutine(selectWrongFrame());
            //else if(currentMininum>=1&&this.name=="frame1")  //그냥 1 깬 상태면 1 못 누르게
            //    StartCoroutine(selectWrongFrame());
            else StartCoroutine(startStage());

        }

    }



    void zoomInFunc()
    {
        cameraControl.orthographicSize = 1.5f;
        if (this.name == "frame1")
        {            
            camera.transform.position = new Vector3(0.3f, 2, -10);
        }
        if (this.name == "frame2")
        {
            camera.transform.position = new Vector3(5.5f, -1.3f, -10);
        }

        zoomIn = true;
    }

    void zoomOutFunc()
    {

            cameraControl.orthographicSize = 5;
            camera.transform.position =firstPos;
       
        zoomIn = false;
    }

    IEnumerator selectWrongFrame()
    {

        nowSelect = true;
        AudioSource.PlayClipAtPoint(wrongFrame, new Vector3(0, 0, -5));
        Debug.Log("you Can't Enter!");
        //개 짖는 소리 후 잠깐 쉼
        yield return new WaitForSeconds(0.5f);
        zoomOutFunc();

        nowSelect = false;
    }


    IEnumerator startStage()
    {
        //개 짖는 소리 후 잠깐 쉼
        AudioSource.PlayClipAtPoint(gogoPast, new Vector3(0, 0, -5),0.5f);
        yield return new WaitForSeconds(0.5f);
        zoomOutFunc();
        //암전 효과?ㅁ?....
        if (this.name == "frame1")
        {
            backGround[0].SetActive(true);
            wallFrame.gameObject.SetActive(false);
        }else if (this.name == "frame2")
        {
            backGround[1].SetActive(true);
            wallFrame.gameObject.SetActive(false);
        }
    }


}
