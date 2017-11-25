using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Rigidbody))]
public class PlayerController : MonoBehaviour {

    public float Thrust = 1;
    public float TurnSpeed = 5;
    public Rigidbody rb;

    public BulletScript Bullet;
    public GameObject Gun;

    public delegate void PlayerMoveForward();
    public event PlayerMoveForward OnPlayerMoveForward;

    public delegate void PlayerMoveBackward();
    public event PlayerMoveBackward OnPlayerMoveBackward;

    public delegate void PlayerTurnLeft();
    public event PlayerTurnLeft OnPlayerTurnLeft;

    public delegate void PlayerTurnRight();
    public event PlayerTurnRight OnPlayerTurnRight;

    public delegate void FireGun();
    public event FireGun OnFireGun;

    //The pool for getting prefabs
    private Pool pool;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        pool = Pool.GetPool(Bullet);
	}
	
	// Update is called once per frame
	void Update () {
		
    }

    /// <summary>
    /// Adds the thrust force to the players forward direction
    /// </summary>
    public void MoveForward()
    {
        rb.AddForce(transform.forward * Thrust);
        //signals any listeners
        if (OnPlayerMoveForward != null)
            OnPlayerMoveForward();
    }

    /// <summary>
    /// adds thrust force to players backward direction
    /// </summary>
    public void MoveBackward()
    {
        rb.AddForce((transform.forward * -1) * Thrust);
        //signals any listeners
        if (OnPlayerMoveBackward != null)
            OnPlayerMoveBackward();
    }

    /// <summary>
    /// turns player right by turn speed
    /// </summary>
    public void TurnRight()
    {
        transform.Rotate(new Vector3(0, TurnSpeed, 0));

        //signals any listeners
        if (OnPlayerTurnRight != null)
            OnPlayerTurnRight();
    }

    /// <summary>
    /// turns player left by turn speed
    /// </summary>
    public void TurnLeft()
    {
        transform.Rotate(new Vector3(0, (TurnSpeed * -1), 0));
        //signals any listeners
        if (OnPlayerTurnLeft != null)
            OnPlayerTurnLeft();
    }

    /// <summary>
    /// Shoots the gun
    /// </summary>
    public void Shoot()
    {

        pool.Get(Gun.transform.position, Gun.transform.rotation);

        //signals any listeners
        if (OnFireGun != null)
            OnFireGun();
    }


    public void OnDestroy()
    {

    }
}
