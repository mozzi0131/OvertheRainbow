using UnityEngine;
using System.Collections;

public class CoffeeMaker : MonoBehaviour {
    //오브젝트
    public GameObject Coffeebag;
    public GameObject Waterpot;
    public GameObject PotwithWater;
    public GameObject Steam;
    public GameObject port;
    public GameObject[] coffeeMaker = new GameObject[2];
    public bool getcoffee, getwater,fillport;

    //효과음
    GameObject audioManager;
    AudioSource audioChange;
    AudioClip boilCoffee;


    void Awake()
    {
       
        coffeeMaker[0] = GameObject.Find("opencoffeepot");
        coffeeMaker[1] = GameObject.Find("closedcoffeepot");
        //Coffeebag = GameObject.Find("beforecoffee");
        //Coffeebag의 경우 지금 왜인지... Find를 인식을 못 해서 드래그 앤 드롭으로 설정했어요
        Steam = GameObject.Find("steam");
        Waterpot = GameObject.Find("WaterPot");
        Steam.SetActive(false);
        port = GameObject.Find("head");
        

    }
	// Use this for initialization
	void Start ()
    {
        //sound
        audioManager = GameObject.Find("soundManager");
        audioChange = audioManager.GetComponent<AudioSource>();
        boilCoffee = audioManager.GetComponent<audioControl>().boilCoffee;

        coffeeMaker[1].SetActive(false);
        getcoffee = false;
        getwater = false;
        fillport = port.GetComponent<ControlPort>().inport;
	}
	
	// Update is called once per frame
	void Update () {
        
        

        if(getcoffee==true)
        {
            if(getwater==true)
            {
                coffeeMaker[0].SetActive(false);
                coffeeMaker[1].SetActive(true);
                Waterpot.GetComponent<CoffeePotControl>().GetCoffee();
            }
        }

       else if(getwater == true)
        {
            if(getcoffee == true)
            {
                coffeeMaker[0].SetActive(false);
                coffeeMaker[1].SetActive(true);
                Waterpot.GetComponent<CoffeePotControl>().GetCoffee();
            }

        }

        if (getwater == true && getcoffee == true && fillport == true)
        {
            MakeCoffee();
            Waterpot.GetComponent<CoffeePotControl>().getCoffee = true;
            getcoffee = false;
            getwater = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject == Coffeebag)
        {
            getcoffee = true;
            other.gameObject.SetActive(false);
        }
        
    }

    void MakeCoffee()
    {
        StartCoroutine(TakeTime());
    }

    IEnumerator TakeTime()
    {
        AudioSource.PlayClipAtPoint(boilCoffee, new Vector3(0, 0, -5));
        yield return new WaitForSeconds(2.0f);
        Steam.SetActive(true);
    }
}
