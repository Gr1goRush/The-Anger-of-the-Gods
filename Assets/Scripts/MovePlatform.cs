using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    public float speed;
    public Transform[] targets;
    int index;

    // Update is called once per frame
    void Update()
    {
        Vector2 dir = (Vector2)targets[index].position - (Vector2)transform.position;
        dir.Normalize();
        transform.Translate(dir * speed* Time.deltaTime);
        if(Vector2.Distance(transform.position, targets[index].position) <= 0.2f)
        {
            index += 1;
            if (index >= targets.Length)
            {
                index = 0;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D rb = collision.rigidbody;
        if (rb)
        {
            if (rb.velocity.y <= 0)
            {
                rb.transform.SetParent(transform);
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        Rigidbody2D rb = collision.rigidbody;
        if (rb)
        {
            rb.transform.SetParent(transform.parent.parent.parent);
        }
    }
}
