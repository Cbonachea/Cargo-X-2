using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Crash : MonoBehaviour
{

    //private Animator animator;
    private int ship_HP = 3;
    private Rigidbody2D ship_rb;
    [SerializeField] private float crashTolerance;
    [SerializeField] private float instantKillSpeed;
    float speed;
    private bool checkingCrash;

    private int damageTimer = 3;
    private int currentDamageTimer = 3;

    private ShipControl shipControl;


    void Start()
    {
        ship_rb = gameObject.GetComponent<Rigidbody2D>();
        shipControl = gameObject.GetComponent<ShipControl>();

        //animator = gameObject.GetComponent<Animator>();
    }


    void FixedUpdate()
    {
        speed = ship_rb.velocity.magnitude;
        if (speed < 1) speed = 0;
        //Debug.Log("Speed is "+ speed );
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        //if(speed < crashTolerance) FindObjectOfType<AudioManager>().Play("SoftCrash");
        if (speed > crashTolerance)
        {
            Debug.Log("Crashed");
            CheckCrash();
            //animator.SetBool("isCracked", true);
        }
        if(speed > instantKillSpeed)
        {
            ship_rb.angularDrag = 0f;
            ship_rb.AddTorque(150f);
            GameEvents.current.InstantExplode();
            Debug.Log("ALERT CATASTROPHIC DISASTER DETECTED");
        }

    }

    private void CheckCrash()
    {
        if (checkingCrash) return;
        checkingCrash = true;

        ship_HP--;
        
        if (ship_HP > 0)
        {
            Debug.Log("Crashed Hull Integrity = " + ship_HP);
            TakeDamage();
            return;
        }
        if (ship_HP < 1)
        {
            Explode();
        }
    }   
    private void Explode()
    {
        ship_rb.angularDrag = 0.1f;
        ship_rb.AddTorque(150f);
        GameEvents.current.Explode();
        Debug.Log("ALERT CATASTROPHIC DISASTER DETECTED");
    }

    private void TakeDamage()
    {
        FindObjectOfType<AudioManager>().Play("Crash");
        StartCoroutine(TakeDamageCoroutine());
    }
    private IEnumerator TakeDamageCoroutine()
    {
        while (currentDamageTimer > 0)
        {
            FindObjectOfType<AudioManager>().Stop("Thrust");
            FindObjectOfType<AudioManager>().Stop("Boost");
            shipControl.canControl = false;
            currentDamageTimer--;
            ship_rb.angularDrag = 0.1f;
            ship_rb.AddTorque(90f);
            //GameEvents.current.EngineOn();
            //GameEvents.current.Yaw_R();
            yield return new WaitForSeconds(.5f);
        }

        ship_rb.angularDrag = 4f;
        shipControl.canControl = true;
        currentDamageTimer = damageTimer;
        checkingCrash = false;
        yield break;
        
    }
}
