using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
                ReloadScene();
                break;
        }
   }

    private void ReloadScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        // SceneManager.GetActiveScene().buildIndex; get scene index number
    }
}
