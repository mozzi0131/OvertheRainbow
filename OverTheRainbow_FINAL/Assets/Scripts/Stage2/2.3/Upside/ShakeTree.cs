using UnityEngine;
using System.Collections;

public class ShakeTree : MonoBehaviour {
    GameObject Master;
    public GameObject Ball2;
    public GameObject Ball3;
    public bool notfall;
    public bool canshake;
    float accelerometerUpdateInterval;
    // The greater the value of LowPassKernelWidthInSeconds, the slower the filtered value will converge towards current input sample (and vice versa).
    float lowPassKernelWidthInSeconds;
    // This next parameter is initialized to 2.0 per Apple's recommendation, or at least according to Brady! ;)
    float shakeDetectionThreshold;

    private float lowPassFilterFactor;
    private Vector3 lowPassValue;
    private Vector3 acceleration;
    private Vector3 deltaAcceleration;
    private float starttime;
    

    // Use this for initialization
    void Awake ()
    {
        //Ball2 = GameObject.Find("ball2");
        //Ball3 = GameObject.Find("ball3");
        Master = GameObject.Find("Master");
        

        notfall = false;
        canshake = false;

        accelerometerUpdateInterval = 1.0f / 60.0f;
        lowPassKernelWidthInSeconds = 1.0f;
        shakeDetectionThreshold = 2.0f;
        lowPassKernelWidthInSeconds = accelerometerUpdateInterval / lowPassKernelWidthInSeconds;
        lowPassValue = Vector3.zero;
        shakeDetectionThreshold *= shakeDetectionThreshold;
        lowPassValue = Input.acceleration;
        starttime = Time.time;
    }
	
	// Update is called once per frame
	void Update ()
    {
        //공이 바닥과 ontriggerenter 하게 되면 intree는 false로 돌림
        if(notfall==true)
        {
            float timecount = Time.time - starttime;
            acceleration = Input.acceleration;
            lowPassValue = Vector3.Lerp(lowPassValue, acceleration, lowPassFilterFactor);
            deltaAcceleration = acceleration - lowPassValue;

            if (deltaAcceleration.sqrMagnitude >= shakeDetectionThreshold && timecount > 1.5f)
            {
                if (canshake != true)
                {
                    canshake = true;
                }
                    canshake = true;
            }
            
            if(canshake == true)
            {
                if(Master.GetComponent<ControlBall>().playball==2)
                {
                    Ball2.transform.Translate(0, -5 * Time.deltaTime, 0);
                }
                else if(Master.GetComponent<ControlBall>().playball == 3)
                    Ball3.transform.Translate(0, -5 * Time.deltaTime, 0);
            }    
        }
    }

    void OnMouseDown()
    {
        canshake = true;
    }

}

