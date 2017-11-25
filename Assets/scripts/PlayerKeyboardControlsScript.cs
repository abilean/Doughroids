using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (PlayerController))]
public class PlayerKeyboardControlsScript : MonoBehaviour {

    PlayerController _pMove;

	// Use this for initialization
	void Start () {
        _pMove = this.gameObject.GetComponent<PlayerController>();
        
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKey(KeyCode.UpArrow))
        {
            _pMove.MoveForward();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKey(KeyCode.LeftArrow))
        {
            _pMove.TurnLeft();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKey(KeyCode.RightArrow))
        {
            _pMove.TurnRight();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKey(KeyCode.DownArrow))
        {
            _pMove.MoveBackward();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _pMove.Shoot();   
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.Instance.PauseGame();
        }

    }
}
