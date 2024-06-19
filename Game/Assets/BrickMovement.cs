using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickMovement : MonoBehaviour
{
    public float moveSpeed = 6;
    public float deletion = -10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + (Vector3.left * moveSpeed) * Time.deltaTime;
        if (transform.position.x < deletion)
        {
            Destroy(gameObject);
        }
    }
}
