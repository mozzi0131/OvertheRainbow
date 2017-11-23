using UnityEngine;
using System.Collections;

public class SecondCameraMoving : MonoBehaviour
{

    bool locked;
    bool _minus20;
    bool _0;

    GameObject camera;
    public GameObject[] arrow = new GameObject[2];

    // Use this for initialization
    void Start()
    {
        locked = false;
        _minus20 = false;
        _0 = true;

        arrow[0] = GameObject.Find("Left");
        arrow[1] = GameObject.Find("Right");
        camera = GameObject.Find("MainCamera");

        arrow[0].SetActive(false);
        arrow[1].SetActive(false);

        Setting();
    }


    public void Setting()
    {
        Debug.Log("setting called");
        switch ((int)camera.transform.position.x)
        {
            case 0:
                arrow[1].SetActive(false);
                arrow[0].SetActive(true);
                break;
            case -20:
                arrow[1].SetActive(true);
                arrow[0].SetActive(false);
                break;
        }
    }
}