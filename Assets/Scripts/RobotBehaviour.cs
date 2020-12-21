using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RobotBehaviour : MonoBehaviour
{

    public bool IsFiringCannon {get; private set;}

    [SerializeField] private float maxSpeed;
    [SerializeField] private float accelConstant;
    [SerializeField] private float turnAngularSpeedConst;
    [SerializeField] private Animator animator;
    [SerializeField] private TextMeshProUGUI healthUI;

    [SerializeField] private Transform cannon;


    private Rigidbody2D rb;
    private Vector3 forward;
    private float angle;
    private float acceleration;
    private float turnAngularSpeed;
    private float cannonTurnAngularSpeed;
    private float speed;
    private float boostConstant;
    private Transform pickedObject;
    private Transform objectInRange;
    private bool isPickingItem;
    private float health = 100f;
    


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        forward = Vector3.up;
        speed = 0f;
        angle = 0f;
        boostConstant = 1f;
        isPickingItem = false;

    }

    private void Update()
    {
        animator.SetFloat("Speed", speed);
        animator.SetFloat("Angle", angle / 360);


        healthUI.text = "HEALTH = " + (int)health;

        // Drop Item if flag is not raised anymore

    }


    // Movement Goes Here
    private void FixedUpdate()
    {

        // adjust speed
        speed = Mathf.Lerp(Mathf.Clamp(speed + acceleration, -maxSpeed * boostConstant, maxSpeed * boostConstant), 0, Time.deltaTime) * boostConstant;

        // adjust orientation
        forward = Quaternion.AngleAxis(turnAngularSpeed, Vector3.forward) * forward;
        
        angle = (Vector3.SignedAngle(Vector3.up, forward, Vector3.forward) + 360) % 360;
        
        //angle = (((angle + turnAngularSpeed) + 360) % 360);

        rb.MovePosition(rb.position +  (Vector2) forward * speed * Time.fixedDeltaTime);
        //transform.position += forward * speed * Time.fixedDeltaTime;

        // cannon rotation adjustments
        cannon.RotateAround(cannon.position, Vector3.forward, cannonTurnAngularSpeed);
    }

    public void Gas()
    {
        acceleration = accelConstant;
    }

    public void Reverse()
    {
        acceleration = -accelConstant;
    }
 
    public void StopForwardInput()
    {
        acceleration = 0f;
    }

    public void Turn(int value)
    {
        turnAngularSpeed = value * turnAngularSpeedConst;
    }

    public void StopTurningInput()
    {
        turnAngularSpeed = 0f;
    }

    public void PickUp(bool value)
    {
        if (value)
        {
            if (pickedObject == null && objectInRange != null)
            {
                pickedObject = objectInRange;
                pickedObject.SetParent(transform); //TODO: can we make an anchor here?
                pickedObject.position = transform.position;
                pickedObject.GetComponent<ItemBehaviour>().IsPickedUp = true;
                objectInRange = null;
            }
        }
        else
        {
            if (pickedObject != null)
            {
                pickedObject.SetParent(null);
                pickedObject.GetComponent<ItemBehaviour>().IsPickedUp = false;
                pickedObject = null;
            }
        }
    }

    internal void TurnCannon(int value)
    {
        cannonTurnAngularSpeed = value * turnAngularSpeedConst;
    }

    internal void FireCannon(bool value)
    {
        IsFiringCannon = value;
    }

    internal void BoostOn()
    {
        boostConstant = 2f;
    }

    internal void BoostOff()
    {
        boostConstant = 1f;
    }

    internal void Damage()
    {
        health -= Time.deltaTime * 2f;
        if(health < 0f)
        {
            animator.SetTrigger("Death");
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Item"))
        {
            if(objectInRange == null) objectInRange = other.transform;
        }
    }
    
    private void OnTriggerExit2D(Collider2D other) {
        if(objectInRange = other.transform)
        {
            objectInRange = null;
        }
    }
}
