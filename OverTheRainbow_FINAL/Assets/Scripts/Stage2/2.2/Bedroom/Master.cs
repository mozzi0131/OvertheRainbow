using UnityEngine;
using System.Collections;

public class Master : MonoBehaviour {
    public bool cookfinish;
    public GameObject[] moving = new GameObject[2];
    public GameObject[] talking = new GameObject[2];
    GameObject colorball;

	// Use this for initialization
	void Start () {
        moving[0] = GameObject.Find("master1");
        moving[1] = GameObject.Find("master2");
        talking[0] = GameObject.Find("sleepyspeech");
        talking[1] = GameObject.Find("awakespeech");
        colorball = GameObject.Find("colorball_1");
        cookfinish = false;
        moving[0].SetActive(true);
        moving[1].SetActive(false);
        talking[0].SetActive(false);
        talking[1].SetActive(false);
        colorball.SetActive(false);
	
	}
	
	// Update is called once per frame
	void OnMouseDown () {
        StartCoroutine(motion());
	}

    IEnumerator motion()
    {
        if(cookfinish==false)
        {
            moving[0].SetActive(false);
            moving[1].SetActive(true); 
            yield return new WaitForSeconds(0.8f);
            talking[0].SetActive(true);
            yield return new WaitForSeconds(1.2f);
            moving[1].SetActive(false);
            moving[0].SetActive(true);
            talking[0].SetActive(false);
        }
        if (cookfinish == true)
        {
            moving[0].SetActive(false);
            moving[1].SetActive(true); 
            yield return new WaitForSeconds(0.8f);
            talking[1].SetActive(true);
            yield return new WaitForSeconds(1.2f);
            moving[1].SetActive(false);
            moving[0].SetActive(true);
            talking[1].SetActive(false);
            yield return new WaitForSeconds(1.2f);
            colorball.SetActive(true);
        }


    }
}
