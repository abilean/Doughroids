using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprinkleController : MonoBehaviour, IPoolable {

    [SerializeField]
    private Material[] colors;

    /// <summary>
    /// amount of damage sprinkle can do
    /// </summary>
    private int _damage = 1;

    private void OnDisable() {
        if(OnDestroyEvent != null)
            OnDestroyEvent();
    }

    public event Action OnDestroyEvent = delegate { };

    /// <summary>
    /// How fast the bullet flies
    /// </summary>
    [SerializeField]
    private float _speed = 10;


    // Use this for initialization
    void Start () {
		if(colors.Length > 0)
        {
            this.gameObject.GetComponentInChildren<Renderer>().material = colors[UnityEngine.Random.Range(0, colors.Length - 1)];
        }
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        this.transform.position += _speed * Time.deltaTime * transform.forward;
    }

    public void OnTriggerEnter(Collider other)
    {
        IHealth health;
        if (other.tag == "Player")
        {
            health =  other.GetComponent<IHealth>();
            if(health == null)
            {
                Debug.LogError("object tagged 'Player' doesn't have IHealth; " + other.name);
            }
            else
            {
                health.TakeDamage(_damage);
                Destructor();
            }
        }
        if(other.tag == "Enemy")
        {
            DonutController donut = other.GetComponent<DonutController>();
            if(donut != null)
            {
                health = other.GetComponent<IHealth>();
                if (health == null)
                {
                    Debug.LogError("object with donut controller doesn't have IHealth; " + other.name);
                }
                else
                {
                    health.AddHealth(1);
                    Destructor();
                }
            }
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
