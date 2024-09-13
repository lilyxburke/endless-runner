using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using Unity.VisualScripting;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite[] characters;
    [SerializeField] private Transform transformVariable;
    [SerializeField] private Rigidbody2D myRigidBody;

    public int health = 3;
    public bool invincible = false;
    public float width;

    [SerializeField] private float jumpStrength = 5.5f;
    [SerializeField] private int jumpCount = 0;
    [SerializeField] private bool alive = true;
    [SerializeField] private float fallMultiplier = 2.5f;
    [SerializeField] private float cooldown = 2;

    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private InputAction touchAction;

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
        touchAction = playerInput.actions["Jump"];
    }
    void Start()
    {
        spriteRenderer.sprite = characters[PlayerPrefs.GetInt("SelectedCharacter")];
        width = spriteRenderer.bounds.size.x;
    }

    void OnEnable()
    {
        touchAction.Enable();
        touchAction.performed += JumpPressed;
    }

    void OnDisable()
    {
        touchAction.Disable();
        touchAction.performed -= JumpPressed;
    }

    private void JumpPressed(InputAction.CallbackContext context)
    {
        float pressed = context.ReadValue<float>();
        if (pressed > 0.5f && jumpCount < 2 && alive)
        {
 
            myRigidBody.velocity = Vector2.up * jumpStrength;
            jumpCount++;
        }
        if (myRigidBody.velocity.y < 0)
        {
            myRigidBody.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 0.45f);
    }

    void Update()
    {
        Vector2 position = new Vector2(transformVariable.position.x, transformVariable.position.y);
        Collider2D[] results = Physics2D.OverlapCircleAll(position, 0.45f);
        Debug.Log(invincible);
        if (!invincible)
        {
            foreach (Collider2D collider in results)
            {
                GameObject obj = collider.transform.gameObject;
                if (obj.layer == 3 && alive && jumpCount == 2)
                {
                    jumpCount = 0;
                }
                else if (obj.layer == 9)
                {
                    obj.GetComponent<BrickMovement>().SetHit();
                    if (health > 0)
                    {
                        LoseHealth(1);
                        if (health == 0)
                        {
                            LogicManager.Instance.GameOver();
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

    public void LoseHealth(int heart)
    {
        health -= heart;
    }
}
