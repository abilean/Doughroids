using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvulnerableAfterHitHealth : StandardHealth {




    /// <summary>
    /// How long the object is invulnerable after getting hit
    /// </summary>
    [SerializeField]
    [Tooltip("How long the object if invulernable after getting hit")]
    private float InvulnerableTimeAfterHitTime = 0f;

    /// <summary>
    /// The current time remaining until the object can get hurt again
    /// </summary>
    private float _currentDelay = 0;


    /// <summary>
    /// Causes the object to take damage equal to amount
    /// calls OnHealthChg, OnHealthPctChg, and OnTakeDmg events
    /// checks for dealth
    /// </summary>
    /// <param name="amount">the damage to be taken</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if amount is 0 or less</exception>
    public override void TakeDamage(int amount)
    {
        if (_currentDelay > 0)
        {
            return;
        }

        base.TakeDamage(amount);

        _currentDelay = InvulnerableTimeAfterHitTime;
    }



    private void Update()
    {
        if (_currentDelay > 0)
        {
            _currentDelay -= Time.deltaTime;
        }
    }
}
