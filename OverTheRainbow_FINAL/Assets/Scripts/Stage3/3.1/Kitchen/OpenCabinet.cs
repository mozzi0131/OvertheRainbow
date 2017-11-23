using UnityEngine;
using System.Collections;

public class OpenCabinet : MonoBehaviour
{
    public GameObject[] DoorObject = new GameObject[2];
    public bool closed;

    //효과음
    GameObject audioManager;
    AudioSource audioChange;
    AudioClip openCabinet;
    AudioClip closeCabinet;

    // Use this for initialization
    void Awake()
    {
        closed = true;

        Debug.Log(gameObject.name);

        switch (gameObject.name)
        {
            case "openbottomcabinet_right":
                DoorObject[0] = gameObject;
                DoorObject[1] = GameObject.Find("bottomcabinet_right");
                break;
            case "bottomcabinet_right":
                DoorObject[0] = gameObject;
                DoorObject[1] = GameObject.Find("openbottomcabinet_right");
                DoorObject[1].SetActive(false);
                break;
        }
    }

    void Start()
    {
        audioManager = GameObject.Find("soundManager");
        audioChange = audioManager.GetComponent<AudioSource>();
        openCabinet = audioManager.GetComponent<audioControl>().openCabinet;
        closeCabinet = audioManager.GetComponent<audioControl>().closeCabinet;
    }

    void OnMouseDown()
    {
        if (gameObject.tag == "Close")
            AudioSource.PlayClipAtPoint(openCabinet, new Vector3(-20, 0, -5));
        else if (gameObject.tag == "Open")
            AudioSource.PlayClipAtPoint(closeCabinet, new Vector3(-20, 0, -5));

        DoorObject[1].SetActive(true);
        DoorObject[0].SetActive(false);
    }
}
