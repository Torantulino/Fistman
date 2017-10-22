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
    private List<GameObject> spiderMinions = new List<GameObject>();
    float startTime;

    // Use this for initialization
    void Start () {
        startTime = Time.time;
        spiderMinions.Add(position1);
        spiderMinions.Add(position2);
        spiderMinions.Add(position3);
        spiderMinions.Add(position4);
        spiderMinions.Add(position5);
    }
	
	// Update is called once per frame
	void Update () {
            if (Time.time - startTime > 1.0f)
            {
                SpawnSpider();
                startTime = Time.time;
            }
    }

    public void Attack()
    {
        if (abilityAvailable)
        {
            //Attack
        }
    }

    void SpawnSpider()
    {
        System.Random r = new System.Random();
        int randomNumber = r.Next(0,5);

        spiderMinions[randomNumber].gameObject.SetActive(true);

    }

}
