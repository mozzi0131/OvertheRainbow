using UnityEngine;
using System.Collections;

public class checkBool : MonoBehaviour {

    stage4Manager checkBools;
    bool nowActCoroutine;

    void Awake()
    {
        checkBools = GameObject.Find("MainCamera").GetComponent<stage4Manager>();
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseDown()
    {
        if (this.name == "books")
        {
            if(!checkBools.nowSleeping)
            {
                if (!nowActCoroutine)
                {
                    //책 각자 흔들리게 합시다.
                }
            }
        }

    }


    IEnumerator shakeAnother(float time, GameObject something)
    {
        float countTime = 0.0f;
        float randomNumber = Random.value;
        int fixedNumber = 1;
        if (randomNumber < 0.5) fixedNumber = -1;
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
        nowActCoroutine = false;
    }
}
