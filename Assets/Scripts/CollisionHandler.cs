using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
   private void OnCollisionEnter(Collision other) 
   {
    switch (other.gameObject.tag){
        case "Friendly":
            Debug.Log("Friendly hit");
            break;
        case "Fuel":
            Debug.Log("Fuel hit");
            break;
        case "Finish":
            Debug.Log("Finish hit");
            break;
        default:
            Debug.Log("Did not hit anything");
            break;
    }
   }
}
