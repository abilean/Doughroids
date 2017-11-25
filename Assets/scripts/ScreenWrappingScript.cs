using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenWrappingScript : MonoBehaviour {

    /// <summary>
    /// The max distance in the X direction (from 0) that the object can fly before wrapping
    /// Defaults to 0, if unset will attempt to load from GameManager
    /// </summary>
    [SerializeField]
    private float _maxX = 0;
    /// <summary>
    /// The max distance in the Z direction (from 0) that the object can fly before wrapping
    /// Defaults to 0, if unset will attempt to load from GameManager
    /// </summary>
    [SerializeField]
    private float _maxZ = 0;
    

	// Use this for initialization
	void Start () {
        if(_maxX == 0)
            _maxX = GameManager.Instance.BoardWidth;
        if(_maxZ ==0)
            _maxZ = GameManager.Instance.BoardHeight;

	}
	
	// Update is called once per frame
	void Update () {
        if(_maxX > 0 && _maxZ >0)
        {
            transform.position = new Vector3( Mathf.Repeat(transform.position.x, _maxX)
                ,transform.position.y, 
                Mathf.Repeat(transform.position.z, _maxZ));
        }
        
	}



}
