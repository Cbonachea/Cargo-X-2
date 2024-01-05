﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameControl : MonoBehaviour
{
    private ShipControl shipControl;

    private Animator animator;
    [SerializeField] internal ParticleSystem sparksRed;
    [SerializeField] internal ParticleSystem sparksBlue;
    private bool thrust;
    private bool boost;

    void Start()
    {

        shipControl = GetComponentInParent<ShipControl>();
        animator = GetComponent<Animator>();
        SubscribeGameEvents();
    }

    private void Update()
    {
        if (shipControl.canControl == false)
        {
            FlameOff();
            sparksRed.Stop();
            sparksBlue.Stop();
        }
        if (thrust)
        {
            animator.SetBool("isBoosting", false);
            animator.SetBool("isThrusting", true);
            sparksRed.Play();
        }
        if (boost)
        {
            animator.SetBool("isThrusting", false);
            animator.SetBool("isBoosting", true);
            sparksBlue.Play();
            sparksRed.Play();
        }
        if (!thrust && !boost)
        {
            animator.SetBool("isThrusting", false);
            animator.SetBool("isBoosting", false);
            sparksRed.Stop();
            sparksBlue.Stop();
        }

    }

    private void FlameOn()
    {
        if (shipControl.canControl == false) return;
        thrust = true;
    }

    private void FlameOff()
    {
        thrust = false;
        boost = false;
    }

    private void BoostOn()
    {
        if (shipControl.canControl == false) return;
        boost = true;
        thrust = false;
    }

    private void SubscribeGameEvents()
    {
        GameEvents.current.onEngineOn += FlameOn;
        GameEvents.current.onEngineOff += FlameOff;
        GameEvents.current.onEngineBoost += BoostOn;
    }

    private void OnDestroy()
    {
        GameEvents.current.onEngineOn -= FlameOn;
        GameEvents.current.onEngineOff -= FlameOff;
        GameEvents.current.onEngineBoost -= BoostOn;
    }
}