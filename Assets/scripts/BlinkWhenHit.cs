using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkWhenHit : MonoBehaviour
{

    /// <summary>
    /// List of renders in children
    /// </summary>
    private Renderer[] _renderers;

    /// <summary>
    /// How long it disapears for each blink
    /// </summary>
    [SerializeField]
    [Tooltip("How long it disapears for each blink")]
    private float _blinkTime = 0.2f;

    /// <summary>
    /// Total time the object is blinking
    /// </summary>
    [SerializeField]
    [Tooltip("Total time the object is blinking")]
    private float _duration = 0.5f;


    // Use this for initialization
    void Start()
    {
        _renderers = gameObject.GetComponentsInChildren<Renderer>();
        GetComponentInParent<IHealth>().OnTakeDmg += HandleHit;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void HandleHit(int amount)
    {
        StartCoroutine(DoBlinks());
    }

    IEnumerator DoBlinks()
    {
        float dur = _duration;
        while (dur > 0f)
        {
            for (int i = 0; i < _renderers.Length; i++)
            {
                dur -= Time.deltaTime;

                //toggle renderer
                _renderers[i].enabled = !_renderers[i].enabled;
            }
            //wait for a bit
            yield return new WaitForSeconds(_blinkTime);
        }


        //make sure renderer is enabled when we exit
        for (int i = 0; i < _renderers.Length; i++)
        {
            _renderers[i].enabled = true;
        }


    }
}


