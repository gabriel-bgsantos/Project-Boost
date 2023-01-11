using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float rocketThrust = 1000f;
    [SerializeField] float rocketRotation = 100f;
    [SerializeField] AudioClip mainEngine;

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
        if(Input.GetKey(KeyCode.Space )){
            rb.AddRelativeForce(Vector3.up * Time.deltaTime * rocketThrust);
            if(!boostAudio.isPlaying){
                boostAudio.PlayOneShot(mainEngine);
            }
        }
        else{
            boostAudio.Stop();
        }
    }

    private void ProcessRotation()
    {
        if(Input.GetKey(KeyCode.A)){
            // transform.Rotate(Vector3.forward * Time.deltaTime * rotationRight); // (0, 0, 1)
            ApplyRotation(rocketRotation);
        }
        else if(Input.GetKey(KeyCode.D)){
            // transform.Rotate(Vector3.back * Time.deltaTime * rotationLeft); // (0, 0, -1)
            ApplyRotation(-rocketRotation);
        }
    }

    private void ApplyRotation(float rocketRotation)
    {
        rb.freezeRotation = true; //freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * Time.deltaTime * rocketRotation);
        rb.freezeRotation = false; //unfreezing rotation so the physics can take over
    }
}

