using UnityEngine;

public class BrickMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float deletion = -10;
    [SerializeField] private bool hit = false;
    [SerializeField] private bool survived = false;
    [SerializeField] private float width;

    [SerializeField] private BrickSpawnScript spawnScript;
    [SerializeField] private SpriteRenderer spriteR;

    void Awake()
    {
        width = spriteR.bounds.size.x;
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
        if (!survived && !hit && !(PlayerScript.Instance.invincible))
        {
            CheckIfSurvived();
        }
    }

    private void CheckIfSurvived()
    {
        if ((transform.position.x + width) < (-4 -(PlayerScript.Instance.width/2)) &&
            (transform.position.x + width) > -6)
        {
            LogicManager.Instance.AddScore(1);
            survived = true;
        }
    }

    public void SetHit()
    {
        hit = true;
    }

}
