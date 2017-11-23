using UnityEngine;
using System.Collections;

public class FinControl : MonoBehaviour {
    public GameObject sparkle;
    public GameObject camera;
    public GameObject Master;
    public GameObject Dog;
    public bool canGo;
    bool gogo;
    Animator DogAni;

    // Use this for initialization
    void Start ()
    {
        sparkle = GameObject.Find("sparkle");
        sparkle.SetActive(false);
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        Master = GameObject.Find("Master");
        Dog = GameObject.Find("Dog");
        DogAni = Dog.GetComponent<Animator>();
        camera = GameObject.Find("MainCamera");
        gogo = false;
    }
	
	// Update is called once per frame
	void OnMouseDown ()
    {
        Master.GetComponent<Master>().cookfinish = true;
        DogAni.SetBool("canGo", true);
        gogo = true;
    }

    void Update()
    {
        if (gogo == true)
        {
            moveDog();
        }

        if (camera.transform.position.x >= 0)
        {
            DogAni.SetBool("canGo", false);
            gogo = false;
        }
    }

    void moveDog()
    {
        Dog.transform.Translate(new Vector3(5.0f * Time.deltaTime, 0, 0));
        camera.transform.Translate(new Vector3(5.0f * Time.deltaTime, 0, 0));
    }    
}
