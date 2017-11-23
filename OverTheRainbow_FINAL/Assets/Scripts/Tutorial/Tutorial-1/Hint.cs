using UnityEngine;
using System.Collections;

public class Hint : MonoBehaviour {
    private GameObject[] Hintimage = new GameObject[2];
	// Use this for initialization
	void Start () {
        Hintimage[0] = GameObject.Find("Hint1");
        Hintimage[1] = GameObject.Find("Hint2");

        Hintimage[0].SetActive(false);
        Hintimage[1].SetActive(false);
        give();
    }

    void give()
    {
        StartCoroutine(giveHint());
    }
	
	// Update is called once per frame
	IEnumerator giveHint()
    {
        yield return new WaitForSeconds(1.0f);
        Hintimage[0].SetActive(true);
        yield return new WaitForSeconds(0.7f);
        Hintimage[0].SetActive(false);
        Hintimage[1].SetActive(true);
        yield return new WaitForSeconds(0.7f);
        Hintimage[1].SetActive(false);
        yield return new WaitForSeconds(1.0f);
        Hintimage[0].SetActive(true);
        yield return new WaitForSeconds(0.7f);
        Hintimage[0].SetActive(false);
        Hintimage[1].SetActive(true);
        yield return new WaitForSeconds(0.7f);
        Hintimage[1].SetActive(false);


    }
}
