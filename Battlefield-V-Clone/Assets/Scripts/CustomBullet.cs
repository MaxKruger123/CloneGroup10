using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomBullet : MonoBehaviour
{

    
   

   private void OnCollisionEnter(Collision collision) 
   {
        
        Destroy(gameObject);
        

   }


}
