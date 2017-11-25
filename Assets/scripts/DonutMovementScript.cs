using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class DonutMovementScript : MonoBehaviour {

    public float MaxSpeed = 10f;
    public float MinSpeed = 0.5f;
    public Rigidbody rb;

    

    // Use this for initialization
    void Start () {
        float thrust = Random.Range(MinSpeed, MaxSpeed);
        rb = GetComponent<Rigidbody>();
        transform.Rotate( new Vector3(0, Random.Range(0, 360),0));
        rb.AddForce(transform.forward * thrust);
    }
	
	// Update is called once per frame
	void Update () {
        
	}
}
