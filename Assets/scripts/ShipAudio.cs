using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ShipAudio : MonoBehaviour {

    [SerializeField]
    AudioSource _engineAudio;

    [SerializeField]
    AudioSource _gunAudio;
    PlayerController _player;

	// Use this for initialization
	void Start () {
        //_engineAudio = this.gameObject.GetComponent<AudioSource>();
        _player = this.gameObject.GetComponentInParent<PlayerController>();

        _player.OnPlayerMoveBackward += HandleMove;
        _player.OnPlayerMoveForward += HandleMove;
        _player.OnFireGun += HandleShoot;
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    private void HandleMove()
    {

        if (!_engineAudio.isPlaying)
            _engineAudio.Play();

    }

    private void HandleShoot()
    {
        _gunAudio.Play();
    }

}
