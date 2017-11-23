using UnityEngine;
using System.Collections;

public class cLightMove : MonoBehaviour {

    bool nowAct = false;
    bool nowActChange = false;
    // public GameObject[] flame=new GameObject[3];
    public GameObject flame;
    int countDown;

    //sound
    GameObject audioManager;
    AudioClip candleFlame;
    AudioClip candleFlameOff;


    // Use this for initialization
    void Start () {
        countDown = 2;
        audioManager = GameObject.Find("soundManager");
        candleFlame = audioManager.GetComponent<audioControl>().candleFlame;
        candleFlameOff = audioManager.GetComponent<audioControl>().candleFlameOff;
    }
	
	// Update is called once per frame
	void Update () {

        if (!nowAct)
        {
            nowAct = true;
            StartCoroutine(move());
            //StartCoroutine(change());
        }


	}


    public IEnumerator checkCount()
    {
        if (countDown == 0)  //불꺼버림
        {

            flame.SetActive(false);
            AudioSource.PlayClipAtPoint(candleFlameOff, new Vector3(0, 0, -5));
        }
        else
        {
            countDown--;
            AudioSource.PlayClipAtPoint(candleFlame, new Vector3(0, 0, -5));
            flame.transform.localScale = (flame.transform.localScale)*0.8f;
        }
        yield return new WaitForSeconds(0.5f);
    }

    //크기 조절
    IEnumerator move()
    {
        Vector3 shrinkVector = new Vector3(transform.localScale.x * 0.4f, transform.localScale.y * 0.4f, 1);
        Vector3 shakeVector = new Vector3(0, 0, 20);
        Vector3 normalVector = transform.localScale;

        float countTime = 0.0f;

        while (countTime < 0.4f)
        {
            yield return new WaitForEndOfFrame();
            countTime += Time.deltaTime;
            transform.localScale = this.transform.localScale - shrinkVector * Time.deltaTime;
            transform.Rotate(shakeVector * Time.deltaTime);
           
        }
        countTime = 0.0f;

        while (countTime < 0.4f)
        {
            yield return new WaitForEndOfFrame();
            countTime += Time.deltaTime;
            transform.localScale = this.transform.localScale + shrinkVector * Time.deltaTime;
            transform.Rotate(-shakeVector * Time.deltaTime);
        }
        transform.localScale = normalVector;
       
       nowAct = false;

    }

    //각도 조절
    //IEnumerator shake()
    //{
    //    Vector3 shakeVector = new Vector3(0, 0, 30);
    //    float countTime = 0.0f;

    //    while (countTime < 0.4f)
    //    {
    //        yield return new WaitForEndOfFrame();
    //        countTime += Time.deltaTime;
           
    //        transform.Rotate(shakeVector * Time.deltaTime);

    //    }
    //    countTime = 0.0f;

    //    while (countTime < 0.4f)
    //    {
    //        yield return new WaitForEndOfFrame();
    //        countTime += Time.deltaTime;
            
    //        transform.Rotate(-shakeVector * Time.deltaTime);
    //    }


    //    nowActChange = false;
    //}

    //IEnumerator change()
    //{
    //    Vector3 shrinkVector = new Vector3(1.5f, 1.5f, 0);
    //    float countTime = 0.0f;


    //    while (countTime < 0.2f)
    //    {
    //        yield return new WaitForEndOfFrame();
    //        countTime += Time.deltaTime;
    //        flame[0].transform.localScale = flame[0].transform.localScale - shrinkVector * Time.deltaTime;

    //    }
    //    flame[1].transform.localScale = flame[0].transform.localScale;
    //    flame[0].SetActive(false);
    //    yield return new WaitForSeconds(0.1f);
    //    flame[1].SetActive(true);

    //    countTime = 0.0f;
    //    while (countTime < 0.2f)
    //    {
    //        yield return new WaitForEndOfFrame();
    //        countTime += Time.deltaTime;
    //        flame[1].transform.localScale = flame[1].transform.localScale + shrinkVector * Time.deltaTime;

    //    }
    //    flame[1].transform.localScale = new Vector3(1, 1, 1);
    //    yield return new WaitForSeconds(0.1f);
    //    countTime = 0.0f;
    //    while (countTime < 0.2f)
    //    {
    //        yield return new WaitForEndOfFrame();
    //        countTime += Time.deltaTime;
    //        flame[1].transform.localScale = flame[1].transform.localScale - shrinkVector * Time.deltaTime;

    //    }
    //    flame[0].transform.localScale = flame[1].transform.localScale;
    //    flame[1].SetActive(false);
    //    yield return new WaitForSeconds(0.1f);
    //    flame[0].SetActive(true);
    //    countTime = 0.0f;
    //    while (countTime < 0.2f)
    //    {
    //        yield return new WaitForEndOfFrame();
    //        countTime += Time.deltaTime;
    //        flame[0].transform.localScale = flame[0].transform.localScale + shrinkVector * Time.deltaTime;

    //    }
    //    flame[0].transform.localScale = new Vector3(1, 1, 1);
    //    nowAct = false;
    //}

    IEnumerator change2()
    {
        Vector3 shrinkVector = new Vector3(2.0f, 2.0f, 0);
        Vector3 normalVector = new Vector3(1, 1, 1);

        float countTime = 0.0f;

        while (countTime < 0.2f)
        {
            yield return new WaitForEndOfFrame();
            countTime += Time.deltaTime;
            transform.localScale = this.transform.localScale - shrinkVector * Time.deltaTime;

        }
        countTime = 0.0f;
        shrinkVector = new Vector3(1.0f, 1.0f, 0);
        while (countTime < 0.5f)
        {
            yield return new WaitForEndOfFrame();
            countTime += Time.deltaTime;
            transform.localScale = this.transform.localScale + shrinkVector * Time.deltaTime;
            checkScale();
        }
        transform.localScale = normalVector;
        nowActChange = false;
    }

    IEnumerator change3()
    {
        flame.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        flame.SetActive(true);
    }

    void checkScale()
    {
        if (this.transform.localScale.x > 1.0f)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

}
