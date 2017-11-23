using UnityEngine;
using System.Collections;

public class somethingIsLocked : MonoBehaviour {

    bool nowOpen;
    stage4Manager checkBools;
    public GameObject text;
    bool cardReader = false;
    public GameObject[] doors = new GameObject[3];
    public GameObject keyForThis;
    public GameObject cctvText;
    string thisName;
    public GameObject[] monitor = new GameObject[2];

    //Audio
    GameObject audioManager;
    AudioSource audioChange;
    AudioClip locked;
    AudioClip openLock;
    AudioClip monitorNoise;
    AudioClip closeCabinet;
    AudioClip openCabinet;
    AudioClip somethingGet;
    AudioClip getgetKey;
    AudioClip wrongWrong;
    AudioClip cradReader;

    void Awake()
    {
        checkBools = GameObject.Find("MainCamera").GetComponent<stage4Manager>();
        if (text == null)
            text = GameObject.Find("textSample");
        thisName = this.name;

        text.SetActive(false);
        if (keyForThis != null) { keyForThis.GetComponent<BoxCollider2D>().enabled = false; keyForThis.SetActive(false); }
        if (cctvText != null) { cctvText.SetActive(false); }


        //BGM
        audioManager = GameObject.Find("soundManager");
        audioChange = audioManager.GetComponent<AudioSource>();
        locked = audioManager.GetComponent<audioControl>().lockedLock;
        openLock = audioManager.GetComponent<audioControl>().getColor;
        monitorNoise = audioManager.GetComponent<audioControl>().monitorN;
        closeCabinet = audioManager.GetComponent<audioControl>().closeC;
        openCabinet = audioManager.GetComponent<audioControl>().openC;
        somethingGet = audioManager.GetComponent<audioControl>().getSomething;
        wrongWrong = audioManager.GetComponent<audioControl>().wrongFrame;
        cradReader = audioManager.GetComponent<audioControl>().cardReader;
        getgetKey = audioManager.GetComponent<audioControl>().getgetKey;


    }




    void OnMouseDown()
    {

        switch (thisName)
        {
            case "lock":
                if (checkBools.keyForDog[0] && checkBools.keyForDog[1]) //열쇠 다 모았을 때!
                {
                    if (checkBools.cctvIsOff)
                    {

                        forSumyeon();
                        //openThis();
                        //Destroy(this.gameObject);
                    }
                    else {
                        
                        StartCoroutine(textPrint(keyForThis)); StartCoroutine(textPrint(cctvText)); checkBools.checkMyDogKey = true;
                       
                    } //여기는 Oops!!!
                }
                else {
                    StartCoroutine(textPrint(text));
                    AudioSource.PlayClipAtPoint(locked, new Vector3(0, 0, -5));
                }//여기는 It's Locked   
                break;
            case "cabinet1":
                if (checkBools.sumyeon && nowOpen) closeThis(); else if (checkBools.sumyeon && !nowOpen) openThis();
                if (!checkBools.sumyeon && nowOpen)
                {
                    //수면제겟
                    Debug.Log("수면제 얻었당");
                    AudioSource.PlayClipAtPoint(getgetKey, new Vector3(0, 0, -5));
                    checkBools.sumyeon = true; Destroy(doors[2]);
                }
                else if (checkBools.keyForSumyeon && checkBools.sumyeon == false)
                {
                    forSumyeon();
                  
                    
                }
                else if(!checkBools.keyForSumyeon) StartCoroutine(textPrint(text));
                    break;
            case "cabinet2-1":
                if (checkBools.keyForScene3 && !nowOpen)
                {
                    forSumyeon();
                    //openThis(); //cctv카드 얻어야하는 거 필요
                }
                else if (!nowOpen) { StartCoroutine(textPrint(text));  }
                break;
            case "cabinet2-2": if (!nowOpen) openThis(); else closeThis(); break;
            case "cardReader":  //card 열쇠를 위한 기믹
                    if (checkBools.dDay == 3) { checkBools.canGetLastKey = true; checkBools.dDay--; }
                if (!checkBools.cctvIsOff)
                {
                    StartCoroutine(textPrint(text)); //You don't have A Card           
                }    
                break;
            default: if (!nowOpen) { AudioSource.PlayClipAtPoint(locked, new Vector3(0, 0, -5)); StartCoroutine(textPrint(text)); } break;

        }

    }

    void forSumyeon()
    {
        //뿅 하는 소리가 나면서 열쇠 나타남
        AudioSource.PlayClipAtPoint(somethingGet, new Vector3(0, 0, -5),0.35f);
        keyForThis.SetActive(true);
        keyForThis.GetComponent<BoxCollider2D>().enabled = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (thisName == "cabinet1")
        {
            if (other.name == "littlekey")
            {
                //여는 소리 
                openThis();
                Destroy(keyForThis);
                
            }
        }
        if (thisName == "lock")
        {
            if (other.name == "combinedkey")
            {
                AudioSource.PlayClipAtPoint(openLock, new Vector3(0, 0, -5));
                audioChange.mute = true;
                openThis();
                Destroy(keyForThis);
                Destroy(this.gameObject);
            }
        }

        if(thisName == "cabinet2-1")
        {
            if (other.name == "keyForCard")
            {
                
                openThis();
               Destroy(keyForThis);
            }
        }

        if (thisName == "cardReader")
        {
            if (other.name == "cctvcard2")
            {
                AudioSource.PlayClipAtPoint(cradReader, new Vector3(0, 0, -5),0.5f);
                Destroy(other);
                //카드 리더기에서만... 얘는 그냥 Off 상태의 리더기임ㅠㅜ
                cctvText.SetActive(true);
                if (monitor[0] != null)
                {
                    StartCoroutine(monitorOff(monitor[0]));
                    StartCoroutine(monitorOff(monitor[1]));
                }
            }
        }
    }

    IEnumerator monitorOff(GameObject obj)
    {
        obj.SetActive(false);
        AudioSource.PlayClipAtPoint(monitorNoise, new Vector3(0, 0, -5));
        yield return new WaitForSeconds(0.4f);
        obj.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        obj.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        AudioSource.PlayClipAtPoint(monitorNoise, new Vector3(0, 0, -5));
        obj.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        obj.SetActive(false);
    }




    IEnumerator textPrint(GameObject obj)
    {
        obj.SetActive(true);
        AudioSource.PlayClipAtPoint(wrongWrong, new Vector3(0, 0, -5));
        yield return new WaitForSeconds(0.7f);
       
        obj.SetActive(false);
    }

    void openThis()
    {
        Debug.Log("OPNE!!!"+this.name);
        nowOpen = true;
        //효과음도 넣자

        
        if (doors[2] != null)
        {
            
            AudioSource.PlayClipAtPoint(openCabinet, new Vector3(0, 0, -5));
            doors[0].SetActive(false);
            doors[1].SetActive(true);
            doors[2].SetActive(true);
        }
        else if (doors[0] != null )
        {
            AudioSource.PlayClipAtPoint(openCabinet, new Vector3(0, 0, -5));
            doors[0].SetActive(false);
            doors[1].SetActive(true);
        }
    }

    void closeThis()
    {
        nowOpen = false;
        if (doors[0] != null)
        {
            AudioSource.PlayClipAtPoint(closeCabinet, new Vector3(0, 0, -5));
            doors[0].SetActive(true);
            doors[1].SetActive(false);
        }
    }


}
