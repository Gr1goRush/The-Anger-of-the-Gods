using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Сloud : MonoBehaviour
{
    public Animator anim;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D rb = collision.rigidbody;
        if (rb)
        {
            if(rb.velocity.y <= 0)
            {
                anim.SetBool("start", true);
            }
        }
    }

    public void ExitAnim()
    {
        anim.SetBool("start", false);
    }
}
