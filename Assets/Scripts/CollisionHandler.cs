using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float delayTime = 1f;
    [SerializeField] AudioClip explosion;
    [SerializeField] AudioClip finish;

    AudioSource audioSource;

    private void Start()
    {

    }

    private void OnCollisionEnter(Collision other) 
    {
        switch (other.gameObject.tag){
             case "Start":
                Debug.Log("Start hit");
                break;
            case "Obstacle":
                PlaySound(explosion);
                StartCrashSequence();
                Debug.Log("Obstacle hit");
                break;
            case "Finish":
                PlaySound(finish);
                StartNextScene();
                Debug.Log("Finish hit");
                break;
            default:
                PlaySound(explosion);
                StartCrashSequence();
                break;
        }
   }

    private void PlaySound(AudioClip soundClip)
    {
        audioSource = GetComponent<AudioSource>();

        if(!audioSource.isPlaying) {
            audioSource.PlayOneShot(soundClip);
        }
        else{
            audioSource.Stop();
        }
    }

    private void StartCrashSequence()
    {
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadScene", delayTime);
    }

    private void StartNextScene()
    {
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextScene", delayTime);
    }

    private void ReloadScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        Debug.Log(currentSceneIndex);
        // SceneManager.GetActiveScene().buildIndex; get scene index number
    }

    private void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings){
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
}
