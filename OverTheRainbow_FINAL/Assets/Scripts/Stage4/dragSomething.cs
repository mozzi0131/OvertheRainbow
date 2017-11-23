using UnityEngine;
using System.Collections;

public class dragSomething : MonoBehaviour {


    stage4Manager checkBools;

    //뼈다귀
    string thisName;
    public GameObject[] mikki = new GameObject[2];
    bool mikkinono;

    //Drag
    public bool canDrag = true;
    private Vector3 screenPoint;

    void Awake()
    {
        checkBools = GameObject.Find("MainCamera").GetComponent<stage4Manager>();
        thisName = this.name;
        if (thisName=="mikki")
        {
            mikki[1].SetActive(false);
            mikki[0].SetActive(true);
        }
        mikkinono = false;
    }




    void OnMouseDown()
    {
        if (canDrag)
        {
            screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);

        }
    }

    void OnMouseDrag()
    {
        if (canDrag)
        {
            Vector3 currentScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            Vector3 currentPos = Camera.main.ScreenToWorldPoint(currentScreenPoint);
            transform.position = currentPos;
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (thisName == "mikki")
        {
            if (other.tag == "monitor2")
            {
                if (!checkBools.mikkiDrop)
                {
                    checkBools.mikkiDrop = true;
                    this.GetComponent<BoxCollider2D>().enabled = false;
                    StartCoroutine(forMikki());
                }

                //여기에 장면2 오브젝트 위치 등등 바꿔야 함. 
            }
        }

        //cctv카드를 가져다 댔을 때!
        if (thisName == "cctvcard2")
        {
            if (other.tag == "cardReader")
            {
                if (checkBools.dDay == 1) checkBools.dDay = 0;
                checkBools.cctvIsOff = true;
                Debug.Log("OPEN!!");
            }
        }

    }

    IEnumerator forMikki()
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(mikki[0]);
        mikki[1].SetActive(true);
        yield return new WaitForSeconds(0.2f);
        mikki[1].SetActive(false);
        yield return new WaitForSeconds(0.2f);
        mikki[1].SetActive(true);

        Destroy(this.gameObject);

    }
}
