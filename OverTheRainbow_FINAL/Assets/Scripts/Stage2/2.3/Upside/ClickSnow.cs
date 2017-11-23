using UnityEngine;
using System.Collections;

public class ClickSnow : MonoBehaviour {
    public GameObject[] snow = new GameObject[3];
    GameObject ball;
    public int snowfall;

    void Awake()
    {
        ball = GameObject.Find("ball");
        snowfall = 0;
        //snow[0] = GameObject.Find("lotofsnow");
        //snow[1] = GameObject.Find("mediumsnow");
        //snow[2] = GameObject.Find("littlesnow");

        snow[1].SetActive(false);
        snow[2].SetActive(false);
    }

	// Use this for initialization
	public void Callsnow ()
    {
        snow[0].SetActive(true);
        snow[1].SetActive(false);
        snow[2].SetActive(false);
    }
	
    void Start()
    {
        //gameObject.SetActive(false);
    }
	// Update is called once per frame
	void OnMouseDown ()
    {
        Debug.Log("clicked!");
        if (snowfall==0)
        {
            snow[1].SetActive(true);
            snow[0].SetActive(false);
            snowfall++;
        }

        if(snowfall==1)
        {
            snow[2].SetActive(true);
            snow[1].SetActive(false);
            snowfall++;
        }
        
        if(snowfall==2)
        {
            ball.GetComponent<BallComponent>().falltree = true;
            ball.GetComponent<BallComponent>().cantouch = true;
            snow[0].SetActive(false);
            snowfall = 0;
        }
            
	}
}
