using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerScript : MonoBehaviour
{
    public int health;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite[] characters;
    [SerializeField] private Transform transformVariable;
    [SerializeField] private Rigidbody2D myRigidBody;
    [SerializeField] private float jumpStrength = 5.5f;
    [SerializeField] private int jumpCount = 0;
    [SerializeField] private bool alive = true;
    [SerializeField] private float fallMultiplier = 2.5f;
    [SerializeField] private float cooldown = 2;
    [SerializeField] private bool invincible = false;

    public static PlayerScript instance { get; private set; }
    public static PlayerScript Instance
    {
        get
        {
            return instance;
        }
    }
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        spriteRenderer.sprite = characters[PlayerPrefs.GetInt("SelectedCharacter")];
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 0.45f);
    }

    void Update()
    {
        Vector2 position = new Vector2(transformVariable.position.x, transformVariable.position.y);
        Jump();
        Collider2D[] results = Physics2D.OverlapCircleAll(position, 0.45f);
        if (!invincible)
        {
            foreach (Collider2D collider in results)
            {
                GameObject obj = collider.transform.gameObject;
                if (obj.layer == 3 && alive)
                {
                    jumpCount = 0;
                }
                else if (obj.layer == 9)
                {

                    if (health > 0)
                    {
                        LoseHealth(1);
                        if (health == 0)
                        {
                            LogicManager.Instance.gameOver();
                            alive = false;
                        }
                        else
                        {
                            StartCoroutine(GiveInvincibility());
                        }
                    }
                }
            }
        }
    }

    IEnumerator GiveInvincibility()
    {
        invincible = true;
        Physics2D.IgnoreLayerCollision(8, 9, true);
        spriteRenderer.color = new Color(1f, 1f, 1f, 0.5f);

        yield return new WaitForSeconds(cooldown);
        Physics2D.IgnoreLayerCollision(8, 9, false);
        spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
        invincible = false;
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !IsDoubleJumping() && alive)
        {
            myRigidBody.velocity = Vector2.up * jumpStrength;
            jumpCount++;
        }
        if (myRigidBody.velocity.y < 0)
        {
            myRigidBody.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
    }
    bool IsDoubleJumping()
    {
        if (jumpCount == 2)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void LoseHealth(int heart)
    {
        health -= heart;
    }
}
