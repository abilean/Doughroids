using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseScoreOnHit : MonoBehaviour {

    [SerializeField]
    private int _pointsOnHit = 1;

    void Awake()
    {
        this.transform.GetComponent<IHealth>().OnTakeDmg += HandleHit;
    }

    /// <summary>
    /// Increases the score went this object takes damage, increases by _pointsOnHit
    /// </summary>
    /// <param name="dmg">not used</param>
    private void HandleHit(int dmg)
    {
        GameManager.Instance.IncreaseScore(_pointsOnHit);
    }
}
