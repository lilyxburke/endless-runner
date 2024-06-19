using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScript : MonoBehaviour
{
    public Rigidbody2D myRigidBody;
    public float flapStrength;
    private int jumpCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        myRigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isDoubleJumping()){
            myRigidBody.velocity = Vector2.up * flapStrength;
            jumpCount++;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpCount = 0;
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
