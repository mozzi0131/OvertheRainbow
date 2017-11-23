using UnityEngine;
using System.Collections;

public class GetWater : MonoBehaviour {
    public GameObject[] faucet = new GameObject[2];
    public bool click;

    //sound
    GameObject audioManager;
    AudioSource audioChange;
    AudioClip sinkSound;

    // Use this for initialization
    void Start () {
        //sound
        audioManager = GameObject.Find("soundManager");
        audioChange = audioManager.GetComponent<AudioSource>();
        sinkSound = audioManager.GetComponent<audioControl>().sinkSound;

        //etc
        faucet[0] = GameObject.Find("faucetwheel1");
        faucet[0] = GameObject.Find("faucetwheel2");
        click = false;
        faucet[0].SetActive(true);
        faucet[1].SetActive(false);
	}
	
	// Update is called once per frame
	void OnMouseDown () {
        Debug.Log(click);
        if(click== false)
        {
            AudioSource.PlayClipAtPoint(sinkSound, new Vector3(0, 0, -5));
            faucet[0].SetActive(false);
            faucet[1].SetActive(true);
            click = true;
            Debug.Log("click change");
        }
        else if (click == true)
        {
            faucet[0].SetActive(true);
            faucet[1].SetActive(false);
            click = false;
        }
    }
}
