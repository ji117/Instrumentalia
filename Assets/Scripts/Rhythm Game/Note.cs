using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0, -speed);
    }
    void Start()
    {
        
    }

   
    void Update()
    {
       
    }

    private void FixedUpdate()
    {
        if (GameController.gameInstance.IsGamePaused())
            rb.velocity = new Vector2(0, 0);
        else
            rb.velocity = new Vector2(0, -speed);
    }
}
