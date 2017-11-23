using UnityEngine;
using System.Collections;

public class PlayingFan : MonoBehaviour
{
    public GameObject Fan;
    public GameObject[] port = new GameObject[2];

    BoxCollider2D boxcol;

    bool longenough;

    // Use this for initialization
    void Awake()
    {
        Fan = GameObject.Find("FanHead");
        //Fan.GetComponent<BoxCollider2D>().isTrigger = false;
        longenough = false;

        switch (gameObject.name)
        {
            case "head_fan":
                port[0] = GameObject.Find("noport_fan");
                port[1] = GameObject.Find("upperport_fan");
                port[1].SetActive(false);
                break;

            case "filledport_fan":
                port[0] = GameObject.Find("upperport_fan");
                port[1] = GameObject.Find("noport_fan");
                break;
        }

        boxcol = Fan.GetComponent<BoxCollider2D>();

    }

    void Update()
    {
        if(longenough)
        {
           boxcol.size = new Vector3 (5,0,0);
        }
    }

    // Update is called once per frame
    void OnMouseDown()
    {
        if(gameObject.name=="head_fan")
        {
            Fan.GetComponent<BoxCollider2D>().enabled = true;
            port[1].SetActive(true);
            port[0].SetActive(false);
        }
        else if (gameObject.name == "filledport_fan")
        {
            longenough = false;
            port[1].SetActive(true);
            port[0].SetActive(false);
        }

    }
}
