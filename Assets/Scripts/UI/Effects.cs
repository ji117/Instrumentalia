using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effects : MonoBehaviour
{
    float alpha;
    Color temp;
    SpriteRenderer render; 
    private void Awake()
    {
        render = this.GetComponent<SpriteRenderer>();
        alpha = render.color.a;
        temp = render.color; 
    }

    private void FixedUpdate()
    {
        alpha -= 0.02f;
        temp.a = alpha;
        render.color = temp;

        if (render.color.a <= 0f)
        {
            Destroy(this.gameObject);
        }
    }
}
