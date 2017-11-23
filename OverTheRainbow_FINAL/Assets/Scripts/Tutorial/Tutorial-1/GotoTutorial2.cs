using UnityEngine;
using System.Collections;

public class GotoTutorial2 : MonoBehaviour {
    private GameObject Dog;
    private GameObject TutorialManager;
    private int speed = 5;

    void Start()
    {
        TutorialManager = GameObject.Find("MainCamera");
        Dog = GameObject.Find("Dog");
    }

	// 강아지 이동
	public void Moving () {
        Dog.transform.position = new Vector3(Dog.transform.position.x, Dog.transform.position.y, Dog.transform.position.z);
        Dog.transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
	}
     
    void OnTriggerEnter2D(Collider2D other)
    {
        //부딪히면 튜토리얼 스테이지를 바꾸는 함수 호출
        TutorialManager.GetComponent<Tutorial1Manager>().ChangeTutorial();
    }
}
