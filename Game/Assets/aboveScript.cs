using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aboveScript : MonoBehaviour
{
    public LogicScript logic;
    
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        logic.addScore(1);
    }
}
