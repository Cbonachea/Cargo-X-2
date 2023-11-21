using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerInput : MonoBehaviour
{

    private bool thrustInput;
    private bool yaw_L_Input;
    private bool yaw_R_Input;
    private bool useInput;
    private bool deploy;

    private void Update()
    {
        thrustInput = Input.GetButton("Vertical");
        yaw_L_Input = Input.GetButton("yaw_L");
        yaw_R_Input = Input.GetButton("yaw_R");
        useInput = Input.GetButton("use");
        deploy = Input.GetButton("deploy");

        if (thrustInput) GameEvents.current.Thrust();
        if (!thrustInput) GameEvents.current.Thrust_Idle();
        if (yaw_L_Input) GameEvents.current.Yaw_L();
        if (!yaw_L_Input) GameEvents.current.Yaw_L_Idle();
        if (yaw_R_Input) GameEvents.current.Yaw_R();
        if (!yaw_R_Input) GameEvents.current.Yaw_R_Idle();
        if (useInput) GameEvents.current.Use();
        if (!useInput) GameEvents.current.Use_Idle();
        if (deploy) GameEvents.current.Deploy();
        if (!deploy) GameEvents.current.Deploy_Idle();

    }
}