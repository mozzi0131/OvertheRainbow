using UnityEngine;
using System.Collections;

public class controlSomething : MonoBehaviour {

    GameObject stageManager;
    public GameObject dog;
    int miniStageNum;


    GameObject audioManager;
    AudioClip forColor;

    // Use this for initialization
    void Start () {
       
        stageManager = GameObject.Find("MainCamera");
        dog = GameObject.Find("Dog");
        miniStageNum = PlayerPrefs.GetInt("MiniStage");
        audioManager = GameObject.Find("soundManager");
        forColor = audioManager.GetComponent<audioControl>().foundKey;
        
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {

        if (this.name == "color")
            ifColor(other);
          
    }

    void ifColor(Collider2D other)
    {      
        if (other.CompareTag("Ground"))
        {
            Debug.Log("controlSomething is working");

            this.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
            AudioSource.PlayClipAtPoint(forColor, new Vector3(0, 0, -10), 0.4f);
            dog.GetComponent<showHint>().nowAct = true;
            dog.GetComponent<controlDog>().canGo = true;
        }
    }


}
