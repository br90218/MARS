using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public enum GameState
    {
        LOSE, WIN, GAMING
    }

    public GameState CurrState;

    [SerializeField] private GameObject part;
    [SerializeField] private int partsNumber;
    [SerializeField] private GameObject mob;
    [SerializeField] private int maxMobNumber;
    [SerializeField] private int startingMobNumberLimit;
    [SerializeField] private float dimensionsLimit = 40f;
    private int collectedParts;
    
    private int mobNumberLimit;
    private int mobNumber;
    private float elapsedTime;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else if (Instance != this)
        {
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
        
        mobNumberLimit = startingMobNumberLimit;
        StartCoroutine(RaiseMobLimit());
        
        for(int i = 0 ; i< mobNumberLimit; i++)
        {
            InstantiateMob();
        }

        InstantiateParts();

    }

    // Update is called once per frame
    void Update()
    {
        if(mobNumber < mobNumberLimit)
        {
            for(int i = mobNumber; i < mobNumberLimit; i++)
            {
                InstantiateMob();
            }
        }
    }


    private IEnumerator RaiseMobLimit()
    {
        while(mobNumberLimit < maxMobNumber)
        {
            yield return new WaitForSeconds(60);
            mobNumberLimit++;
        }
    }

    private void InstantiateParts()
    {
        for (int i = 0; i < partsNumber; i++)
        {
            var x = Random.Range(15f, dimensionsLimit) * (Random.Range(0f, 1f) > 0.5 ? 1 : -1);
        var y = Random.Range(15f, dimensionsLimit) * (Random.Range(0f, 1f) > 0.5 ? 1 : -1);
            var location = new Vector3(x, y, 0f);
            Instantiate(part, location, Quaternion.identity, null);
        }
    }

    internal void DeductMob()
    {
        mobNumber--;
    }

    private void InstantiateMob()
    {
        var x = Random.Range(15f, dimensionsLimit) * (Random.Range(0f, 1f) > 0.5 ? 1 : -1);
        var y = Random.Range(15f, dimensionsLimit) * (Random.Range(0f, 1f) > 0.5 ? 1 : -1);


        var location = new Vector3(x, y, 0f);
        Instantiate(mob, location, Quaternion.identity, null);
        mobNumber++;
    }

    internal void CollectPart()
    {
        collectedParts++;
        if (collectedParts == partsNumber)
        {
            SceneManager.LoadScene(3);
        }
    }
}
