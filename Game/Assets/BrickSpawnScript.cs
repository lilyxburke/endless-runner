using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BrickSpawnScript : MonoBehaviour
{
    public GameObject singleBrick;
    public GameObject doubleTallBrick;
    public GameObject doubleSingleBrick;
    public GameObject longBrick;
    public Transform transformVariable;

    //should be decimal
    public float spawnRate = 2;
    private float timer = 0;
    public float spawnOffset = 5;
    // Start is called before the first frame update
    void Start()
    {
        spawnBrick();
    }

    // Update is called once per frame
    void Update()
    {

        if (timer < spawnRate)
        {
            timer += Time.deltaTime;
        }else
        {
            spawnBrick();
            timer = 0;
        }

    }
    void spawnBrick()
    {
        GameObject brick;
        int choice = Random.Range(1, 5);
        transformVariable.position = new Vector3(transform.position.x, -1.22f, transform.position.z);
        switch (choice)
        {
            case 1:
                brick = singleBrick;
                transformVariable.position = new Vector3(transform.position.x, -1.38f , transform.position.z);
                break;
            case 2:
                brick = doubleTallBrick;
                break;
            case 3:
                brick = doubleSingleBrick;
                break;
            case 4:
                brick = longBrick;
                break;
            default:
                brick = singleBrick;
                break;
        }

        float smallestDistance = transform.position.x - spawnOffset;
        float largestDistance = transform.position.x + spawnOffset;
        
        Debug.Log(brick.transform.position);
        Instantiate(brick, new Vector3(Random.Range(smallestDistance,largestDistance), transform.position.y, 0), transform.rotation);
    }
}
