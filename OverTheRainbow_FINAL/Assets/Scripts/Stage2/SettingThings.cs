using UnityEngine;
using System.Collections;

public class SettingThings : MonoBehaviour
{
    GameObject camerabutton;
    GameObject camera;
    GameObject Dog;
    GameObject miniStage1;
    // Use this for initialization
    void Start()
    {
        miniStage1 = GameObject.Find("miniStage1");
        int stageType = PlayerPrefs.GetInt("MainStage");
        int miniNum = PlayerPrefs.GetInt("MiniStage");
        camerabutton = GameObject.Find("CameraMoving");
        camera = GameObject.Find("MainCamera");
        Dog = GameObject.Find("Dog");
    }

    public void call()
    {
        camerabutton.SetActive(true);
        camera.transform.position = new Vector3(0, 0, -10);
        Dog.transform.position = new Vector3(-6.7f, -4.6f, -1);
    }

}