using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(IHealth))]
public class HealthGraphicIcons : MonoBehaviour {

    [SerializeField]
    private GameObject[] _healthIcons;

	// Use this for initialization
	void Start () {
        initializeHealthIcons();
        this.gameObject.GetComponent<IHealth>().OnTakeDmg += HandleTakeDamage;
        this.gameObject.GetComponent<IHealth>().OnAddHealth += HandleAddHealth;
	}
	



	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// Puts the right amount of health up for the current health
    /// </summary>
    private void initializeHealthIcons()
    {
        if(_healthIcons == null)
        {
            _healthIcons = new GameObject[0];
        }
        for (int i = 0; i < _healthIcons.Length; i++)
        {
            int totalHealth = this.gameObject.GetComponent<IHealth>().CurrentHp;
            if (i < totalHealth)
            {
                _healthIcons[i].SetActive(true);
            }
            else
            {
                _healthIcons[i].SetActive(false);
            }
        }
    }

    /// <summary>
    /// Takes away "amount" number of health graphics
    /// </summary>
    /// <param name="amount">number of health graphics to take away</param>
    private void HandleTakeDamage(int amount)
    {
        for(int i = 0; i < amount; i++)
        {
            foreach(GameObject icon in _healthIcons)
            {
                if (icon.activeSelf)
                {
                    icon.SetActive(false);
                    break;
                }
            }
        }
    }

    /// <summary>
    /// adds 'amount' number of health icons back
    /// </summary>
    /// <param name="amount"></param>
    private void HandleAddHealth(int amount)
    {
        for (int i = 0; i < amount; i++)  
        {
            foreach (GameObject icon in _healthIcons)  //search for inactive icons
            {
                if (!(icon.activeSelf))   //if the icon is NOT active
                {
                    icon.SetActive(true);
                    break;
                }
            }
        }
    }

}
