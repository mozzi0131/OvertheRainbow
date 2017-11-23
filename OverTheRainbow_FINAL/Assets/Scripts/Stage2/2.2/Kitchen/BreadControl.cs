using UnityEngine;
using System.Collections;

public class BreadControl : MonoBehaviour {
    private GameObject FinCook;
    public GameObject[] childBread = new GameObject[3];
    public GameObject[] BreadBag = new GameObject[2];
    bool getoffbag;
    bool insidecabinet;
	// Update is called once per frame

        void Start()
    {
        FinCook = GameObject.Find("FinCook");
        childBread[0] = GameObject.Find("Bread");
        childBread[1] = GameObject.Find("Bread(1)");
        childBread[2] = GameObject.Find("Bread(2)");
        getoffbag = false;
        insidecabinet = true;
        BreadBag[0] = GameObject.Find("closedbreadbag");
        BreadBag[1] = GameObject.Find("emptybreadbag");
        BreadBag[1].SetActive(false);
        childBread[0].GetComponent<BoxCollider2D>().enabled = false;
        childBread[1].GetComponent<BoxCollider2D>().enabled = false;
        childBread[2].GetComponent<BoxCollider2D>().enabled = false;
    }
	void OnMouseDown () {
        Debug.Log("childcount:"+gameObject.transform.childCount);
        if(insidecabinet && !getoffbag)
        {
            gameObject.transform.parent = FinCook.transform;
            gameObject.transform.position = new Vector3(-17.49f, 0.06f, 0);
            insidecabinet = false;
        }
        else if(!insidecabinet && !getoffbag)
        {
            BreadBag[0].SetActive(false);
            BreadBag[1].SetActive(true);
            getoffbag = true;
        }
        else if(getoffbag)
        {
            childBread[0].transform.GetComponentInChildren<DragBread>().bread = true;
            childBread[0].GetComponent<BoxCollider2D>().enabled = true;
            childBread[1].transform.GetComponentInChildren<DragBread>().bread = true;
            childBread[1].GetComponent<BoxCollider2D>().enabled = true;
            childBread[2].transform.GetComponentInChildren<DragBread>().bread = true;
            childBread[2].GetComponent<BoxCollider2D>().enabled = true;
            childBread[0].transform.parent = FinCook.transform;
            childBread[1].transform.parent = FinCook.transform;
            childBread[2].transform.parent = FinCook.transform;
            gameObject.SetActive(false);
        }
	}
}
