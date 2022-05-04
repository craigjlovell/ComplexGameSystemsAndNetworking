using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Mirror;
using System;

// Basic Hitpoint class demonstrating a SyncVar with a hook
public class Health : NetworkBehaviour 
{
    [SyncVar(hook = "onHealthChange")]
	public float health = 100;
    public float maxHealth = 100;

    HealthBar healthBar;

    public ParticleSystem hitFX;

    void Start()
    {
        // add a healthbar to the to the canvas
        healthBar = HealthBarManager.instance.AddHealthBar(this);
    }

    public void ApplyDamage(float damage)
    {
        // play the blood FX
        //if (damage > 0 && hitFX)
        //    hitFX.Play();

        // subtract the damage
        health -= damage;

        // update our health bar
        if (healthBar)
            healthBar.UpdateMeter();
    }

    public void onHealthChange(System.Single oldValue, System.Single newValue)
    {
        hitFX.Play();
    }
}
