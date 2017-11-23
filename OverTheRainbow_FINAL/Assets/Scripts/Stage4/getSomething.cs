using UnityEngine;
using System.Collections;

public class getSomething : MonoBehaviour {

    stage4Manager checkBools;
    string thisName;

    GameObject audioManager;
    AudioSource audioChange;
    AudioClip getgetKey;


    void Awake()
    {
        audioManager = GameObject.Find("soundManager");
        audioChange = audioManager.GetComponent<AudioSource>();
        getgetKey = audioManager.GetComponent<audioControl>().getgetKey;
    }
    // Use this for initialization
    void Start () {

        thisName = this.name;
        checkBools = GameObject.Find("MainCamera").GetComponent<stage4Manager>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseDown()
    {
        switch (thisName)
        {
            case "key1":
                if (checkBools.mikkiDrop)
                {
                    AudioSource.PlayClipAtPoint(getgetKey, new Vector3(0, 0, -5));
                    checkBools.keyForSumyeon = true; checkBools.dDay--; Destroy(this.gameObject); Debug.Log(checkBools.dDay);
                }
                else Debug.Log("??"); break;
            case "halfKey1":
                if (checkBools.dustOff&& !checkBools.keyForDog[0])
                {
                    AudioSource.PlayClipAtPoint(getgetKey, new Vector3(0, 0, -5));
                    checkBools.keyForDog[0] = true; checkBools.dDay--; Destroy(this.gameObject); Debug.Log(checkBools.dDay);
                } break;
            case "halfKey2":
                if (checkBools.noBooks)
                {
                    AudioSource.PlayClipAtPoint(getgetKey, new Vector3(0, 0, -5));
                    checkBools.keyForDog[1] = true; checkBools.dDay--; Destroy(this.gameObject); Debug.Log(checkBools.dDay);
                } break;
            case "key2": if(checkBools.canGetLastKey && checkBools.checkMyDogKey)
                {
                    AudioSource.PlayClipAtPoint(getgetKey, new Vector3(0, 0, -5));
                    Debug.Log("GetKey!!");
                    checkBools.keyForScene3 = true;
                    checkBools.dDay--;
                    Debug.Log(checkBools.dDay);
                    Destroy(this.gameObject);
                }
                break;
            default: Debug.Log("CLLLLLIIIKKKKK GET"); break;
        }
       
    }
}
