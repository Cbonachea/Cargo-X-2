using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetDeliveryTarget : MonoBehaviour
{

    [SerializeField] internal GameObject[] residences;
    [SerializeField] internal GameObject deliveryTarget;
    [SerializeField] internal bool targetSet;
    [SerializeField] internal bool canSetTarget = true;
    internal int index;

    private void Start()
    {
        SubscribeGameEvents();
        residences = GameObject.FindGameObjectsWithTag("Residence");
    }

    internal void TrySetTarget()
    {
        if (canSetTarget)
        {
            canSetTarget = false;
            SetTarget();
        }
    }

    private void SetTarget()
    {
        if (targetSet) return;
        targetSet = true;
        index = Random.Range(0, residences.Length);
        deliveryTarget = residences[index];
        deliveryTarget.tag = "deliveryTarget";
        Debug.Log("Residence Target index " + index);
        Debug.Log("Delivery Target Set");
        //GameEvents.current.CargoLoad();
    }

    private void UnSetTarget()
    {
        deliveryTarget.tag = "Residence";
        targetSet = false;
        canSetTarget = true;
    }

    private void SubscribeGameEvents()
    {
        //GameEvents.current.onCargoLoad += TrySetTarget;
        GameEvents.current.onDeliveryComplete += UnSetTarget;
        GameEvents.current.onDeliveryFailed += UnSetTarget;
    }

    private void OnDestroy()
    {
        //GameEvents.current.onCargoLoad -= TrySetTarget;
        GameEvents.current.onDeliveryComplete -= UnSetTarget;
        GameEvents.current.onDeliveryFailed -= UnSetTarget;
    }

}