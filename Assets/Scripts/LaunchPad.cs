using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchPad : MonoBehaviour
{
    private float startPosition;
    private float endPosition;

    private Transform launchPad;

    private void Start()
    {
        startPosition = 2f;
        endPosition = 7.15f;
        SubscribeGameEvents();
        launchPad = GetComponent<Transform>();
    }


    private void StartUp()
    {
        launchPad.Translate(launchPad.position.x,endPosition,launchPad.position.z);
    }

    private void SubscribeGameEvents()
    {

    }
    private void OnDestroy()
    {
        
    }
}
