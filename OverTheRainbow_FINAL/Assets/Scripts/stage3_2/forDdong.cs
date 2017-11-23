using UnityEngine;
using System.Collections;

public class forDdong : MonoBehaviour {

    public GameObject miniStage;
    bool firstFind;
    int countNum;

    void Awake()
    {
        miniStage = GameObject.Find("3_2_1");
        firstFind = false;
        countNum = 0;
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
       
	
        if(miniStage == null)
        {
            Destroy(this.gameObject);
        }
	}

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.name == "handkerchiefOnGround")
        {
            countNum++;
            if(countNum>2)
            Destroy(this.gameObject);
        }

    }
}
