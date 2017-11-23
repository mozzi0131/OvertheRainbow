using UnityEngine;
using System.Collections;

public class ControlPort : MonoBehaviour {
    public GameObject[] port = new GameObject[2];
    public GameObject coffemaker;
    public bool inport;
	// Use this for initialization
	void Awake ()
    {
        coffemaker = GameObject.Find("Coffeepot");
        switch(gameObject.name)
        {
            case "head":
                port[0] = GameObject.Find("noport");
                port[1] = GameObject.Find("upperport");
                port[1].SetActive(false);
                break;

            case "filledport":
                port[0] = GameObject.Find("upperport");
                port[1] = GameObject.Find("noport");
                break;
        }

        inport = false;
	
	}
	
	// Update is called once per frame
	void OnMouseDown ()
    {
        print("onmousedown detected!");
        port[1].SetActive(true);
        port[0].SetActive(false);

        if (gameObject.name == "head")
        {
            coffemaker.GetComponent<CoffeeMaker>().fillport = true;

        }
        else if(gameObject.name=="filledport")
            coffemaker.GetComponent<CoffeeMaker>().fillport  = false;
    }
}
