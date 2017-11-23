using UnityEngine;
using System.Collections;


//master에게 붙어있는 코드
public class ControlBall : MonoBehaviour {
    public GameObject[] ball = new GameObject[4];
    GameObject balloon;
    public int playball;
    public bool thrown;

    void Awake()
    {
        ball[0] = GameObject.Find("ball1");
        ball[1] = GameObject.Find("ball2");
        ball[2] = GameObject.Find("ball3");
        ball[3] = GameObject.Find("ball4");
        balloon = GameObject.Find("talking");
        playball = 0;
        thrown = false;

        ball[1].SetActive(false);
        ball[2].SetActive(false);
        ball[3].SetActive(false);
        balloon.SetActive(false);
    }
	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void OnMouseDown ()
    {
        if(!thrown)
        {
            thrown = true;
            ball[playball].GetComponent<Animation>().Play();
            playball++;
        }
        if(thrown == true)
        {
            StartCoroutine(talking());
        }
	}

    IEnumerator talking()
    {
        balloon.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        balloon.SetActive(false);
    }
}
