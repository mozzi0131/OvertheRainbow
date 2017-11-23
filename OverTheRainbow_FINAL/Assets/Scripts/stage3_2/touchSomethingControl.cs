using UnityEngine;
using System.Collections;

public class touchSomethingControl : MonoBehaviour
{

    stage3_2_Manager checkBools;
    string thisName;
    GameObject dogDog;

    public GameObject[] onOff = new GameObject[5];

    //상자 문...
    bool act2;
    bool nowOpen;
    //bird (3-2-1)
    int touchCount;
    bool goRotate;
    float countTime;
    Quaternion firstRotation;

    //3-2-2
    public GameObject addObj;
    public GameObject[] forWatering;

    //Sound
    GameObject audioManager;
    AudioSource audioChange;
    AudioClip bird1;
    AudioClip lightNoise;
    AudioClip watering;
    AudioClip endTree;
    AudioClip paperbag;
    AudioClip faucetWater;
    AudioClip getgetSeed;
    AudioClip hanZo;
    AudioClip closeD;


    void Awake()
    {

        checkBools = GameObject.Find("miniStage2").GetComponent<stage3_2_Manager>();
        thisName = this.name;
        touchCount = 0;
        act2 = false;
        goRotate = false;
        countTime = 0;
        dogDog = GameObject.Find("dog_2");

        if (addObj != null) addObj.SetActive(false);
        if (thisName == "carFront") firstRotation = onOff[3].transform.rotation;

        //3-2-2
        if (thisName == "wateringPot")
        {
            for (int i = 0; i < onOff.Length; i++) onOff[i].SetActive(false);
        }


       
    }

    // Use this for initialization
    void Start()
    {
        audioManager = GameObject.Find("soundManager");
        audioChange = audioManager.GetComponent<AudioSource>();
        bird1 = audioManager.GetComponent<audioControl>().bird1;
        lightNoise = audioManager.GetComponent<audioControl>().monitorN;
        watering = audioManager.GetComponent<audioControl>().watering;
        endTree = audioManager.GetComponent<audioControl>().foundKey;
        paperbag = audioManager.GetComponent<audioControl>().paperBag;
        faucetWater = audioManager.GetComponent<audioControl>().faucet;
        getgetSeed = audioManager.GetComponent<audioControl>().getgetKey;
        hanZo = audioManager.GetComponent<audioControl>().rotateBlock;
        closeD = audioManager.GetComponent<audioControl>().lightSwitch;
    }

    // Update is called once per frame
    void Update()
    {

        if (goRotate && onOff[3] != null)
        {
            rotateSomething(onOff[3]);
        }

    }

    void rotateSomething(GameObject obj)
    {
        countTime += Time.deltaTime / 1.0f;
        obj.transform.rotation = Quaternion.Lerp(firstRotation, Quaternion.Euler(0, 0, 0), countTime);


    }

