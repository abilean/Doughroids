using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableOnDeath : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
        this.gameObject.GetComponent<IHealth>().OnDie += HandleDeath;
    }

    private void HandleDeath()
    {
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
