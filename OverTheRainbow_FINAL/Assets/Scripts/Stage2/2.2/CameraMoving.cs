
using UnityEngine;
using System.Collections;

public class CameraMoving : MonoBehaviour {
    public GameObject Camera;
    GameObject right;
    GameObject left;

	// Use this for initialization
	void Start () {
        Camera = GameObject.Find("MainCamera");
        right = GameObject.Find("Right");
        left = GameObject.Find("Left");
	}
	
	// Update is called once per frame
	public void SetButton () {

        if(gameObject.transform.position.x==-20)
        {
            left.SetActive(false);
            right.SetActive(true);
        }
        else if(gameObject.transform.position.x==0)
        {
            right.SetActive(false);
            left.SetActive(true);
        }

        
    }
}