    void OnMouseDown()
    {
        if (!dogDog.GetComponent<stage3_2_Dog>().canGo)  //개가 안 걷고 있을 때만
        {
            switch (thisName)
            {
                case "bird": AudioSource.PlayClipAtPoint(bird1, new Vector3(0, 0, -5));
                    int birdNum = touchCount % 3; touchCount++;
                    onOff[birdNum].SetActive(false); onOff[(birdNum + 1) % 3].SetActive(true);
                    if (birdNum + 1 == 3) Instantiate(onOff[3], new Vector3(2.75f, 3.18f, 0), Quaternion.Euler(0, 0, 0)); //똥임 
                    break;
                case "birdFalse":
                    AudioSource.PlayClipAtPoint(bird1, new Vector3(0, 0, -5));

                    StartCoroutine(falseBird());
                    break;

                case "light_red":
                case "light_green":
                    if (!checkBools.electricityOn) StartCoroutine(spark(onOff[0]));
                    else
                    {
                        if (thisName == "light_green" && !checkBools.stage3_2_1Clear)
                        {
                            AudioSource.PlayClipAtPoint(getgetSeed, new Vector3(0, 0, -5));
                            onOff[0].SetActive(true);
                            checkBools.stage3_2_1Clear = true;
                            checkBools.gogoDoggy = true;
                        }
                    }

                    break;
                case "forElectricity":
                    if (checkBools.canOnElectricity && act2)
                    {
                        act2 = false;
                        if (checkBools.electricityOn)
                        {
                            //GameObject 조절 해야함. 소리도
                            onOff[2].SetActive(false);
                            onOff[3].SetActive(true);
                            checkBools.electricityOn = false;

                        }
                        else
                        {
                            onOff[3].SetActive(false);
                            onOff[2].SetActive(true);
                            checkBools.electricityOn = true;
                        }

                    }
                    else
                    {
                        //GameObject 껐다 켜기. 문 열었다 닫기임.
                        onOffCloseOpen();
                        if (checkBools.canOnElectricity && nowOpen) act2 = true;
                    }
                    break;
                //예외처리 해두기 
                case "carFront": if (checkBools.needHandkerchief && onOff[3] != null) goRotate = true; break;
                //손수건 꺼내벌임 
                case "armGizmo":
                case "handkerchief":
                    if (touchCount < 2) touchCount++;
                    else
                    {
                        this.gameObject.GetComponent<triggerControl>().triggerOn = true;
                        onOff[0].SetActive(false); //낙하 전
                        onOff[1].SetActive(true); //낙하 후 
                        Destroy(this.GetComponent<touchSomethingControl>());
                    }

                    break;

                //3-2-2 시작. 터치하는 건 물뿌리개, 수도꼭지, 씨앗봉투
                case "faucet": waterOnOff(addObj); rotateFaucet(onOff[0]); break;
                case "bongTu": //봉투쨔응 디폴트가 흔들리는거 씨앗이 뿅 나옴 
                    if (touchCount < 4) touchCount++;
                    else if (!checkBools.getSeed)
                    {
                        waterOnOff(addObj); //그냥 씨앗 보이게
                        AudioSource.PlayClipAtPoint(getgetSeed, new Vector3(0, 0, -5));
                        checkBools.getSeed = true;
                        StartCoroutine(seedEffect(onOff[0]));
                    }
                    AudioSource.PlayClipAtPoint(paperbag, new Vector3(0, 0, -5));
                    break;
                case "wateringPot":
                    if (checkBools.getSeed&&checkBools.canWatering && !checkBools.canDoongZi)
                    {

                        //물 주는 자세 디폴트 (코루틴 말고 껐다 켜자)
                        if (touchCount > 4)
                        {
                            AudioSource.PlayClipAtPoint(endTree, new Vector3(0, 0, -5), 0.4f);
                            checkBools.canDoongZi = true; Debug.Log("나무 끝!");
                            addObj.SetActive(true);
                        }
                        else
                        {
                            if (touchCount == 0)
                                onOff[touchCount++].SetActive(true);
                            else
                            {
                                onOff[touchCount - 1].SetActive(false);
                                onOff[touchCount++].SetActive(true);
                            }
                            //나무 쨔응들 자라야함. []배열로 touchCount-1 켜주자!아니면 touchCount++; 로 켜자 
                        }
                        StartCoroutine(waterGoGo());
                    }
                    else if (checkBools.canDoongZi) Destroy(this.gameObject);
                    break;
                case "treeHanZoGak":
                    if (checkBools.birdInTree && !checkBools.canGetColor)
                    {
                        touchCount++;
                        if (touchCount > 4)  //누를때 효과 주자
                        {
                            //두개 튀어나와야됨. 새, 공
                            onOff[0].SetActive(true);
                            onOff[1].SetActive(true);
                            //거슬리면 이 다음에 얘 디스트로이 
                            //새가 펑 튀어나옴. 뭐 기타 등등 
                            checkBools.canGetColor = true;
                        }
                        AudioSource.PlayClipAtPoint(hanZo, new Vector3(0, 0, -5));
                    }
                    
                    break;



            }
        }


    }

    //3-2-2


    IEnumerator seedEffect(GameObject obj)
    {
        Debug.Log("fasf");
        obj.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        obj.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        obj.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        obj.SetActive(false);
        Destroy(this.GetComponent<touchSomethingControl>()); //얘 파괴 
    }

    void rotateFaucet(GameObject obj)
    {
        if (addObj.active)
        {
            AudioSource.PlayClipAtPoint(faucetWater, new Vector3(0, 0, -5));
            obj.transform.rotation = Quaternion.Euler(0, 70, 0);
        }
        else
        {
            obj.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    void waterOnOff(GameObject obj)
    {
        if (obj.active)
        {
            obj.SetActive(false);
        }
        else obj.SetActive(true);
    }


    IEnumerator waterGoGo()
    {
        forWatering[0].SetActive(false);
        AudioSource.PlayClipAtPoint(watering, new Vector3(0, 0, -5));
        forWatering[1].SetActive(true);

        yield return new WaitForSeconds(0.3f);
        forWatering[1].SetActive(false);
        forWatering[0].SetActive(true);

    }


    //3-2-1
    void onOffCloseOpen()
    {
        if (nowOpen)
        {
            AudioSource.PlayClipAtPoint(closeD, new Vector3(0, 0, -5));
            onOff[1].SetActive(false);
            onOff[0].SetActive(true);            
            nowOpen = false;
        }
        else
        {
            onOff[0].SetActive(false);
            onOff[1].SetActive(true);
            nowOpen = true;
        }
    }

    IEnumerator falseBird()
    {
        float randomNumber = Random.value;
        int control = 1;
        if (randomNumber < 0.5f) control = -1;

        if (control == 1)
        {
            onOff[0].SetActive(false);
            onOff[1].SetActive(true);
            yield return new WaitForSeconds(0.4f);
            onOff[1].SetActive(false);
            onOff[0].SetActive(true);
            yield return new WaitForSeconds(0.2f);
        }
        onOff[0].SetActive(false);
        onOff[1].SetActive(true);
        yield return new WaitForSeconds(0.4f);
        onOff[1].SetActive(false);
        onOff[0].SetActive(true);

    }

    IEnumerator spark(GameObject obj)
    {
        Debug.Log("fasf");
        obj.SetActive(true);
        AudioSource.PlayClipAtPoint(lightNoise, new Vector3(0, 0, -5),0.8f);
        yield return new WaitForSeconds(0.3f);
        obj.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        obj.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        AudioSource.PlayClipAtPoint(lightNoise, new Vector3(0, 0, -5));
        obj.SetActive(false);
    }
}
