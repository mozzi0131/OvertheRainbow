using UnityEngine;
using System.Collections;

public class TouchColor : MonoBehaviour {
    GameObject dog;
    GameObject camera;
    bool go;
    public int getballoon;

    Animator DogAni;

	// Use this for initialization
	void Awake ()
    {
        getballoon = 0;
        dog = GameObject.Find("Dog");
        camera = GameObject.Find("MainCamera");
        DogAni = dog.GetComponent<Animator>();
	}

    void Update()
    {
        if(go==true)
        {
            dog.transform.Translate(5.0f * Time.deltaTime, 0, 0);
        }
    }
	
	// Update is called once per frame
	void OnMouseDown ()
    {
        DogAni.SetBool("canGo", true);
        go = true;
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.name=="Dog")
        {
            go = false;
            camera.GetComponent<stageManager>().clearStage();
        }
    }
}
