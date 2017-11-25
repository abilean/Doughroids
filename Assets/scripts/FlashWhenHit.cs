using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashWhenHit : MonoBehaviour {

    /// <summary>
    /// List of renders in children
    /// </summary>
    private Renderer[] _renderers;

    /// <summary>
    /// The original colors of the renders, in same order
    /// </summary>
    private Color[] _originalColors;

    [SerializeField]
    private float _flashTime = 0.2f;

    [SerializeField]
    private Color _collisionColor = Color.white;

	// Use this for initialization
	void Start () {
        _renderers = gameObject.GetComponentsInChildren<Renderer>();
        _originalColors = new Color[_renderers.Length];
        for(int i =0; i< _renderers.Length; i++)
        {
           _originalColors[i] = _renderers[i].material.color;
        }

        GetComponentInParent<IHealth>().OnTakeDmg += HandleHit;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void HandleHit(int amount)
    {
         StartCoroutine(DoFlash( _flashTime));
    }

    IEnumerator DoFlash( float flashTime)
    {

        for(int i = 0; i < _renderers.Length; i++) {
            _renderers[i].material.color = _collisionColor;
        }
        //wait for a bit
        yield return new WaitForSeconds(flashTime);


        //make sure renderer is enabled when we exit
        for (int i = 0; i < _renderers.Length; i++)
        {
            _renderers[i].material.color = _originalColors[i];
        }
    }
}
