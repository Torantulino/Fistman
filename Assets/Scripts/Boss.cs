using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {

    public float health;
    public bool abilityAvailable; //Had the ability been triggered?
    public GameObject position0;
    public GameObject position1;
    public GameObject position2;
    public GameObject position3;
    public GameObject position4;
    public GameObject position5;
    public GameObject position6;
    public GameObject position7;
    public GameObject position8;
    public GameObject position9;
    public GameObject position10;
    public GameObject wSpider1;
    public GameObject wSpider2;
    public GameObject wSpider3;
    public GameObject wSpider4;
    public GameObject wSpider5;
    public GameObject wSpider6;
    public GameObject wSpider7;
    public GameObject wSpider8;
    public GameObject attackSpider;
    public GameObject ThePlayer;
    private PlayerScript playerScript;
    private List<GameObject> WallSpiders = new List<GameObject>();
    private List<GameObject> spiderMinions = new List<GameObject>();
    private float startTime;

    // Use this for initialization
    void Start ()
    {
        playerScript = ThePlayer.GetComponent<PlayerScript>();
        startTime = Time.time;
        PopulateSpiderMinions();
        PopulateWallSpiders();
        abilityAvailable = false;
    }
	
	// Update is called once per frame
	void Update ()
    {

        //Spider Respawn
        if (Time.time - startTime > 2.0f)
            {
            if (playerScript.isGrounded)
            {
                //Boss Minion Spawn
                SpawnSpider();
                //Spider Respawn
                RespawnWallSpider();
            }
            //Attack Spider Spawn
            Attack();
            //Reset 
            startTime = Time.time;
            }
        
    }

    private void PopulateSpiderMinions()
    {
        spiderMinions.Add(position0);
        spiderMinions.Add(position1);
        spiderMinions.Add(position2);
        spiderMinions.Add(position3);
        spiderMinions.Add(position4);
        spiderMinions.Add(position5);
        spiderMinions.Add(position6);
        spiderMinions.Add(position7);
        spiderMinions.Add(position8);
        spiderMinions.Add(position9);
        spiderMinions.Add(position10);
    }

    private void PopulateWallSpiders()
    {
        WallSpiders.Add(wSpider1);
        WallSpiders.Add(wSpider2);
        WallSpiders.Add(wSpider3);
        WallSpiders.Add(wSpider4);
        WallSpiders.Add(wSpider5);
        WallSpiders.Add(wSpider6);
        WallSpiders.Add(wSpider7);
        WallSpiders.Add(wSpider8);
    }

    //Boss attack
    public void Attack()
    {
        if (abilityAvailable)
        {
            Debug.Log("ATTACK SPIDER");
            //Attack
            Instantiate<GameObject>(attackSpider, transform); 
            
        }
    }

    //Boss Minion Spiders
    void SpawnSpider()
    {
        foreach (GameObject spiderMinion in spiderMinions)
        {
            spiderMinion.transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    //Wall Spider Respawn
    void RespawnWallSpider()
    {
        foreach (GameObject WallSpider in WallSpiders)
        {
            WallSpider.SetActive(true);
        }
    }
}
