using UnityEngine;
using System.Collections;

//ball에 붙는 코드.

public class BallComponent : MonoBehaviour {
    GameObject Dog;
    GameObject master;
    public GameObject child;
    GameObject camera;
    GameObject beforeground;
    GameObject Tree;
    GameObject[] ground = new GameObject[2];

    Animator DogAni;

    //moveDelay
    bool firstStand = false;

    public bool cantouch;
    bool dogmoving;//개를 움직이게 해 주는 boolean값.
    public bool falltree;//공이 나무에서 떨어졌을 때(눈 완전히 치우기 전까지 false 유지)
    public bool intree;//공이 나무에 걸릴 때

	// Use this for initialization
	void Start ()
    {
        Dog = GameObject.Find("Dog");
        master = GameObject.Find("Master");
        camera = GameObject.Find("MainCamera");
        Tree = GameObject.Find("Tree");

        beforeground = GameObject.Find("Ground");
        ground[0] = GameObject.Find("littleholeground");
        ground[1] = GameObject.Find("bigholeground");


        ground[1].GetComponent<SpriteRenderer>().enabled = false;

        DogAni = Dog.GetComponent<Animator>();

        cantouch = true;
        dogmoving = false;
        intree = false;
        falltree = true;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(dogmoving==true)
        {
            DogAni.SetBool("canGo", true);
            if (!firstStand)
            {
                firstStand = true;
                StartCoroutine(forStandUp(1.1f));

            }
            MovingDog();
        }

        if (gameObject.transform.name == "ball2" || gameObject.transform.name=="ball3")
        {

            if(gameObject.transform.localPosition.y <= -4)
            {
                Tree.GetComponent<ShakeTree>().notfall = false;
                falltree = true;
                intree = false;
                cantouch = true;
            }
        }
	}

    void OnMouseDown()
    {
        if (master.GetComponent<ControlBall>().thrown == true && intree == false && falltree == true)
        {
            if (gameObject.transform.name == "ball2" || gameObject.transform.name == "ball3")
            {
                if(cantouch)
                {
                    dogmoving = true;
                }
            }
            else
                dogmoving = true;
        }
       
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //공과 개가 부딪혔을 때
        if(other.gameObject == Dog)
        {
            if(gameObject.name=="ball4")
            {
                ground[1].GetComponent<SpriteRenderer>().enabled = true;
                ground[0].SetActive(false);
                Dog.SetActive(false);
                StartCoroutine(Waiting());
                camera.transform.position = new Vector3(11.5f, -10, -10);
                gameObject.SetActive(false);
            }
            else
            {
                dogmoving = false;
                gameObject.transform.SetParent(Dog.transform);
                gameObject.transform.localPosition = new Vector3(1.1f, 1.06f, 1);
                Dog.transform.eulerAngles = new Vector3(0, 180, 0);
                dogmoving = true;
            }
        }

        //주인과 공이 부딪혔을 때
        if(other.gameObject==master&& master.GetComponent<ControlBall>().thrown == true)
        {
            dogmoving = false;
            DogAni.SetBool("canGo", false);
            Dog.transform.eulerAngles = new Vector3(0, 0, 0);
            Dog.transform.position = new Vector3(-6.6f, -4.66f, -1);
            int i = master.GetComponent<ControlBall>().playball;
            master.GetComponent<ControlBall>().thrown = false;
            master.GetComponent<ControlBall>().ball[i].SetActive(true);
            gameObject.SetActive(false);
        }

        //나무에 공이 부딪혔을 때(2,3번 anim일 때만.)
        if(other.transform.name == "Tree")
        {
            if(gameObject.transform.name == "ball2" || gameObject.transform.name == "ball3")
            {
                intree = true;
                other.GetComponent<ShakeTree>().notfall = true;
                falltree = false;
            }
        }
    }

    void MovingDog()
    {

        
        Dog.transform.Translate(5.0f * Time.deltaTime, 0, 0);
    }

    IEnumerator Waiting()
    {
        yield return new WaitForSeconds(1.0f);
    }

    IEnumerator forStandUp(float time)
    {
        yield return new WaitForSeconds(time);
    }
}

