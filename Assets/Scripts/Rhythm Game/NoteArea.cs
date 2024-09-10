using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteArea : MonoBehaviour
{
    private bool isNoteColliding = false;
    public KeyCode key;
    Collider2D noteCollidedWith; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(key) && isNoteColliding)
        {
            Destroy(noteCollidedWith.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Note Colliding");
        isNoteColliding = true;
        noteCollidedWith = collision;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isNoteColliding = false;
    }


}
