using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropZoneBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D other) {
        if(other.CompareTag("Item"))
        {
            print("hi");
            if(!other.GetComponent<ItemBehaviour>().IsPickedUp)
            {
                Destroy(other.gameObject);
                GameManager.Instance.CollectPart();
            }
        }
    }
}
