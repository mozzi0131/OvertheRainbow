using UnityEngine;
using System.Collections;

public class GotoTutorial3 : MonoBehaviour {

    private GameObject TutorialManager;
    private int speed = 5;

    void Start()
    {
        TutorialManager = GameObject.Find("MainCamera");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //부딪히면 튜토리얼 스테이지를 바꾸는 함수 호출
        TutorialManager.GetComponent<Tutorial1Manager>().ChangeTutorial();
    }
}
