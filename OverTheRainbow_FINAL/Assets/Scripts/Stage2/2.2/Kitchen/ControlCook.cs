using UnityEngine;
using System.Collections;

public class ControlCook : MonoBehaviour {
    public int breadnum;
    GameObject camera;
    GameObject audioManager;
    GameObject fincontrol;
    public GameObject getCoffee;
    AudioClip fincook;
    private bool call;
    bool firstcall;

    void Awake()
    {
        camera = GameObject.Find("MainCamera");
        fincontrol = GameObject.Find("FinCook");
        getCoffee = GameObject.Find("WaterPot");
    }
	// Use this for initialization
	void Start ()
    {
        call = false;
        firstcall = false;
        audioManager = GameObject.Find("soundManager");
        fincook = audioManager.GetComponent<audioControl>().chimSound;
        breadnum = 0;
	}

    // Update is called once per frame
    void Update()
    {
        if (breadnum >= 2 && getCoffee.GetComponent<CoffeePotControl>().coffeeFinish == true && camera.GetComponent<Camera>().transform.position.x < 0 )
        {
            if(firstcall == false)
            {
                call = true;
                firstcall = true;
            }
        }

        if(call==true)
        {
            StartCoroutine(Waiting());
            call = false;
        }
    }

    IEnumerator Waiting()
    {
        AudioSource.PlayClipAtPoint(fincook, new Vector3(0, 0, -5));
        yield return new WaitForSeconds(fincook.length);
        fincontrol.GetComponent<BoxCollider2D>().enabled = true;
        fincontrol.GetComponent<FinControl>().sparkle.SetActive(true);
    }
}
