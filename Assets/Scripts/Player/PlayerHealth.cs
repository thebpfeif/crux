using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class PlayerHealth : MonoBehaviour {

    public int StartingHealth;
    public int CurrentHealth;
    public Slider HealthSlider; 

    private void Awake()
    {
        CurrentHealth = StartingHealth;
        HealthSlider.value = StartingHealth; 
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void TakeDamage ( int damage )
    {
        CurrentHealth -= damage;
        HealthSlider.value = CurrentHealth; 
    }
}
