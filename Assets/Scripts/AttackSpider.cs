using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSpider : MonoBehaviour {

    private GameObject player;
    private GameObject boss;
    private Rigidbody2D rb2d;
    private float speed = 5.0f;
    private float angle;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        boss = GameObject.FindWithTag("Boss");
        rb2d = GetComponent<Rigidbody2D>();
        transform.position = boss.transform.position- new Vector3(0, 5);
    }

    // Use this for initialization
    void Start () {
        Vector2 playerPos = player.transform.position;
        Vector2 attackSpiderPos = transform.position;
        //Rotate
        float distX = playerPos.x - attackSpiderPos.x;
        float distY = playerPos.y - attackSpiderPos.y;
        angle = Mathf.Atan2(distY, distX) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);

        //Move
        rb2d.velocity = (player.transform.position - transform.position).normalized * speed;
        Destroy(this.gameObject, 5);
    }
	
	// Update is called once per frame
	void FixedUpdate () {

	}
}
