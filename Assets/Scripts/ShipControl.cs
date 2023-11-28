using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]

public class ShipControl : MonoBehaviour
{
    private SetDeliveryTarget setDeliveryTarget;
    private FuelSystem fuelSystem;
    private Money money;

    internal bool canControl = true;
    [SerializeField] private bool canLoadCargo = true;
    [SerializeField] private bool landedOnRestaurant = false;
    [SerializeField] private bool hasCargo = false;

    private Rigidbody2D ship_rb;
    private Transform ship_Transform;
    [SerializeField] private GameObject cargo;
    private Transform cargo_Transform;
    private Rigidbody2D cargo_rb;
    private Transform cargoDropLocation;

    private bool thrusting;
    private bool boosting;
    private bool yaw_L;
    private bool yaw_R;
    private bool deploy;

    private bool canPumpFuel;
    private bool pumpingFuel;
    private bool fuelTankFull;
    private bool fuelTankEmpty;
    private bool canAffordFuel;
    [SerializeField] internal bool onFuelStation = false;

    [SerializeField] private float thrust = 40f;
    [SerializeField] private float boost = 30f;
    [SerializeField] private float torque = 40f;
    [SerializeField] private float angularDrag = 0f;

    //private AreaEffector2D rocketBlast;

    private void Start()
    {
        setDeliveryTarget = GetComponent<SetDeliveryTarget>();
        fuelSystem = GetComponent<FuelSystem>();
        money = GetComponent<Money>();
        ship_Transform = GetComponent<Transform>();
        ship_rb = GetComponent<Rigidbody2D>();
        cargoDropLocation = GetComponent<Transform>();
        //rocketBlast = GetComponentInChildren<AreaEffector2D>();
        SubscribeGameEvents();
    }

    private void Update()
    {
        if (!canControl)
        {
            Thrust_Idle();
            return;
        }

        CheckFuelTankLevel();
        if (landedOnRestaurant && canLoadCargo) LoadCargo();
        if (onFuelStation) CheckCashFlow();
        if (canAffordFuel && !fuelTankFull && onFuelStation) canPumpFuel = true;
        if (!canAffordFuel || fuelTankFull || !onFuelStation)
        {
            canPumpFuel = false;
            GameEvents.current.FuelFill_Idle();
        }
        //if (canPumpFuel) fuelToolTip.enabled = true;
        //if (!canPumpFuel) fuelToolTip.enabled = false;
    }

    private void CheckFuelTankLevel()
    {
        if (fuelSystem.currentFuel <= 0) fuelTankEmpty = true;
        if (fuelSystem.currentFuel >= 1) fuelTankEmpty = false;
        if (fuelSystem.currentFuel >= (fuelSystem.maxFuel) - 1) fuelTankFull = true;
        if (fuelSystem.currentFuel < (fuelSystem.maxFuel) - 1) fuelTankFull = false;
    }

    private void CheckCashFlow()
    {
        if (money.currentMoney >= money.fuelPrice) canAffordFuel = true;
        else canAffordFuel = false;
    }

    private void CheckPump()
    {
        if(canPumpFuel) GameEvents.current.FuelFill();
    }

    private void CheckPump_Idle()
    {
        GameEvents.current.FuelFill_Idle();
    }

