using UnityEngine;
using Random = UnityEngine.Random;

public class BrickSpawnScript : MonoBehaviour
{
    public GameObject singleBrick;
    public GameObject doubleTallBrick;
    public GameObject doubleSingleBrick;
    public GameObject longBrick;
    public Transform transformVariable;
    public LoadCharacter characterScript;
    public GameObject[][] blockOptions;

    public bool stopSpawn;
    public float spawnRate = 1;
    private float timer = 0;
    public float spawnOffset = 4;
    public LogicScript logic;
    public float moveSpeed = 4;

    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        spawnBrick();
    }

    void Update()
    {

        if (timer < spawnRate)
        {
            timer += Time.deltaTime;
        }else
        {
            if (!stopSpawn)
            {
                spawnBrick();
                timer = 0;
            }
        }

    }
    void spawnBrick()
    {
        GameObject brick;
        int choice = Random.Range(1, 5);
        transformVariable.position = new Vector3(transform.position.x, -1.22f, transform.position.z);
        switch (choice)
        {
            case 1:
                brick = singleBrick;
                transformVariable.position = new Vector3(transform.position.x, -1.38f , transform.position.z);
                break;
            case 2:
                brick = doubleTallBrick;
                break;
            case 3:
                brick = doubleSingleBrick;
                break;
            case 4:
                brick = longBrick;
                break;
            default:
                brick = singleBrick;
                break;
        }

        float smallestDistance = transform.position.x - spawnOffset;
        float largestDistance = transform.position.x + spawnOffset;

        float x = Random.Range(smallestDistance, largestDistance);

        Vector3 spawnPosition = new Vector3(x, transform.position.y, 0);
        while (!CanSpawnHere(spawnPosition))
        {
            
            spawnPosition = new Vector3(x-1f, transform.position.y, 0);

            CanSpawnHere(spawnPosition);
        }
        Instantiate(brick, spawnPosition, transform.rotation);

    }

    public bool CanSpawnHere(Vector3 spawnPosition)
    {

        Collider2D[] intersect = Physics2D.OverlapCircleAll(spawnPosition, 0.2f);
        if(intersect.Length == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void IncreaseSpeed()
    {
        if (logic.score % 15 == 0 && logic.score != 0)
        {
            moveSpeed += 1.5f;
        }
    }
}
