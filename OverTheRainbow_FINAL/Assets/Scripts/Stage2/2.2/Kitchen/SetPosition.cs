using UnityEngine;
using System.Collections;

public class SetPosition : MonoBehaviour {
    GameObject audioManager;
    GameObject Fincontrol;
    AudioSource audioChange;
    AudioClip cupOnCoaster;
    GameObject aftercup;
    GameObject emptycup;

    void Awake()
    {
        emptycup = GameObject.Find("emptycup");
        aftercup = GameObject.Find("filledcup");
        aftercup.SetActive(false);
        Fincontrol = GameObject.Find("FinCook");
    }

    void Start()
    {
        audioManager = GameObject.Find("soundManager");
        audioChange = audioManager.GetComponent<AudioSource>();
        cupOnCoaster = audioManager.GetComponent<audioControl>().cupOnCoaster;
    }
    

	// Use this for initialization
	void OnMouseDown()
    {
        gameObject.transform.parent = Fincontrol.transform;
        gameObject.transform.position = new Vector3(-27.9f, -0.5f, 0);
        AudioSource.PlayClipAtPoint(cupOnCoaster, new Vector3(0, 0, -5));
    }

    public void changeCup()
    {
        aftercup.SetActive(true);
        emptycup.SetActive(false);
    }


}
