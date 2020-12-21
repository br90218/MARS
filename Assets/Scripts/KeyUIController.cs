using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class KeyUIController : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    public static GameObject DraggedKey;
    Vector3 startPosition;
    Transform startParent;

    public void OnBeginDrag(PointerEventData eventData)
    {   
        DraggedKey = this.gameObject;
        startPosition = Input.mousePosition;
        startParent = transform.parent;
        //transform.SetParent(transform.root);
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        DraggedKey = null;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        // if(transform.parent == startParent)
        // {
        //     transform.position = startPosition;
        // }
        // else
        // {
            
        // }
        transform.position = transform.parent.position;
        GetComponent<KeyController>().SubscribeToComponent(transform.parent.GetComponent<ItemSlotBehaviour>().Component4Slot);
    }
}
