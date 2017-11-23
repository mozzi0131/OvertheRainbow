using UnityEngine;
using System.Collections;

public class doorKnob : MonoBehaviour {

    public bool canOpen = false;
    public GameObject[] knob = new GameObject[2];
    public GameObject text;
    public GameObject dog;
    int currentMiniNum = 0;
    bool firstOpen = false;

    //Sound
    GameObject audioManager;
    public AudioClip doorLocked;
    public AudioClip doorOpen;

    // Use this for initialization
    void Start () {
        if (PlayerPrefs.GetInt("MainStage") == 0)
        {
            currentMiniNum = PlayerPrefs.GetInt("MiniStage");
        }
        else
        {
            currentMiniNum = PlayerPrefs.GetInt("CurrentMiniStage");
        }

        dog = GameObject.Find("Dog");
        audioManager = GameObject.Find("soundManager");
        doorLocked = audioManager.GetComponent<audioControl>().doorLocked;
        doorOpen = audioManager.GetComponent<audioControl>().doorOpen;

    }
	
	// Update is called once per frame
	void Update () {

        if (canOpen)
        {
            if(!firstOpen)
            {
                firstOpen = true;
                AudioSource.PlayClipAtPoint(doorOpen, new Vector3(0, 0, -5));
            }
        }
        
	}

    void OnMouseDown()
    {

        if (!canOpen)
        {   //문 잠긴 소리

            StartCoroutine(tryOpenTheDoor());
            StartCoroutine(textPrint(text));
            if(currentMiniNum!=2)
            dog.GetComponent<showHint>().canGiveHint = true;
        }

    }


    IEnumerator tryOpenTheDoor()
    {
        AudioSource.PlayClipAtPoint(doorLocked, new Vector3(0, 0, -5));
        knob[0].SetActive(false);
        knob[1].SetActive(true);
        yield return new WaitForSeconds(0.3f);
        knob[1].SetActive(false);
        knob[0].SetActive(true);
    }

    IEnumerator textPrint(GameObject obj)
    {
        obj.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        obj.SetActive(false);
    }
}
