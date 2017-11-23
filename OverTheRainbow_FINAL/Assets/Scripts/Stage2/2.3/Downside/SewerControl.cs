using UnityEngine;
using System.Collections;

public class SewerControl : MonoBehaviour {
    GameObject[] sewer;
    public float minSwipeDistY;
    public float minSwipeDistX;
    private Vector2 startPos;

    GameObject balloon3;

    bool open;

    void Awake()
    {
        balloon3 = GameObject.Find("Balloon3");
        sewer[0] = GameObject.Find("sewerhole");
        sewer[1] = GameObject.Find("sewerholebar");
        open = false;
    }

    void OnMouseDown()
    {
        balloon3.GetComponent<BalloonControl>().seweropened = true;

        if (balloon3.GetComponent<BalloonControl>().seweropened == true)
        {
            sewer[1].SetActive(true);
            balloon3.GetComponent<BalloonControl>().seweropened = false;
        }
        else if(balloon3.GetComponent<BalloonControl>().seweropened == false)
        {
            sewer[1].SetActive(false);
            balloon3.GetComponent<BalloonControl>().seweropened = true;
        }
    }

    /*void Update()
    {
        //#if UNITY_ANDROID
        if (Input.touchCount > 0)
        {
            Touch touch = Input.touches[0];

            if(gameObject.GetComponent<BoxCollider2D>().bounds.Contains(touch.position))
            {
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        startPos = touch.position;
                        break;
                    case TouchPhase.Ended:
                        float swipeDistVertical = (new Vector3(0, touch.position.y, 0) - new Vector3(0, startPos.y, 0)).magnitude;

                        if (swipeDistVertical > minSwipeDistY)
                        {
                            float swipeValue = Mathf.Sign(touch.position.y - startPos.y);
                            if (swipeValue > 0)
                                sewer[1].SetActive(false);
                            else if (swipeValue < 0)
                                sewer[1].SetActive(true);
                        }
                        break;
                }
            }
        }*/
    }
