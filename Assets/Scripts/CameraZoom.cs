using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] private CinemachineBrain brain;
    [SerializeField] private CinemachineVirtualCamera vCam;
    [SerializeField] private Rigidbody2D ship_rb;


    [SerializeField] private float speed;
    [SerializeField] private float upperBound = 20f;
    [SerializeField] private float lowerBound = 3f;

    private void Update()
    {
        speed = Mathf.Abs(ship_rb.velocity.y);
    }

    private void FixedUpdate()
    {
        if (speed >= upperBound)
        {
            speed = upperBound;
        }
        if (speed <= lowerBound)
        {
            speed = lowerBound;
        }
        SetOrthoSize();
    }

    private void SetOrthoSize()
    {
        vCam.m_Lens.OrthographicSize = speed;
        //Debug.Log("OrthoSize Set");
    }
}
