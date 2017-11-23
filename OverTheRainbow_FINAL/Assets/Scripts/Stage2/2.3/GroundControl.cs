using UnityEngine;
using System.Collections;

public class GroundControl : MonoBehaviour {
    GameObject beforeground;
    GameObject[] ground = new GameObject[2];
    GameObject master;
    GameObject camera;


	// Use this for initialization
	void Start ()
    {
        beforeground = GameObject.Find("Ground");
        ground[0] = GameObject.Find("littleholeground");
        ground[1] = GameObject.Find("bigholeground");
        master = GameObject.Find("Master");
        camera = GameObject.Find("MainCamera");

        beforeground.SetActive(false);
        ground[1].GetComponent<SpriteRenderer>().enabled = false;
        ground[1].GetComponent<BoxCollider2D>().enabled = false;
	}
	
	// Update is called once per frame
	void OnTriggerEnter2D (Collider2D other)
    {
        if(other.name == "littleholeground" && master.GetComponent<ControlBall>().playball==4)
        {
            other.gameObject.SetActive(false);
            ground[1].SetActive(true);
            gameObject.transform.position = new Vector3(4.5f,-14.6f,-1);
        }
	}

    IEnumerator waiting()
    {
        yield return new WaitForSeconds(1.0f);
        camera.transform.position = new Vector3(11.5f, -10, -10);
    }
}
