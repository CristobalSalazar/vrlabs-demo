using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] private float lookSensitivity = 1;
    [SerializeField] private Transform playerBody;
    private float _xAxisClamp;
    private bool _canLook;


    void Start()
    {
        LockCursor();
        SimulationEvents.On(EventType.SimulationStarted, AllowLook);
        SimulationEvents.On(EventType.SimulationFailed, ProhibitLook);
        SimulationEvents.On(EventType.SimulationSuccess, ProhibitLook);
    }

    void OnDestroy()
    {
        SimulationEvents.Unsubscribe(EventType.SimulationStarted, AllowLook);
        SimulationEvents.Unsubscribe(EventType.SimulationFailed, ProhibitLook);
        SimulationEvents.Unsubscribe(EventType.SimulationSuccess, ProhibitLook);
    }

    void Update()
    {
        if (_canLook)
        {
            CameraRotation();
        }
    }

    private void AllowLook()
    {
        _canLook = true;
    }

    private void ProhibitLook()
    {
        _canLook = false;
    }

    private void CameraRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * lookSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * lookSensitivity * Time.deltaTime;

        _xAxisClamp += mouseY;
        if (_xAxisClamp > 90.0f)
        {
            _xAxisClamp = 90.0f;
            mouseY = 0.0f;
            ClampXAxisToValue(270f);
        }
        else if (_xAxisClamp < -90.0f)
        {
            _xAxisClamp = -90.0f;
            mouseY = 0.0f;
            ClampXAxisToValue(90f);
        }

        Vector3 rotationVector = Vector3.left * mouseY;
        transform.Rotate(rotationVector);
        playerBody.Rotate(Vector3.up * mouseX);
    }


    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.lockState = CursorLockMode.None;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void ClampXAxisToValue(float value)
    {
        Vector3 eulerRotation = transform.eulerAngles;
        eulerRotation.x = value;
        transform.eulerAngles = eulerRotation;
    }

}
