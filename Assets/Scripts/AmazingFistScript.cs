using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmazingFistScript : MonoBehaviour {

    PlayerScript PlayerScr = new PlayerScript();
    public GameObject ThePlayer;
    public bool isPunching;

	// Use this for initialization
	void Start ()
    {
        PlayerScr = ThePlayer.GetComponent<PlayerScript>();
	}

    void Update()
    {
        if (!isPunching)
        {
            PlayerScr.NoHitting();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerScr.FistCollision(collision);
    }
}
