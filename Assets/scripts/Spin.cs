using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour {

    public float rotationX = 1;
    public float rotationY = 1;
    public float rotationZ = 1;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.Rotate((new Vector3(rotationX, rotationY, rotationZ)) );
    }
}
