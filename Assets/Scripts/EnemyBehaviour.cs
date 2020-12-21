using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{

    [SerializeField] private Animator animator;
    [SerializeField] private float wanderingRadius = 1f;
    [SerializeField] private float wanderingSpeed = 1f;
    [SerializeField] private float pursuingSpeed = 2f;

    private bool isChasing;
    private Transform player;
    private Transform chasingItem;
    private Transform pickedUpItem;
    private Vector3 destination;
    private Coroutine idleRoutine;
    private Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        GetComponentInChildren<EnemyBehaviour>().enabled = true;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null || chasingItem != null)
        {
            if (chasingItem && chasingItem.GetComponent<ItemBehaviour>().IsPickedUp)
            {
                chasingItem = null;
                isChasing = false;
            }
            else
            {
                isChasing = true;
            }


        }
        else
        {
            isChasing = false;
        }

        if (Vector3.Distance(transform.position, destination) < 0.05f)
        {
            if (chasingItem != null)
            {
                pickedUpItem = chasingItem;
                pickedUpItem.SetParent(transform);
                pickedUpItem.localPosition = new Vector3(0f, -0.3f, 0f);
                pickedUpItem.GetComponent<ItemBehaviour>().IsPickedUp = true;
                chasingItem = null;
            }
        }

    }

    private void FixedUpdate()
    {
        if (isChasing)
        {
            if (idleRoutine != null)
            {
                StopCoroutine(idleRoutine);
                idleRoutine = null;
            }
            if (player != null) destination = player.position;
            else if (chasingItem != null) destination = chasingItem.position;
            rb.MovePosition(rb.position + (Vector2)((destination-transform.position).normalized * Time.fixedDeltaTime * pursuingSpeed));
            //transform.position = Vector3.MoveTowards(transform.position, destination, Time.fixedDeltaTime * pursuingSpeed);
        }
        else
        {
            if (idleRoutine == null)
            {
                idleRoutine = StartCoroutine(IdleRoutine());
            }
        }

    }

    private IEnumerator IdleRoutine()
    {
        yield return new WaitForSeconds(Random.Range(2f, 4f));
        while (true)
        {
            DecideDestination();
            while (Vector3.Distance(transform.position, destination) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, destination, Time.fixedDeltaTime * wanderingSpeed);
                yield return new WaitForFixedUpdate();
            }
            yield return new WaitForSeconds(Random.Range(2f, 4f));
        }
    }

    private void OnCollisionStay2D(Collision2D other) {
        if(other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponentInParent<RobotBehaviour>().Damage();
        }
    }


    private void DecideDestination()
    {
        var angle = Random.Range(0, Mathf.PI * 2);
        destination = transform.position + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0f) * wanderingRadius;
    }

    internal void SetPlayer(Transform value)
    {
        if (pickedUpItem == null) player = value;
    }

    internal void UnsetPlayer()
    {
        player = null;
    }

    internal void SetItem(Transform value)
    {
        chasingItem = value;
    }

    internal void UnsetItem()
    {
        chasingItem = null;
    }



    internal void Kill()
    {
        animator.SetTrigger("Death");
    }

    private void DestroyMe()
    {
        if(pickedUpItem)
        {
            pickedUpItem.GetComponent<ItemBehaviour>().IsPickedUp = false;
            pickedUpItem.SetParent(null);
        }
        
        GameManager.Instance.DeductMob();
        Destroy(this.gameObject);
    }
}
