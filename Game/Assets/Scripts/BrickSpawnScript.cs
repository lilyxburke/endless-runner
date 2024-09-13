using UnityEngine;
using Random = UnityEngine.Random;
public class BrickSpawnScript : MonoBehaviour
{
    [SerializeField] private GameObject[] blockOptions;
    [SerializeField] private ObjectPooler objectPooler;

    public bool stopSpawn;
    public float moveSpeed;
    [SerializeField] private float spawnRate;
    [SerializeField] private float timer;
    [SerializeField] private float spawnOffset;

    private void Update()
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
    private void spawnBrick()
    {
        int choice = Random.Range(0, 4);
        string name = blockOptions[choice].name.Replace("Clone", "");
        
        float smallestDistance = transform.position.x - spawnOffset;
        float largestDistance = transform.position.x + spawnOffset;

        float x = Random.Range(smallestDistance, largestDistance);
        Vector3 spawnPosition = new Vector3(x, blockOptions[choice].transform.position.y, 0);

        if (CanSpawnHere(spawnPosition, blockOptions[choice]))
        {
            objectPooler.SpawnBrick(name, spawnPosition);
        }
    }

    private bool CanSpawnHere(Vector3 spawnPosition, GameObject obj)
    {
        Collider2D[] intersect = Physics2D.OverlapCircleAll(spawnPosition, ((obj.GetComponent<BrickObject>().width / 2) + 0.1f));
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

        if (LogicManager.Instance.score % 15 == 0 && LogicManager.Instance.score != 0)
        {
            moveSpeed += 1.5f;

        }
    }
}
