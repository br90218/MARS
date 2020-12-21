using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBehaviour : MonoBehaviour
{
    [SerializeField] private RobotBehaviour parent;

    [SerializeField] private GameObject roundPrefab;

    [SerializeField] private float roundGap;
    [SerializeField] private float fireOffset;
    private Coroutine firingRoutine;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (parent.IsFiringCannon)
        {
            if (firingRoutine == null)
            {
                firingRoutine = StartCoroutine(CannonFireRoutine());
            }

        }
        else
        {
            if (firingRoutine != null)
            {
                StopCoroutine(firingRoutine);
                firingRoutine = null;
            }
        }
    }
    private IEnumerator CannonFireRoutine()
    {
        while (true)
        {
            var round = Instantiate(roundPrefab, transform.position + transform.up * fireOffset, transform.rotation, null);
            round.SetActive(true);
            yield return new WaitForSeconds(roundGap);
        }
    }
}
