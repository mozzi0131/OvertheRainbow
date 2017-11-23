using UnityEngine;
using System.Collections;

public class dragForStage3 : MonoBehaviour
{


    //Drag
    public bool firstDrag = true;
    private Vector3 screenPoint;

    //3-2-1전깃줄 드래그 
    float lastY;
    Vector3 currentPos;
    public GameObject[] addEffect; //0은 전기 찌리찌리 1은 손수건 
    

    //통합
    stage3_2_Manager checkBools;
    string thisName;

    //3-2-2 씨앗이랑 waterPot
    bool nowWatering;
    float countTime;
    GameObject fixedWaterPot;
    public GameObject[] addObjsForSeed;


    //Sound
    GameObject audioManager;
    AudioSource audioChange;
    AudioClip onGround;
    AudioClip getgetWater;

    void Awake()
    {
        checkBools = GameObject.Find("miniStage2").GetComponent<stage3_2_Manager>();
        thisName = this.name;
        nowWatering = false;
        countTime = 0;

        audioManager = GameObject.Find("soundManager");
        audioChange = audioManager.GetComponent<AudioSource>();
        onGround = audioManager.GetComponent<audioControl>().foundKey;
        getgetWater = audioManager.GetComponent<audioControl>().getgetKey;
    }


    void Update()
    {
        if (nowWatering)
        {
            countTime += Time.deltaTime;

            //물을 받는다. 일정초가 지나면 물을 다 받았다고 간주한다. 
            if (countTime > 2.0f)
            {
                Debug.Log("Full!!");
                if (!checkBools.getWater)
                {
                    AudioSource.PlayClipAtPoint(getgetWater, new Vector3(0, 0, -5));
                    checkBools.getWater = true;
                }
            }
        }
    }

    void OnMouseDown()
    {

        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);

    }

    void OnMouseDrag()
    {

        if (thisName == "cable" && checkBools.canDragCable)
        {
            //rotation이 특정 지점 넘어가면 못 넘어가게 바꿔야함

            Vector3 currentScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            if (!firstDrag) lastY = currentPos.y;
            currentPos = Camera.main.ScreenToWorldPoint(currentScreenPoint);
            if (firstDrag) lastY = currentPos.y;

            if (!firstDrag && lastY < currentPos.y)
                transform.Rotate(new Vector3(0, 0, 1), 0.4f);
            else if (!firstDrag && lastY > currentPos.y)
                transform.Rotate(new Vector3(0, 0, -1), 0.4f);
            else firstDrag = false;
        }
        else if (thisName == "handkerchiefOnGround" && checkBools.downHandkerchief)
        {
            Vector3 currentScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            currentPos = Camera.main.ScreenToWorldPoint(currentScreenPoint);
            transform.position = currentPos;
        }

        //3-2-2 단순 드래그! 
        if (thisName == "seed" || thisName=="wateringPot")
        {
            Vector3 currentScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            currentPos = Camera.main.ScreenToWorldPoint(currentScreenPoint);
            transform.position = currentPos;
        }


    }


    void OnTriggerEnter2D(Collider2D other)
    {

        if (thisName == "cable")
        {
            if (other.name == "forCable")
            {
                checkBools.canOnElectricity = true;
                //콜라이더 파괴하고 자리 보정 넣어야함. 
                addEffect[1].SetActive(false);
                this.transform.rotation = Quaternion.Euler(this.transform.rotation.x, this.transform.rotation.y, 52.8f);
                Destroy(this.GetComponent<dragForStage3>());
            }
        }
        if (thisName == "handkerchiefOnGround")
        {
            if (other.name == "cable")
            {
                checkBools.canDragCable = true;
                //손수건 넣고, 효과 없애고
                Destroy(other.GetComponent<dragForStage3>().addEffect[0]);                
                other.GetComponent<dragForStage3>().addEffect[1].SetActive(true);
                //얘 파괴해버리고.
                Destroy(this.gameObject);
            }
        }

        //3-2-2
        if (thisName == "seed")
        {
            if (other.name == "dirt")
            {
                checkBools.dirtGetSeed = true;
                addObjsForSeed[0].SetActive(false);
                addObjsForSeed[1].SetActive(true);
                //되면 땅에 씨앗이 묻힌 것 같은 흙더미...
                Destroy(this.gameObject);
            }
        }

        if (thisName == "wateringPot")
        {
            if (other.name == "water")
            {
                nowWatering = true;
            }
            else if (other.name == "fixedSpot")  //고정된 자리에 갔을 때
            {
                if (checkBools.getWater)
                {
                    checkBools.canWatering = true;
                    //더 이상 드래그는 노노 
                    Destroy(this.GetComponent<dragForStage3>());                 
                }
            }
        }

        if(thisName =="colorball" || thisName == "trueBall")
        {
            if (other.name == "Ground")
            {
                AudioSource.PlayClipAtPoint(onGround, new Vector3(0, 0, -5), 0.3f);
                this.GetComponent<Rigidbody2D>().isKinematic = true;
                checkBools.gogoDoggy = true;
                checkBools.stage3_2_2Clear = true;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {

        if (thisName == "wateringPot")
        {
            if (other.name == "water")
            {
                nowWatering = false;
            }
        }
    }

}
