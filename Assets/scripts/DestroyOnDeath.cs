﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnDeath : MonoBehaviour {


	// Use this for initialization
	void Start () {
        this.gameObject.GetComponent<IHealth>().OnDie += HandleDeath;
	}
	
    private void HandleDeath()
    {
        Destroy(this.gameObject);
    }


	// Update is called once per frame
	void Update () {
		
	}
}
