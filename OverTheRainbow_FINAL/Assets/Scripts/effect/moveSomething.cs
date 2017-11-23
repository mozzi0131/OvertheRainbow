using UnityEngine;
using System.Collections;

public class moveSomething : MonoBehaviour {

    bool nowAct = false;
    string thisName;

    //Sound
    GameObject audioManager;

    AudioClip forToybone;

    void Awake()
    {
        audioManager = GameObject.Find("soundManager");
        thisName = this.name;

        //나중에 좀 수정하자 
        forToybone = audioManager.GetComponent<audioControl>().candleFlameOff;


    }
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseDown()
    {
        if(!nowAct)
        {
            nowAct = true;
            StartCoroutine(moveThis(0.2f, 0.002f));

            if(thisName=="toybone") AudioSource.PlayClipAtPoint(forToybone, new Vector3(0, 0, -5));

        }

        
    }


    public IEnumerator moveThis(float time,float magnitude)
    {

        float countTime = 0.0f;
        float randomNumber = Random.value;
        int fixedNumber = -1;
        if (randomNumber < 0.5) fixedNumber = -1;
        float dValue = 1 / time * fixedNumber;
        Vector3 nVector = new Vector3(0, dValue * magnitude,0 );
        Vector3 firstSize = this.transform.localScale;



        while (countTime < time)
        {
            yield return new WaitForEndOfFrame();
            countTime += Time.deltaTime;

            transform.localScale -= nVector;

        }
        countTime = 0.0f;

        while (countTime < 2 * time)
        {
            yield return new WaitForEndOfFrame();
            countTime += Time.deltaTime;

            transform.localScale += nVector;
        }
        countTime = 0.0f;

        while (countTime < time)
        {
            yield return new WaitForEndOfFrame();
            countTime += Time.deltaTime;

            transform.localScale -= nVector;

        }
        transform.localScale = firstSize;
        nowAct = false;

    }


}
