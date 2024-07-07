using UnityEngine;
using System.Collections;

public class aboveScript : MonoBehaviour
{
    public LogicScript logic;

    void Awake()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        logic.addScore(1);


    }

}
