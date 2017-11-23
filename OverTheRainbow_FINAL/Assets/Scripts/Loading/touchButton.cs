using UnityEngine;
using System.Collections;

public class touchButton : MonoBehaviour {


    void OnMouseDown()
    {
        GameObject.Find("controller").GetComponent<LoadSceneControl>().Load();
    }
}
