using UnityEngine;
using System.Collections;

public class DogMoving : MonoBehaviour {
    private float speed = 5.0f;
    public GameObject S1manager;
    public int balloonnum;
    public int miniNum;
    public bool CanGo;
    public bool UpStair;
    public bool Fly;


    //강아지 관련 변수 초기화
    void Start()
    {
        miniNum = PlayerPrefs.GetInt("MiniStage");
        balloonnum = 0;
        CanGo = false;
        UpStair = false;
        Fly = false;
        S1manager = GameObject.Find("MainCamera");
        
        if(PlayerPrefs.GetInt("MiniStage")==4)
        {
            gameObject.transform.position = new Vector3(-6, 0.2f, 0);
        }
    }

    //변수 체크해서 강아지 이동을 제어하는 함수 호출
    void Update()
    {
        
        if (miniNum < 3)
        {
            if (CanGo)
                Moving();
        }
        else if (miniNum == 3)
        {
            if (UpStair)
                GoUp();
       }
       else if (miniNum == 4)
       {
            if (Fly)
                FlyingDog();
        }
    }

    //강아지의 이동에 관해 제어하는 함수 moving, goup, flyingdog
    public void Moving()
    {
        gameObject.transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
    }

    public void GoUp()
    {
        gameObject.transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
    }

    public void FlyingDog()
    {
        gameObject.transform.Translate(new Vector3(Time.deltaTime, 0, 0));
    }
    
    //색에 충돌했을 때만 스테이지가 변화함.
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Color"))
        {
            S1manager.GetComponent<Stage1Manager>().ChangeStage1();
        }

        if(other.CompareTag("Dish"))
        {
            gameObject.transform.Translate(new Vector3(0, other.gameObject.transform.localScale.y, 0));
        }

        if(other.CompareTag("Table"))
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, 1.5f, gameObject.transform.position.z);
        }

    }
}
