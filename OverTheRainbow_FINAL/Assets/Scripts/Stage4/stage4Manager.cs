using UnityEngine;
using System.Collections;

public class stage4Manager : MonoBehaviour {

    Camera thisCamera;
    GameObject[] scenes = new GameObject[3];
    GameObject[] dogFood = new GameObject[3];
    GameObject[] cageDoors = new GameObject[3];
    GameObject[] cageLocks = new GameObject[3];
    public GameObject[] days = new GameObject[8];
    public GameObject[] dogdogs;
    GameObject guard;

    //열린거 제외, 반시계 방향으로 0,1,2 체크.

    //여는 게 가능한지 판정
    public bool keyForScene3;
    public bool canGetLastKey;
    public bool keyForSumyeon;
    public bool sumyeon;
    public bool cctvIsOff;
    public bool nowSleeping;
    public bool[] keyForDog;
    public bool mikkiDrop;
    public bool noBooks;
    public bool checkMyDogKey;
    public bool dustOff;
    

    public int dDay; //dDay 7일 전부터 시작. 


    void Awake()
    {
        thisCamera = this.GetComponent<Camera>();
        scenes[0] = GameObject.Find("scene1");
        scenes[1] = GameObject.Find("scene2");
        scenes[2] = GameObject.Find("scene3");
        dogFood[0] = GameObject.Find("food1");
        dogFood[1] = GameObject.Find("food2");
        dogFood[2] = GameObject.Find("food3");
        cageDoors[0]= GameObject.Find("cagedoor2");
        cageDoors[1] = GameObject.Find("cagedoor3");
        cageDoors[2] = GameObject.Find("cagedoor4");
        cageLocks[0]= GameObject.Find("lock2");
        cageLocks[1] = GameObject.Find("lock3");
        cageLocks[2] = GameObject.Find("lock4");
        guard = GameObject.Find("Guard");
        dDay = 7;
        int startNum = 26;
        for(int i=0; i < days.Length; i++)
        {
            days[i] = GameObject.Find(startNum + "");
            startNum--;
        }
        
        setScene(1);
        
        keyForDog = new bool[2];
        keyForDog[0] = false;
        keyForDog[1] = false;
        mikkiDrop = false;
        keyForScene3 = false;
        canGetLastKey = false;
        keyForSumyeon = false;
        sumyeon = false;
        cctvIsOff = false;
        nowSleeping = false;
        noBooks = false;
        checkMyDogKey = false;
        dustOff = false;
    }


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void zoomInCamera(float x1, float y1, float z1, float size1)
    {
        this.transform.position = new Vector3(x1, y1, z1);
        thisCamera.orthographicSize = size1;

    }

    public void setNormal()
    {
        this.transform.position = new Vector3(0,0,-10);
        thisCamera.orthographicSize = 5;
    }

    public void setScene(int sceneNum)
    {

        if(sceneNum == 1)
        {

            float randomNumber = Random.value;
            int fixedNumber = 1;
            if (randomNumber < 0.34) fixedNumber = 2; else if (randomNumber > 0.64) fixedNumber = 0;

            for (int i = 0; i < dogFood.Length; i++)
            {
                if (i == fixedNumber) dogFood[i].SetActive(true);
                else dogFood[i].SetActive(false);

            }

            for (int j=0; j < days.Length; j++)
            {
                if (j == dDay) days[j].SetActive(true);
                else days[j].SetActive(false);
            }

        }
        if (sceneNum == 3)
        {
            guard.GetComponent<guardAct>().nowSleep = true;
            guard.GetComponent<guardAct>().nowActCoroutine = false;
            guard.GetComponent<guardAct>().checkHat();
        }

        for(int i=0; i < scenes.Length; i++)
        {
            if (i == sceneNum-1) scenes[i].SetActive(true);
            else scenes[i].SetActive(false);

        }

        setDogCage();
    }


    //개는 총 세번 없어짐.
    void setDogCage()
    {
        if (mikkiDrop&&dDay>3) dogdogs[1].SetActive(true);

        if(sumyeon && dDay == 5)
        {
            Destroy(dogdogs[2]);
            Destroy(cageLocks[2]);

        }else if (dDay <=3&& dogdogs[0] != null)
        {
           
                Destroy(dogdogs[0]);
                Destroy(cageLocks[0]);
            dogdogs[4].SetActive(true);
            

        }

        if (checkMyDogKey&&canGetLastKey)
        {
            Destroy(dogdogs[3]);
            Destroy(cageLocks[1]);


        }
    }
}
