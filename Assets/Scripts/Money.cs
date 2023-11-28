using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Money : MonoBehaviour
{
    [SerializeField] private Text moneyUI;

    [SerializeField] internal float currentMoney = 1.25f;
    
    private float payout;
    internal float fuelPrice;
    private bool buyingFuel;
    internal bool canBuyFuel = true;

    private void Start()
    {
        SubscribeGameEvents();
        fuelPrice = .45f;
    }

    private void Update()
    {
        if (currentMoney < fuelPrice)
        {
            canBuyFuel = false;
            GameEvents.current.FuelFill_Idle();
        }

        if (currentMoney >= fuelPrice) canBuyFuel = true;

        currentMoney = Mathf.Round(currentMoney * 100f) / 100f;
        moneyUI.text = ("$" + currentMoney.ToString());
    }

    internal void Payout()
    {
        payout = Random.Range(6.00f, 13.00f);
        currentMoney += payout;
    }

    internal void StartBuyFuel()
    {
        if (buyingFuel && canBuyFuel) return;
        buyingFuel = true;
        StartCoroutine(BuyFuel());
    }

    internal void StopBuyFuel()
    {
        if (!buyingFuel) return;
        buyingFuel = false;
        StopCoroutine(BuyFuel());
    }

    IEnumerator BuyFuel()
    {
        while (buyingFuel && currentMoney >= fuelPrice)
        {
            currentMoney -= fuelPrice;
            yield return new WaitForSeconds(.009f);
        }
    }

    private void SubscribeGameEvents()
    {
        GameEvents.current.onDeliveryComplete += Payout;
        GameEvents.current.onFuelFill += StartBuyFuel;
        GameEvents.current.onFuelFill_Idle += StopBuyFuel;
        GameEvents.current.onUse_Input_Idle += StopBuyFuel;

    }
    private void OnDestroy()
    {
        GameEvents.current.onDeliveryComplete -= Payout;
        GameEvents.current.onFuelFill -= StartBuyFuel;
        GameEvents.current.onFuelFill_Idle -= StopBuyFuel;
        GameEvents.current.onUse_Input_Idle -= StopBuyFuel;
    }
}
