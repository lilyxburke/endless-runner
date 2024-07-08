using UnityEngine;

public class BrickMovement : MonoBehaviour
{
    public float moveSpeed;
    public float deletion = -10;
    public BrickSpawnScript spawnScript;

    void Awake()
    {
        spawnScript = GameObject.FindGameObjectWithTag("Spawn").GetComponent<BrickSpawnScript>();
    }
    void Update()
    {
        transform.position = transform.position + (Vector3.left * moveSpeed) * Time.deltaTime;
        if (transform.position.x < deletion)
        {
            Destroy(gameObject);
        }
        moveSpeed = spawnScript.moveSpeed;
    }
}
