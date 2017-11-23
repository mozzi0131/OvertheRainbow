using UnityEngine;
using System.Collections;

public class innerCageAct : MonoBehaviour {

    stage4Manager checkBools;
    public GameObject[] dogsInCage = new GameObject[3]; //각 케이지의 개들

    //두번째 케이지
    public GameObject mikkiInCage;
    public GameObject sparkle;

    //먼지 쌓인 케이지. 반쪽 짜리 열쇠 있는 거
    public GameObject[] dust = new GameObject[2];
    public GameObject case4;
    public GameObject forCollider;
    int touchDust;

    void Awake()
    {
        checkBools = GameObject.Find("MainCamera").GetComponent<stage4Manager>();
        touchDust = 0;
        forCollider.GetComponent<BoxCollider2D>().enabled = false;

    }

    // Use this for initialization
    void Start () {
       
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseDown()
    {
        if (case4.active && !checkBools.dustOff)
        {
            touchDust++;
            if (touchDust == 2)
            {
                dust[0].SetActive(false);
            }
            else if (touchDust == 4)
            {
                dust[1].SetActive(false);
            }
            else if (!dust[1].active)
            {
                forCollider.GetComponent<BoxCollider2D>().enabled = true;
                checkBools.dustOff = true;
            }
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }

    public void setCase2()
    {
        if (checkBools.dDay <= 3 && mikkiInCage != null) Destroy(mikkiInCage);
        if (checkBools.mikkiDrop&&!checkBools.keyForSumyeon)  //개가 미끼 물고 있는 거.
        {
            mikkiInCage.SetActive(true);
            //이러면 안대...
            Destroy(dogsInCage[0]);
        }
        else
        {
            if(sparkle!=null)
            StartCoroutine(forCase2());
            
        }
    }

    public void setCase3()
    {

    }

    public void setCase4()
    {

    }

    IEnumerator forCase2()
    {
        sparkle.SetActive(true);
        StartCoroutine(sparkle.GetComponent<dreamingMove>().moveSomething(sparkle, 0.2f));
        yield return new WaitForSeconds(1.7f);
        StartCoroutine(sparkle.GetComponent<dreamingMove>().moveSomething(sparkle, 0.2f));
        yield return new WaitForSeconds(1.7f);
        StartCoroutine(sparkle.GetComponent<dreamingMove>().moveSomething(sparkle, 0.2f));
        yield return new WaitForSeconds(1.7f);
        sparkle.SetActive(false);
    }
}
