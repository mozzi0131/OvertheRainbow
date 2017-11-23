using UnityEngine;
using System.Collections;

public class DragBread : MonoBehaviour {
   public bool bread;
    private Vector3 screenPoint;
    public GameObject Toast;
    public int rightclick;
    public GameObject Dish;

	// Use this for initialization
	void Awake () {
        Toast = GameObject.Find("Toaster");
        Dish = GameObject.Find("plate");
        bread = false;
        
        Toast.GetComponent<ToastBar>().inputBread.SetActive(false);
        Toast.GetComponent<ToastBar>().Toastbar.gameObject.SetActive(false);
        Toast.GetComponent<ToastBar>().enabled = false;
	}
	
	// Update is called once per frame
	void OnMouseDown ()
    {
        if(bread == true)
            screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        
	}

    void OnMouseDrag()
    {
        if (bread == true)
        {
            Vector3 currentScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            Vector3 currentPos = Camera.main.ScreenToWorldPoint(currentScreenPoint);
            transform.position = currentPos;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Toaster") 
        {
            Debug.Log("toast detected!");
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            Toast.GetComponent<ToastBar>().enabled = true;
            Toast.GetComponent<ToastBar>().Toastbar.gameObject.SetActive(true);
            Toast.GetComponent<ToastBar>().inputBread.SetActive(true);
            other.gameObject.GetComponent<ToastBar>().Bread = gameObject;
        }
    }

   public void Burn()
    {
        Debug.Log("Burn called!");
        Toast.GetComponent<ToastBar>().getoffToast = 0;
        Toast.GetComponent<ToastBar>().Toastbar.gameObject.SetActive(false);
        Toast.GetComponent<ToastBar>().enabled = false;
        Toast.GetComponent<ToastBar>().inputBread.SetActive(false);
        gameObject.SetActive(false);
    }

    public void Setting()
    {
        print("setting called");
        switch(Dish.GetComponent<ControlCook>().breadnum)
        {
            case 0:
                Dish.GetComponent<ControlCook>().breadnum++;
                gameObject.GetComponent<SpriteRenderer>().enabled = true;
                Toast.GetComponent<ToastBar>().Toastbar.gameObject.SetActive(false);
                gameObject.transform.position = new Vector3(-25.3f, 0.2f, 0);
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
                Toast.GetComponent<ToastBar>().getoffToast = 0;
                Toast.GetComponent<ToastBar>().Toastbar.gameObject.SetActive(false);
                Toast.GetComponent<ToastBar>().enabled = false;
                Toast.GetComponent<ToastBar>().inputBread.SetActive(false);
                break;
            case 1:
                Dish.GetComponent<ControlCook>().breadnum++;
                gameObject.GetComponent<SpriteRenderer>().enabled = true;
                Toast.GetComponent<ToastBar>().Toastbar.gameObject.SetActive(false);
                gameObject.transform.position = new Vector3(-25.6f, 0.2f, 0);
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
                Toast.GetComponent<ToastBar>().getoffToast = 0;
                Toast.GetComponent<ToastBar>().Toastbar.gameObject.SetActive(false);
                Toast.GetComponent<ToastBar>().enabled = false;
                Toast.GetComponent<ToastBar>().inputBread.SetActive(false);
                break;
            case 2:
                Dish.GetComponent<ControlCook>().breadnum++;
                gameObject.GetComponent<SpriteRenderer>().enabled = true;
                Toast.GetComponent<ToastBar>().Toastbar.gameObject.SetActive(false);
                gameObject.transform.position = new Vector3(-25.59f, 0.2f, 0);
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
                Toast.GetComponent<ToastBar>().getoffToast = 0;
                Toast.GetComponent<ToastBar>().Toastbar.gameObject.SetActive(false);
                Toast.GetComponent<ToastBar>().enabled = false;
                Toast.GetComponent<ToastBar>().inputBread.SetActive(false);
                break;
        }
    }

    

}
