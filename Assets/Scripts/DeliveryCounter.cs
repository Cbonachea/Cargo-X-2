using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DeliveryCounter : MonoBehaviour
{

    [SerializeField] Text moneyUI;
    int deliveryCount;

    private void Start()
    {
        GameEvents.current.onDeliveryComplete += IncrementDeliveryCounter;
        deliveryCount = 0;
        moneyUI.text = deliveryCount.ToString();
    }
    void IncrementDeliveryCounter()
    {
        deliveryCount++;
        moneyUI.text = deliveryCount.ToString();
    }


    private void OnDestroy()
    {
        GameEvents.current.onDeliveryComplete -= IncrementDeliveryCounter;

    }
}
