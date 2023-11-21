using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineControl : MonoBehaviour
{

    private FuelSystem fuelSystem;
    private ShipControl shipControl;

    private bool canControl = true;

    private bool tryPowerOn;
    private bool tryToggleEngineMode;
    private bool toggleEngineMode;
    private bool engineModeToggled;
    private bool hasFuel;
    private bool boostEngaged;
    private enum EngineStatus { off = 0, on = 1, boost = 2 };
    private EngineStatus engineStatus = EngineStatus.off;

    private void Awake()
    {
        fuelSystem = GetComponent<FuelSystem>();
        shipControl = GetComponent<ShipControl>();
    }

    private void Start()
    {
        SubscribeGameEvents();    
    }

    private void Update()
    {
        if (!canControl) return;
        {

            if (tryToggleEngineMode)
            {
                toggleEngineMode = CheckCanToggleBoost();
                if (engineModeToggled) return;
                if (toggleEngineMode) boostEngaged = !boostEngaged;
                engineModeToggled = true;
            }

            if (tryPowerOn)
            {
                hasFuel = CheckFuel();
                if (hasFuel && !boostEngaged) engineStatus = EngineStatus.on;
                if (hasFuel && boostEngaged) engineStatus = EngineStatus.boost;
                if (!hasFuel) engineStatus = EngineStatus.off;
            }

            if (!tryPowerOn) engineStatus = EngineStatus.off;

            if (engineStatus == EngineStatus.off) GameEvents.current.EngineOff(); 
            if (engineStatus == EngineStatus.on) GameEvents.current.EngineOn(); 
            if (engineStatus == EngineStatus.boost) GameEvents.current.EngineBoost(); 
        }
    }

    private bool CheckFuel()
    {
        if (fuelSystem.currentFuel > 0) return true;
        else return false;
    }

    private bool CheckCanToggleBoost()
    {
        if (shipControl.onFuelStation) return false;
        else return true;
    }

    private void OnTryPowerOn()
    {
        if (tryPowerOn) return;
        tryPowerOn = true;
    }    
    private void OnTryPowerOff()
    {
        if (!tryPowerOn) return;
        tryPowerOn = false;
    }
    private void OnTryToggleEngineMode()
    {
        if (tryToggleEngineMode) return;
        if (tryPowerOn) return;
        if (engineModeToggled) return;
        tryToggleEngineMode = true;
    }    
    private void OnTryToggleEngineMode_Idle()
    {
        if (!tryToggleEngineMode) return;
        tryToggleEngineMode = false;
        engineModeToggled = false;
    }

    private void Explode()
    {
        GameEvents.current.EngineOff();
        canControl = false;
    }

    private void SubscribeGameEvents()
    {
        GameEvents.current.onCrash += Explode;
        GameEvents.current.onThrust_Input += OnTryPowerOn;
        GameEvents.current.onThrust_Input_Idle += OnTryPowerOff;
        GameEvents.current.onUse_Input += OnTryToggleEngineMode;
        GameEvents.current.onUse_Input_Idle += OnTryToggleEngineMode_Idle;
    }

    private void OnDestroy()
    {
        GameEvents.current.onCrash -= Explode;
        GameEvents.current.onThrust_Input -= OnTryPowerOn;
        GameEvents.current.onThrust_Input_Idle -= OnTryPowerOff;
        GameEvents.current.onUse_Input -= OnTryToggleEngineMode;
        GameEvents.current.onUse_Input_Idle -= OnTryToggleEngineMode_Idle;
    }

}
