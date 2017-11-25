using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonutController : MonoBehaviour , IPoolable{

    [SerializeField]
    private int _damage = 1;

    private void OnDisable() {
        if(OnDestroyEvent != null)
            OnDestroyEvent();
    }

    public event Action OnDestroyEvent = delegate { };

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// Handles when the donut collides with another object
    /// if player, deal damage
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<IHealth>().TakeDamage(_damage);
        }
    }

    /// <summary>
    /// Handles when the donut collides with another object
    /// if player, deal damage
    /// </summary>
    /// <param name="other"></param>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            collision.collider.GetComponent<IHealth>().TakeDamage(_damage);
        }
    }
}
