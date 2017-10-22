using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    private int fistMass;
    private int playerMass;
    private float health;
    private float verticalSpeed;
    private float horizontalSpeed;
    private Vector2 movementVector;
    private Vector2 zero;
    private bool isGrounded;
    private Rigidbody2D rgdBody;
    private LayerMask envLayerMask;
    private float hozInput;
    private float runSpeed;
    private float jumpPower;
    private float bossBouncePower;
    private Vector2 mousePos;
    public Transform fist;
    private Vector2 fistPos;
    private float angle;
    private bool punchAvail;
    public GameObject playerBody;
    private Animator playerAnimator;
    private SpriteRenderer playerSpriteRen;
    private Animator fistAnimator;
    public bool isPunching;

    private void Awake()
    {
        rgdBody = GetComponent<Rigidbody2D>();
    }

    // Use this for initialization
    void Start ()
    {

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

    public float Health
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
        }
        
    }

    public void PlayerIdle()
    {
        playerAnimator.SetBool("isRunning", false);
        playerAnimator.SetBool("isJumping", false);
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
    void OnTriggerEnter2D(Collider2D col)
    {
        switch (col.gameObject.tag)
        {
            case "Spider":
                col.gameObject.SetActive(false);
                //Destroy(col.gameObject);
                punchAvail = true;
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


}
