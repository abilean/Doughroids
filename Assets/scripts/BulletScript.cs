using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour, IPoolable {
    /// <summary>
    /// How fast the bullet flies
    /// </summary>
    [SerializeField]
    private float _speed = 10;
    /// <summary>
    /// How many seconds before the bullet disapears
    /// </summary>
    [SerializeField]
    private float _lifetime = 2;

    /// <summary>
    /// How much damamge the bullet will do
    /// </summary>
    [SerializeField]
    private int _damage = 1;

    private void OnDisable() { OnDestroyEvent(); }

    public event Action OnDestroyEvent = delegate { };

    // Use this for initialization
    void Start () {
        Invoke("Destructor", _lifetime);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        this.transform.position += _speed * Time.deltaTime * transform.forward;
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            other.GetComponent<IHealth>().TakeDamage(_damage);
            Destructor();
        }
    }


    /// <summary>
    /// Destroys the bullet
    /// </summary>
    private void Destructor()
    {
        this.gameObject.SetActive(false);
    }
}
