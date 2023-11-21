using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetIndicator : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject targetIndicator;


    void Start()
    {
        SubscribeGameEvents();
    }

    private void Update()
    {
        if (targetIndicator.tag == "deliveryTarget") ActivateIndicator();
        if (targetIndicator.tag == "Residence") DeactivateIndicator();
    }

    private void ActivateIndicator()
    {
        spriteRenderer.enabled = true;
    }

    private void DeactivateIndicator()
    {
        spriteRenderer.enabled = false;
    }


    private void SubscribeGameEvents()
    {
        //GameEvents.current.onCargoLoad += ActivateIndicator;
        GameEvents.current.onDeliveryComplete += DeactivateIndicator;
        GameEvents.current.onDeliveryFailed += DeactivateIndicator;
    }

    private void OnDestroy()
    {
        //GameEvents.current.onCargoLoad -= ActivateIndicator;
        GameEvents.current.onDeliveryComplete -= DeactivateIndicator;
        GameEvents.current.onDeliveryFailed -= DeactivateIndicator;
    }
}
