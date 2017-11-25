using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprinkleLauncher : MonoBehaviour {

    [SerializeField]
    private SprinkleController _sprinklePrefab;

    [SerializeField]
    private GameObject[] _launchSpots;

    public delegate void SprinkleLaunched();
    public event SprinkleLaunched OnSprinkleLaunched;

    //The pool for getting prefabs
    private Pool _pool;

    // Use this for initialization
    void Start () {
        this.gameObject.GetComponent<IHealth>().OnTakeDmg += HandleHit;
        _pool = Pool.GetPool(_sprinklePrefab);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void HandleHit(int amount)
    {
        for(int i = 0; i < amount; i++)
        {
            Transform shootFrom = this.transform;
            if(_launchSpots.Length > 0)
            {
                shootFrom = _launchSpots[(int)Random.Range(0, _launchSpots.Length - 1)].transform;
            }
            _pool.Get(shootFrom.position, shootFrom.rotation, this.transform.parent);

            

            if(OnSprinkleLaunched != null)
            {
                OnSprinkleLaunched();
            }
        }
    }
}
