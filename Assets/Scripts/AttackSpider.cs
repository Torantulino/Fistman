using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSpider : MonoBehaviour {

    private GameObject Player;
    private Rigidbody2D rb2d;
    public float speed = 20;
	// Use this for initialization
	void Start () {
        Player = GameObject.FindWithTag("Player");
        rb2d = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        rb2d.velocity = Player.transform.position.normalized * speed;
	}
}
