using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GachaCapsule : MonoBehaviour
{
    private Rigidbody2D gachaCapsule_rb;

    private void Start()
    {
        GameEvents.current.onCargoDrop += Release;
        gachaCapsule_rb = GetComponent<Rigidbody2D>();
    }

    private void Release()
    {
        gachaCapsule_rb.AddForce(transform.up * -300f);
        gachaCapsule_rb.AddTorque(90f);
    }

    private void OnDestroy()
    {
        GameEvents.current.onCargoDrop -= Release;
    }
}
