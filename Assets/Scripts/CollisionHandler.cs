using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float delayTime = 1f;
    [SerializeField] AudioClip explosion;
    [SerializeField] AudioClip finish;
    [SerializeField] ParticleSystem crashParticles;
    [SerializeField] ParticleSystem successParticles;

    AudioSource audioSource;

    bool isTransitioning = false;

    private void Start() 
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision other) 
    {
        if (isTransitioning){ 
            return; //if it is true, do nothing
        }

        else {
            switch (other.gameObject.tag){
                case "Start":
                    Debug.Log("Start hit");
                    break;
                case "Obstacle":
                    StartCrashSequence();
                    Debug.Log("Obstacle hit");
                    break;
                case "Finish":
                    StartNextScene();
                    Debug.Log("Finish hit");
                    break;
                default:
                    StartCrashSequence();
                    break;
            }
        }
   }

    private void PlaySound(AudioClip soundClip)
    {
        if(!audioSource.isPlaying) {
            audioSource.PlayOneShot(soundClip);
        }
        else{
            audioSource.Stop();
        }
    }

    private void StartCrashSequence()
    {
        crashParticles.Play();
        isTransitioning = true; //do not play collision audio 2 times (disabled switch)
        audioSource.Stop(); //do not bug the audio, muting it after collision
        PlaySound(explosion); 
        crashParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadScene", delayTime);
    }

    private void StartNextScene()
    {
        isTransitioning = true; //do not play collision audio 2 times (disabled switch)
        audioSource.Stop(); //do not bug the audio, muting it after collision 
        PlaySound(finish);
        successParticles.Play();
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
