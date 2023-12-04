using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smoke : MonoBehaviour
{
    Rigidbody2D smoke_rb;

    private void Start()
    {
        smoke_rb = GetComponent<Rigidbody2D>();
        smoke_rb.AddForce(transform.up * -300f);
        StartCoroutine(DestroySmoke());
    }

    IEnumerator DestroySmoke()
    {
        yield return new WaitForSeconds(1.1f);
        Destroy(gameObject);
    }
}
