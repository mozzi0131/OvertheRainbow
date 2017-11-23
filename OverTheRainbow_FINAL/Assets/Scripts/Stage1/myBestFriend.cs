using UnityEngine;
using System.Collections;

public class myBestFriend : MonoBehaviour {

    GameObject dogDog;

    void Awake()
    {
        dogDog = GameObject.Find("Dog");
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseDown()
    {
        Debug.Log("dsaf22233");
        StartCoroutine(dogDog.GetComponent<controlDog>().bark());
    }

}
