using UnityEngine;
using System.Collections;

public class TakeNum : MonoBehaviour {
    GameObject Door;
    bool fin;
    int i;

    AudioClip pressbutton;
    GameObject audioManager;

    // Use this for initialization
    void Start ()
    {
        Door = GameObject.Find("Door");
        i = 0;
        fin = false;

        audioManager = GameObject.Find("soundManager");
        pressbutton = audioManager.GetComponent<audioControl>().doorbutton;
    }
	
	// Update is called once per frame
	void OnMouseDown ()
    {
        AudioSource.PlayClipAtPoint(pressbutton, new Vector3(0, 0, -5));
        switch(gameObject.name)
        {
            case "0":
                if (Calcul() != 3)
                {
                    Door.GetComponent<Password>().inputnum[Calcul()] = 0;
                }
                else
                    Door.GetComponent<Password>().Compare();
                break;
            case "1":
                if (Calcul() != 3)
                    Door.GetComponent<Password>().inputnum[Calcul()] = 1;
                else
                    Door.GetComponent<Password>().Compare();
                break;
            case "2":
                if (Calcul() != 3)
                    Door.GetComponent<Password>().inputnum[Calcul()] = 2;
                else
                    Door.GetComponent<Password>().Compare();
                break;
            case "3":
                if (Calcul() != 3)
                    Door.GetComponent<Password>().inputnum[Calcul()] = 3;
                else
                    Door.GetComponent<Password>().Compare();
                break;
            case "4":
                if (Calcul() != 3)
                    Door.GetComponent<Password>().inputnum[Calcul()] = 4;
                else
                    Door.GetComponent<Password>().Compare();
                break;
            case "5":
                if (Calcul() != 3)
                    Door.GetComponent<Password>().inputnum[Calcul()] = 5;
                else
                    Door.GetComponent<Password>().Compare();
                break;
            case "6":
                if (Calcul() != 3)
                    Door.GetComponent<Password>().inputnum[Calcul()] = 6;
                else
                    Door.GetComponent<Password>().Compare();
                break;
            case "7":
                if (Calcul() != 3)
                    Door.GetComponent<Password>().inputnum[Calcul()] = 7;
                else
                    Door.GetComponent<Password>().Compare();
                break;
            case "8":
                if (Calcul() != 3)
                    Door.GetComponent<Password>().inputnum[Calcul()] = 8;
                else
                    Door.GetComponent<Password>().Compare();
                break;
            case "9":
                if (Calcul() != 3)
                    Door.GetComponent<Password>().inputnum[Calcul()] = 9;
                else
                    Door.GetComponent<Password>().Compare();
                break;
        }
	}

    int Calcul()
    {
        int i = 0;

        while (Door.GetComponent<Password>().inputnum[i] != 10)
            i++;

        if(i==3)
        {
            return i;
            fin = true;
        }
        else
            return i;
    }
}
