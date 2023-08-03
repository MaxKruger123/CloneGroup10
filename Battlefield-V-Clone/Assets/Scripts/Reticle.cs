using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reticle : MonoBehaviour
{
    private RectTransform reticle;


    public float restingSize;
    public float maxSize;
    public float speed;
    private float currentsize;

    private void Start()
    {
        reticle = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (isMoving)
        {
            currentsize = Mathf.Lerp(currentsize, maxSize, Time.deltaTime * speed);
        }
        else
        {
            currentsize = Mathf.Lerp(currentsize, restingSize, Time.deltaTime * speed);
        }

        reticle.sizeDelta = new Vector2(currentsize, currentsize);
    }

    bool isMoving
    {
        get
        {
            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0 || Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
                return true;
            else
                return true;
        }
    }
}
