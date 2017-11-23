using UnityEngine;
using System.Collections;

public class cheat : MonoBehaviour {
    GameObject Door;
	// Use this for initialization
	void Start () {
        Door = GameObject.Find("Door");
	}
	
	// Update is called once per frame
	void OnMouseDown () {
        Door.GetComponent<Password>().unlock = false;
	}
}
