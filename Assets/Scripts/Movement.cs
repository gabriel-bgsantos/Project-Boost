using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    //to do: line 58 and 67 SFX
    //to do: solve bug of not turning boost particles off after crashing (CollisionHandler)

    [SerializeField] float rocketThrust = 1000f;
    [SerializeField] float rocketRotation = 100f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem leftEngineParticles;
    [SerializeField] ParticleSystem rightEngineParticles;

    Rigidbody rb;
    AudioSource boostAudio;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        boostAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    private void ProcessThrust()
    {
        if(Input.GetKey(KeyCode.Space)){
            StartThrust();
        }
        
        else{
           StopThrust(); 
        }
    }

    private void ProcessRotation()
    {
        if(Input.GetKey(KeyCode.D)){
            RotateLeft();
        }
        
        else if(Input.GetKey(KeyCode.A)){
            RotateRight();
        }
        
        else{
            StopRotation();
        }
    }

    private void StartThrust()
    {
        rb.AddRelativeForce(Vector3.up * Time.deltaTime * rocketThrust); 
        if(!mainEngineParticles.isPlaying){
            mainEngineParticles.Play();
        }
        
        if(!boostAudio.isPlaying){
            boostAudio.PlayOneShot(mainEngine);
        }
    }

    private void StopThrust()
    {
        boostAudio.Stop();
        mainEngineParticles.Stop();
    }

    private void RotateLeft()
    {
        // transform.Rotate(Vector3.forward * Time.deltaTime * rotationRight); // (0, 0, 1)
        ApplyRotation(rocketRotation);
        if(!rightEngineParticles.isPlaying){
            rightEngineParticles.Play();        
        }
    }

    private void RotateRight()
    {
        // transform.Rotate(Vector3.back * Time.deltaTime * rotationLeft); // (0, 0, -1)
        ApplyRotation(-rocketRotation);
        if(!leftEngineParticles.isPlaying){
            leftEngineParticles.Play();
        }
    }

    private void ApplyRotation(float rocketRotation)
    {
        rb.freezeRotation = true; //freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * Time.deltaTime * rocketRotation);
        rb.freezeRotation = false; //unfreezing rotation so the physics can take over
    }

    private void StopRotation()
    {
        leftEngineParticles.Stop();
        rightEngineParticles.Stop();
    }
}

