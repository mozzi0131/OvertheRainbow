using UnityEngine;
using System.Collections;

public class CoffeePotControl : MonoBehaviour {
    private Vector3 screenPoint;
    public GameObject[] pots = new GameObject[3];
    public GameObject CoffeeMaker;
    public GameObject[] cup = new GameObject[2];
    public bool getWater, getCoffee,coffeeFinish;

    //효과음
    GameObject audioManager;
    AudioSource audioChange;
    AudioClip coffeepotWater;
    AudioClip coffeeInCup;
    AudioClip potWater;


    void Awake()
    {
        pots[0] = GameObject.Find("coffeepot");
        pots[1] = GameObject.Find("waterfilledpot");
        pots[2] = GameObject.Find("coloredcoffeepot");
        pots[1].SetActive(false);
        pots[2].SetActive(false);
        cup[0] = GameObject.Find("cup");
        cup[1] = GameObject.Find("filledcup");
        CoffeeMaker = GameObject.Find("Coffeepot");
    }


	void Start ()
    {
        //sound
        audioManager = GameObject.Find("soundManager");
        audioChange = audioManager.GetComponent<AudioSource>();
        coffeeInCup = audioManager.GetComponent<audioControl>().coffeeInCup;
        coffeepotWater = audioManager.GetComponent<audioControl>().coffeepotWater;
        potWater = audioManager.GetComponent<audioControl>().potWater;

        //etc
        getWater = false;
        getCoffee = false;
        coffeeFinish = false;
	}


    void OnMouseDown()
    { 
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
    }

    void OnMouseDrag()
    {
        Vector3 currentScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 currentPos = Camera.main.ScreenToWorldPoint(currentScreenPoint);
        transform.position = currentPos;
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D other)
    {
        if(!getWater && other.name =="water")
        {
            getWater = true;
            StartCoroutine(Waiting());
            
        }
        if(other.gameObject.name == "Coffeepot" && getWater==true)
        {
            other.gameObject.GetComponent<CoffeeMaker>().getwater = true;
            getWater = false;
            pots[1].SetActive(false);
            pots[0].SetActive(true);
            AudioSource.PlayClipAtPoint(coffeepotWater, new Vector3(0, 0, -5));
            
        }
        if(other.name=="cup" && getCoffee)
        {
            pots[2].SetActive(false);
            pots[0].SetActive(true);
            other.gameObject.GetComponent<SetPosition>().changeCup();
            gameObject.transform.position = new Vector3(-19.6f, 0, 0);
            AudioSource.PlayClipAtPoint(coffeeInCup, new Vector3(0, 0, -5));
            coffeeFinish = true;
           
        }
    }

    public void GetCoffee()
    {
        pots[0].SetActive(false);
        pots[2].SetActive(true);
    }

    IEnumerator Waiting()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        AudioSource.PlayClipAtPoint(potWater, new Vector3(0, 0, -5));
        yield return new WaitForSeconds(potWater.length);
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        CoffeeMaker.GetComponent<CoffeeMaker>().PotwithWater = gameObject;
        pots[1].SetActive(true);
        pots[0].SetActive(false);
    }
}
