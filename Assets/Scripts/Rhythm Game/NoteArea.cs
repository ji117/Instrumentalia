using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteArea : MonoBehaviour
{
    private bool isNoteColliding = false;
    private float delay = 0f;
    public KeyCode key;
    public Detector detector;
    Collider2D noteCollidedWith;
    SpriteRenderer sr;
    Color original;
    

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        original = sr.color;
    }

    void Update()
    {
        if (Input.GetKeyDown(key) && detector.IsBothColliding() && delay <= 0)
        {
            noteCollidedWith.gameObject.SetActive(false);
            Destroy(noteCollidedWith.gameObject);
            GameController.gameInstance.AddScore(200);
            Debug.Log("Perfect!");
        }
        else if (Input.GetKeyDown(key) && isNoteColliding && delay <= 0)
        {
            noteCollidedWith.gameObject.SetActive(false);
            Destroy(noteCollidedWith.gameObject);
            GameController.gameInstance.AddScore(100);
            Debug.Log("Good!");
        }

        if (Input.GetKeyDown(key) && delay <= 0)
        {
            StartCoroutine(Pressed());
            delay = 2.0f;
        }

    }

    private void FixedUpdate()
    {
        if (delay >= 0)
        {
            delay = delay - 0.1f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isNoteColliding = true;
        noteCollidedWith = collision;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isNoteColliding = false;
    }

    IEnumerator Pressed()
    {
        sr.color = new Color(0, 0, 0);
        yield return new WaitForSeconds(0.2f);
        sr.color = original;
    }


}
