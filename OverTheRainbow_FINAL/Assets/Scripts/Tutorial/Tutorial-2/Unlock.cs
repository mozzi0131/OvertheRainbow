using UnityEngine;
using System.Collections;

public class Unlock : MonoBehaviour {

    private GameObject Dog;
    public GameObject unlock;
    private int speed = 5;
    public bool canOpen = false;

    void Start()
    {
        Dog = GameObject.Find("Dog");
        unlock.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (canOpen && other.CompareTag("Key"))
        {
            Dog.GetComponent<DogControl>().canGo = true;
            unlock.SetActive(true); 
            gameObject.SetActive(false);
        }
    }
}
