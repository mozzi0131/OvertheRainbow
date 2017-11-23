using UnityEngine;
using System.Collections;

public class dreamingMove : MonoBehaviour {


    bool nowAct = false;
    public int countNum = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (!nowAct)
        {
            nowAct = true;
            if (this.name == "sparkle")
            {
                if (countNum < 3)
                {
                    countNum++;
                    StartCoroutine(moveSomething(this.gameObject, 0.2f));
                }
                else this.gameObject.SetActive(false);
            }
            else if (this.name == "sparkle1") Debug.Log("음");
            else if(this.name=="bird") StartCoroutine(moveSomething(this.gameObject, 0.05f));
            else
                StartCoroutine(moveSomething(this.gameObject, 0.015f));
        }
	}

    public IEnumerator moveSomething(GameObject obj,float scale)
    {
        Vector3 normal = obj.transform.localScale;
        // float scale = 0.1f;
        float countTime = 0.0f;

        while (countTime < 0.8f)
        {
            yield return new WaitForEndOfFrame();
            countTime += Time.deltaTime;
            obj.transform.localScale -= new Vector3(scale * Time.deltaTime, scale * Time.deltaTime, 0);

        }
        countTime = 0.0f;
        while (countTime < 0.8f)
        {
            yield return new WaitForEndOfFrame();
            countTime += Time.deltaTime;
            obj.transform.localScale += new Vector3(scale * Time.deltaTime, scale * Time.deltaTime, 0);
        }

        obj.transform.localScale = normal;
        nowAct = false;

       


    }
}
