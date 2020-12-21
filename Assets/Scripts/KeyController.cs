using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KeyController : MonoBehaviour
{
    public string Key;

    [SerializeField] private Animator animator;

    public float MaximumBatteryLife = 10f;
    private bool isInUse;
    private bool isPressed;
    private float batteryLife = 60f;
    private UnityEvent keyEvent;
    private UnityEvent keyExitEvent;



    // Start is called before the first frame update
    void Start()
    {
        if (keyEvent == null) keyEvent = new UnityEvent();
        if (keyExitEvent == null) keyExitEvent = new UnityEvent();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(Key))
        {
            keyEvent.Invoke();
            isPressed = true;
        }

        if (Input.GetKeyUp(Key))
        {
            keyExitEvent.Invoke();
            isPressed = false;
        }

        if (isInUse)
        {
            if (batteryLife < 0f && isPressed)
            {
                keyExitEvent.Invoke();
                isPressed = false;
            }
            else if (isPressed)
            {
                batteryLife -= Time.deltaTime;
            }
        }


        animator.SetFloat("Battery Life", batteryLife / 60f);

    }

    public void SubscribeToComponent(RobotComponents component)
    {
        keyEvent.RemoveAllListeners();
        keyExitEvent.RemoveAllListeners();
        switch (component)
        {
            case RobotComponents.FORWARD:
                keyEvent.AddListener(ControlsHub.Instance.MoveForward);
                keyExitEvent.AddListener(ControlsHub.Instance.StopGasBrake);
                isInUse = true;
                break;
            case RobotComponents.BACKWARD:
                keyEvent.AddListener(ControlsHub.Instance.MoveBackward);
                keyExitEvent.AddListener(ControlsHub.Instance.StopGasBrake);
                isInUse = true;
                break;
            case RobotComponents.LEFT:
                keyEvent.AddListener(ControlsHub.Instance.TurnLeft);
                keyExitEvent.AddListener(ControlsHub.Instance.StopTurning);
                isInUse = true;
                break;
            case RobotComponents.RIGHT:
                keyEvent.AddListener(ControlsHub.Instance.TurnRight);
                keyExitEvent.AddListener(ControlsHub.Instance.StopTurning);
                isInUse = true;
                break;
            case RobotComponents.PICK:
                keyEvent.AddListener(ControlsHub.Instance.PickItem);
                keyExitEvent.AddListener(ControlsHub.Instance.DropItem);
                isInUse = true;
                break;
            case RobotComponents.CANNONLEFT:
                keyEvent.AddListener(ControlsHub.Instance.RotateCannonLeft);
                keyExitEvent.AddListener(ControlsHub.Instance.StopRotatingCannon);
                isInUse = true;
                break;
            case RobotComponents.CANNONRIGHT:
                keyEvent.AddListener(ControlsHub.Instance.RotateCannonRight);
                keyExitEvent.AddListener(ControlsHub.Instance.StopRotatingCannon);
                isInUse = true;
                break;
            case RobotComponents.FIRE:
                keyEvent.AddListener(ControlsHub.Instance.FireCannon);
                keyExitEvent.AddListener(ControlsHub.Instance.StopFireCannon);
                isInUse = true;
                break;
            case RobotComponents.BOOST:
                keyEvent.AddListener(ControlsHub.Instance.TurnOnBoost);
                keyExitEvent.AddListener(ControlsHub.Instance.TurnOffBoost);
                isInUse = true;
                break;
            case RobotComponents.BAY:
                UnSubscribe();
                break;
        }
    }

    public void UnSubscribe()
    {
        keyEvent.RemoveAllListeners();
        keyExitEvent.RemoveAllListeners();
        isInUse = false;
    }
}
