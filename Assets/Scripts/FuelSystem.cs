using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelSystem : MonoBehaviour
{

    [SerializeField] private Slider slider;

    internal float maxFuel = 100f;
    [SerializeField] internal float currentFuel;
    [SerializeField] private float fuelDrainRate;
    private float defaultDrainRate;
    private float boostDrainRate;
    private float fuelFillRate;

    [SerializeField] private bool fuelDrainStarted;
    [SerializeField] private bool fuelFillStarted;

    private Money money;

    private void Start()
    {
        currentFuel = 85f;
        defaultDrainRate = .09f;
        boostDrainRate = .11f;
        fuelFillRate = .4f;
        money = GetComponent<Money>();
        SubscribeGameEvents();
        SetFuel(currentFuel);
    }

    private void SetFuel(float currentFuel)
    {
        slider.value = currentFuel;
    }

    private void StartBoost()
    {
        if (fuelDrainStarted) return;
        StartFuelDrain(boostDrainRate);
    }

    private void StartEngine()
    {
        if (fuelDrainStarted) return;
        StartFuelDrain(defaultDrainRate);
    }

    private void StartFuelDrain(float fuelDrainRate)
    {
        fuelDrainStarted = true;
        StartCoroutine(FuelDrainCoroutine(fuelDrainRate));
        Debug.Log("Fuel drain started");
    }
    private void StopFuelDrain()
    {
        if (!fuelDrainStarted) return;
        fuelDrainStarted = false;
        StopCoroutine(FuelDrainCoroutine(fuelDrainRate));
        Debug.Log("Fuel drain stopped");
    }

    private void StartFuelFill()
    {
        if (fuelFillStarted) return;
        fuelFillStarted = true;
        StartCoroutine(FuelFillCoroutine());
        Debug.Log("Fuel fill started");
    }    
    private void StopFuelFill()
    {
        if (!fuelFillStarted) return;
        fuelFillStarted = false;
        StopCoroutine(FuelFillCoroutine());
        Debug.Log("Fuel fill stopped");
    }


    IEnumerator FuelDrainCoroutine(float fuelDrainRate)
    {
        while (currentFuel > 0 && fuelDrainStarted) 
        {
            currentFuel -= fuelDrainRate;
            SetFuel(currentFuel);
            yield return new WaitForSeconds(.1f);
        }
    }

    IEnumerator FuelFillCoroutine()
    {
        while (currentFuel < 100 && money.canBuyFuel && fuelFillStarted)
        {
            currentFuel += fuelFillRate;
            SetFuel(currentFuel);
            yield return new WaitForSeconds(.01f);
        }
    }

    private void SubscribeGameEvents()
    {
        GameEvents.current.onEngineOn += StartEngine;
        GameEvents.current.onEngineBoost += StartBoost;
        GameEvents.current.onEngineOff += StopFuelDrain;        
        GameEvents.current.onFuelFill += StartFuelFill;
        GameEvents.current.onFuelFill_Idle += StopFuelFill;
    }

    private void OnDestroy()
    {
        GameEvents.current.onEngineOn -= StartEngine;
        GameEvents.current.onEngineBoost -= StartBoost;
        GameEvents.current.onEngineOff -= StopFuelDrain;
        GameEvents.current.onFuelFill -= StartFuelFill; 
        GameEvents.current.onFuelFill_Idle -= StopFuelFill;
    }
}
