using UnityEngine;
using System.Collections;

public class ArrowScript : MonoBehaviour {
    public GameObject camera;
    public GameObject StartGame;
    public GameObject Dog;
    
    // Use this for initialization
    void Start()
    {
        camera = GameObject.Find("MainCamera");
        StartGame = GameObject.Find("StartGame");
        Dog = GameObject.Find("Dog");
    }
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseDown()
    {
        Debug.Log(gameObject.name);
     if(gameObject.name=="leftarrow")
        {
            Debug.Log("leftarrow picked");
           // StartGame.transform.Translate(-20, 0, 0);
            camera.transform.Translate(-20, 0, 0);
            Dog.transform.Translate(-20, 0, 0);
            gameObject.transform.parent.Translate(-20, 0, 0);
            StartGame.GetComponent<CameraChanging>().Setting();
        }
        if (gameObject.name == "rightarrow")
        {
            Debug.Log("rightarrow picked!");
            //StartGame.transform.Translate(20, 0, 0);
            camera.transform.Translate(20, 0, 0);
            Dog.transform.Translate(20, 0, 0);
            gameObject.transform.parent.Translate(20, 0, 0);
            StartGame.GetComponent<CameraChanging>().Setting();
        }
    }
}
