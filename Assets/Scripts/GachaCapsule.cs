using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GachaCapsule : MonoBehaviour
{

    private Rigidbody2D gachaCapsule_rb;

    private void Start()
    {
        gachaCapsule_rb = GetComponent<Rigidbody2D>();
        gachaCapsule_rb.AddForce(transform.up * -30f);
    }

}
