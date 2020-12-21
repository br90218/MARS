using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ControlsHub : MonoBehaviour
{
    public static ControlsHub Instance;
    [SerializeField] private RobotBehaviour robot;

    UnityEvent moveForwardEvent, moveBackwardEvent, turnRightEvent, turnLeftEvent;



    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void MoveForward()
    {
        robot.Gas();
    }

    public void MoveBackward()
    {
        robot.Reverse();
    }

    public void StopGasBrake()
    {
        robot.StopForwardInput();
    }

    public void TurnRight()
    {
        robot.Turn(-1);
    }

    public void TurnLeft()
    {
        robot.Turn(1);
    }

    public void StopTurning()
    {
        robot.StopTurningInput();
    }

    public void PickItem()
    {
        robot.PickUp(true);
    }

    public void DropItem()
    {
        robot.PickUp(false);
    }

    public void RotateCannonRight()
    {
        robot.TurnCannon(-1);
    }

    public void RotateCannonLeft()
    {
        robot.TurnCannon(1);
    }

    public void StopRotatingCannon()
    {
        robot.TurnCannon(0);
    }

    public void FireCannon()
    {
        robot.FireCannon(true);
    }
    public void StopFireCannon()
    {
        robot.FireCannon(false);
    }

    public void TurnOnBoost()
    {
        robot.BoostOn();
    }

    public void TurnOffBoost()
    {
        robot.BoostOff();
    }
}

public enum RobotComponents
{
    FORWARD, BACKWARD, RIGHT, LEFT, BAY, PICK, CANNONRIGHT, CANNONLEFT, FIRE, BOOST
}
