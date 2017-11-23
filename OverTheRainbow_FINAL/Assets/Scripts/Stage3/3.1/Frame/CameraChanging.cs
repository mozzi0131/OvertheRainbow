using UnityEngine;
using System.Collections;

public class CameraChanging : MonoBehaviour
{
    bool firsttime;
    bool locked;
    bool _20;
    bool _minus20;
    bool _0;
    bool _40;

    GameObject Door;
    GameObject camera;
    GameObject ball;
    public GameObject[] arrow = new GameObject[2];

	// Use this for initialization
	void Start ()
    {
        firsttime = false;
        locked = false;
        _20 = false;
        _minus20 = false;
        _0 = true;
        _40 = false;

        ball = GameObject.Find("ball");
        arrow[0] = GameObject.Find("leftarrow");
        arrow[1] = GameObject.Find("rightarrow");
        camera = GameObject.Find("MainCamera");
        Door = GameObject.Find("Door");

        arrow[0].SetActive(false);
        arrow[1].SetActive(false);

        StartCoroutine(Waiting());
    }

    // Update is called once per frame
    void Update()
    {
        if (firsttime == true)
            ball.transform.Translate(5.0f * Time.deltaTime, 0, 0);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.name == "ball")
        {
            firsttime = false;
            Setting();
            Destroy(other.gameObject);
        }
    }

    public void Setting()
    {
        Debug.Log("setting called");
        switch ((int)camera.transform.position.x)
        {
            case 0:
                arrow[0].SetActive(true);
                arrow[1].SetActive(true);
                break;
            case -20:
                arrow[1].SetActive(true);
                arrow[0].SetActive(false);
                break;
            case 20:
                if (Door.GetComponent<Password>().unlock==true)
                {
                    arrow[0].SetActive(true);
                    arrow[1].SetActive(false);
                }
                else if (Door.GetComponent<Password>().unlock == false)
                {
                    arrow[0].SetActive(true);
                    arrow[1].SetActive(true);
                }
                 break;
            case 40:
                arrow[0].SetActive(true);
                arrow[1].SetActive(false);
                break;
            }
     }

    IEnumerator Waiting()
    {
        yield return new WaitForSeconds(2.0f);
        firsttime = true;
    }
}
