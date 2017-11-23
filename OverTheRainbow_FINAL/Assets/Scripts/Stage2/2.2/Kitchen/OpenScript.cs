using UnityEngine;
using System.Collections;

public class OpenScript : MonoBehaviour {
    public GameObject[] DoorObject = new GameObject[2];
    public bool closed;

    //효과음
    GameObject audioManager;
    AudioSource audioChange;
    AudioClip openCabinet;
    AudioClip closeCabinet;

    // Use this for initialization
    void Awake() {
        closed = true;

        Debug.Log(gameObject.name);

        switch (gameObject.name)
        {
            case "topopen":
                DoorObject[0] = gameObject;
                DoorObject[1]= GameObject.Find("topclosed");
                break;
            case "secondtopopen":
                DoorObject[0] = gameObject;
                DoorObject[1] = GameObject.Find("secondtopclosed");
                break;
            case "thirdtopopen":
                DoorObject[0] = gameObject;
                DoorObject[1] = GameObject.Find("thirdtopclosed");
                break;
            case "topclosed":
                DoorObject[0] = gameObject;
                DoorObject[1] = GameObject.Find("topopen");
                DoorObject[1].SetActive(false);
                break;
            case "secondtopclosed":
                DoorObject[0] = gameObject;
                DoorObject[1] = GameObject.Find("secondtopopen");
                DoorObject[1].SetActive(false);
                break;
            case "thirdtopclosed":
                DoorObject[0] = gameObject;
                DoorObject[1] = GameObject.Find("thirdtopopen");
                DoorObject[1].SetActive(false);
                break;
        }
	}

    void Start()
    {
        audioManager = GameObject.Find("soundManager");
        audioChange = audioManager.GetComponent<AudioSource>();
        openCabinet = audioManager.GetComponent<audioControl>().openCabinet;
        closeCabinet = audioManager.GetComponent<audioControl>().closeCabinet;
    }

    /*public void SettingDoor()
    {
        switch (gameObject.name)
        {
            case "topopen":
                DoorObject[0] = gameObject;

                DoorObject[1] = GameObject.Find("topclosed");
                break;
            case "secondtopopen":
                DoorObject[0] = gameObject;
                DoorObject[1] = GameObject.Find("secondtopclosed");
                break;
            case "thirdtopopen":
                DoorObject[0] = gameObject;
                DoorObject[1] = GameObject.Find("thirdtopclosed");
                break;
            case "topclosed":
                DoorObject[0] = gameObject;
                DoorObject[1] = GameObject.Find("topopen");
                DoorObject[1].SetActive(false);
                break;
            case "secondtopclosed":
                DoorObject[0] = gameObject;
                DoorObject[1] = GameObject.Find("secondtopopen");
                DoorObject[1].SetActive(false);
                break;
            case "thirdtopclosed":
                DoorObject[0] = gameObject;
                DoorObject[1] = GameObject.Find("thirdtopopen");
                DoorObject[1].SetActive(false);
                break;
        }
    }*/
	
	// Update is called once per frame
	void OnMouseDown ()
    {
        if (gameObject.tag == "Close")
            AudioSource.PlayClipAtPoint(openCabinet, new Vector3(0, 0, -5));
        else if (gameObject.tag == "Open")
            AudioSource.PlayClipAtPoint(closeCabinet, new Vector3(0, 0, -5));

        DoorObject[1].SetActive(true);
        DoorObject[0].SetActive(false);
    }
        
	}

