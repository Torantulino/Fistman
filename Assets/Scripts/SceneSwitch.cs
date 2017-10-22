using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {


    }
    void Update()
    {
        if (Input.anyKey)
        {
            SceneManager.LoadScene("LEVEL1");
        }
    }
}
