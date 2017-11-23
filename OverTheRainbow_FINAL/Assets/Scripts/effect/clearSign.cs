using UnityEngine;
using System.Collections;

public class clearSign : MonoBehaviour {

    public GameObject[] sign = new GameObject[2];
    Color red = new Color(255, 0, 0);
    SpriteRenderer[] image=new SpriteRenderer[2];
	// Use this for initialization
	void Start () {

        if (sign[0] != null) {
            image[0] = sign[0].GetComponent<SpriteRenderer>();
            image[1] = sign[1].GetComponent<SpriteRenderer>();
        }
        else { Debug.Log("Plz check clearSign");  }

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public IEnumerator coloredSign()
    {
        
        image[0].color = red;
        image[1].color = red;
        yield return new WaitForSeconds(0.1f);

    }
}
