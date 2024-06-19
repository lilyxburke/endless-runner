using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScript : MonoBehaviour
{
    public Rigidbody2D myRigidBody;
    public float flapStrength;
    private int jumpCount = 0;
    public LogicScript logic;
    private bool alive = true;
    // Start is called before the first frame update
    void Start()
    {
        myRigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(alive);
        if (Input.GetKeyDown(KeyCode.Space) && !isDoubleJumping() && alive){
            myRigidBody.velocity = Vector2.up * flapStrength;
            jumpCount++;
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
            logic.gameOver();
            alive = false;
        }
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
