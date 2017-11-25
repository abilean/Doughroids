using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecreaseScoreOnHeal : MonoBehaviour {

    [SerializeField]
    private int _pointsOnHeal = 1;

    void Awake()
    {
        this.transform.GetComponent<IHealth>().OnAddHealth += HandleHeal;
    }

    /// <summary>
    /// Decreases the score whent he object heals, decreases by _pointsOnHeal
    /// </summary>
    /// <param name="amt">not used</param>
    private void HandleHeal(int amt)
    {
        GameManager.Instance.DecreaseScore(_pointsOnHeal);
    }
}
