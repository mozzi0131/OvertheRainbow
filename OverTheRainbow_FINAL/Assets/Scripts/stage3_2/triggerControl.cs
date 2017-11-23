using UnityEngine;
using System.Collections;

public class triggerControl : MonoBehaviour {


    stage3_2_Manager checkBools;
    string thisName;

    public bool triggerOn;
    public GameObject ddong;

    //손수건 
    public GameObject[] handK = new GameObject[2];
    public GameObject allArm;

    GameObject audioManager;
    AudioSource audioChange;
    AudioClip handKG;

    void Awake()
    {
        checkBools = GameObject.Find("miniStage2").GetComponent<stage3_2_Manager>();
        thisName = this.name;

        audioManager = GameObject.Find("soundManager");
        audioChange = audioManager.GetComponent<AudioSource>();
        handKG = audioManager.GetComponent<audioControl>().lightSwitch;

        triggerOn = false;
    }


    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
        if(triggerOn)
        { 
                this.transform.position -= new Vector3(0, 4 * Time.deltaTime, 0);

        }
	}


    void OnTriggerEnter2D(Collider2D other)
    {

        if (thisName == "ddong(Clone)")
        {
            float randomNumber = Random.value;
            float randomNumber1 = Random.value;

            if (other.name == "carFront")
            {
                triggerOn = false;
                checkBools.needHandkerchief = true;
                Instantiate(ddong, new Vector3(2.0f + randomNumber, - 0.5f - randomNumber1, -1),Quaternion.Euler(0,0,0));
                Destroy(this.gameObject);
            }
        }

        if (thisName == "handkerchief")
        {
            if (other.name == "Ground"&&!checkBools.downHandkerchief)
            {
                AudioSource.PlayClipAtPoint(handKG, new Vector3(0, 0, -5));
                triggerOn = false;
                handK[0].SetActive(false);
                handK[1].SetActive(true);
                checkBools.downHandkerchief = true;
                Destroy(allArm);
                //떨어진 모양...
            }
        }


    }
}
