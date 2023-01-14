using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    [SerializeField] Vector3 movementVector;
    [SerializeField] float period = 2f;
    // [SerializeField] [Range(0,10)] float movementFactor; //create a vector3 for more complex movements | dont need it once we got line 27;
    
    Vector3 startingPosition;
    float movementFactor;
   
    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //watch class 58~59 again for a better undertanding
        
        if (period <= Mathf.Epsilon) { return; } //Mathf.Epsilon is the lowest float possible, so it's more secure to do this way in order to prevent errors
        //if (period == 0f) { return; } //this is wrong because it's hard for two floats to be COMPLETELY 100% equal, they may vary in decimal places
        
        float cycles = Time.time / period; // continually growing over time
        const float tau = Mathf.PI * 2; // constant value of 6.283 (radian)
        float rawSinWave = Mathf.Sin(cycles * tau); // going from -1 to 1

        movementFactor = (rawSinWave + 1f) / 2; // recalculated to go from 0 to 1, so it's cleaner

        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
    }
}
