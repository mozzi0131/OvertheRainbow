using UnityEngine;
using System.Collections;

public class endingDog : MonoBehaviour {


    public bool canGo;
    bool firstStand;

    Animator dogAni;
    float speed = 0;
    public GameObject myColor;
    endingControl endCont;

    //Sound
    GameObject audioManager;
    AudioSource audioChange;
    AudioClip tikTik;
    AudioClip tokTok;

    void Awake()
    {

        endCont = GameObject.Find("endingManager").GetComponent<endingControl>();


        audioManager = GameObject.Find("soundManager");
        audioChange = audioManager.GetComponent<AudioSource>();
        tikTik = audioManager.GetComponent<audioControl>().tikTik;
        tokTok = audioManager.GetComponent<audioControl>().tokTok;

    }
	// Use this for initialization
	void Start () {

        dogAni = GetComponent<Animator>();
        canGo = false;
        firstStand = false;
    }
	
	// Update is called once per frame
	void Update () {


        if (canGo)
        {
            dogAni.SetBool("canGo", true);
            if (!firstStand)
            {
                firstStand = true;
                StartCoroutine(forStandUp(1.1f));

            }
            moveDog();
        }

    }


    IEnumerator forStandUp(float time)
    {
        yield return new WaitForSeconds(time);
        speed = 5.5f;
    }


    void moveDog()
    {
        this.transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Color")
        {
            dogAni.SetBool("canGo", false);
            canGo = false;
            StartCoroutine(getColor());
        }


    }


    IEnumerator getColor()
    {
        //소리나 효과 같은거 
        yield return new WaitForSeconds(3.0f);
        endCont.lastAtc1 = true;
        AudioSource.PlayClipAtPoint(tikTik, new Vector3(0, 0, -5));
        AudioSource.PlayClipAtPoint(tokTok, new Vector3(0, 0, -5));
        Destroy(myColor);
        Destroy(this.gameObject);
    }


}
