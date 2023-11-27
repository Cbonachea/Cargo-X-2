using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeGeneratorScript : MonoBehaviour
{

    [SerializeField] GameObject smokePrefab;

    Transform smokeLocation;
    bool generatingSmoke;

    void Start()
    {
        SubscribeGameEvents();
        smokeLocation = GetComponent<Transform>();
    }

    void StartGenerateSmoke()
    {
        if (generatingSmoke) return;
        generatingSmoke = true;
        StartCoroutine(GenerateSmokeCoroutine());
    }
    void StopGenerateSmoke()
    {
        if (!generatingSmoke) return;
        generatingSmoke = false;
        StopCoroutine(GenerateSmokeCoroutine());
    }

    IEnumerator GenerateSmokeCoroutine()
    {
        while (generatingSmoke)
        {
            var newCargo = Instantiate(smokePrefab, smokeLocation);
            newCargo.transform.parent = null;
            newCargo.transform.Rotate(0, 0, Random.Range(-30, 30));
            yield return new WaitForSeconds(.08f);
        }
    }

    void SubscribeGameEvents()
    {
        GameEvents.current.onEngineOn += StartGenerateSmoke;
        GameEvents.current.onEngineOff += StopGenerateSmoke;
    }

    private void OnDestroy()
    {
        GameEvents.current.onEngineOn -= StartGenerateSmoke;
        GameEvents.current.onEngineOff -= StopGenerateSmoke;
    }
}
