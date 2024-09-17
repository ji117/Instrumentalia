using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour
{
    public Detector secondDetector; 
    bool noteColliding = false;
    bool collidingWithBoth = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        noteColliding = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        noteColliding = false;
    }

    private void Update()
    {
        if (!GameController.gameInstance.IsGameOver())
        {
            if (noteColliding && secondDetector.IsNoteColliding())
                collidingWithBoth = true;
            else
                collidingWithBoth = false;
        }
    }

    public bool IsBothColliding()
    {
        return collidingWithBoth;
    }

    public bool IsNoteColliding()
    {
        return noteColliding;
    }

}
