using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardHealth : MonoBehaviour, IHealth
{
    /// <summary>
    /// Max health the object can have
    /// Cannot be less than 1
    /// </summary>
    [SerializeField]
    private int _maxHealth = 10;

    /// <summary>
    /// objects current health
    /// </summary>
    private int _currentHealth;

    public int CurrentHp
    {
        get { return _currentHealth; }
        internal set { _currentHealth = value; }
    }

    /// <summary>
    /// The current percentage of health
    /// </summary>
    public float CurrentHpPct
    {
        get {
            if (_maxHealth < 0)
                return 0;
            else
                return _currentHealth / _maxHealth;
        }
    } 

    /// <summary>
    /// Thrown when health changes, Int is current health
    /// </summary>
    public event System.Action<int> OnHealthChg = delegate{ };

    /// <summary>
    /// Thrown when the object takes damage, Int is the damage amount;
    /// </summary>
    public event System.Action<int> OnTakeDmg = delegate { };

    /// <summary>
    /// thrown when health changes, float is current health percentage
    /// </summary>
    public event System.Action<float> OnHealthPctChg = delegate { };

    public event System.Action<int> OnAddHealth = delegate { };

    /// <summary>
    /// Thrown when this object dies
    /// </summary>
    public event System.Action OnDie = delegate { };

    /// <summary>
    /// Causes the object to take damage equal to amount
    /// calls OnHealthChg, OnHealthPctChg, and OnTakeDmg events
    /// checks for dealth
    /// </summary>
    /// <param name="amount">the damage to be taken</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if amount is 0 or less</exception>
    public virtual void TakeDamage(int amount)
    {
        if(amount <= 0)
        {
            throw new ArgumentOutOfRangeException("Damage amount in TakeDamage was out of range: " + amount + " in " + this.gameObject.name);
        }

        _currentHealth -= amount;

        if(OnHealthChg != null)
            OnHealthChg(_currentHealth);
        if(OnHealthPctChg != null)
            OnHealthPctChg(CurrentHpPct);
        if(OnTakeDmg != null)
            OnTakeDmg(amount);

        if (_currentHealth <= 0)
            Die();
    }

    /// <summary>
    /// Adds amount to current health as long as health isn't at max and amount isn't less than 1
    /// calls OnHealthChg, OnHealthPctChg, and OnAddHealth events
    /// </summary>
    /// <param name="amount">amount of health to add</param>
    public virtual void AddHealth(int amount)
    {
        //Makes sure health isn't at max and the amount isn't less than 1
        if(CurrentHpPct >= 100  || amount <=0)
        {
            return;
        }

        //check if amount would increase health over max health
        if(_currentHealth + amount > _maxHealth)
        {
            //set amount to increas health to max health
            amount = _maxHealth - _currentHealth;
        }

        _currentHealth += amount;

        if (OnHealthChg != null)
            OnHealthChg(_currentHealth);
        if (OnHealthPctChg != null)
            OnHealthPctChg(CurrentHpPct);
        if (OnAddHealth != null)
            OnAddHealth(amount);

    }

    /// <summary>
    /// Calls OnDie Event and Destroys the object
    /// </summary>
    protected virtual void Die()
    {
        if(OnDie != null)
        {
            OnDie();
        }

    }

    void Awake()
    {
        if (_maxHealth <= 0)
            _maxHealth = 1;
        _currentHealth = _maxHealth;
    }

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
