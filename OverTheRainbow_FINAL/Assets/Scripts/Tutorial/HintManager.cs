using UnityEngine;
using System.Collections;

public class HintManager : MonoBehaviour {

    private GameObject[] Hintimage1 = new GameObject[2];
    private GameObject[] Hintimage2 = new GameObject[2];

    //NullException이 많으면 그냥 public으로
    void Awake()
    {
        Hintimage1[0] = GameObject.Find("Hint1");
        Hintimage1[1] = GameObject.Find("Hint2");
        Hintimage1[0].SetActive(false);
        Hintimage1[1].SetActive(false);

        Hintimage2[0] = GameObject.Find("Hint2.1");
        Hintimage2[1] = GameObject.Find("Hint2.2");
        Hintimage2[0].SetActive(false);
        Hintimage2[1].SetActive(false);

        give();
    }

    void give()
    {
        int miniNum = PlayerPrefs.GetInt("MiniStage");

        if (miniNum == 0)
        {
            StartCoroutine(giveHint(Hintimage1));
        }
        else if (miniNum == 1)
        {
            StartCoroutine(giveHint(Hintimage2));
        }
    }

    // 정규화
    IEnumerator giveHint(GameObject[] Hint)
    {
        yield return new WaitForSeconds(1.0f);
        Hint[0].SetActive(true);
        yield return new WaitForSeconds(0.7f);
        Hint[0].SetActive(false);
        Hint[1].SetActive(true);
        yield return new WaitForSeconds(0.7f);
        Hint[1].SetActive(false);
        yield return new WaitForSeconds(1.0f);
        Hint[0].SetActive(true);
        yield return new WaitForSeconds(0.7f);
        Hint[0].SetActive(false);
        Hint[1].SetActive(true);
        yield return new WaitForSeconds(0.7f);
        Hint[1].SetActive(false);
        yield return new WaitForSeconds(1.0f);
        Hint[0].SetActive(true);
        yield return new WaitForSeconds(0.7f);
        Hint[0].SetActive(false);
        Hint[1].SetActive(true);
        yield return new WaitForSeconds(0.7f);
        Hint[1].SetActive(false);


    }
}
