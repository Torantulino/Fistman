using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    private int fistMass;
    private int playerMass;
    private int health;
    private float verticalSpeed;
    private float horizontalSpeed;
    private float hozInput;
    private float runSpeed;
    private float jumpPower;
    private float bossBouncePower;
    private float angle;
    private bool isGrounded;
    private bool punchAvail;
    public bool isPunching;
    private Vector2 movementVector;
    private Vector2 zero;
    private Vector2 mousePos;
    private Vector2 fistPos;
    private Rigidbody2D rgdBody;
    private LayerMask envLayerMask;
    public Transform fist;
    public GameObject playerBody;
    private Animator playerAnimator;
    private Animator fistAnimator;
    private SpriteRenderer playerSpriteRen;
    public AudioSource music;
    public Camera MainCam;
    public GameObject spiderBoss;
    private Boss BossScript;

    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;
    public GameObject heart4;
    public GameObject heart5;
    public GameObject fistPowerUp;
    public List<GameObject> UIHearts = new List<GameObject>();

    private void Awake()
    {
        rgdBody = GetComponent<Rigidbody2D>();
    }

    // Use this for initialization
    void Start ()
    {
        BossScript = spiderBoss.GetComponent<Boss>();

        PopulateHearts();
        health = 5;
        
        zero.x = 0;
        zero.y = 0;

        isPunching = false;

        runSpeed = 4.0f;
        jumpPower = 5.0f;
        bossBouncePower = 3.0f;

        movementVector = zero;

        envLayerMask = LayerMask.GetMask("Environment");

        playerAnimator = playerBody.GetComponent<Animator>();
        playerSpriteRen = playerBody.GetComponent<SpriteRenderer>();
        fistAnimator = fist.GetComponentInChildren<Animator>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        CheckIfGrounded();
        if (isGrounded)
        {
            punchAvail = true;
            fistPowerUp.SetActive(true);
            if (!Input.GetKeyDown("space"))
            {
                playerAnimator.SetBool("isJumping", false);
            }
            playerAnimator.SetBool("Grounded", true);
        }
        else
        {
            playerAnimator.SetBool("Grounded", false);
        }


    }

    //Use for physics
    private void FixedUpdate()
    {

        //Fist Rotation
        if (!isPunching)
        {
            mousePos = Input.mousePosition;
            fistPos = Camera.main.WorldToScreenPoint(fist.position);
            float distX = mousePos.x - fistPos.x;
            float distY = mousePos.y - fistPos.y;
            angle = Mathf.Atan2(distY, distX) * Mathf.Rad2Deg;
            fist.rotation = Quaternion.Euler(0, 0, angle);
        }
        else
        {
            fist.rotation = Quaternion.Euler(0, 0, angle);
        }

        //Player Rotation
        if (!isGrounded)
        {
            playerBody.transform.rotation = Quaternion.Euler(0, 0, angle -90);
        }
        else
        {
            
            playerBody.transform.rotation = Quaternion.Euler(0, 0, 0);
        }

    }

    public int Health
    {
        get
        {
            return health;
        }
        set
        {
            //Validation
            health = value;
        }
    }

    public void Jump()
    {
        if (isGrounded)
        {
            playerAnimator.SetBool("isJumping", true);
            rgdBody.velocity = new Vector2(rgdBody.velocity.x, jumpPower);
        }
    }

    public void Move(float direction)
    {
        if (isGrounded)
        {
            rgdBody.velocity = new Vector2(direction * runSpeed, rgdBody.velocity.y);

            //Flip Animation for left and right.
            if (direction > 0)
            { 
                playerSpriteRen.flipX = false;
                playerAnimator.SetBool("isRunning", true);
            }
            if (direction<0)
            {
                playerAnimator.SetBool("isRunning", true);
                playerSpriteRen.flipX = true;
            }
            if (!music.isPlaying)
            {
                music.Play();
            }
        }
        
    }

    public void PlayerIdle()
    {
        playerAnimator.SetBool("isRunning", false);
        playerAnimator.SetBool("isJumping", false);
        music.Stop();
    }

    public void Punch(Vector3 mouseLoc)
    {
        //If a punch is available
        if (punchAvail)
        {
            //Fling Player
            rgdBody.gravityScale = 0.0f;
            StartCoroutine(GravitySwitch());
            Vector2 force = (mouseLoc - Camera.main.WorldToScreenPoint(rgdBody.position)).normalized;
            rgdBody.AddForce(force * 1000);

            //Animate Fist
            fistAnimator.SetTrigger("isPunching");

            //Set Punch Unavailable
            punchAvail = false;
            fistPowerUp.SetActive(false);
        }
    }

    IEnumerator GravitySwitch()
    {
        yield return new WaitForSeconds(0.2f);
        rgdBody.gravityScale = 2.0f;
    }

    public void HookShot(Vector2 mouseLoc)
    {
        //Validation (must be attached to player)
    }

    public void CheckIfGrounded()
    {
        RaycastHit2D hit2D = Physics2D.Raycast(rgdBody.position - new Vector2(0f, 1.0f), Vector2.down, 0.2f, envLayerMask);

        if (hit2D)
        {
            isGrounded = true;
            fistAnimator.SetBool("Grounded", true);

        }
        else
        {
            isGrounded = false;
            fistAnimator.SetBool("Grounded", false);
        }


    }

    public void NoHitting()
    {
        isPunching = false;
    }

    private void PlayerInjured()
    {

        //Take Damage
        health--;
        
        //Remove heart
        UIHearts[health].SetActive(false);

        //Check if dead.##########
        if (health < 1)
        {
            Application.Quit();
            Debug.Log("health is less than one");
        }

        //Play Injured Sound

        //Play Injured Animation
        playerAnimator.SetTrigger("Injured");
    }

    //Player Collisions
    void OnTriggerEnter2D(Collider2D col)
    {
        switch (col.gameObject.tag)
        {
            case "Spider":
                if (col.gameObject.activeInHierarchy)
                {
                    PlayerInjured();
                }
                break;
            case "BossLeftSide":
                // bounce back
                rgdBody.velocity = new Vector2(-bossBouncePower, rgdBody.velocity.y);
                //take damage
                PlayerInjured();
                break;
            case "BossRightSide":
                // bounce back
                rgdBody.velocity = new Vector2(bossBouncePower, rgdBody.velocity.y);
                //take damage
                PlayerInjured();
                break;
            case "CameraZoomout":
                {
                    //Zoomout Camera For BossFight
                    //##LERP##
                    BossScript.abilityAvailable = true;
                    MainCam.orthographicSize = Mathf.Lerp(5, 10, Time.deltaTime * 3);
                    break;
                }

        }
    }

    //Fist Collisions
    public void FistCollision(Collider2D col)
    {
        switch (col.gameObject.tag)
        {
            case "Spider":
                col.gameObject.SetActive(false);
                punchAvail = true;
                PowerUpFist();
                fistPowerUp.SetActive(true);
                break;
            case "BossLeftSide":
                // bounce back
                rgdBody.velocity = new Vector2(-bossBouncePower, rgdBody.velocity.y);
                break;
            case "BossRightSide":
                // bounce back
                rgdBody.velocity = new Vector2(bossBouncePower, rgdBody.velocity.y);
                break;
            case "BossTop":
                break;

        }
    }

    void PowerUpFist()
    {
        //If it doesn't scale evenly, arguments should be relative to object being scaled-> multiply current object size by constant
        fist.localScale += new Vector3(0.1f, 0.1f);
    }

    void PopulateHearts()
    {
        UIHearts.Add(heart1);
        UIHearts.Add(heart2);
        UIHearts.Add(heart3);
        UIHearts.Add(heart4);
        UIHearts.Add(heart5);
    }

}
