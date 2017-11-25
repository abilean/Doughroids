using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealth  {

    float CurrentHpPct { get; }

    int CurrentHp { get; }

    /// <summary>
    /// Thrown when health changes, Int is current health
    /// </summary>
    event System.Action<int> OnHealthChg;

    /// <summary>
    /// Thrown when the object takes damage, Int is the damage amount;
    /// </summary>
    event System.Action<int> OnTakeDmg;

    /// <summary>
    /// thrown when health changes, float is current health percentage
    /// </summary>
    event System.Action<float> OnHealthPctChg;

    /// <summary>
    /// Thrown when health is increased
    /// </summary>
    event System.Action<int> OnAddHealth;

    /// <summary>
    /// Thrown when this object dies
    /// </summary>
    event System.Action OnDie;

    /// <summary>
    /// Causes the object to take damage equal to amount
    /// </summary>
    /// <param name="amount">the damage to be taken</param>
    void TakeDamage(int amount);

    /// <summary>
    /// Adds Amount to current Hp
    /// </summary>
    /// <param name="amount">amount of health to add</param>
    void AddHealth(int amount);

}

