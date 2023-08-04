using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomBullet : MonoBehaviour
{

    
   

   private void OnCollisionEnter(Collision collision) 
   {
        if (collision.gameObject.tag == "Barrel")
        {
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 10f);
            Destroy(gameObject, 1f);
        }

        Destroy(gameObject, 1f);

    }


}
