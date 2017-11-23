using UnityEngine;
using System.Collections;

public class Callbutton : MonoBehaviour {
    GameObject camera;
	// Use this for initialization
	void Start ()
    {
        camera = GameObject.Find("MainCamera");

        camera.GetComponent<SettingThings>().call();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
