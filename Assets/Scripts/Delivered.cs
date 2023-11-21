using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delivered : MonoBehaviour
{
    private int capsuleHP = 1;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "deliveryTarget")
        {
            Destroy(gameObject);
            GameEvents.current.DeliveryComplete();
        }
        else if(capsuleHP > 0)
        {
            capsuleHP--;
        }
        else if(capsuleHP == 0)
        {
            Destroy(gameObject);
            GameEvents.current.DeliveryFailed();
        }
    }

}
