using UnityEngine;
using System.Collections;

public class showHint : MonoBehaviour {

    private GameObject[] Hintimage = new GameObject[3];
    public GameObject[] Hint1 = new GameObject[2];
    public GameObject[] Hint2 = new GameObject[2];
    public GameObject[] Hint3 = new GameObject[2];
    public bool nowAct = false;
    public bool canGiveHint = false;
    int currentMiniNum = 0;
    int countNum = 0;

    // Use this for initialization
    void Start()
    {
        


        Hint1[0] = GameObject.Find("Hint0-1");
        Hint1[1] = GameObject.Find("Hint0-2");
        Hint2[0] = GameObject.Find("Hint1-1");
        Hint2[1] = GameObject.Find("Hint1-2");
        Hint3[0] = GameObject.Find("Hint2-1");
        Hint3[1] = GameObject.Find("Hint2-2");

        for(int i=0; i < 2; i++)
        {
            Hint1[i].SetActive(false);
        }
        for (int i = 0; i < 2; i++)
        {
            Hint2[i].SetActive(false);
        }
        for (int i = 0; i < 2; i++)
        {
            Hint3[i].SetActive(false);
        }

        for (int j=0; j < Hintimage.Length; j++)
        {
            Hintimage[j] = GameObject.Find("HintBalloon" + (j+1));           
        }

        if (PlayerPrefs.GetInt("MainStage") == 0)
        {
            currentMiniNum = PlayerPrefs.GetInt("MiniStage");
        }
        else
        {
            currentMiniNum = PlayerPrefs.GetInt("CurrentMiniStage");
        }

    }




    void Update()
    {
        if (!nowAct&&canGiveHint)
        {
            nowAct = true;
            if (countNum < 3)
            {
                countNum++;
                switch (currentMiniNum)
                {
                    case 0:
                        StartCoroutine(hint1());
                        break;
                    case 1:
                        StartCoroutine(hint2(0.3f, Hint2[1]));
                        break;
                    case 2:
                        StartCoroutine(hint3(0.4f, Hint3[1]));
                        break;
                    default: break;
                }
            }

        }

    }
    


    IEnumerator hint1()
    {
        Hintimage[0].SetActive(true);
        Hint1[1].SetActive(true);
        yield return new WaitForSeconds(0.9f);
        Hint1[1].SetActive(false);
        Hint1[0].SetActive(true);
        yield return new WaitForSeconds(0.9f);
        Hint1[0].SetActive(false);
        Hint1[1].SetActive(true);
        yield return new WaitForSeconds(0.9f);
        Hint1[1].SetActive(false);
        Hint1[0].SetActive(true);
        yield return new WaitForSeconds(0.9f);
        Hint1[0].SetActive(false);

        Hintimage[0].SetActive(false);
        nowAct = false;
        canGiveHint = false;
    }


    IEnumerator hint2(float time, GameObject something)
    {
        float countTime = 0.0f;
        float randomNumber = Random.value;
        int fixedNumber = 1;       
        float dValue = 1 / time * fixedNumber;
        Vector3 shakeVector = new Vector3(0, 0, dValue * 8);
        Quaternion firstState = something.transform.rotation;

        Hintimage[1].SetActive(true);
        Hint2[0].SetActive(true);
        Hint2[1].SetActive(true);

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
        Hintimage[1].SetActive(false);
        Debug.Log("ACT2");
        nowAct = false;
        canGiveHint = false;
    }

    IEnumerator hint3(float time, GameObject something)
    {
        float countTime = 0.0f;
        float randomNumber = Random.value;
        int fixedNumber = 1;
        float dValue = 1 / time * fixedNumber;
        Vector3 shakeVector = new Vector3(0, 0, dValue * 8);
        Quaternion firstState = something.transform.rotation;

        Hintimage[2].SetActive(true);
        Hint3[0].SetActive(true);
        Hint3[1].SetActive(true);

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
        Hintimage[2].SetActive(false);
        nowAct = false;
        canGiveHint = false;

    }

}
