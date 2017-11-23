using UnityEngine;
using System.Collections;

public class guardAct : MonoBehaviour {

    stage4Manager checkBools;
    public bool nowActCoroutine;
    public bool nowSleep = true;
    public GameObject[] hat = new GameObject[2];
    public GameObject sleepText;
    public GameObject wakeUpObj;


    GameObject audioManager;
    AudioSource audioChange;
    AudioClip zzZZZZZ;


    void Awake()
    {
        checkBools = GameObject.Find("MainCamera").GetComponent<stage4Manager>();
        if (sleepText != null) sleepText.SetActive(false);
        if (wakeUpObj != null) wakeUpObj.SetActive(false);


        audioManager = GameObject.Find("soundManager");
        audioChange = audioManager.GetComponent<AudioSource>();
        zzZZZZZ = audioManager.GetComponent<audioControl>().snoring;

        checkHat();
    }

    void Update () {

        if (nowSleep)
        {
            if (!nowActCoroutine)
            {
                nowActCoroutine = true;
                sleepText.SetActive(true);
                StartCoroutine(zzZ(0.5f, 0.002f, sleepText));
            }
            //수면 상태의 글씨 움직이는 거!
        }
        else
        {
            sleepText.SetActive(false);
            if (!nowActCoroutine)
            {
                nowActCoroutine = true;
                //임시방편
                StartCoroutine(wakeUp(0.3f,this.gameObject));
            }
        }

        if (checkBools.sumyeon == true && checkBools.dDay < 5) checkBools.nowSleeping = true;
	
	}

    IEnumerator zzZ(float time,float magnitude,GameObject zzZ)
    {

        float countTime = 0.0f;
        float dValue = 1 / time * magnitude;
        Vector3 nVector = new Vector3(dValue * 1, dValue * 1, 0);
        Vector3 firstScale = new Vector3(1, 1, 1);

        AudioSource.PlayClipAtPoint(zzZZZZZ, new Vector3(0, 0, -5));
        while (countTime < 2*time)
        {
            yield return new WaitForEndOfFrame();
            countTime += Time.deltaTime;

            zzZ.transform.localScale += nVector;

        }
        countTime = 0.0f;

        while (countTime < 2 * time)
        {
            yield return new WaitForEndOfFrame();
            countTime += Time.deltaTime;

            zzZ.transform.localScale -= nVector;
        }

        zzZ.transform.localScale = firstScale;
        nowActCoroutine = false;

        if (checkBools.nowSleeping)
        {
            wakeUpObj.SetActive(false);
        }

    }

    IEnumerator wakeUp(float time, GameObject something)
    {
        nowSleep = false;
        checkHat();
        float countTime = 0.0f;
        float randomNumber = Random.value;
        int fixedNumber = 1;
        if (randomNumber < 0.5) fixedNumber = -1;
        float dValue = 1 / time * fixedNumber;
        Vector3 shakeVector = new Vector3(0, 0, dValue * 1);
        Quaternion firstState = something.transform.rotation;

        wakeUpObj.SetActive(true);

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

        wakeUpObj.SetActive(false);

        while (countTime < time)
        {
            yield return new WaitForEndOfFrame();
            countTime += Time.deltaTime;

            something.transform.Rotate(shakeVector * Time.deltaTime);

        }
        something.transform.rotation = firstState;
        yield return new WaitForSeconds(2.0f);
        nowActCoroutine = false;
        nowSleep = true;
        checkHat();
    }

    void OnMouseDown()
    {
        Debug.Log("clickGuard");
        //수면제 먹기 전에는 잠들어벌임
        if (!checkBools.nowSleeping) nowSleep = false;
    }

    public void checkHat()
    {
        if (nowSleep)
        {
            hat[0].SetActive(false);
            hat[1].SetActive(true);
        }
        else
        {
            hat[1].SetActive(false);
            hat[0].SetActive(true);
        }
    }
}
