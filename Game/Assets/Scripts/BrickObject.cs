using UnityEngine;

public class BrickObject : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float deletion = -10;
    [SerializeField] private bool hit = false;
    [SerializeField] private bool survived = false;
    public float width;

    [SerializeField] private BrickSpawnScript spawnScript;
    [SerializeField] private SpriteRenderer spriteR;
    [SerializeField] private ObjectPooler objectPooler;

    private void Awake()
    {
        width = spriteR.bounds.size.x;
        spawnScript = GameObject.FindGameObjectWithTag("Spawn").GetComponent<BrickSpawnScript>();
        objectPooler = GameObject.Find("ObjectPooler").GetComponent<ObjectPooler>();
    }

    private void OnEnable()
    {
        hit = false;
        survived = false;
    }
    private void Update()
    {
        transform.position = transform.position + (Vector3.left * moveSpeed) * Time.deltaTime;
        if (transform.position.x < deletion)
        {
            this.gameObject.SetActive(false);

            objectPooler.ReturnToPool(this.gameObject.name.Replace("(Clone)", ""), this.gameObject);
        }
        moveSpeed = spawnScript.moveSpeed;
        if (!survived && !hit && !(PlayerScript.Instance.invincible))
        {
            CheckIfSurvived();
        }
    }

    private void CheckIfSurvived()
    {
        if ((transform.position.x + width) < (-4 - (PlayerScript.Instance.width / 2)) &&
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