    void FixedUpdate()
    {
        if (!canControl) return;
        {
            if (thrusting)
            {
                ship_rb.AddForce(transform.up * thrust);
                //rocketBlast.enabled = true;
                FindObjectOfType<AudioManager>().Play("Thrust");
            }

            if (!thrusting)
            {
                FindObjectOfType<AudioManager>().Stop("Thrust");
                //rocketBlast.enabled = false;
            }

            if (boosting)
            {
                ship_rb.AddForce(transform.up * (thrust + boost));
                //rocketBlast.enabled = true;
                FindObjectOfType<AudioManager>().Play("Boost");
            }

            if (!boosting)
            {
                //rocketBlast.enabled = false;
                FindObjectOfType<AudioManager>().Stop("Boost");
            }

            if (yaw_R == true) { ship_rb.AddTorque(-torque * Mathf.Deg2Rad, ForceMode2D.Impulse); }

            if (yaw_L == true) { ship_rb.AddTorque(torque * Mathf.Deg2Rad, ForceMode2D.Impulse); }

            if (deploy == true && hasCargo == true && !landedOnRestaurant)
            {
                cargo_rb.isKinematic = false;
                cargo_rb.velocity = ship_rb.velocity;
                hasCargo = false;
                canLoadCargo = true;
                GameEvents.current.CargoDrop();
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Restaurant" && canControl)
        {
            landedOnRestaurant = true;
            //LoadCargo();
        }
        if (collision.gameObject.tag == "FuelStation" && canControl)
        {
            onFuelStation = true;
            Debug.Log("On fuel station");
        }

    }    
    void OnCollisionExit2D(Collision2D collision)
    {
        landedOnRestaurant = false;
        onFuelStation = false;
    }

    void LoadCargo()
    {
        canLoadCargo = false;

        if (!hasCargo)
        {
            CreateCargo();
            Debug.Log("Collided with Restaurant");
            //GameEvents.current.CargoLoad();
        }
    }

    void CreateCargo()
    {
        hasCargo = true;
        var newCargo = Instantiate(cargo, cargoDropLocation.position, cargoDropLocation.rotation);
        cargo_Transform = newCargo.GetComponent<Transform>();
        cargo_rb = newCargo.GetComponent<Rigidbody2D>();
        cargo_Transform.parent = ship_Transform;
        setDeliveryTarget.TrySetTarget();
    }

    private void Explode()
    {
        canControl = false;
        StartCoroutine(ExplosionDelayCoroutine());
        FindObjectOfType<AudioManager>().Stop("Thrust");
        FindObjectOfType<AudioManager>().Stop("Boost");
    }    
    private void InstantExplode()
    {
        canControl = false;
        FindObjectOfType<AudioManager>().Stop("Thrust");
        FindObjectOfType<AudioManager>().Stop("Boost");
        FindObjectOfType<AudioManager>().Play("Explode");
        FindObjectOfType<AudioManager>().Play("Crash");
        Destroy(gameObject);
    }

    IEnumerator ExplosionDelayCoroutine()
    {
        FindObjectOfType<AudioManager>().Play("Crash");
        yield return new WaitForSeconds(2.5f);
        FindObjectOfType<AudioManager>().Play("Explode");
        FindObjectOfType<AudioManager>().Play("Crash");
        Destroy(gameObject);
    }


    private void Thrust()
    {
        thrusting = true;
        boosting = false;
    }        
    private void Boost()
    {
        boosting = true;
        thrusting = false;
    }    
    private void Thrust_Idle()
    {
        thrusting = false;
        boosting = false;
    }    
    private void OnYaw_L()
    {
        yaw_L = true;
    }    
    private void OnYaw_L_Idle()
    {
        yaw_L = false;
    }    
    private void OnYaw_R()
    {
        yaw_R = true;
    }    
    private void OnYaw_R_Idle()
    {
        yaw_R = false;
    }    
    private void OnDeploy()
    {
        deploy = true;
    }    
    private void OnDeploy_Idle()
    {
        deploy = false;
    }

    private void SubscribeGameEvents()
    {
        //GameEvents.current.onCargoLoad += CreateCargo;
        GameEvents.current.onExplode += Explode;
        GameEvents.current.onInstantExplode += InstantExplode;
        GameEvents.current.onEngineOn += Thrust;
        GameEvents.current.onEngineBoost += Boost;     
        GameEvents.current.onEngineOff += Thrust_Idle;        
        GameEvents.current.onYaw_L_Input += OnYaw_L;
        GameEvents.current.onYaw_L_Input_Idle += OnYaw_L_Idle;        
        GameEvents.current.onYaw_R_Input += OnYaw_R;
        GameEvents.current.onYaw_R_Input_Idle += OnYaw_R_Idle;        
        GameEvents.current.onDeploy_Input += OnDeploy;
        GameEvents.current.onDeploy_Input_Idle += OnDeploy_Idle;

        GameEvents.current.onUse_Input += CheckPump;
        GameEvents.current.onUse_Input_Idle += CheckPump_Idle;
    }

    private void OnDestroy()
    {
        //GameEvents.current.onCargoLoad -= CreateCargo;
        GameEvents.current.onExplode -= Explode;
        GameEvents.current.onInstantExplode -= InstantExplode;
        GameEvents.current.onEngineOn -= Thrust;        
        GameEvents.current.onEngineBoost -= Boost;
        GameEvents.current.onEngineOff -= Thrust_Idle;
        GameEvents.current.onYaw_L_Input -= OnYaw_L;
        GameEvents.current.onYaw_L_Input_Idle -= OnYaw_L_Idle;
        GameEvents.current.onYaw_R_Input -= OnYaw_R;
        GameEvents.current.onYaw_R_Input_Idle -= OnYaw_R_Idle;
        GameEvents.current.onDeploy_Input -= OnDeploy;
        GameEvents.current.onDeploy_Input_Idle -= OnDeploy_Idle;

        GameEvents.current.onUse_Input -= CheckPump;
        GameEvents.current.onUse_Input_Idle -= CheckPump_Idle;

    }

}
