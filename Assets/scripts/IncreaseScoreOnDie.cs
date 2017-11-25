using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseScoreOnDie : MonoBehaviour {

    [SerializeField]
    private int _pointsOnDeath = 1;

    void Awake()
    {
        this.transform.GetComponent<IHealth>().OnDie += HandleDeath;
    }

    /// <summary>
    /// Increases score when object dies, by _pointsOnDeath
    /// </summary>
    private void HandleDeath()
    {
        GameManager.Instance.IncreaseScore(_pointsOnDeath);
    }

}
