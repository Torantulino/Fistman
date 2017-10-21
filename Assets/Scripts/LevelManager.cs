using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    public GameObject ThePlayer;

    PlayerScript Player;

	// Use this for initialization
	void Start () {
        Player = ThePlayer.GetComponent<PlayerScript>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        if (Input.GetKeyDown("space"))
        {
            //Jump!
            Player.Jump();
            
        }

        if (Input.GetAxis("Horizontal") != 0.0f)
        {
            //Move
            float hozInput = Input.GetAxis("Horizontal");
            Player.Move(hozInput);
        }

        if (Input.GetMouseButtonDown(0))
        {
            //If menu isn't active
            //Punch!
            Player.Punch(Input.mousePosition);
        }
        if (Input.GetMouseButtonDown(1))
        {
            //If menu isn't active
        }
	}
}
