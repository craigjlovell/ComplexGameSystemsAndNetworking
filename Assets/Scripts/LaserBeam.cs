﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class LaserBeam : NetworkBehaviour 
{

    public LineRenderer lineRenderer;
    public float coolDown;
    public ParticleSystem fireFX;
    int index = 1;

    // Use this for initialization
    void Start()
    {
        // turn off the linerenderer
        ShowLaser(false);
        CharacterMovement cm = GetComponent<CharacterMovement>();
        if (cm)
            index = cm.index;
    }

    // Update is called once per frame
    void Update()
    {
        // count down, and hide the laser after half a second
        if (coolDown > 0)
        {
            coolDown -= Time.deltaTime;
            if (coolDown < 0.5f)
                ShowLaser(false);
        }

        // only check controls if we're the local player
        if (!isLocalPlayer)
            return;

        // activate the laser if the space bar has been pressed and we're off cooldown
        if (Input.GetButtonDown("Fire" + index) && coolDown <= 0)
            CmdFire();
    }

    [Command]
    void CmdFire()
    {
        // tell all clients todo it too
        RpcFire();
    }

    void ShowLaser(bool show)
    {
        lineRenderer.enabled = show;
    }

    void DoLaser()
    {
        // trigger the visuals - this should happen on all machines individually
        ShowLaser(true);
        coolDown = 1.0f;

        // more visual fx, a burst around the firing nozzle
        if (fireFX)
            fireFX.Play();

        // do a raycast to subtract health. we only want to do this on the server rather than each client doing their own raycast
        if (!isServer)
            return;

        // do a raycast against anything which may have a health on it
        RaycastHit hit;
        if (Physics.Raycast(new Ray(transform.position, transform.forward), out hit, 10.0f))
        {
            Health health = hit.transform.GetComponent<Health>();
            if (health)
            {
                // subtract health from the other player
                health.ApplyDamage(20);
            }
        }
    }

    [ClientRpc]
    void RpcFire()
    {
        DoLaser();
    }

}
