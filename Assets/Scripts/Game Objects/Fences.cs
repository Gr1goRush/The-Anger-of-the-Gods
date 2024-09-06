using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fences : MonoBehaviour
{
    public float force = 400;
    public bool direct;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Rigidbody2D rb = collision.attachedRigidbody;
        if (rb && collision.isTrigger)
        {
            if (!direct)
            {
                if (!Grek.singenton.health.isDamaged) return;
                Vector2 dir = rb.position - (Vector2)transform.position;
                dir.Normalize();
                dir.y = 1;
                rb.velocity = Vector2.zero;
                rb.AddForce(dir * force);
                Grek.singenton.health.Damage();
            }
            else
            {
                rb.velocity = Vector2.zero;
                rb.AddForce(Vector2.up * force);
                Grek.singenton.health.Damage();
            }
        }
    }

    public void Delate()
    {
        Destroy(transform.parent.gameObject);
    }
}