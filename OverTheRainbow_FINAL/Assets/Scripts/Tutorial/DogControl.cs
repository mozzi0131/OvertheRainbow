using UnityEngine;
using System.Collections;

public class DogControl : MonoBehaviour {

    private GameObject Dog;
    private GameObject TutorialManager;
    private int speed = 5;
    public bool canGo = false;  //튜토리얼 2단계에서 쓰일 변수
    int miniNum; //MiniStage 저장용 변수

    void Start()
    {
        TutorialManager = GameObject.Find("MainCamera");
        Dog = GameObject.Find("Dog");
        miniNum = PlayerPrefs.GetInt("MiniStage");
    }

    void Update()
    {
        if(miniNum==1) //튜토리얼 2단계일 때
        {
            if (canGo) Moving();  //허락(Unlock)이 떨어지면 움직임.
        }


    }

    // 강아지 이동
    public void Moving()
    {
        Dog.transform.position = new Vector3(Dog.transform.position.x, Dog.transform.position.y, Dog.transform.position.z);
        Dog.transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(miniNum!=1)  //두번째 튜토리얼이 아니면
        TutorialManager.GetComponent<Tutorial1Manager>().ChangeTutorial();
        else if (other.CompareTag("Door"))
        {
            TutorialManager.GetComponent<Tutorial1Manager>().ChangeTutorial();
        }
    }
}
