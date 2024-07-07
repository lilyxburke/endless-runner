using System.Collections;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    public Rigidbody2D myRigidBody;
    public SpriteRenderer mySpriteRenderer;
    public CircleCollider2D myCollider;

    [Range(1,10)]
    public float flapStrength;
    private int jumpCount = 0;
    public LogicScript logic;
    private bool alive = true;
    public float fallMultiplier = 2.5f;
    public float cooldown;

    void Start()
    {
        myRigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isDoubleJumping() && alive){
            myRigidBody.velocity = Vector2.up * flapStrength;
            jumpCount++;
        }
        if (myRigidBody.velocity.y < 0)
        {
            myRigidBody.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") && alive)
        {
            jumpCount = 0;
        }
        else if (collision.gameObject.CompareTag("Block"))
        {
            if (logic.hearts > 0)
            {
                logic.loseHearts(1);
                StartCoroutine(GiveInvincibility());
            }
             
            if(logic.hearts == 0)
            {
                logic.gameOver();
                alive = false;
            }
            
        }
    }

    IEnumerator GiveInvincibility() 
    {
        Physics2D.IgnoreLayerCollision(8, 9, true);
        mySpriteRenderer.color = new Color(1f, 1f, 1f, 0.5f);

        yield return new WaitForSeconds(cooldown);
        Physics2D.IgnoreLayerCollision(8, 9, false);
        mySpriteRenderer.color = new Color(1f, 1f, 1f, 1f);
    }

    bool isDoubleJumping()
    {
        if(jumpCount == 2)
        {
            return true;
        }
        else
        {
            return false;   
        }
    }
}
