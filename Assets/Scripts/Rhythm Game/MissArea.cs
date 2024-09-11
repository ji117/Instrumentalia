using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissArea : MonoBehaviour
{
    public GameController controller;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.SetActive(false);
        Destroy(collision.gameObject);
        controller.AddMiss();
        Debug.Log("Missed!"); 
    }
}
