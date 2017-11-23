using UnityEngine;
using System.Collections;

public class GetColor : MonoBehaviour {
    private float speed = 50.0f;
    public GameObject Dog;
    private Vector3 screenPoint;
    public float enterPositionY;
    Rigidbody2D rb;
    private int miniNum;

    public GameObject ColorCollider;

    //눈속임
    public bool nowAct = false;
    public bool inWind = false;

    //mininum0 무조건 적인 터치는 불가능하게 ㅠ한다.
    public bool canFallen = false;


    void Start()
    {
        miniNum = PlayerPrefs.GetInt("MiniStage");
    rb = this.GetComponent<Rigidbody2D>();
       ColorCollider.SetActive(true); //얘가 항상 쓰이는게 아닌데...
    }

    void Update()
    {
        if(!inWind) rb.constraints = ~RigidbodyConstraints2D.FreezePositionY;

    }

    void OnMouseDown()
    {
        if (miniNum != 2 && miniNum !=0)
        {
            screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
            ColorCollider.SetActive(false);
        } else if (miniNum == 0)
        {
            if (canFallen)
            {
                screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
                ColorCollider.SetActive(false);
            }
        }
    }


    void OnCollisionEnter2D(Collision2D other)
    {
        
        if (other.gameObject.name == "Ground")
        {
            Dog.GetComponent<DogMoving>().CanGo = true;
        }

    }



    void OnTriggerStay2D(Collider2D other)
    {
       
        if (other.CompareTag("Wind"))
        {
            inWind = true;
            rb.constraints = RigidbodyConstraints2D.FreezePositionY;
            if (!nowAct)
            {
              
                nowAct = true;
                StartCoroutine(move());
            }
        }
    }




    //혼자 움직임
    IEnumerator move()
    {
        Vector3 drag_vector = new Vector3(0, 0.2f, 0);

        float countTime = 0.0f;  //얘를 바꾸게 하고 fadeTime은 가만히 둔다.

        while (countTime < 0.3f)
        {
            yield return new WaitForEndOfFrame();
            countTime += Time.deltaTime;
            transform.Translate(drag_vector * Time.deltaTime);
            
        }
        
        countTime = 0.0f;

        while (countTime < 0.3f)
        {
            yield return new WaitForEndOfFrame();
            countTime += Time.deltaTime;
            transform.Translate((-1) * drag_vector * Time.deltaTime);
           
        }



        nowAct = false;
        inWind = false;

    }

}
