using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ToastBar : MonoBehaviour {
    public Scrollbar Toastbar; // 얘만 ui에 있는 scrollbar를 그대로 끌어다 썼어요
    public float getoffToast = 0;
    public bool propercall;
    public GameObject Bread;//이 Bread 변수는 DragBread에서 설정해줌
    public GameObject inputBread;

	// Use this for initialization
	void Awake () {
        inputBread = GameObject.Find("InputBread");
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (getoffToast < 100)
        {
            getoffToast += (Time.deltaTime *4);
            Toastbar.size = getoffToast / 100f;
        }
        else
        {
            if (getoffToast >= 85)
            {
                Bread.GetComponent<DragBread>().Burn();
            }

        }   
	}

    void OnMouseDown()
    {
        print("Onmousedown works!"+getoffToast);
        if (getoffToast >= 75 && getoffToast <= 80)
        {
            print("proper call");
            Bread.GetComponent<DragBread>().Setting();
        }
        else
            ;
    }
}
