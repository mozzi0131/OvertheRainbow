using UnityEngine;
using System.Collections;

public class cageAct : MonoBehaviour {

    public GameObject myLock;
    bool nowOpen;
    public GameObject innerCage;
    public GameObject[] cageFloor = new GameObject[4];
    innerCageAct innerAct;
    string thisName;
    public GameObject dogSound;


    //BGM
    GameObject audioManager;
    AudioSource audioChange;
    AudioClip metalGate;
    AudioClip yipyap;
    AudioClip bark;
    AudioClip bigDog;

    void Awake()
    {
        if (this.name == "cagedoor1")
            nowOpen = true;

        thisName = this.name;
        if (innerCage != null)
        {
            innerAct = innerCage.GetComponent<innerCageAct>();
            innerCage.SetActive(false);
        }
        if (dogSound != null) dogSound.SetActive(false);

        //BGM
        audioManager = GameObject.Find("soundManager");
        audioChange = audioManager.GetComponent<AudioSource>();
        metalGate = audioManager.GetComponent<audioControl>().metalgate;
        yipyap = audioManager.GetComponent<audioControl>().yipyap;
        bark = audioManager.GetComponent<audioControl>().dogBark;
        bigDog = audioManager.GetComponent<audioControl>().bigDog;

    }



    void OnMouseDown()
    {
        if ( !nowOpen&& myLock==null) //철창문 열어벌이기, 자물쇠 없을 때만. 
        {
           
            this.transform.position -= new Vector3(0.5f,0, 0);
            this.transform.rotation = Quaternion.Euler(0, 40, 0);
            AudioSource.PlayClipAtPoint(metalGate, new Vector3(0, 0, -5));
            nowOpen = true;
        }
        else if(thisName != "cagedoor" &&  nowOpen || thisName=="cagedoor2") //확대샷 넣기 
        {
            if (!innerCage.active)
            {
                innerCage.SetActive(true);

                for (int i = 0; i < cageFloor.Length; i++) cageFloor[i].SetActive(false);

                switch (thisName)
                {

                    case "cagedoor1": cageFloor[0].SetActive(true); break;
                    case "cagedoor2": cageFloor[1].SetActive(true); innerAct.setCase2(); break;
                    case "cagedoor3": cageFloor[2].SetActive(true); break;
                    case "cagedoor4": cageFloor[3].SetActive(true); break;

                }
            }
        }else if (!nowOpen)
        {
            if(dogSound !=null)
            StartCoroutine(dogSoundOn());

        }else if(thisName == "cagedoor" && nowOpen)
        {
            GameObject.Find("MainCamera").GetComponent<stageManager>().clearStage();
        }
        
    }

    IEnumerator dogSoundOn()
    {
        switch (thisName)
        {

            case "cagedoor": AudioSource.PlayClipAtPoint(bark, new Vector3(0, 0, -5)); break;           
            case "cagedoor3": AudioSource.PlayClipAtPoint(bigDog, new Vector3(0, 0, -5)); break;
            case "cagedoor4": AudioSource.PlayClipAtPoint(yipyap, new Vector3(0, 0, -5)); break;

        }

        dogSound.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        dogSound.SetActive(false);

    }
   
}
