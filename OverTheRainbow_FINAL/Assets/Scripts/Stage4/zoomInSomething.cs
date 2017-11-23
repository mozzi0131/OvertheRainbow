using UnityEngine;
using System.Collections;

public class zoomInSomething : MonoBehaviour {

    stage4Manager controlViewing;
    public int touchCount;
    string thisName;
    public GameObject innerCage;

    void Awake()
    {
        controlViewing = GameObject.Find("MainCamera").GetComponent<stage4Manager>();
        touchCount = 0;
        thisName = this.name;
        
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseDown()
    {

            touchCount++;

            switch (thisName)
            {
                case "cctv1":
                case "cctv2":
                if (!innerCage.active)
                {
                    switch (touchCount)
                    {
                        //1차 확대
                        case 1: controlViewing.zoomInCamera(-5.3f, 3, -10, 2); break;
                        case 2: controlViewing.zoomInCamera(-6.2f, 3.8f, -10, 0.5f); break;
                        case 3: controlViewing.setNormal(); changeScene(3); break;

                    }
                }
                else touchCount = 0;
                    break;
                case "monitor1":
                case "monitor2":
                    controlViewing.zoomInCamera(-0.8f, 0.5f, -10, 2); //한번 누르면 둘 다 확대
                    if (touchCount > 1)
                    {
                        controlViewing.setNormal();
                        if (this.name == "monitor1")
                            changeScene(1);
                        else changeScene(2);
                    }
                    break;
                default: //단순 줌인 줌 아웃
                    if (touchCount % 2 == 0) controlViewing.setNormal();
                    else
                    {
                        controlViewing.zoomInCamera(4.8f, 1.5f, -10, 2); touchCount = 1;
                    }
                    break;
            }



    }

    void changeScene(int sceneNum)
    {
        touchCount = 0;
        controlViewing.setScene(sceneNum);

    }
}
