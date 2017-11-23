using UnityEngine;
using System.Collections;


public class shakeSomething : MonoBehaviour {

    bool nowAct = false;

    //촛불Control
    public GameObject lightControl;
    cLightMove offLight;

    //꽃
    public GameObject[] flower = new GameObject[3];
    GameObject audioManager;
    AudioClip forFlower;

    //1.3
    public GameObject dog;
    AudioClip forBox;

    //stage4
    AudioClip paperFlip;
    AudioClip paperBag;
    AudioClip toytoyBall;
    AudioClip bird2;

    string thisName;
    

	// Use this for initialization
	void Start () {

        thisName = this.name;
        if(lightControl!=null)
        offLight = lightControl.GetComponent<cLightMove>();
        audioManager = GameObject.Find("soundManager");
        forFlower = audioManager.GetComponent<audioControl>().rotateBlock;
        forBox = audioManager.GetComponent<audioControl>().candleFlameOff;
        paperFlip = audioManager.GetComponent<audioControl>().paper;
        paperBag = audioManager.GetComponent<audioControl>().paperBag;
        toytoyBall = audioManager.GetComponent<audioControl>().toyBall;
        bird2 = audioManager.GetComponent<audioControl>().bird2;


    }
	
	// Update is called once per frame
	void Update () {


	}

    void OnMouseDown()
    {
        if (!nowAct)
        {
            nowAct = true;
            Debug.Log("Click");

            if (flower[0] == null) 
            {
                
                if (thisName == "calendar")
                {
                    StartCoroutine(shake(0.2f));
                }
                else if(this.gameObject.active) StartCoroutine(shake(0.1f));
            }
            else
            {
                if (thisName == "flower")
                {
                    AudioSource.PlayClipAtPoint(forFlower, new Vector3(0, 0, -5));
                    for (int j = 0; j < flower.Length; j++)
                        StartCoroutine(shakeAnother(0.2f, flower[j]));
                }
            }

            //촛불 일 때 
            if (lightControl != null)
                StartCoroutine(offLight.checkCount());

            if(thisName == "color")
            {
                AudioSource.PlayClipAtPoint(forFlower, new Vector3(0, 0, -5));
                if (dog != null)
                {
                    dog.GetComponent<showHint>().canGiveHint = true;
                    
                }
            }
            switch (thisName) {

                case "foodbag": AudioSource.PlayClipAtPoint(paperBag, new Vector3(0, 0, -5)); break;
                case "calendar": AudioSource.PlayClipAtPoint(paperFlip, new Vector3(0, 0, -5)); break;
                case "boxtop": AudioSource.PlayClipAtPoint(forBox, new Vector3(0, 0, -5)); break;
                case "trueBall":
                case "toyball": AudioSource.PlayClipAtPoint(toytoyBall, new Vector3(0, 0, -5)); break;
                case "ballAndBird": AudioSource.PlayClipAtPoint(bird2, new Vector3(0, 0, -5)); break;

            }
           
        }
    }


    public IEnumerator shake(float time)
    {

        float countTime = 0.0f;
        float randomNumber = Random.value;
        int fixedNumber = 1;
        if (randomNumber < 0.5) fixedNumber = -1;
        float dValue = 1 / time * fixedNumber;
        Vector3 shakeVector = new Vector3(0, 0, dValue * 8);
        Quaternion firstState = transform.rotation;



        while (countTime < time)
        {
            yield return new WaitForEndOfFrame();
            countTime += Time.deltaTime;

            transform.Rotate(shakeVector * Time.deltaTime);

        }
        countTime = 0.0f;

        while (countTime < 2 * time)
        {
            yield return new WaitForEndOfFrame();
            countTime += Time.deltaTime;

            transform.Rotate(-shakeVector * Time.deltaTime);
        }
        countTime = 0.0f;

        while (countTime < time)
        {
            yield return new WaitForEndOfFrame();
            countTime += Time.deltaTime;

            transform.Rotate(shakeVector * Time.deltaTime);

        }
        transform.rotation = firstState;
        nowAct = false;

    }


    IEnumerator shakeAnother(float time,GameObject something)
    {
        float countTime = 0.0f;
        float randomNumber = Random.value;
        int fixedNumber = 1;
        if (randomNumber < 0.5) fixedNumber =-1;
        float dValue = 1 / time * fixedNumber;
        Vector3 shakeVector = new Vector3(0, 0, dValue * 8);
        Quaternion firstState = something.transform.rotation;

        while (countTime < time)
        {
            yield return new WaitForEndOfFrame();
            countTime += Time.deltaTime;

            something.transform.Rotate(shakeVector * Time.deltaTime);

        }
        countTime = 0.0f;

        while (countTime < 2 * time)
        {
            yield return new WaitForEndOfFrame();
            countTime += Time.deltaTime;

            something.transform.Rotate(-shakeVector * Time.deltaTime);
        }
        countTime = 0.0f;

        while (countTime < time)
        {
            yield return new WaitForEndOfFrame();
            countTime += Time.deltaTime;

            something.transform.Rotate(shakeVector * Time.deltaTime);

        }
        something.transform.rotation = firstState;
        nowAct = false;
    }
}
