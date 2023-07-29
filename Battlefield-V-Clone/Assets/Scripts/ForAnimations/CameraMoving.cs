using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoving : MonoBehaviour
{
    private GameObject cam;
    private Animator camRun;
    private void Awake()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera");
    }
    // Start is called before the first frame update
    void Start()
    {
        camRun = cam.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            camRun.SetBool("RunMovement", true);
        }
        else
            camRun.SetBool("RunMovement", false);
    }
}
