using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGroupController : MonoBehaviour
{


    private RectTransform rectTransform;
    private bool isBatteryBayOpen;
    [SerializeField] private Vector2 defaultPos;
    [SerializeField] private Vector2 batteryBayOpenPos;

    private Coroutine BatteryBayCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        isBatteryBayOpen = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(BatteryBayCoroutine == null)
            {
                BatteryBayCoroutine = StartCoroutine(BatteryBayRoutine());
            }
        }
    }


    private IEnumerator BatteryBayRoutine()
    {
        var targetPosition = isBatteryBayOpen ? defaultPos : batteryBayOpenPos;

        var distanceIndex = 0f;
        while(distanceIndex < 1f)
        {
            rectTransform.anchoredPosition = Vector2.Lerp(rectTransform.anchoredPosition, targetPosition, distanceIndex);
            distanceIndex += Time.deltaTime * 1.5f;
            yield return null;
        }
        isBatteryBayOpen = !isBatteryBayOpen;
        BatteryBayCoroutine = null;
    }
}
