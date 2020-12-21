using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonRoundBehaviour : MonoBehaviour
{

    [SerializeField] private float speed;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        transform.position += speed * transform.up * Time.fixedDeltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Enemy"))
        {
            // Tell enemy to go eat shit
            other.GetComponent<EnemyBehaviour>().Kill();
            Destroy(this.gameObject);
        }
    }
}
