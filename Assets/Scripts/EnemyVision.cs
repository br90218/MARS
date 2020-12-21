using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    private EnemyBehaviour parent;
    
    // Start is called before the first frame update
    void Start()
    {
        parent = GetComponentInParent<EnemyBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(!parent) return;
        if(other.CompareTag("Player"))
        {
            parent.SetPlayer(other.transform);
        }
        else if(other.CompareTag("Item"))
        {
            if(!other.GetComponent<ItemBehaviour>().IsPickedUp)
            {
                parent.SetItem(other.transform);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("Player"))
        {
            parent.UnsetPlayer();
        }
    }
}
