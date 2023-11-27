using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;

    private void Awake()
    {
        current = this;
    }

    public event Action onUse_Input;
    public event Action onUse_Input_Idle;
    public event Action onThrust_Input;
    public event Action onThrust_Input_Idle;
    public event Action onBoost_Input;
    public event Action onBoost_Input_Idle;
    public event Action onYaw_L_Input;
    public event Action onYaw_L_Input_Idle;
    public event Action onYaw_R_Input;
    public event Action onYaw_R_Input_Idle;
    public event Action onLeftClick_Input;
    public event Action onLeftClick_Input_Idle;
    public event Action onTakeDamage;
    public event Action onCrash;
    public event Action onExplode;
    public event Action onInstantExplode;
    public event Action onDeploy_Input;
    public event Action onDeploy_Input_Idle;

    public event Action onEngineOn;
    public event Action onEngineOff;
    public event Action onEngineBoost;

    public event Action onCargoLoad;
    public event Action onCargoDrop;
    public event Action onDeliveryComplete;
    public event Action onDeliveryFailed;

    public event Action onFuelFill;
    public event Action onFuelFill_Idle;
   
      
    public void DeliveryComplete()
    {
        if (onDeliveryComplete != null)
        {
            onDeliveryComplete();
        }
    }    
    public void FuelFill()
    {
        if (onFuelFill != null)
        {
            onFuelFill();
        }
    }      
    public void FuelFill_Idle()
    {
        if (onFuelFill_Idle != null)
        {
            onFuelFill_Idle();
        }
    }     
    public void DeliveryFailed()
    {
        if (onDeliveryFailed != null)
        {
            onDeliveryFailed();
        }
    }      
    public void CargoLoad()
    {
        if (onCargoLoad != null)
        {
            onCargoLoad();
        }
    }       
    public void CargoDrop()
    {
        if (onCargoDrop != null)
        {
            onCargoDrop();
        }
    }       
    public void Use()
    {
        if (onUse_Input != null)
        {
            onUse_Input();
        }
    }       
    public void Use_Idle()
    {
        if (onUse_Input_Idle != null)
        {
            onUse_Input_Idle();
        }
    }      
    public void Yaw_L()
    {
        if (onYaw_L_Input != null)
        {
            onYaw_L_Input();
        }
    }       
    public void Yaw_L_Idle()
    {
        if (onYaw_L_Input_Idle != null)
        {
            onYaw_L_Input_Idle();
        }
    }         
    public void Yaw_R()
    {
        if (onYaw_R_Input != null)
        {
            onYaw_R_Input();
        }
    }       
    public void Yaw_R_Idle()
    {
        if (onYaw_R_Input_Idle != null)
        {
            onYaw_R_Input_Idle();
        }
    }        
    public void LeftClick_Input()
    {
        if (onLeftClick_Input != null)
        {
            onLeftClick_Input();
        }
    }    
    public void LeftClick_Input_Idle()
    {
        if (onLeftClick_Input_Idle != null)
        {
            onLeftClick_Input_Idle();
        }
    }
    public void TakeDamage()
    {
        if (onTakeDamage != null)
        {
            onTakeDamage();
        }
    }     
    public void Crash()
    {
        if (onCrash != null)
        {
            onCrash();
        }
    }      
    public void Explode()
    {
        if (onExplode != null)
        {
            onExplode();
        }
    }        
    public void InstantExplode()
    {
        if (onInstantExplode != null)
        {
            onInstantExplode();
        }
    }        
    public void Thrust()
    {
        if (onThrust_Input != null)
        {
            onThrust_Input();
        }
    }       
    public void Thrust_Idle()
    {
        if (onThrust_Input_Idle != null)
        {
            onThrust_Input_Idle();
        }
    }    
    public void Boost()
    {
        if (onBoost_Input != null)
        {
            onBoost_Input();
        }
    }
    public void Boost_Idle()
    {
        if (onBoost_Input_Idle != null)
        {
            onBoost_Input_Idle();
        }
    }      
    public void Deploy()
    {
        if (onDeploy_Input != null)
        {
            onDeploy_Input();
        }
    }
    public void Deploy_Idle()
    {
        if (onDeploy_Input_Idle != null)
        {
            onDeploy_Input_Idle();
        }
    }
    public void EngineOn()
    {
        if (onEngineOn != null)
        {
            onEngineOn();
        }
    }    
    public void EngineOff()
    {
        if (onEngineOff != null)
        {
            onEngineOff();
        }
    }    
    public void EngineBoost()
    {
        if (onEngineBoost != null)
        {
            onEngineBoost();
        }
    }

}