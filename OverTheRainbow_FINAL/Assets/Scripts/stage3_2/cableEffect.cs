using UnityEngine;
using System.Collections;

public class cableEffect : MonoBehaviour {

    //통합
    stage3_2_Manager checkBools;
    string thisName;
    GameObject eEffect;

    GameObject audioManager;
    AudioSource audioChange;
    AudioClip spark;

    void Awake()
    {

        checkBools = GameObject.Find("miniStage2").GetComponent<stage3_2_Manager>();
        thisName = this.name;
        eEffect = GameObject.Find("electricityEffect");

        audioManager = GameObject.Find("soundManager");
        audioChange = audioManager.GetComponent<AudioSource>();
        spark = audioManager.GetComponent<audioControl>().spark1;
        eEffect.SetActive(false);
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseDown()
    {
        if (eEffect != null)
        {
            AudioSource.PlayClipAtPoint(spark, new Vector3(0, 0, -5));
            StartCoroutine(effectControl(eEffect));
        }

    }


    IEnumerator effectControl(GameObject obj)
    {
        obj.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        obj.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        obj.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        obj.SetActive(false);
    }
}
