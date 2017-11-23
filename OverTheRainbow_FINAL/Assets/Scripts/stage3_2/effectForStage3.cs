using UnityEngine;
using System.Collections;

public class effectForStage3 : MonoBehaviour
{

    string thisName;


    public GameObject[] forWiper = new GameObject[2];

    //Sound
    GameObject audioManager;
    AudioSource audioChange;
    AudioClip carHorn;

    void Awake()
    {
        thisName = this.name;

        audioManager = GameObject.Find("soundManager");
        audioChange = audioManager.GetComponent<AudioSource>();
        carHorn = audioManager.GetComponent<audioControl>().carHorn;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseDown()
    {
        if (thisName == "carBumper")
        {
            AudioSource.PlayClipAtPoint(carHorn, new Vector3(0, 0, -5), 0.5f);
            StartCoroutine(wiperMove(forWiper[0], forWiper[1]));
        }


    }


    IEnumerator wiperMove(GameObject obj1, GameObject obj2)
    {

        float randomNumber = Random.value;
        int control = 1;
        if (randomNumber < 0.5f) control = -1;

        if (control == 1)
        {
            obj1.transform.rotation = Quaternion.Euler(0, 0, -20);
            obj2.transform.rotation = Quaternion.Euler(0, 0, -20);
        }
        else
        {
            obj1.transform.rotation = Quaternion.Euler(0, 0, -10);
            obj2.transform.rotation = Quaternion.Euler(0, 0, -10);
        }

        yield return new WaitForSeconds(0.6f);

        obj1.transform.rotation = Quaternion.Euler(0, 0, 0);
        obj2.transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
