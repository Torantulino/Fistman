using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {

    public float health;
    private bool abilityAvailable; //is the ability on cooldown?
    public GameObject position1;
    public GameObject position2;
    public GameObject position3;
    public GameObject position4;
    public GameObject position5;
    public GameObject wSpider1;
    public GameObject wSpider2;
    public GameObject wSpider3;
    public GameObject wSpider4;
    public GameObject wSpider5;
    public GameObject wSpider6;
    public GameObject wSpider7;
    public GameObject wSpider8;
    public List<GameObject> WallSpiders = new List<GameObject>();



    private List<GameObject> spiderMinions = new List<GameObject>();
    float startTime;

    // Use this for initialization
    void Start ()
    {
        startTime = Time.time;
        PopulateSpiderMinions();
        PopulateWallSpiders();
    }
	
	// Update is called once per frame
	void Update ()
    {
        //Spider Respawn
        if (Time.time - startTime > 2.0f)
        {
            //Boss Minion Spawn
            SpawnSpider();
            //Spider Respawn
            RespawnWallSpider();
            startTime = Time.time;
        }

    }

    private void PopulateSpiderMinions()
    {
        spiderMinions.Add(position1);
        spiderMinions.Add(position2);
        spiderMinions.Add(position3);
        spiderMinions.Add(position4);
        spiderMinions.Add(position5);
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

    public void Attack()
    {
        if (abilityAvailable)
        {
            //Attack
        }
    }

    //Boss Minion Spiders
    void SpawnSpider()
    {
        System.Random r = new System.Random();
        int randomNumber = r.Next(0,5);

        spiderMinions[randomNumber].gameObject.SetActive(true);
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
