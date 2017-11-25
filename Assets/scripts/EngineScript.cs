using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class EngineScript : MonoBehaviour {

    /// <summary>
    /// True for back engine, false for front engine
    /// </summary>
    [SerializeField]
    private bool BackEngine = true;


    PlayerController _pMove;
    ParticleSystem _engine;

	// Use this for initialization
	void Start () {
		_pMove = this.gameObject.GetComponentInParent<PlayerController>();
        _engine = this.gameObject.GetComponent<ParticleSystem>();

        if (BackEngine)
            _pMove.OnPlayerMoveForward += HandlePlayerMove;
        else
            _pMove.OnPlayerMoveBackward += HandlePlayerMove;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void HandlePlayerMove()
    {
        _engine.Play();
    }
}
