using UnityEngine;
using UnityEngine.UI;

public class BirdScript : MonoBehaviour
{
    private GameObject Glass;
    private bool ActavieOnce = true;
    private AudioSource myAduioSoruce;
    public AudioClip FlySFX,ScoreSFX,DiedSFX;
    public AudioClip[] SuccessSFX;
    public static BirdScript instance;
    private Button FlapButton;
    private Rigidbody2D myRigidBody;
    private Animator anim;
    private float forwardSpeed = 3.2f;
    private float bounceSpeed = 6f;
    private bool DidFlap;
    public bool isAlive;
    public int Score;

    private void Awake()
    {
        Score = 0;
        myAduioSoruce = GetComponent<AudioSource>();
        myRigidBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        FlapButton = GameObject.FindGameObjectWithTag("FlapButton").GetComponent<Button>();
        if (instance == null)
            instance = this;
        FlapButton.onClick.AddListener(() => FlapTheBird());
        SetCameraX();
        Glass = transform.GetChild(0).gameObject;
        Glass.SetActive(false);
        isAlive = true;
    }

    void FixedUpdate()
    {
        if (isAlive) 
        {
            Vector3 temp = transform.position;
            temp.x += forwardSpeed * Time.deltaTime;
            transform.position = temp;
            if (DidFlap) 
            {
                DidFlap = false;
                myAduioSoruce.PlayOneShot(FlySFX);
                myRigidBody.velocity = new Vector2(0f, bounceSpeed);
                anim.SetTrigger("Flap");
            }
            if (myRigidBody.velocity.y >= 0)
                transform.rotation = Quaternion.Euler(0, 0, 0);
            else 
            {
                float angle = 0;
                angle = Mathf.Lerp(0, -90, -myRigidBody.velocity.y / 7);
                transform.rotation = Quaternion.Euler(0, 0, angle);
            }
            if (Score % 20 == 0 && Score!=0 && Score != 60 && ActavieOnce) 
            {
                ActavieOnce = false;
                int rand = Random.Range(0, SuccessSFX.Length);
                myAduioSoruce.PlayOneShot(SuccessSFX[rand]);
            }
            if (Score >= 40)
                Glass.SetActive(true);
            if(Score == 60)
                myAduioSoruce.PlayOneShot(SuccessSFX[2]);
        }
    }
    void SetCameraX() 
    {
        CameraScript.offsetX = (Camera.main.transform.position.x - transform.position.x) - 1f;
    }
    public float GetPositionX() 
    {
        return transform.position.x;
    }
    public void FlapTheBird() 
    {
        DidFlap = true;
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Pipe")) 
        {
            if (isAlive) 
            {
                isAlive = false;
                anim.SetTrigger("Bird Died");
                myAduioSoruce.PlayOneShot(DiedSFX);
                GamePlayController.instance.PlayerDiedShowScore(Score);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PipeHolder"))
        {
            Score++;
            myAduioSoruce.PlayOneShot(ScoreSFX);
            GamePlayController.instance.SetScore(Score);
        }
    }
}
