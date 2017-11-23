using UnityEngine;
using System.Collections;

public class BalloonControl : MonoBehaviour {
    GameObject[] balloon = new GameObject[3];
    GameObject Dog_down;
    GameObject Dog;
    GameObject camera;
    GameObject Master;
    GameObject colorball;
    public bool seweropened;
    public bool[] dogtouched = new bool[3];
    public bool enoughsize;
    bool changecall;
    Vector3 screenPoint;

    // Use this for initialization
    void Awake()
    {
        balloon[0] = GameObject.Find("Balloon1");
        balloon[1] = GameObject.Find("Balloon2");
        balloon[2] = GameObject.Find("Balloon3");
        Dog_down = GameObject.Find("Dog_down");
        Dog = GameObject.Find("Dog");
        camera = GameObject.Find("MainCamera");
        Master = GameObject.Find("Master");
        colorball = GameObject.Find("colorball_2");
    }

    void Start()
    {
        colorball.GetComponent<CircleCollider2D>().enabled = false;
        colorball.GetComponent<SpriteRenderer>().enabled = false;

        dogtouched[0] = false;
        dogtouched[1] = false;
        dogtouched[2] = false;

        enoughsize = false;
        seweropened = false;
        changecall = false;
	}

    void Update()
    {
        if(colorball.GetComponent<TouchColor>().getballoon==3)
        {
            Dog_down.transform.Translate(0, 0.5f * Time.deltaTime, 0);
            if (Dog_down.transform.localPosition.y >= 5)
            {
                Dog_down.SetActive(false);
                if(changecall==false)
                {
                    changecall = true;
                    StartCoroutine(changepos());
                }
            }
        }
    }

    void OnMouseDown()
    {
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
    }

    void OnMouseDrag()
    {
        if (gameObject.name=="Balloon1")
        {
            Vector3 currentScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            Vector3 currentPos = Camera.main.ScreenToWorldPoint(currentScreenPoint);
            transform.position = currentPos;
        }

        if(gameObject.name == "Balloon3"&&seweropened==true)
        {
            Vector3 currentScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            Vector3 currentPos = Camera.main.ScreenToWorldPoint(currentScreenPoint);
            transform.position = currentPos;
        }

        if(gameObject.name=="Balloon2" && enoughsize==true)
        {
            Vector3 currentScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            Vector3 currentPos = Camera.main.ScreenToWorldPoint(currentScreenPoint);
            transform.position = currentPos;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.name=="Dog_down")
        {
            switch(gameObject.name)
            {
                case "Balloon1":
                    gameObject.transform.SetParent(other.gameObject.transform);
                    gameObject.GetComponent<BoxCollider2D>().enabled = false;
                    colorball.GetComponent<TouchColor>().getballoon++;
                    break;
                case "Balloon2":
                    gameObject.transform.SetParent(other.gameObject.transform);
                    gameObject.GetComponent<BoxCollider2D>().enabled = false;
                    colorball.GetComponent<TouchColor>().getballoon++;
                    break;
                case "Balloon3":
                    gameObject.transform.SetParent(other.gameObject.transform);
                    gameObject.GetComponent<BoxCollider2D>().enabled = false;
                    colorball.GetComponent<TouchColor>().getballoon++;
                    break;
            }
        }
    }

    IEnumerator changepos()
    {
        Dog.SetActive(true);
        Dog.transform.position = new Vector3(2,-4.66f,-1);
        Dog.transform.eulerAngles = new Vector3(0, 180, 0);
        colorball.GetComponent<CircleCollider2D>().enabled = true;
        colorball.GetComponent<SpriteRenderer>().enabled = true;
        Master.SetActive(false);
        yield return new WaitForSeconds(1.0f);
        camera.transform.position = new Vector3(0,0,-10);
    }
}
