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


    private void Awake()
    {
        rgdBody = GetComponentInChildren<Rigidbody2D>();
    }

    // Use this for initialization
    void Start () {

        health = 5;
        zero.x = 0;
        zero.y = 0;

        runSpeed = 4.0f;
        jumpPower = 5.0f;

        movementVector = zero;

        envLayerMask = LayerMask.GetMask("Environment");
	}
	
	// Update is called once per frame
	void Update ()
    {
        CheckIfGrounded();

        
    }

    //Use for physics
    private void FixedUpdate()
    {


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
            rgdBody.velocity = new Vector2(rgdBody.velocity.x, jumpPower);
        }
    }

    public void Move(float direction)
    {
        rgdBody.velocity = new Vector2(direction * runSpeed, rgdBody.velocity.y);
    }

    public void Punch(Vector2 mouseLoc)
    {
        //Validation
    }

    public void HookShot(Vector2 mouseLoc)
    {
        //Validation (must be attached to player)
    }

    public void CheckIfGrounded()
    {
        RaycastHit2D hit2D = Physics2D.Raycast(rgdBody.position - new Vector2(0f, 0.5f), Vector2.down, 0.1f, envLayerMask);


        if (hit2D)
        {
            isGrounded = true;

        }
        else
        {
            isGrounded = false;
        }


    }
}
