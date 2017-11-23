using UnityEngine;
using System.Collections;

public class birdControl : MonoBehaviour
{

    stage3_2_Manager checkBools;
    string thisName;

    float countTime;
    Vector3 firstPos;

    bool rightPos1;
    public GameObject treeAndBird;
    public GameObject treeHanZoGak;

    bool flying;
    public GameObject[] fly;
    public GameObject sitdown;
    bool nowAct;

    void Awake()
    {
        checkBools = GameObject.Find("miniStage2").GetComponent<stage3_2_Manager>();
        thisName = this.name;
        countTime = 0;
        firstPos = this.transform.position;
        rightPos1 = false;
        flying = true;
        nowAct = false;
        sitdown.SetActive(false);

    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (checkBools.stage3_2_2Bird && !rightPos1)
        {

            countTime += Time.deltaTime / 5.0f;
            if (this.transform.position.x <= -3.5f)
            {
                rightPos1 = true; checkBools.stage3_2_2Bird = false; countTime = 0;
                checkBools.gogoDoggy = true;
                flying = false;
                
            }
            else
            {
                this.transform.position = Vector3.Lerp(firstPos, new Vector3(-3.5f, firstPos.y, firstPos.z), countTime);

            }
        }

        if (checkBools.canDoongZi && !checkBools.birdInTree)
        {
            flying = true;
            this.transform.rotation = Quaternion.Euler(0, 180, 0);
            this.transform.position += new Vector3(1.5f * Time.deltaTime, 0, 0);
        }

        if (flying)
        {
            sitdown.SetActive(false);
            if (!nowAct) { nowAct = true; StartCoroutine(nowFly()); }
        }
        else
        {
            fly[0].SetActive(false);
            fly[1].SetActive(false);
            sitdown.SetActive(true);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "treeAndBird")
        {
            if (checkBools.canDoongZi)
            {
                treeHanZoGak.SetActive(true);
                checkBools.birdInTree = true;
                Destroy(this.gameObject);
            }
        }

    }

    IEnumerator nowFly()
    {
        
        fly[0].SetActive(true);
        fly[1].SetActive(false);
        yield return new WaitForSeconds(0.4f);
        fly[0].SetActive(false);
        fly[1].SetActive(true);
        yield return new WaitForSeconds(0.4f);
        fly[1].SetActive(false);
        fly[0].SetActive(true);
        nowAct = false;
    }

}

