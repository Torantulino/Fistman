using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    public GameObject player;
    private Vector3 offset;
    private Vector3 targetPos;
    private Vector3 velocity;

    // Use this for initialization
    void Start () {
        velocity = Vector3.zero;
        offset = transform.position - player.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        targetPos = player.transform.position + offset;
    }

    private void LateUpdate()
    {
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, 0.3f);
    }
}
